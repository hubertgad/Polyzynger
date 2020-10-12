using PolyzyngerApplication.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Installers
{
    internal class InstallerExe : IInstaller
    {
        public Task InstallAsync(string tempPath, string arguments)
        {
            var tcs = new TaskCompletionSource<object>();

            Process p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = $"\"{ tempPath }\"",
                    Arguments = $"{ arguments }",
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