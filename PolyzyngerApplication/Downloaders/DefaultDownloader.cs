using PolyzyngerApplication.Interfaces;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Downloaders
{
    class DefaultDownloader : IDownloader
    {
        public async Task DownloadAsync(string uri, string tempPath, State state, string patchUri = null, string patchTempPath = null)
        {
            var downloads = (patchTempPath == null) ? 1 : 2;
            Debug.WriteLine(patchTempPath == null);
            Debug.WriteLine(downloads);
            double progress1 = 0;
            double progress2 = 0;

            await Task.Delay(500);

            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
            
            using WebClient client = new WebClient();
            
            client.DownloadProgressChanged += (s, e) =>
            {
                var delta = e.ProgressPercentage - progress1;
                state.DownloadProgress += delta / downloads;
                progress1 = e.ProgressPercentage;
            };

            client.DownloadFileCompleted += (s, e) =>
            {
                state.DownloadProgress += (100 - progress1) / downloads;
                tcs.SetResult(0);
            };

            client.DownloadFileAsync(new Uri(uri), tempPath);

            if (patchTempPath != null)
            {
                TaskCompletionSource<int> tcs2 = new TaskCompletionSource<int>();

                using WebClient client2 = new WebClient();

                client.DownloadProgressChanged += (s, e) =>
                {
                    var delta = e.ProgressPercentage - progress2;
                    state.DownloadProgress += delta / downloads;
                    progress2 = e.ProgressPercentage;
                };

                client.DownloadFileCompleted += (s, e) =>
                {
                    state.DownloadProgress = (100 - progress2) / downloads;
                    tcs2.SetResult(0);
                };

                await tcs2.Task;
            }

            await tcs.Task;
        }
    }
}