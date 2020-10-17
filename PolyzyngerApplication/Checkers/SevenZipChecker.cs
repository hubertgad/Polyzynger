using PolyzyngerApplication.Interfaces;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Checkers
{
    internal class SevenZipChecker : IChecker
    {
        public async Task<string> CheckLatestVersionPathAsync(string initialUri = null)
        {
            using WebClient client = new WebClient();
                
            string page = await client.DownloadStringTaskAsync("https://www.7-zip.org/download.html");
            
            string version = Regex.Match(page, "7-Zip [0-9]{2}[.][0-9]{2}", RegexOptions.IgnoreCase).Value;
            
            string versionNo = Regex.Match(version, "[0-9]+[.][0-9]+").Value;
            
            versionNo = versionNo.Replace(".", "");
            
            return initialUri.Replace("1900", versionNo);
        }
    }
}