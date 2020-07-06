using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XinstApp.Installers.SevenAds
{
    class SevenAdsController
    {
        public List<SevenBase> Ads { get; set; }

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
            Ads = new List<SevenBase>
            {
                Icon.Instance,
                Themepack.Instance,
                SearchBarHider.Instance,
                DesktopArranger.Instance
            };
        }

        public async Task Install()
        {
            foreach (var ad in Ads)
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