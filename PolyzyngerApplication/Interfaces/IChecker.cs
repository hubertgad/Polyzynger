using System.Threading.Tasks;

namespace PolyzyngerApplication.Interfaces
{
    internal interface IChecker
    {
        Task<string> CheckLatestVersionPathAsync(string initialUri = null);
    }
}