using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Polyzynger.Installers.SevenAds
{
    class SevenAdsController
    {
        public List<Script> Ads { get; set; }

        private static SevenAdsController _instance = null;

        public static SevenAdsController Instance
        {
            get
            {
                if (_instance == null) { _instance = new SevenAdsController(); }
                return _instance;
            }
        }

        private SevenAdsController()
        {
            Ads = new List<Script>
            {
                Icon.Instance,
                Themepack.Instance,
                SearchBarHider.Instance,
                DesktopArranger.Instance
            };
        }

        public async Task Perform()
        {
            foreach (var ad in Ads)
            {
                if (ad.Controls.CheckBox.IsChecked.Value)
                {
                    ad.Controls.Status.Content = "PERFORMING";
                    try
                    {
                        await ad.Perform();
                        ad.Controls.Status.Content = "DONE";
                        ad.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)144, (byte)0));
                    }
                    catch
                    {
                        ad.Controls.Status.Content = "ERROR";
                        ad.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)144, (byte)0, (byte)0));
                    }
                }
            }
        }
    }
}