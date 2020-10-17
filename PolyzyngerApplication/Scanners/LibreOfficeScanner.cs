using PolyzyngerApplication.Interfaces;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Scanners
{
    internal class LibreOfficeScanner : IScanner
    {
        public async Task<string> CheckLatestVersionPathAsync(string initialUri = null)
        {
            using WebClient client = new WebClient();

            string page = await client.DownloadStringTaskAsync("https://pl.libreoffice.org/pobieranie/stabilna/");

            string version = Regex.Match(page, "LibreOffice [0-9]+[.][0-9]+[.][0-9]+", RegexOptions.IgnoreCase).Value;

            string versionNo = Regex.Match(version, "[0-9]+[.][0-9]+[.][0-9]+").Value;

            return initialUri.Replace("6.3.6", versionNo);
        }
    }
}