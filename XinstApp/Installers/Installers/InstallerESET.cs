using System.Collections.Generic;

namespace Polyzynger.Installers
{
    class InstallerESET : Installer
    {
        private static List<InstallerESET> Installers { get; set; } = new List<InstallerESET>();

        public InstallerESET(string remotePath, string fileName, string content, string arguments = null, bool isChecked = true) : this()
        {
            this.RemotePath = remotePath;
            this.FileName = fileName;
            this.Controls.CheckBox.Content = content;
            this.Arguments = arguments ?? this.Arguments;
            this.Controls.CheckBox.IsChecked = isChecked;
        }

        private InstallerESET()
        {
            Installers.Add(this);
            this.Controls.CheckBox.Checked += CheckBox_Checked;
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var installer in Installers)
            {
                if (installer != this)
                {
                    installer.Controls.CheckBox.IsChecked = false;
                }
            }
        }
    }
}