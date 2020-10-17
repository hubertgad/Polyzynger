using PolyzyngerApplication.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Executors
{
    internal class Executor : IExecutor
    {
        public virtual Task ExecuteAsync(string file, string arguments = null)
        {
            var tcs = new TaskCompletionSource<object>();

            Process p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = $"\"{ file }\"",
                    Arguments = $"{ arguments }",
                    Verb = "runas",
                    CreateNoWindow = true,
                    UseShellExecute = false
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