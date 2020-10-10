namespace Polyzynger.Installers
{
    class InstallerChrome : Installer
    {
        public InstallerChrome(string remotePath, string fileName, string content, string arguments = null)
        {
            this.RemotePath = remotePath;
            this.FileName = fileName;
            this.Controls.CheckBox.Content = content;
            this.Arguments = arguments ?? this.Arguments;
            this.Controls.CheckBox.IsChecked = true;
        }
    }
}