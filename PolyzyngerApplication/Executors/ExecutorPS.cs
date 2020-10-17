using PolyzyngerApplication.Interfaces;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Executors
{
    internal class ExecutorPS : IExecutor
    {
        public async Task ExecuteAsync(string file, string arguments = null)
        {
            await Task.Run(() =>
            {
                using PowerShell ps = PowerShell.Create();

                ps.AddScript(file);

                var result = ps.BeginInvoke();

                ps.EndInvoke(result);
            });
        }
    }
}