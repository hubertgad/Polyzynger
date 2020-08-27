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
            this.RemotePath = "https://download.eset.com/com/eset/apps/business/eea/windows/latest/eea_nt64.msi";
            this.FileName = "eea_nt64.msi";
            this.Controls.CheckBox.Content = "ESET Endpoint";
        }
    }
}