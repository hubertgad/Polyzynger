using System.Threading.Tasks;

namespace PolyzyngerApplication.Interfaces
{
    internal interface IDownloader
    {
        Task DownloadAsync(string source, string destination, State state, double downloads = 1);
    }
}