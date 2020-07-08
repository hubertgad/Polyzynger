using System;
using System.IO;
using System.Threading.Tasks;
using XinstApp.Resources;

namespace XinstApp.Installers.SevenAds
{
    class Icon : Script
    {
        private static Icon _instance = null;

        public static Icon Instance
        {
            get
            {
                if (_instance == null) { _instance = new Icon(); }
                return _instance;
            }
        }

        private Icon()
        {
            this.Controls.CheckBox.Content = "Seven Icon";
        }

        public override Task Perform()
        {
            string sevenDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString(), "Seven");
            Directory.CreateDirectory(sevenDir);
            string iconDestination = Path.Combine(sevenDir, "Seven.ico");
            string linkDestination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString(), "Prosimy o opinię!.url");
            string lnkText = ContentLoading.GetResource("Prosimy o opinie!.url");
            lnkText = lnkText.Replace("USERNAME", Environment.UserName.ToString());
            CopyResource("Seven.ico", iconDestination);
            using (Stream stream = File.Create(linkDestination))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    return writer.WriteAsync(lnkText);
                }
            }
        }
    }
}