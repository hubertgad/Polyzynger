using System.Threading.Tasks;

namespace PolyzyngerApplication.Interfaces
{
    internal interface IDownloader
    {
        Task DownloadAsync(string uri, string tempPath, State state, string patchUri = null, string patchTempPath = null);
    }
}