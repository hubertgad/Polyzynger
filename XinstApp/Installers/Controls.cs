using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Polyzynger.Installers
{
    public class Controls
    {
        public CheckBox CheckBox { get; set; }
        public ProgressBar ProgressBar { get; set; }
        public Label Status { get; set; }

        public Controls()
        {
            this.CheckBox = new CheckBox
            {
                IsChecked = true,
                Margin = new Thickness(5)
            };

            this.ProgressBar = new ProgressBar
            {
                Visibility = Visibility.Hidden,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(5),
                Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)147, (byte)217))
            };

            this.Status = new Label
            {
                Content = "",
                FontSize = 10,
                FontWeight = FontWeight.FromOpenTypeWeight(750)
            };

            Grid.SetColumn(this.CheckBox, 0);
            Grid.SetColumn(this.ProgressBar, 1);
            Grid.SetColumn(this.Status, 1);
        }
    }
}