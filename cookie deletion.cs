string targetUrl = "https://www.example.com/ (replace with the actual website URL)"; // This URL won't work, replace it with the target website

using (var httpClient = new HttpClient())
{
    // Assuming headers contain the session cookie information
    foreach (var header in headers)
    {
        if (header.Key.ToLower().Contains("cookie")) // Check if header key is related to cookies (case-insensitive)
        {
            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }

    var response = await httpClient.DeleteAsync(targetUrl);

    // Check response status code (might not be successful as deleting cookies through DELETE requests is not standard)
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Cookie deletion attempt successful (might not actually delete the cookie)! ");
    }
    else
    {
        Console.WriteLine($"Cookie deletion attempt failed with status code: {response.StatusCode}");
    }
}



string targetUrl = "TARGET_DELETE_URL";

using (var httpClient = new HttpClient())
{
    // Assuming headers contain relevant information (e.g., authorization token)
    httpClient.DefaultRequestHeaders.Add(headers);

    var response = await httpClient.DeleteAsync(targetUrl);

    // Check response status code for success (e.g., 200 or 204)
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Deletion successful!");
    }
    else
    {
        Console.WriteLine($"Deletion failed with status code: {response.StatusCode}");
    }
}
