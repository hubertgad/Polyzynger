namespace XinstApp.Installers
{
    class InstallerJava8 : Installer
    {
        private static InstallerJava8 _instance;
        public static InstallerJava8 Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallerJava8(); }
                return _instance;
            }
        }

        private InstallerJava8()
        {
            this.RemotePath = "https://javadl.oracle.com/webapps/download/AutoDL?BundleId=242990_a4634525489241b9a9e1aa73d9e118e6";
            this.FileName = "jre.exe";
            this.Arguments = "/s";
            this.Controls.CheckBox.Content = "Java 8";
        }
    }
}