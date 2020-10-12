using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PolyzyngerUI.Controls
{
    class StatusLabel : Label
    {
        public StatusLabel()
        {
            Style = TryFindResource("CustomLabel") as Style;
        }

        public new object Content
        {
            get
            {
                return base.Content;
            }
            set
            {
                if (value.ToString() == "Downloading")
                {
                    base.Content = "DOWNLOADING";
                    Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)0, (byte)0));
                }
                if (value.ToString() == "Waiting")
                {
                    base.Content = "WAITING";
                    Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)0, (byte)0));
                }
                if (value.ToString() == "Installing")
                {
                    base.Content = ">> INSTALLING";
                    Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)147, (byte)217));
                }
                if (value.ToString() == "Updating")
                {
                    base.Content = "UPDATING";
                    Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)147, (byte)217));
                }
                if (value.ToString() == "Done")
                {
                    base.Content = "DONE";
                    Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)144, (byte)0));
                }
                if (value.ToString() == "Error")
                {
                    base.Content = "ERROR";
                    Foreground = new SolidColorBrush(Color.FromRgb((byte)144, (byte)0, (byte)0));
                }
            }
        }
    }
}