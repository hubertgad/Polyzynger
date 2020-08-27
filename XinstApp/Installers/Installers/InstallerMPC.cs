using System.Net;
using System.Text.RegularExpressions;

namespace XinstApp.Installers
{
    class InstallerMPC : Installer
    {
        private static InstallerMPC _instance = null;

        public static InstallerMPC Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallerMPC(); }
                return _instance;
            }
        }

        private InstallerMPC()
        {
            this.RemotePath = "https://files3.codecguide.com/K-Lite_Codec_Pack_1548_Standard.exe";
            this.FileName = "K-Lite_Codec_Pack_1548_Standard.exe";
            this.Arguments = "/verysilent";
            this.Controls.CheckBox.Content = "K-Lite Codecs";
        }
        
        protected override string EstablishLastestVersionPath()
        {
            Regex regex = new Regex($"https://files[d].codecguide.com/.*?.exe", RegexOptions.IgnoreCase);

            using (WebClient client = new WebClient())
            {
                string page = client.DownloadString("https://codecguide.com/download_k-lite_codec_pack_standard.htm");
                this.NewRemotePath = Regex.Match(page, "https://files[\\d].codecguide.com/.*?.exe", RegexOptions.IgnoreCase).Value;
                
                return base.EstablishLastestVersionPath();
            }
        }
    }
}
