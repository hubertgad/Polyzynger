using System.IO;

namespace XinstApp.Installers
{
    class InstallerTV : Installer
    {
        private static InstallerTV _instance = null;

        public static InstallerTV Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallerTV(); }
                return _instance;
            }
        }

        private InstallerTV()
        {
            this.remotePath = "https://download.teamviewer.com/download/TeamViewer_Setup.exe";
            this.fileName = "TeamViewer_Setup.exe";
            this.localPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.arguments = "/S /norestart";
            this.Controls.CheckBox.Content = "TeamViewer";
        }
        //TODO: Installer TV
    }
}