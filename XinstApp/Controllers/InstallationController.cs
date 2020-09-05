using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Polyzynger.Installers;

namespace Polyzynger.Controllers
{
    class InstallationController
    {
        public List<Installer> Installers { get; set; }
        public List<Installer> Browsers { get; set; }
        public List<Installer> Runtimes { get; set; }
        public List<Installer> Multimedia { get; set; }
        public List<Installer> Office { get; set; }
        public List<Installer> Security { get; set; }
        public List<Installer> Utilities { get; set; }
        private readonly static SemaphoreSlim _installationSemaphore = new SemaphoreSlim(1);
        private static InstallationController _instance = null;

        public static InstallationController Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallationController(); }
                return _instance;
            }
        }

        private InstallationController()
        {
            Multimedia = new List<Installer>
            {
                InstallerMPC.Instance,
            };
            Browsers = new List<Installer>
            {
                InstallerChrome.Instance
            };
            Runtimes = new List<Installer>
            {
                InstallerJava8.Instance,
                InstallerAdobeAir.Instance
            };
            Office = new List<Installer>
            {
                InstallerAdobeReader.Instance,
                InstallerLOffice.Instance
            };
            Utilities = new List<Installer>
            {
                Installer7Zip.Instance,
                InstallerTV.Instance
            };
            Security = new List<Installer>
            {
                InstallersCreator.CreateNOD32Installer(),
                InstallersCreator.CreateEISInstaller(),
                InstallersCreator.CreateESSPInstaller(),
                InstallersCreator.CreateEEAInstaller()
            };

            this.Installers = new List<Installer>();
            this.Installers.AddRange(this.Browsers);
            this.Installers.AddRange(this.Runtimes);
            this.Installers.AddRange(this.Office);
            this.Installers.AddRange(this.Security);
            this.Installers.AddRange(this.Utilities);
            this.Installers.AddRange(this.Multimedia);
        }

        public Task Perform()
        {
            List<Task> tasks = new List<Task>();
            foreach (var installer in this.Installers) 
            {
                if (installer.Controls.CheckBox.IsChecked.Value) 
                { 
                    tasks.Add(InstallAsync(installer)); 
                } 
            }
            return Task.WhenAll(tasks);
        }

        private async Task InstallAsync(Installer installer)
        {
            installer.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)0, (byte)0));

            installer.Controls.ProgressBar.Visibility = Visibility.Visible;

            try
            {
                installer.Controls.Status.Content = "DOWNLOADING...";
                await installer.DownloadAsync();
            }
            catch
            {
                installer.Controls.Status.Content = "DOWNLOAD ERROR";
                installer.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)144, (byte)0, (byte)0));
                return;
            }
            finally
            {
                installer.Controls.ProgressBar.Visibility = Visibility.Hidden;
            }

            installer.Controls.Status.Content = "WAITING";

            try
            {
                await _installationSemaphore.WaitAsync();
                installer.Controls.Status.Content = ">> INSTALLING";
                installer.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)147, (byte)217));
                await installer.Install();
            }
            catch
            {
                installer.Controls.Status.Content = "INSTALL ERROR";
                installer.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)144, (byte)0, (byte)0));
                return;
            }
            finally
            {
                _installationSemaphore.Release();
                installer.DeleteTempFiles();
            }

            installer.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)144, (byte)0));
            installer.Controls.Status.Content = "DONE";
        }
    }
}