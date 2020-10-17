using PolyzyngerApplication.Interfaces;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Scanners
{
    internal class AdobeAirScanner : IScanner
    {
        public async Task<string> CheckLatestVersionPathAsync(string initialUri = null)
        {
            using WebClient client = new WebClient();
            
            string page = await client.DownloadStringTaskAsync("https://get.adobe.com/air/");
            
            string version = Regex.Match(page, "Version [0-9]+[.][0-9]+", RegexOptions.IgnoreCase).Value;
            
            string versionNo = Regex.Match(version, "[0-9]+[.][0-9]+").Value;
            
            return initialUri.Replace("32.0", versionNo);
        }
    }
}