using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        this.Text = "Ping Website and Get IP Address";

        // Create and configure TextBox for URL input
        TextBox textBoxUrl = new TextBox
        {
            Location = new System.Drawing.Point(15, 15),
            Width = 300
        };
        this.Controls.Add(textBoxUrl);

        // Create and configure Button to trigger the ping
        Button buttonPing = new Button
        {
            Location = new System.Drawing.Point(330, 12),
            Text = "Ping"
        };
        buttonPing.Click += (sender, e) => PingWebsite(textBoxUrl.Text);
        this.Controls.Add(buttonPing);

        // Create and configure TextBox for displaying results
        TextBox textBoxResults = new TextBox
        {
            Location = new System.Drawing.Point(15, 50),
            Width = 400,
            Height = 300,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical
        };
        this.Controls.Add(textBoxResults);
    }

    private async string PingWebsite(string url)
    {
        try
        {
            // Get the IP address from the URL
            IPAddress[] addresses = await Dns.GetHostAddressesAsync(url);

            if (addresses.Length > 0)
            {
                IPAddress ipAddress = addresses[0];

                // Ping the IP address
                using (Ping ping = new Ping())
                {
                    PingReply reply = await ping.SendPingAsync(ipAddress);

                    if (reply.Status == IPStatus.Success)
                    {
                        DisplayResults($"Ping to {url} [{ipAddress}] was successful.\nRoundtrip time: {reply.RoundtripTime} ms");
                    }
                    else
                    {
                        DisplayResults($"Ping to {url} [{ipAddress}] failed. Status: {reply.Status}");
                    }
                }
            }
            else
            {
                DisplayResults($"No IP addresses found for {url}");
            }
        }
        catch (Exception ex)
        {
            DisplayResults($"Error: {ex.Message}");
        }
    }

    private void DisplayResults(string result)
    {
        // Append results to the results TextBox
        foreach (Control control in this.Controls)
        {
            if (control is TextBox textBox && textBox.Multiline)
            {
                textBox.AppendText(result + Environment.NewLine);
            }
        }
    }
}
