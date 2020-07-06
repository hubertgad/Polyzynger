using System.IO;

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
            this.remotePath = "https://download.documentfoundation.org/libreoffice/stable/6.3.6/win/x86_64/LibreOffice_6.3.6_Win_x64.msi"; //TODO: Updater Libre Office
            this.fileName = "LibreOffice_6.3.6_Win_x64.msi";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.Controls.CheckBox.Content = "Libre Office";
            this.Controls.CheckBox.IsChecked = false;
        }
        //TODO: Installer Libre Office
    }
}