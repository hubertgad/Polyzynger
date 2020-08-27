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
            this.RemotePath = "https://download.teamviewer.com/download/TeamViewer_Setup.exe";
            this.FileName = "TeamViewer_Setup.exe";
            this.Arguments = "/S /norestart";
            this.Controls.CheckBox.Content = "TeamViewer";
            this.Controls.CheckBox.IsChecked = false;
        }
    }
}