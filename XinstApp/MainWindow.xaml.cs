using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Polyzynger.Installers;
using System.Linq;
using Polyzynger.Installers.SevenAds;
using Polyzynger.Controllers;
using Polyzynger.Utilities;

namespace Polyzynger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InstallationController InstallationController => InstallationController.Instance;
        SevenAdsController SevenAdsController => SevenAdsController.Instance;

        public MainWindow()
        {
            InitializeComponent();
            MouseDown += (o, e) => { if (e.ChangedButton == MouseButton.Left) this.DragMove(); };

            this.SSID.Text = "seven-guest";
            this.password.Password = "Seven123"; //easy, it's public
            this.output.Text += "Output:";

            AddColumnDefinitions(this.ColumnOne);
            AddColumnDefinitions(this.ColumnTwo);
            AddColumnDefinitions(this.ColumnThree);

            AddGroupToColumn(SevenAdsController.Ads.Select(q => q.Controls).ToList(), "Seven");
            AddGroupToColumn(InstallationController.Browsers.Select(q => q.Controls).ToList(), "Browsers");
            AddGroupToColumn(InstallationController.Runtimes.Select(q => q.Controls).ToList(), "Runtimes");
            AddGroupToColumn(InstallationController.Office.Select(q => q.Controls).ToList(), "Office Apps");
            AddGroupToColumn(InstallationController.Security.Select(q => q.Controls).ToList(), "Security");
            AddGroupToColumn(InstallationController.Utilities.Select(q => q.Controls).ToList(), "Utilities");
            AddGroupToColumn(InstallationController.Multimedia.Select(q => q.Controls).ToList(), "Multimedia");

            this.ColumnOne.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Star) });
            this.ColumnTwo.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Star) });
            this.ColumnThree.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Star) });
        }

        /// <summary>
        /// Divides given grid into three columns.
        /// </summary>
        /// <param name="grid">Grid to divide.</param>
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

        /// <summary>
        /// 
        /// </summary>
        private async void StartButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (!ConnectionChecker.IsConnectedToInternet())
            {
                ShowNoInternetWarning();
                return;
            }

            EnableInterface(false);

            //List<Task> tasks = new List<Task>
            //{
            //    InstallationController.Perform(),
            //    SevenAdsController.Perform()
            //};
            //await Task.WhenAll(tasks);

            await Task.WhenAll(
                InstallationController.Perform(), 
                SevenAdsController.Perform());

            EnableInterface(true);
        }

        /// <summary>
        /// Simply shows an warning message box which informs that there is no Internet connection.
        /// </summary>
        private void ShowNoInternetWarning()
        {
            MessageBox.Show(
                "No Internet connection detected. Please, connect to the Internet and try again.",
                "Warning", 
                MessageBoxButton.OK, 
                MessageBoxImage.Warning);
        }

        /// <summary>
        /// Enables or disables all controls depending on passed bool variable.
        /// </summary>
        /// <param name="value">True to enable or false to disable controls.</param>
        private void EnableInterface(bool value)
        {
            this.ConnectToWiFiButton.IsEnabled = value;
            this.StartButton.IsEnabled = value;
            this.CheckButton.IsEnabled = value;
            this.UncheckButton.IsEnabled = value;
            foreach (var installer in InstallationController.Installers) { installer.Controls.CheckBox.IsEnabled = value; }
            foreach (var ad in SevenAdsController.Instance.Ads) { ad.Controls.CheckBox.IsEnabled = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckButton_Click(object sender, RoutedEventArgs e) => CheckAll(true);

        /// <summary>
        /// 
        /// </summary>
        private void UncheckButton_Click(object sender, RoutedEventArgs e) => CheckAll(false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void CheckAll(bool value)
        {
            foreach (var installer in InstallationController.Installers) { installer.Controls.CheckBox.IsChecked = value; }
            foreach (var ad in SevenAdsController.Instance.Ads) { ad.Controls.CheckBox.IsChecked = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private async void ConnectToWiFi_Click(object sender, RoutedEventArgs e)
        {
            await WiFi.ConnectToWiFiAsync(this.SSID.Text, this.password.Password);
        }
    }
}