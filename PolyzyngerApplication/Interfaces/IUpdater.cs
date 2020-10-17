using System.Threading.Tasks;

namespace PolyzyngerApplication.Interfaces
{
    internal interface IUpdater
    {
        Task UdateAsync(string patchTempPath);
    }
}