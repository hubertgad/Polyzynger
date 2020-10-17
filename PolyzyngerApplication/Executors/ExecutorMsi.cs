using PolyzyngerApplication.Interfaces;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Executors
{
    internal class ExecutorMsi : Executor, IExecutor
    {
        public override Task ExecuteAsync(string file, string arguments = null)
        {
            return base.ExecuteAsync($"msiexec.exe", $"/i \"{ file }\" /q /norestart");
        }
    }
}