using PolyzyngerApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Checkers
{
    class AdobeReaderChecker : IChecker
    {
        public async Task<(string installer, string patch)> CheckLatestVersionPathAsync()
        {
            string patchRemotePath = string.Empty;

            await Task.Run(async () =>
            {
                string initialFTPDirPath = "ftp://ftp.adobe.com/pub/adobe/reader/win/AcrobatDC/";

                List<string> initialDirLst = await ListFTPDirectoryAsync(initialFTPDirPath);

                int latestPatchNo = GetLatestPatchNo(initialDirLst);

                string endFTPDirPath = Path.Combine(initialFTPDirPath, latestPatchNo.ToString());

                List<string> endDirLst = await ListFTPDirectoryAsync(endFTPDirPath);

                var patchFileName = MatchPatchFileName(endDirLst, latestPatchNo);

                patchRemotePath = Path.Combine(endFTPDirPath, patchFileName);

                patchRemotePath = patchRemotePath.Replace("ftp://ftp.adobe.com/", "http://ardownload.adobe.com/");

            });
            System.Diagnostics.Debug.WriteLine(patchRemotePath);
            Console.WriteLine(patchRemotePath);
            return (string.Empty, patchRemotePath);
        }

        private string MatchPatchFileName(List<string> list, int versionNo)
        {
            Regex regex = new Regex($"(\\w+{ versionNo }\\.(msp))");

            return regex.Match(list.FirstOrDefault(q => regex.Match(q).Success)).Value;
        }

        private int GetLatestPatchNo(List<string> directories)
            => directories.Where(q => int.TryParse(q, out _)).Select(q => int.Parse(q)).Max();

        private async Task<List<string>> ListFTPDirectoryAsync(string path)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);

            request.Method = WebRequestMethods.Ftp.ListDirectory;
            
            List<string> result = new List<string>();

            using (FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync())
            {
                Stream responseStream = response.GetResponseStream();

                using StreamReader reader = new StreamReader(responseStream);

                while (!reader.EndOfStream) 
                { 
                    result.Add(await reader.ReadLineAsync()); 
                }
            }

            return result;
        }
    }
}