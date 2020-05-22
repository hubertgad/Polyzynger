using System.IO;

namespace XinstApp.Installers
{
    class InstallerMPC : Installer
    {
        private static InstallerMPC instance = null;

        public static InstallerMPC GetInstance()
        {
            if (instance == null) { instance = new InstallerMPC(); }
            return instance;
        }

        private InstallerMPC()
        {
            this.remotePath = "https://files3.codecguide.com/K-Lite_Codec_Pack_1548_Standard.exe";
            this.fileName = "K-Lite_Codec_Pack_1548_Standard.exe";  //TODO: Updater MPC
                                                                    // https://codecguide.com/download_k-lite_codec_pack_standard.htm
            this.localPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.arguments = "/verysilent";
            this.Controls.CheckBox.Content = "K-Lite Codecs";
        }
        //TODO: Installer MPC
    }
}