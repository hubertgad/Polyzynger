using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Polyzynger.Resources;

namespace Polyzynger.Utilities
{
    public static class WiFi
    {
        private static string _template;

        public static Task ConnectToWiFiAsync(string ssid, string password)
        {
            string temp = Path.Combine(Path.GetTempPath(), "profile.xml");

            _template = ContentLoading.GetResource("WiFiProfileTemplate.xml");

            string profile = Regex.Replace(_template, "{SSID}", ssid).Replace("{password}", password);

            using (Stream fileStream = File.Create(temp))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(profile);
                }
            }

            var tcs = new TaskCompletionSource<object>();

            Process p = new Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = "netsh";
            p.StartInfo.Arguments = $"wlan add profile filename=\"{temp}\"";
            p.Start();
            p.WaitForExit();
            p.Exited += (s, e) =>
            {
                File.Delete(temp);
                tcs.SetResult(null);
            };

            return tcs.Task;
        }
    }
}