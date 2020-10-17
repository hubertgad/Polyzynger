using PolyzyngerApplication.Interfaces;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Updaters
{
    class AdobeReaderUpdater : IUpdater
    {
        public Task UdateAsync(string patchTempPath)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            string tempPath = Path.Combine(Path.GetTempPath(), "temp.cmd");

            using (Stream fileStream = File.Create(tempPath))
            {
                using StreamWriter streamWriter = new StreamWriter(fileStream);

                streamWriter.Write($"START /WAIT msiexec /update \"{ patchTempPath }\" /qn");
            }

            Process p = new Process()
            {
                StartInfo = 
                { 
                    FileName = tempPath,
                    Verb = "runas",
                    CreateNoWindow = true,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            p.Exited += (s, e) =>
            {
                if (File.Exists(tempPath)) File.Delete(tempPath);
                tcs.SetResult(null);
            };

            p.Start();

            return tcs.Task;
        }
    }
}