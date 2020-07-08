using System;
using System.IO;
using System.Threading.Tasks;

namespace XinstApp.Installers.SevenAds
{
    class DesktopArranger : Script
    {
        private static DesktopArranger _instance = null;

        public static DesktopArranger Instance
        {
            get
            {
                if (_instance == null) { _instance = new DesktopArranger(); }
                return _instance;
            }
        }

        private DesktopArranger()
        {
            this.Controls.CheckBox.Content = "Arrange Desktop";
        }

        public override Task Perform()
        {
            string script = Resources.ContentLoading.GetResource("AdjustDesktopIcons.ps1");
            if (script == null || script == "")
            {
                return null;
            }
            return Task.Run(() =>
            {
                ExecuteScript(script);
                DeleteEdgeShotcut();
            });
        }
        private static void DeleteEdgeShotcut()
        {
            Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory));
            Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            void Delete(string folder)
            {
                string path = Path.Combine(folder, "Microsoft Edge.lnk");
                if (File.Exists(path)) File.Delete(path);
            }
        }
    }
}