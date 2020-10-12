using System.Threading.Tasks;

namespace PolyzyngerApplication.Interfaces
{
    internal interface IInstaller
    {
        Task InstallAsync(string tempPath, string arguments);
    }
}