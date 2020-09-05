namespace Polyzynger.Installers
{
    static class InstallersCreator
    {
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