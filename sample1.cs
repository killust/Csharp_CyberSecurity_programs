private async Task SendCustomRequest(string url, string customization)
{
    using (var httpClient = new HttpClient())
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

        // Parse customization input
        var customizationLines = customization.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        foreach (var line in customizationLines)
        {
            var parts = line.Split(new[] { '=' }, 2);
            if (parts.Length == 2)
            {
                var key = parts[0].Trim();
                var value = parts[1].Trim();

                if (key.Equals("Method", StringComparison.OrdinalIgnoreCase))
                {
                    requestMessage.Method = new HttpMethod(value);
                }
                else
                {
                    requestMessage.Headers.TryAddWithoutValidation(key, value);
                }
            }
        }

        // Copy intercepted headers
        foreach (var header in interceptedHeaders)
        {
            if (!requestMessage.Headers.Contains(header.Key))
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        // Add intercepted cookies
        if (!string.IsNullOrEmpty(interceptedCookies) && !requestMessage.Headers.Contains("Cookie"))
        {
            requestMessage.Headers.Add("Cookie", interceptedCookies);
        }

        // Send the modified request
        var response = await httpClient.SendAsync(requestMessage);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Display response (optional)
        this.Invoke((MethodInvoker)delegate
        {
            textBoxHttps.AppendText(responseContent);
            textBoxHttps.AppendText(Environment.NewLine);
        });
    }
}





















private async Task SendCustomRequest(string url, string customization)
{
    using (var httpClient = new HttpClient())
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);





        if (webView != null && webView.CoreWebView2 != null)
    {
        var request = webView.CoreWebView2.Environment.CreateWebResourceRequest(
            url, "POST", null, "application/x-www-form-urlencoded");

        // Apply intercepted headers
        foreach (var header in interceptedHeaders)
        {
            request.Headers.SetHeader(header.Key, header.Value);
        }

        // Apply intercepted cookies
        if (!string.IsNullOrEmpty(interceptedCookies))
        {
            request.Headers.SetHeader("Cookie", interceptedCookies);
        }

        // Apply customizations from the textbox
        if (!string.IsNullOrEmpty(customization))
        {
            var customHeaders = customization.Split('&');
            foreach (var header in customHeaders)
            {
                var parts = header.Split('=');
                if (parts.Length == 2)
                {
                    request.Headers.SetHeader(parts[0], parts[1]);
                }
            }
        }

        // Send the modified request
        var response = await httpClient.SendAsync(requestMessage);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Display response (optional)
        this.Invoke((MethodInvoker)delegate
        {
            textBoxHttps.AppendText(responseContent);
            textBoxHttps.AppendText(Environment.NewLine);
        });
    }
    }

}



































private async Task SendDeleteRequest(string url)
{
    using (var httpClient = new HttpClient())
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);

        // Add headers as needed
        requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0");
        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
        requestMessage.Headers.Connection.Add("keep-alive");

        // Send the request
        var response = await httpClient.SendAsync(requestMessage);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Display response (optional)
        this.Invoke((MethodInvoker)delegate
        {
            textBoxHttps.AppendText(responseContent);
            textBoxHttps.AppendText(Environment.NewLine);
        });
    }
}
