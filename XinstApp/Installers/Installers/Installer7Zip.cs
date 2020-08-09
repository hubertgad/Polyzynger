using System.IO;

namespace XinstApp.Installers
{
    class Installer7Zip : Installer
    {
        private static Installer7Zip _instance = null;

        public static Installer7Zip Instance
        {
            get
            {
                if (_instance == null) { _instance = new Installer7Zip(); }
                return _instance;
            }
        }

        private Installer7Zip() : base()
        {
            this.remotePath = "https://www.7-zip.org/a/7z1900-x64.msi"; //TODO: Updater 7-zip
            this.fileName = "7z1900-x64.msi";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.Controls.CheckBox.Content = "7-Zip";
        }
    }
}