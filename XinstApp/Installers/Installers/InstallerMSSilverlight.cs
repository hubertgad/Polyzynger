﻿using System;
using System.IO;

namespace XinstApp.Installers
{
    [Obsolete]
    sealed class InstallerMSSilverlight : Installer
    {
        private static InstallerMSSilverlight _instance;
        public static InstallerMSSilverlight Instance
        {
            get 
            {
                if (_instance == null) { _instance = new InstallerMSSilverlight(); }
                return _instance; 
            }
        }

        private InstallerMSSilverlight()
        {
            this.remotePath = "https://download.microsoft.com/download/D/D/F/DDF23DF4-0186-495D-AA35-C93569204409/50918.00/Silverlight_x64.exe";
            this.fileName = "Silverlight_x64.exe";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.arguments = "/q /norestart";
            this.Controls.CheckBox.Content = "MS Silverlight";
            this.Controls.CheckBox.IsChecked = false;
        }
    }
}