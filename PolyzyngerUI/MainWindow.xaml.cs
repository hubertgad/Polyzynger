using System.Configuration;
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
        public static Facade Facade { get; } = Facade.Instance;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (GChromeCheckBox.IsChecked.Value)
                await Facade.InstallGoogleChromeAsync((s, st) => AssignStateToControls(GChromeStatus, GChromeProgressBar, st));

            if (AReaderCheckBox.IsChecked.Value)
                await Facade.InstallAdobeReaderAsync((s, st) => AssignStateToControls(AReaderStatus, AReaderProgressBar, st));
        }

        private void AssignStateToControls(StatusLabel label, CustomProgressBar bar, State state)
        {
            label.Content = state.Stage.ToString();
            bar.Value = state.DownloadProgress;
            Percentage.Content = state.DownloadProgress.ToString();
        }
    }
}