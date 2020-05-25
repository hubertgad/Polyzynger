using System.IO;

namespace XinstApp.Installers
{
    class InstallerESET : Installer
    {
        private static InstallerESET _instance;
        public static InstallerESET Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallerESET(); }
                return _instance;
            }
        }

        private InstallerESET()
        {
            this.remotePath = "https://download.eset.com/com/eset/apps/business/eea/windows/latest/eea_nt64.msi";
            this.fileName = "eea_nt64.msi";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.Controls.CheckBox.Content = "ESET";
        }

        //TODO: ESET Installer
    }
}