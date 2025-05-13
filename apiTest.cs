using System; // Import the System namespace
using System.Net.Http; // Import the HttpClient class
using System.Text; // Import the Encoding class
using System.Threading.Tasks; // Import the Task class
using System.Windows.Forms; // Import the Windows Forms classes
using HtmlAgilityPack; // Import the HtmlAgilityPack library for web scraping
using Newtonsoft.Json; // Import the Json library for serialization/deserialization
using Microsoft.Web.WebView2.WinForms; // Import WebView2

namespace ApiCrudTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // Initialize the form components
            InitializeWebView(); // Initialize WebView2
        }

        // Event handler for button click
        private async void btnTestApi_Click(object sender, EventArgs e)
        {
            txtResults.Clear(); // Clear the results textbox
            txtCrawledData.Clear(); // Clear the crawled data textbox
            await RunApiTests(); // Run the API tests
        }

        // Method to run API tests
        private async Task RunApiTests()
        {
            string baseUrl = txtUrl.Text; // Get the base URL from user input

            using (HttpClient client = new HttpClient()) // Create a new HttpClient instance
            {
                try
                {
                    // Create a new product object using user input
                    var newProduct = new
                    {
                        Name = txtProductName.Text,
                        Price = double.Parse(txtProductPrice.Text)
                    };

                    // Send a POST request to create the product
                    var createResponse = await client.PostAsync(baseUrl, new StringContent(JsonConvert.SerializeObject(newProduct), Encoding.UTF8, "application/json"));
                    createResponse.EnsureSuccessStatusCode(); // Ensure the request was successful

                    // Deserialize the response to a Product object
                    var createdProduct = JsonConvert.DeserializeObject<Product>(await createResponse.Content.ReadAsStringAsync());
                    txtResults.AppendText("Create: Success\n"); // Append success message to the results

                    // Send a GET request to read the product
                    var readResponse = await client.GetAsync($"{baseUrl}/{createdProduct.Id}");
                    readResponse.EnsureSuccessStatusCode(); // Ensure the request was successful

                    // Deserialize the response to a Product object
                    var readProduct = JsonConvert.DeserializeObject<Product>(await readResponse.Content.ReadAsStringAsync());
                    txtResults.AppendText($"Read: Success - {readProduct.Name} with Price {readProduct.Price}\n"); // Append success message with product details to the results

                    // Display the product page in WebView2
                    string productPageUrl = $"{baseUrl}/{readProduct.Id}";
                    webView.Source = new Uri(productPageUrl);

                    // Get the product price from the webpage
                    double webpagePrice = await GetPriceFromWebPage(productPageUrl);
                    txtResults.AppendText($"Webpage Price: {webpagePrice}\n"); // Append the webpage price to the results

                    // Check if the webpage price matches the API price
                    if (webpagePrice == readProduct.Price)
                    {
                        txtResults.AppendText("Price verification: Success\n"); // Append success message
                    }
                    else
                    {
                        txtResults.AppendText("Price verification: Failed\n"); // Append failure message
                    }

                    // Update the product price
                    readProduct.Price = double.Parse(txtProductPrice.Text) + 10; // Example update: increment by 10
                    var updateResponse = await client.PutAsync($"{baseUrl}/{readProduct.Id}", new StringContent(JsonConvert.SerializeObject(readProduct), Encoding.UTF8, "application/json"));
                    updateResponse.EnsureSuccessStatusCode(); // Ensure the request was successful

                    // Append success message with new price to the results
                    txtResults.AppendText($"Update: Success - New Price {readProduct.Price}\n");

                    // Send a DELETE request to delete the product
                    var deleteResponse = await client.DeleteAsync($"{baseUrl}/{readProduct.Id}");
                    deleteResponse.EnsureSuccessStatusCode(); // Ensure the request was successful
                    txtResults.AppendText("Delete: Success\n"); // Append success message to the results

                    txtResults.AppendText("All tests passed successfully."); // Append final success message
                }
                catch (HttpRequestException e)
                {
                    txtResults.AppendText($"Request error: {e.Message}"); // Append request error message to the results
                }
                catch (Exception e)
                {
                    txtResults.AppendText($"Unexpected error: {e.Message}"); // Append unexpected error message to the results
                }
            }
        }

        // Method to get the price from the webpage
        private async Task<double> GetPriceFromWebPage(string url)
        {
            using (HttpClient client = new HttpClient()) // Create a new HttpClient instance
            {
                var response = await client.GetStringAsync(url); // Get the webpage content as a string
                var doc = new HtmlDocument(); // Create a new HtmlDocument instance
                doc.LoadHtml(response); // Load the webpage content into the HtmlDocument

                // Use XPath to find the price element
                var priceNode = doc.DocumentNode.SelectSingleNode("//span[@class='product-price']");
                var priceText = priceNode?.InnerText.Trim('$') ?? "0";
                txtCrawledData.AppendText($"Crawled Price: {priceText}\n"); // Display the crawled price in the textbox
                return double.Parse(priceText); // Parse the price and return it
            }
        }

        // Method to initialize WebView2
        private async void InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async(null); // Initialize WebView2
        }
    }

    // Define the Product class to match the API response
    public class Product
    {
        public int Id { get; set; } // Product ID
        public string Name { get; set; } // Product Name
        public double Price { get; set; } // Product Price
    }
}
