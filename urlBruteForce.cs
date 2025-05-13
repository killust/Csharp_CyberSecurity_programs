fullDomain = $"{subdomain.Trim()}.{domain}";

try
{

    var result2 = await dnsLookup.QueryAsync(fullDomain, QueryType.A);

    if (result2.Answers.Count > 0)
    {
        txtResults.AppendText($"{fullDomain} Exists\r\n");
    }
}
catch (DnsResponseException)
{

}
    }

    txtResults.AppendText($"The attack surface consists of the following entry points\r\n");

}
else
{
    MessageBox.Show("Please enter a domain name.");
}