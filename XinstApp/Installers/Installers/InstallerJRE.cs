using System.IO;

namespace XinstApp.Installers
{
    class InstallerJRE : Installer
    {
        private static InstallerJRE _instance;
        public static InstallerJRE Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallerJRE(); }
                return _instance;
            }
        }

        private InstallerJRE()
        {
            this.remotePath = "https://github.com/AdoptOpenJDK/openjdk14-binaries/releases/download/jdk-14.0.1%2B7.1/OpenJDK14U-jre_x64_windows_hotspot_14.0.1_7.msi"; //TODO: Updater OpenJDK
            this.fileName = "OpenJDK14U-jre_x64_windows_hotspot_14.0.1_7.msi";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.arguments = "/q /norestart";
            this.Controls.CheckBox.Content = "OpenJDK JRE";
        }
    }
}