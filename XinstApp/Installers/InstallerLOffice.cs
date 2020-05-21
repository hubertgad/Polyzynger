using System.IO;

namespace XinstApp.Installers
{
    class InstallerLOffice : Installer
    {
        private static InstallerLOffice instance = null;

        public static InstallerLOffice GetInstance()
        {
            if (instance == null) { instance = new InstallerLOffice(); }
            return instance;
        }

        private InstallerLOffice() : base()
        {
            this.remotePath = "https://download.documentfoundation.org/libreoffice/stable/6.3.6/win/x86_64/LibreOffice_6.3.6_Win_x64.msi"; //TODO: Updater Libre Office
            this.fileName = "LibreOffice_6.3.6_Win_x64.msi";
            this.localPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.arguments = "/qn";
            this.Controls.CheckBox.Content = "Libre Office";
        }
        //TODO: Installer Libre Office
    }
}