using System;
using System.Net;

class Program
{
    static void Main(string[] args)
    {

        string iAddress[10];
        int i = 0;
        Console.WriteLine("Enter the website URL (e.g., www.google.com): ");
        string url = Console.ReadLine();

        try
        {
            // Get the IP addresses associated with the URL
            IPHostEntry hostEntry = Dns.GetHostEntry(url);

            Console.WriteLine($"IP addresses for {url}:");
            foreach (IPAddress ipAddress in hostEntry.AddressList)
            {
                i = 0;
                Console.WriteLine(ipAddress);
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    iAddress[i] = ipAddress.ToString(); // Return the first IPv4 address found
                    i++
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadLine();
    }
}
