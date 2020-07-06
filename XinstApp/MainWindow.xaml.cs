using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XinstApp.Installers;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Threading;
using XinstApp.Installers.SevenAds;

namespace XinstApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly static SemaphoreSlim sem = new SemaphoreSlim(1);
        internal static MainWindow Main;
        List<Installer> Installers { get; set; }
        List<Installer> Browsers { get; set; }
        List<Installer> Runtimes { get; set; }
        List<Installer> Multimedia { get; set; }
        List<Installer> Office { get; set; }
        List<Installer> Security { get; set; }
        List<Installer> Utilities { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MouseDown += (o, e) => { if (e.ChangedButton == MouseButton.Left) this.DragMove(); }; //TODO: block alt+f4 closing, add some onclose behaviour

            this.SSID.Text = "seven-guest";
            this.password.Password = "Seven123$";
            this.output.Text += "Output:";

            AddColumnDefinitions(this.ColumnOne);
            AddColumnDefinitions(this.ColumnTwo);
            AddColumnDefinitions(this.ColumnThree);

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
                InstallerJRE.Instance,
                InstallerAdobeAir.Instance,
                InstallerMSSilverlight.Instance
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
                InstallerESET.Instance
            };

            this.Installers = new List<Installer>();
            this.Installers.AddRange(this.Browsers);
            this.Installers.AddRange(this.Runtimes);
            this.Installers.AddRange(this.Office);
            this.Installers.AddRange(this.Security);
            this.Installers.AddRange(this.Utilities);
            this.Installers.AddRange(this.Multimedia);

            AddGroupToColumn(SevenAdsController.Instance.Ads.Select(q => q.Controls).ToList(), "Seven items");
            AddGroupToColumn(Runtimes.Select(q => q.Controls).ToList(), "Runtimes");
            AddGroupToColumn(Office.Select(q => q.Controls).ToList(), "Office Apps");
            AddGroupToColumn(Security.Select(q => q.Controls).ToList(), "Security");
            AddGroupToColumn(Utilities.Select(q => q.Controls).ToList(), "Utilities");
            AddGroupToColumn(Multimedia.Select(q => q.Controls).ToList(), "Multimedia");

            this.ColumnOne.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Star) });
            this.ColumnTwo.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Star) });
            this.ColumnThree.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Star) });
        }

        private void AddColumnDefinitions(Grid grid)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Star) });
        }

        /// <summary>
        /// Adds List of Controls to an Grid. 
        /// </summary>
        /// <param name="group">Controls in a group.</param>
        /// <param name="groupName">Name that will be shown as a Label.</param>
        private void AddGroupToColumn(List<Controls> group, string groupName)
        {
            Grid column = ColumnThree.Children.Count < ColumnTwo.Children.Count ? ColumnThree 
                : ColumnTwo.Children.Count < ColumnOne.Children.Count ? ColumnTwo 
                : ColumnOne;

            column.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            Label label = new Label() { Content = groupName, FontWeight = FontWeight.FromOpenTypeWeight(700) };
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, column.RowDefinitions.Count - 1);
            column.Children.Add(label);

            foreach (Controls control in group)
            {
                column.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                Grid.SetRow(control.CheckBox, column.RowDefinitions.Count - 1);
                Grid.SetRow(control.ProgressBar, column.RowDefinitions.Count - 1);
                Grid.SetRow(control.Status, column.RowDefinitions.Count - 1);
                column.Children.Add(control.CheckBox);
                column.Children.Add(control.ProgressBar);
                column.Children.Add(control.Status);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) { Main = this; }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            EnableInterface(false);

            List<Task> tasks = new List<Task>();
            foreach (var installer in this.Installers) { if (installer.Controls.CheckBox.IsChecked.Value) tasks.Add(InstallAsync(installer)); }
            tasks.Add(SevenAdsController.Instance.Install());
            await Task.WhenAll(tasks);

            EnableInterface(true);
        }

        private void EnableInterface(bool value)
        {
            this.StartButton.IsEnabled = value;
            this.CheckButton.IsEnabled = value;
            this.UncheckButton.IsEnabled = value;
            foreach (var installer in this.Installers) { installer.Controls.CheckBox.IsEnabled = value; }
            foreach (var ad in SevenAdsController.Instance.Ads) { ad.Controls.CheckBox.IsEnabled = value; }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) => Close();

        private async Task InstallAsync(Installer installer)
        {
            installer.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)0, (byte)0));

            DownloadProgressChangedEventHandler downloadHandler =
                (s, e) => this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    installer.Controls.ProgressBar.Value = e.ProgressPercentage;
                }));

            installer.Controls.ProgressBar.Visibility = Visibility.Visible;
            installer.Controls.Status.Content = "DOWNLOADING...";
            int result = await installer.DownloadFileAsync(downloadHandler);
            installer.Controls.ProgressBar.Visibility = Visibility.Hidden;

            installer.Controls.Status.Content = "WAITING";
            try
            {
                await sem.WaitAsync();
                installer.Controls.Status.Content = ">> INSTALLING";
                installer.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)147, (byte)217));
                await installer.Install();
            }
            catch (Exception e)
            {
                installer.Controls.Status.Content = "INSTALL ERROR";
                installer.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)144, (byte)0, (byte)0));

                WriteLine(e.Message);
                WriteLine(e.ToString());

                WriteLine($"Błąd podczas instalacji {installer.GetType()} :(");
            }
            finally
            {
                sem.Release();

                installer.Controls.Status.Content = "CLEANING";
                installer.Controls.Status.Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)144, (byte)0));
                installer.DeleteTempFiles();

                installer.Controls.Status.Content = "DONE";
            }
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e) => CheckAll(true);

        private void UncheckButton_Click(object sender, RoutedEventArgs e) => CheckAll(false);

        private void CheckAll(bool value)
        {
            foreach (var installer in this.Installers) { installer.Controls.CheckBox.IsChecked = value; }
            foreach (var ad in SevenAdsController.Instance.Ads) { ad.Controls.CheckBox.IsChecked = value; }
        }

        void WriteLine(string message)
        {
            output.Visibility = Visibility.Visible;
            output.AppendText(DateTime.Now.Hour.ToString("D2") + ":");
            output.AppendText(string.Format(DateTime.Now.Minute.ToString("D2")) + ":");
            output.AppendText(DateTime.Now.Second.ToString("D2") + " >> ");
            output.AppendText(message);
            output.AppendText(Environment.NewLine);
        }

        private async void ConnectToWiFi_Click(object sender, RoutedEventArgs e)
        {
            await WiFi.ConnectToWiFiAsync(this.SSID.Text, this.password.Password);
        }
    }
}