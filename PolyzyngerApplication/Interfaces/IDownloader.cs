using System.Threading.Tasks;

namespace PolyzyngerApplication.Interfaces
{
    internal interface IDownloader
    {
        Task DownloadAsync(string uri, string tempPath, State state, double downloads = 1);
    }
}