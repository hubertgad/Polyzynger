using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            this.remotePath = "https://www.7-zip.org/a/7z1900-x64.msi"; //TODO: Add method which finds lastest version of 7-zip
            this.fileName = "7z1900-x64.msi";
            this.localPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.arguments = "/qn";
            this.Controls.CheckBox.Content = "7-Zip";
        }
    }
}
