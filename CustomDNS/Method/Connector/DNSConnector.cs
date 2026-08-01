using CustomDNS.Method.Selector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;



namespace CustomDNS.Method.Connector
{
    class DNSConnector : DNSSelector
    {
        public static bool CONNECT()
        {
            bool isConnected = false;
            output= error="";

            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni =>
                    ni.OperationalStatus == OperationalStatus.Up &&
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                     ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
                .ToList();

            if (!interfaces.Any())
            {
                Console.WriteLine("هیچ کارت شبکه فعالی پیدا نشد.");
                return false;
            }

            string adapterName = interfaces[0].Name;
            Console.WriteLine("کارت فعال: " + adapterName);



            if (RunNetshCommand($"interface ip set dns name=\"{adapterName}\" static {DNSSELECTED01}") && RunNetshCommand($"interface ip add dns name=\"{adapterName}\" {DNSSELECTED02} index=2") )
            {

                isConnected = true;
                return true;
            }

            
            return isConnected;
        }
        public static bool DISCONNECT()
        {
          
            if (ResetDnsToDhcp())
            {
                DNSSELECTED01 = null;
                DNSSELECTED02 = null;
                DNSNAMESELECTED = null;
                DNSIDSELECTED = -1;
                return true;
            }
            else return false;
        }

        public static bool ResetDnsToDhcp()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up &&
                             (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                              ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
                .ToList();

            if (!interfaces.Any())
            {
                Console.WriteLine("کارت شبکه فعال پیدا نشد.");
                return false;
            }

            string adapterName = interfaces[0].Name;
            Console.WriteLine("کارت شبکه فعال: " + adapterName);

            var psi = new ProcessStartInfo("netsh", $"interface ip set dns name=\"{adapterName}\" source=dhcp")
            {
                Verb = "runas",
                UseShellExecute = true,
                CreateNoWindow = true,

                WindowStyle = ProcessWindowStyle.Hidden
            };

            try
            {
                var process = Process.Start(psi);
                process.WaitForExit();
                Console.WriteLine("DNS به حالت DHCP ریست شد.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("خطا در ریست DNS: " + ex.Message);
                return false;
            }
        }

        static string output, error;
        static bool RunNetshCommand(string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo("netsh", arguments)
            {
                Verb = "runas",
                UseShellExecute = false,     // حتما باید false باشه برای Redirect
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };


            try
            {
                Process p = Process.Start(psi);
                p.WaitForExit();

                output = p.StandardOutput.ReadToEnd();
                error = p.StandardError.ReadToEnd();
                return p.ExitCode == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    
}
