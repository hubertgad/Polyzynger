using System.Threading.Tasks;

namespace PolyzyngerApplication.Interfaces
{
    internal interface IExecutor
    {
        Task ExecuteAsync(string file, string arguments = null);
    }
}