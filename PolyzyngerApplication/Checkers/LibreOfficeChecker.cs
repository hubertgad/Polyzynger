using PolyzyngerApplication.Interfaces;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Checkers
{
    class LibreOfficeChecker : IChecker
    {
        public async Task<(string installer, string patch)> CheckLatestVersionPathAsync(string installerUri)
        {
            string newInstallerUri = string.Empty;

            await Task.Run(() =>
            {
                using WebClient client = new WebClient();

                string page = client.DownloadString("https://pl.libreoffice.org/pobieranie/stabilna/");

                string version = Regex.Match(page, "LibreOffice [0-9]+[.][0-9]+[.][0-9]+", RegexOptions.IgnoreCase).Value;

                string versionNo = Regex.Match(version, "[0-9]+[.][0-9]+[.][0-9]+").Value;

                newInstallerUri = installerUri.Replace("6.3.6", versionNo);
            });

            return (newInstallerUri, string.Empty);
        }
    }
}