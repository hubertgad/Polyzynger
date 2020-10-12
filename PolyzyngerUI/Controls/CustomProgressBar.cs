using System.Windows;
using System.Windows.Controls;

namespace PolyzyngerUI.Controls
{
    class CustomProgressBar : ProgressBar
    {
        public CustomProgressBar()
        {
            Style = TryFindResource("CustomProgressBar") as Style;
            Visibility = Visibility.Hidden;
        }

        public new double Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                base.Value = value;
                if (value > 0 && value < 100)
                {
                    Visibility = Visibility.Visible;
                }
                else
                {
                    Visibility = Visibility.Hidden;
                }
            }
        }
    }
}