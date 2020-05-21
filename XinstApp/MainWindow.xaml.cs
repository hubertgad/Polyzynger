using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using XinstApp.Installers;

namespace XinstApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static MainWindow Main;
        List<Installer> Installers { get; set; }
        public CheckBox CheckBoxTest { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Installers = new List<Installer> 
            {
                InstallerChrome.GetInstance(), 
                Installer7Zip.GetInstance(), 
                InstallerLOffice.GetInstance()
            };

            for (int i = 0, j = 0; i < Installers.Count; i++)
            {
                var grid = GridOne;
                if (j == 0) { grid = GridOne; }
                if (j == 1) { grid = GridTwo; }
                if (j == 2) { grid = GridThree; }
                j++;
                if (j == 3) j = 0;
                Grid.SetRow(Installers[i].Controls.CheckBox, i);
                Grid.SetRow(Installers[i].Controls.ProgressBar, i);
                Grid.SetRow(Installers[i].Controls.Status, i);
                grid.Children.Add(Installers[i].Controls.CheckBox);
                grid.Children.Add(Installers[i].Controls.ProgressBar);
                grid.Children.Add(Installers[i].Controls.Status);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) { Main = this; }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            this.StartButton.IsEnabled = false;
            List<Task> tasks = new List<Task>();
            foreach (var installer in this.Installers)
            {
                if (installer.Controls.CheckBox.IsChecked.Value) tasks.Add(InstallAsync(installer));
            }
            await Task.WhenAll(tasks);
            this.StartButton.IsEnabled = true;
        }

        private async Task InstallAsync(Installer installer)
        {
            var bar = installer.Controls.ProgressBar;
            var status = installer.Controls.Status;

            DownloadProgressChangedEventHandler downloadHandler = 
                (object sender, DownloadProgressChangedEventArgs e) => this.Dispatcher.BeginInvoke(new Action(() => installer.Controls.ProgressBar.Value = e.ProgressPercentage));
            
            await installer.DownloadAsync(downloadHandler);

        }
    }
}