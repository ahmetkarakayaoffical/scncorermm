using System.Net;

namespace scncore_rmm_web_console.Classes.Setup
{
    public class DNS
    {
        public static async Task<(bool, string)> Check_Dns_Forward_Reverse(string host)
        {
            try
            {
                IPAddress[] addresses = await Dns.GetHostAddressesAsync(host);

                foreach (var ip in addresses)
                {
                    IPHostEntry reverse = await Dns.GetHostEntryAsync(ip);

                    if (!reverse.HostName.Equals(host, StringComparison.OrdinalIgnoreCase))
                    {
                        return (false, reverse.HostName); // Hostnames do not match
                    }
                    else
                    {
                        return (true, reverse.HostName);
                    }
                }

                return (false, "No IP addresses found.");
            }
            catch (Exception ex)
            {
                Logging.Handler.Error("DNS", "Check_Dns_Forward_Reverse", ex.Message);
                return (false, "Undefined.");
            }
        }
    }
}
