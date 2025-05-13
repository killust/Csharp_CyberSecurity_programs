using System;
using System.Net.Http;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using System.Collections.Generic;

namespace BannerGrabber
{
    public partial class Form1 : Form
    {
        private WebView2 webView;
        private TextBox headersTextBox;
        private TextBox vulnerabilitiesTextBox;
        private TextBox urlTextBox;
        private Button analyzeButton;

        public Form1()
        {
            InitializeComponent();
            InitializeWebView();
            InitializeTextBoxes();
            InitializeButton();
        }


        private void InitializeTextBoxes()
        {
            urlTextBox = new TextBox
            {
                Dock = DockStyle.Top,
                PlaceholderText = "Enter URL to analyze"
            };
            this.Controls.Add(urlTextBox);

            headersTextBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Left,
                Width = this.ClientSize.Width / 2,
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(headersTextBox);

            vulnerabilitiesTextBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(vulnerabilitiesTextBox);
        }

        private void InitializeButton()
        {
            analyzeButton = new Button
            {
                Text = "Analyze",
                Dock = DockStyle.Top
            };
            analyzeButton.Click += new EventHandler(AnalyzeButton_Click);
            this.Controls.Add(analyzeButton);
        }

        private async void AnalyzeButton_Click(object sender, EventArgs e)
        {
            string url = urlTextBox.Text;

            // Ensure the URL starts with "https://"
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute) || !url.StartsWith("https://"))
            {
                MessageBox.Show("Please enter a valid HTTPS URL.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            webView.Source = new Uri(url);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    var headers = response.Headers;
                    var contentHeaders = response.Content.Headers;

                    headersTextBox.Clear();
                    vulnerabilitiesTextBox.Clear();

                    headersTextBox.AppendText("Existing Headers:\n\n");
                    vulnerabilitiesTextBox.AppendText("Missing Headers and Their Vulnerabilities:\n\n");

                    foreach (var header in headers)
                    {
                        headersTextBox.AppendText($"{header.Key}: {string.Join(", ", header.Value)}\n");
                    }

                    foreach (var contentHeader in contentHeaders)
                    {
                        headersTextBox.AppendText($"{contentHeader.Key}: {string.Join(", ", contentHeader.Value)}\n");
                    }

                    AnalyzeVulnerabilities(headers, contentHeaders);
                }
                catch (Exception ex)
                {
                    headersTextBox.AppendText($"Error: {ex.Message}\n");
                }
            }
        }

    }
}
