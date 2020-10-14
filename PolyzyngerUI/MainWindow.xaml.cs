using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using PolyzyngerApplication;
using PolyzyngerApplication.API;
using PolyzyngerUI.Controls;

namespace PolyzyngerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Facade Facade { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Facade = Facade.Instance;
            SSID.Text = "SSID";
            Password.Password = "password";
        }

        private async void StartButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            EnableInterface(false);

            List<Task> tasks = new List<Task>();

            if (SIconCheckBox.IsChecked.Value)
                tasks.Add(Facade.CopySevenIconAsync((s, st) => AssignControls(SIconStatus, null, st)));

            if (SThemeCheckBox.IsChecked.Value)
                tasks.Add(Facade.ApplySevenThemeAsync((s, st) => AssignControls(SThemeStatus, null, st)));

            if (HSearchBarCheckBox.IsChecked.Value)
                tasks.Add(Facade.CopySevenIconAsync((s, st) => AssignControls(HSearchBarStatus, null, st)));

            if (ADesktopCheckBox.IsChecked.Value)
                tasks.Add(Facade.CopySevenIconAsync((s, st) => AssignControls(ADesktopStatus, null, st)));

            if (KLiteCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallKLiteCodecsAsync((s, st) => AssignControls(KLiteStatus, KLiteProgressBar, st)));

            if (GChromeCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallGoogleChromeAsync((s, st) => AssignControls(GChromeStatus, GChromeProgressBar, st)));

            if (AReaderCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallAdobeReaderAsync((s, st) => AssignControls(AReaderStatus, AReaderProgressBar, st)));

            if (LOfficeCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallLibreOfficeAsync((s, st) => AssignControls(LOfficeStatus, LOfficeProgressBar, st)));

            if (SZipCheckBox.IsChecked.Value)
                tasks.Add(Facade.Install7ZipAsync((s, st) => AssignControls(SZipStatus, SZipProgressBar, st)));

            if (TVCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallTeamViewerAsync((s, st) => AssignControls(TVStatus, TVProgressBar, st)));

            if (JavaCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallJavaAsync((s, st) => AssignControls(JavaStatus, JavaProgressBar, st)));

            if (AAirCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallAdobeAirAsync((s, st) => AssignControls(AAirStatus, AAirProgressBar, st)));

            if (EsetNod32CheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallESETNOD32Async((s, st) => AssignControls(EsetNod32Status, EsetNod32ProgressBar, st)));

            if (EsetISCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallESETISAsync((s, st) => AssignControls(EsetISStatus, EsetISProgressBar, st)));

            if (EsetSSPCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallESETSSPAsync((s, st) => AssignControls(EsetSSPStatus, EsetSSPProgressBar, st)));

            if (EsetEndpointCheckBox.IsChecked.Value)
                tasks.Add(Facade.InstallEndPointAsync((s, st) => AssignControls(EsetEndpointStatus, EsetEndpointProgressBar, st)));

            await Task.WhenAll(tasks);

            EnableInterface(true);
        }

        private void AssignControls(StatusLabel label, CustomProgressBar bar, State state)
        {
            label.UpdateContent(state);
            if (bar != null) bar.Value = state.DownloadProgress;
        }

        private void UncheckButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAll(false);
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAll(true);
        }

        private async void ConnectToWiFiButton_Click(object sender, RoutedEventArgs e)
        {
            await Facade.ConnectToWiFi(async (s, e) => await Task.CompletedTask);
        }

        private void CheckAll(bool value)
        {
            SIconCheckBox.IsChecked = value;
            SThemeCheckBox.IsChecked = value;
            HSearchBarCheckBox.IsChecked = value;
            ADesktopCheckBox.IsChecked = value;
            KLiteCheckBox.IsChecked = value;
            GChromeCheckBox.IsChecked = value;
            AReaderCheckBox.IsChecked = value;
            LOfficeCheckBox.IsChecked = value;
            SZipCheckBox.IsChecked = value;
            TVCheckBox.IsChecked = value;
            JavaCheckBox.IsChecked = value;
            AAirCheckBox.IsChecked = value;
            EsetNod32CheckBox.IsChecked = value;
            EsetISCheckBox.IsChecked = value;
            EsetSSPCheckBox.IsChecked = value;
            EsetEndpointCheckBox.IsChecked = value;
        }

        private void EnableInterface(bool value)
        {
            StartButton.IsEnabled = value;
            CheckButton.IsEnabled = value;
            UncheckButton.IsEnabled = value;
            SSID.IsEnabled = value;
            Password.IsEnabled = value;
            ConnectToWiFiButton.IsEnabled = value;
            SIconCheckBox.IsEnabled = value;
            SThemeCheckBox.IsEnabled = value;
            HSearchBarCheckBox.IsEnabled = value;
            ADesktopCheckBox.IsEnabled = value;
            KLiteCheckBox.IsEnabled = value;
            GChromeCheckBox.IsEnabled = value;
            AReaderCheckBox.IsEnabled = value;
            LOfficeCheckBox.IsEnabled = value;
            SZipCheckBox.IsEnabled = value;
            TVCheckBox.IsEnabled = value;
            JavaCheckBox.IsEnabled = value;
            AAirCheckBox.IsEnabled = value;
            EsetNod32CheckBox.IsEnabled = value;
            EsetISCheckBox.IsEnabled = value;
            EsetSSPCheckBox.IsEnabled = value;
            EsetEndpointCheckBox.IsEnabled = value;
        }
    }
}