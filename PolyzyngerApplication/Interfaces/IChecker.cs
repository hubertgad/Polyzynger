using System.Threading.Tasks;

namespace PolyzyngerApplication.Interfaces
{
    internal interface IChecker
    {
        Task<(string installer, string patch)> CheckLatestVersionPathAsync();
    }
}