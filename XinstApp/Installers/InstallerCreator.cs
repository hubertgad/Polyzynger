namespace Polyzynger.Installers
{
    static class InstallerCreator
    {
        public static InstallerChrome CreateChromeInstaller()
        {
            return new InstallerChrome(
                remotePath: "https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B7A3ACD62-E5A4-2144-FC0C-A17C986F1B00%7D%26lang%3Dpl%26browser%3D4%26usagestats%3D1%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-stable-statsdef_1%26installdataindex%3Dempty/update2/installers/ChromeSetup.exe",
                fileName: "ChromeSetup.exe",
                arguments: "/silent /install",
                content: "Google Chrome");
        }

        public static InstallerESET CreateNOD32Installer()
        {
            return new InstallerESET(
                remotePath: "https://download.eset.com/com/eset/tools/installers/live_eav/latest/eset_nod32_antivirus_live_installer.exe",
                fileName:   "eset_nod32_antivirus_live_installer.exe",
                content:    "ESET NOD32",
                arguments:  "--silent --accepteula --avr-disable");
        }

        public static InstallerESET CreateEISInstaller()
        {
            return new InstallerESET(
                remotePath: "https://download.eset.com/com/eset/tools/installers/live_eis/latest/eset_internet_security_live_installer.exe",
                fileName:   "eset_internet_security_live_installer.exe",
                content:    "ESET IS",
                arguments:  "--silent --accepteula --avr-disable",
                isChecked:  false);
        }

        public static InstallerESET CreateESSPInstaller()
        {
            return new InstallerESET(
                remotePath: "https://download.eset.com/com/eset/tools/installers/live_essp/latest/eset_smart_security_premium_live_installer.exe",
                fileName:   "eset_internet_security_live_installer.exe",
                content:    "ESET SSP",
                arguments:  "--silent --accepteula --avr-disable",
                isChecked:  false);
        }
        
        public static InstallerESET CreateEEAInstaller()
        {
            return new InstallerESET(
                remotePath: "https://download.eset.com/com/eset/apps/business/eea/windows/latest/eea_nt64.msi",
                fileName:   "eea_nt64.msi",
                content:    "ESET Endpoint",
                isChecked:  false );
        }
    }
}