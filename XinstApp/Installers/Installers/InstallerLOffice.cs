using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace XinstApp.Installers
{
    class InstallerLOffice : Installer
    {
        private static InstallerLOffice _instance = null;

        public static InstallerLOffice Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallerLOffice(); }
                return _instance;
            }
        }

        private InstallerLOffice() : base()
        {
            this.remotePath = "https://download.documentfoundation.org/libreoffice/stable/6.3.6/win/x86_64/LibreOffice_6.3.6_Win_x64.msi";
            this.fileName = "LibreOffice_6.3.6_Win_x64.msi";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.Controls.CheckBox.Content = "Libre Office";
            this.Controls.CheckBox.IsChecked = false;
        }
        
        protected override string EstablishLastestVersionPath()
        {
            using (WebClient client = new WebClient())
            {
                string page = client.DownloadString("https://pl.libreoffice.org/pobieranie/stabilna/");
                string version = Regex.Match(page, "LibreOffice [0-9]+[.][0-9]+[.][0-9]+", RegexOptions.IgnoreCase).Value;
                string versionNo = Regex.Match(version, "[0-9]+[.][0-9]+[.][0-9]+").Value;
                this.newRemotePath = this.remotePath.Replace("6.3.6", versionNo);
                
                return base.EstablishLastestVersionPath();
            }
        }
    }
}
