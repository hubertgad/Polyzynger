using System.IO;

namespace XinstApp.Installers
{
    class Installer7Zip : Installer
    {
        private static Installer7Zip _instance = null;

        public static Installer7Zip Instance 
        { 
            get { return _instance ?? _instance = new Installer7Zip(); } 
        }

        private Installer7Zip() : base()
        {
            this.remotePath = "https://www.7-zip.org/a/7z1900-x64.msi"; //TODO: Check 7-zip EstablishLastestVersionPath()
            this.fileName = "7z1900-x64.msi";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.Controls.CheckBox.Content = "7-Zip";
        }
        
        ///<inheritdoc/>
        protected override string EstablishLastestVersionPath()
        {
            using (WebClient client = new WebClient())
            {
                string page = client.DownloadString("https://www.7-zip.org/download.html");
                string version = Regex.Match(page, "7-Zip [0-9]{2}[.][0-9]{2}", RegexOptions.IgnoreCase).Value;
                string versionNo = Regex.Match(version, "[0-9]+[.][0-9]+").Value;
                versionNo = versionNo.Replace(".", "");
                this.newRemotePath = this.remotePath.Replace("1900", versionNo);
                
                return base.EstablishLastestVersionPath();
            }
        }
    }
}
