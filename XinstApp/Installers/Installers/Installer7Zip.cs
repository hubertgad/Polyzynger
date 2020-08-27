using System.Net;
using System.Text.RegularExpressions;

namespace XinstApp.Installers
{
    class Installer7Zip : Installer
    {
        private static Installer7Zip _instance = null;

        public static Installer7Zip Instance 
        {
            get
            {
                if (_instance is null) { _instance = new Installer7Zip(); }
                return _instance;
            } 
        }

        private Installer7Zip() : base()
        {
            this.RemotePath = "https://www.7-zip.org/a/7z1900-x64.msi";
            this.FileName = "7z1900-x64.msi";
            this.Controls.CheckBox.Content = "7-Zip";
        }
        
        /// <inheritdoc/>
        protected override string EstablishLastestVersionPath()
        {
            using (WebClient client = new WebClient())
            {
                string page = client.DownloadString("https://www.7-zip.org/download.html");
                string version = Regex.Match(page, "7-Zip [0-9]{2}[.][0-9]{2}", RegexOptions.IgnoreCase).Value;
                string versionNo = Regex.Match(version, "[0-9]+[.][0-9]+").Value;
                versionNo = versionNo.Replace(".", "");
                this.NewRemotePath = this.RemotePath.Replace("1900", versionNo);
                
                return base.EstablishLastestVersionPath();
            }
        }
    }
}