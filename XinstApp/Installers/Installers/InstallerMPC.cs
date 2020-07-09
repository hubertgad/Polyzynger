using System.IO;

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
            this.remotePath = "https://files3.codecguide.com/K-Lite_Codec_Pack_1548_Standard.exe";
            this.fileName = "K-Lite_Codec_Pack_1548_Standard.exe";  //TODO: Updater MPC
                                                                    // https://codecguide.com/download_k-lite_codec_pack_standard.htm
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.arguments = "/verysilent";
            this.Controls.CheckBox.Content = "K-Lite Codecs";
        }
    }
}