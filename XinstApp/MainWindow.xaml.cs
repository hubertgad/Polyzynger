using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            MouseDown += (o, e) => { if (e.ChangedButton == MouseButton.Left) this.DragMove(); }; //TODO: block alt+f4 closing, add some on close behaviour
            Installers = new List<Installer> 
            {
                //TODO: Change all GetInstance() to Instance prop
                InstallerChrome.Instance, 
                Installer7Zip.Instance, 
                InstallerMPC.GetInstance(), 
                InstallerMSSilverlight.Instance, 
                InstallerJRE.Instance, 

                InstallerLOffice.Instance,
                InstallerTV.GetInstance()
            };

            AddColumnDefinitions(this.ColumnOne);
            AddColumnDefinitions(this.ColumnTwo);
            AddColumnDefinitions(this.ColumnThree);
            for (int i = 0; i < this.Installers.Count; i++)
            {
                Grid column;
                if (i <= this.Installers.Count / 3) { column = this.ColumnOne; }
                else if (i <= this.Installers.Count * 2 / 3) { column = this.ColumnTwo; }
                else { column = this.ColumnThree; }
                column.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                Grid.SetRow(Installers[i].Controls.CheckBox, column.RowDefinitions.Count - 1);
                Grid.SetRow(Installers[i].Controls.ProgressBar, column.RowDefinitions.Count - 1);
                Grid.SetRow(Installers[i].Controls.Status, column.RowDefinitions.Count - 1);
                column.Children.Add(Installers[i].Controls.CheckBox);
                column.Children.Add(Installers[i].Controls.ProgressBar);
                column.Children.Add(Installers[i].Controls.Status);
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e) { Main = this; }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            this.StartButton.IsEnabled = false;
            this.ExitButton.IsEnabled = false;
            List<Task> tasks = new List<Task>();
            foreach (var installer in this.Installers)
            {
                if (installer.Controls.CheckBox.IsChecked.Value) tasks.Add(InstallAsync(installer));
            }
            await Task.WhenAll(tasks);
            this.StartButton.IsEnabled = true;
            this.ExitButton.IsEnabled = true;
        }

        private async Task InstallAsync(Installer installer)
        {
            var bar = installer.Controls.ProgressBar;
            var status = installer.Controls.Status;

            DownloadProgressChangedEventHandler downloadHandler = 
                (object sender, DownloadProgressChangedEventArgs e) => this.Dispatcher.BeginInvoke(new Action(() => installer.Controls.ProgressBar.Value = e.ProgressPercentage));
            
            await installer.DownloadAsync(downloadHandler);
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Close();        
    }
}
