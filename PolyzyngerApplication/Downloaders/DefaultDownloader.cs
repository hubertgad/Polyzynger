using PolyzyngerApplication.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Downloaders
{
    class DefaultDownloader : IDownloader
    {
        public Task DownloadAsync(string source, string destination, State state, double downloads = 1)
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

            client.DownloadFileAsync(new Uri(source), destination);

            return tcs.Task;
        }
    }
}