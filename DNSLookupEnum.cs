using DnsClient;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private async void btnDiscover_Click(object sender, EventArgs e)
    {
        string domain = txtDomain.Text.Trim();

        if (!string.IsNullOrEmpty(domain))
        {
            var dnsLookup = new LookupClient();
            var result = await dnsLookup.QueryAsync(domain, QueryType.ANY);

            // Process and display the results
            foreach (var record in result.Answers)
            {
                // Display each record type (A, CNAME, MX, etc.) in your UI
                listBoxResults.Items.Add($"{record.RecordType}: {record.ToString()}");
            }
        }
        else
        {
            MessageBox.Show("Please enter a domain name.");
        }
    }
}












string domain = txtDomain.Text.Trim();

if (!string.IsNullOrEmpty(domain))
{
    var dnsLookup = new LookupClient();
    txtResults.Clear();

    // Read subdomains from file
    string[] subdomains = File.ReadAllLines("subdomains.txt");

    foreach (var subdomain in subdomains)
    {
        string fullDomain = $"{subdomain}.{domain}";
        try
        {
            var result = await dnsLookup.QueryAsync(fullDomain, QueryType.A);

            if (result.Answers.Count > 0)
            {
                foreach (var record in result.Answers)
                {
                    txtResults.AppendText($"{fullDomain} - {record.RecordType}: {record}\r\n");
                }
            }
        }
        catch (DnsResponseException)
        {
            // Ignore DNS resolution errors
        }
    }
}
else
{
    MessageBox.Show("Please enter a domain name.");
}