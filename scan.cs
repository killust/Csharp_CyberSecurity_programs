using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortScannerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonScan_Click(object sender, EventArgs e)
        {
            string ipAddress = textBoxIpAddress.Text;
            StringBuilder report = new StringBuilder();

            report.AppendLine($"Scanning IP: {ipAddress}");
            report.AppendLine($"Scan started at: {DateTime.Now}");
            report.AppendLine();

            int[] portsToCheck = new int[]
            {
                20, 21, 22, 23, 25, 49, 53, 69, 80, 110, 119, 135, 137, 138, 139, 143, 161, 162, 389, 443, 445, 514, 636, 873, 993, 995, 1433, 1434, 3306, 3389, 5060, 5061, 5900, 8080
            };

            foreach (int port in portsToCheck)
            {
                bool isOpen = await IsPortOpenAsync(ipAddress, port);
                if (isOpen)
                {
                    report.AppendLine($"Port {port}: Open");
                    report.AppendLine(GetPortVulnerability(port));
                }
                else
                {
                    report.AppendLine($"Port {port}: Closed");
                }
            }

            report.AppendLine();
            report.AppendLine($"Scan completed at: {DateTime.Now}");

            textBoxReport.Text = report.ToString();
        }

        private async Task<bool> IsPortOpenAsync(string ipAddress, int port)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    var connectTask = tcpClient.ConnectAsync(ipAddress, port);
                    var timeoutTask = Task.Delay(1000); // 1 second timeout
                    var completedTask = await Task.WhenAny(connectTask, timeoutTask);

                    if (completedTask == connectTask)
                    {
                        await connectTask; // Ensure exception is thrown if ConnectAsync failed
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SocketException)
                {
                    return false;
                }
            }
        }