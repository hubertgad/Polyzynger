using System.Threading.Tasks;

namespace PolyzyngerApplication.Interfaces
{
    internal interface IScanner
    {
        Task<string> CheckLatestVersionPathAsync(string initialUri = null);
    }
}