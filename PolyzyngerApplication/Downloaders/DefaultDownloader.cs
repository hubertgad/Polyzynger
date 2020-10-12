using PolyzyngerApplication.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Downloaders
{
    class DefaultDownloader : IDownloader
    {
        public async Task DownloadAsync(string uri, string tempPath, State state, string patchUri = null, string patchTempPath = null)
        {
            double downloads = string.IsNullOrEmpty(patchUri) ? 1 : 2;

            Task downloadInstaller = DownloadFile(uri, tempPath, state, downloads);

            if (!string.IsNullOrEmpty(patchUri))
            {
                await DownloadFile(patchUri, patchTempPath, state, downloads);
            }

            await downloadInstaller;
        }

        private Task DownloadFile(string uri, string tempPath, State state, double downloads)
        {
            double progress = 0;

            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

            using WebClient client = new WebClient();

            client.DownloadProgressChanged += (s, e) =>
            {
                var delta = e.ProgressPercentage - progress;
                state.DownloadProgress += delta / downloads;
                progress = e.ProgressPercentage;
            };

            client.DownloadFileCompleted += (s, e) =>
            {
                state.DownloadProgress += (100 - progress) / downloads;
                tcs.SetResult(0);
            };

            client.DownloadFileAsync(new Uri(uri), tempPath);

            return tcs.Task;
        }
    }
}