using Fiddler;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Text;
using System.Windows.Forms;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        InitializeWebView2();
    }

    private async void InitializeWebView2()
    {
        await webView21.EnsureCoreWebView2Async(null);
        webView21.CoreWebView2.Navigate("https://example.com/login");
    }

    private void btnStartAnalysis_Click(object sender, EventArgs e)
    {
        FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
        FiddlerApplication.BeforeResponse += FiddlerApplication_BeforeResponse;
        FiddlerApplication.Startup(8888, FiddlerCoreStartupFlags.Default);
    }

    private void btnStopAnalysis_Click(object sender, EventArgs e)
    {
        FiddlerApplication.Shutdown();
    }

    private void FiddlerApplication_BeforeRequest(Session oSession)
    {
        StringBuilder requestInfo = new StringBuilder();
        requestInfo.AppendLine($"Request intercepted: {oSession.RequestMethod} {oSession.fullUrl}");

        foreach (var header in oSession.oRequest.headers)
        {
            requestInfo.AppendLine($"{header.Name}: {header.Value}");
        }

        AnalyzeTokens(oSession.oRequest.headers, requestInfo, "Request");

        this.Invoke((MethodInvoker)delegate
        {
            textBoxHttps.AppendText(requestInfo.ToString());
            textBoxHttps.AppendText(Environment.NewLine);
        });
    }

    private void FiddlerApplication_BeforeResponse(Session oSession)
    {
        StringBuilder responseInfo = new StringBuilder();
        responseInfo.AppendLine($"Response intercepted: {oSession.fullUrl}");

        foreach (var header in oSession.oResponse.headers)
        {
            responseInfo.AppendLine($"{header.Name}: {header.Value}");
        }

        AnalyzeTokens(oSession.oResponse.headers, responseInfo, "Response");

        this.Invoke((MethodInvoker)delegate
        {
            textBoxHttps.AppendText(responseInfo.ToString());
            textBoxHttps.AppendText(Environment.NewLine);
        });
    }

    private void AnalyzeTokens(HTTPHeaders headers, StringBuilder info, string context)
    {
        foreach (var header in headers)
        {
            if (header.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase) && header.Value.StartsWith("Bearer "))
            {
                string token = header.Value.Substring("Bearer ".Length).Trim();
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                    if (jwtToken != null)
                    {
                        var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");
                        if (expClaim != null)
                        {
                            var exp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value)).UtcDateTime;
                            if (exp < DateTime.UtcNow)
                            {
                                info.AppendLine($"[VULNERABILITY] {context} Token has expired.");
                                txtVuln.AppendText($"{context} Token has expired.\n");
                            }
                        }
                        else
                        {
                            info.AppendLine($"[VULNERABILITY] {context} Token does not have an expiry claim.");
                            txtVuln.AppendText($"{context} Token does not have an expiry claim.\n");
                        }

                        if (jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase) == false)
                        {
                            info.AppendLine($"[VULNERABILITY] {context} Token is not signed with HMAC SHA-256.");
                            txtVuln.AppendText($"{context} Token is not signed with HMAC SHA-256.\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    info.AppendLine($"[ERROR] {context} Token analysis failed: {ex.Message}");
                    txtVuln.AppendText($"Token analysis failed: {ex.Message}\n");
                }
            }
        }
    }
}
