using PolyzyngerApplication.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Executors
{
    internal class ExecutorMsi : IExecutor
    {
        public Task ExecuteAsync(string file, string arguments = null)
        {
            var tcs = new TaskCompletionSource<object>();

            Process p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = $"msiexec.exe",
                    Arguments = $"/i \"{ file }\" /q /norestart",
                    Verb = "runas"
                },
                EnableRaisingEvents = true
            };

            p.Exited += (s, e) =>
            {
                tcs.SetResult(null);
            };

            p.Start();

            return tcs.Task;
        }
    }
}