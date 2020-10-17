using PolyzyngerApplication.Interfaces;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Scanners
{
    internal class KLiteScanner : IScanner
    {
        public async Task<string> CheckLatestVersionPathAsync(string initialUri = null)
        {
            using WebClient client = new WebClient();

            string page = await client.DownloadStringTaskAsync("https://codecguide.com/download_k-lite_codec_pack_standard.htm");
            
            return Regex.Match(page, "https://files[\\d].codecguide.com/.*?.exe", RegexOptions.IgnoreCase).Value;
        }
    }
}