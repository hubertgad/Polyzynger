using PolyzyngerApplication;
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

        public void UpdateContent(State state)
        {
            Content = state.Stage.ToString();
            
            if (state.Stage == Stage.SCANNING)
            {
                Foreground = new SolidColorBrush(Color.FromRgb((byte)75, (byte)0, (byte)130));
            }

            if (state.Stage == Stage.DOWNLOADING)
            {
                Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)0, (byte)0));
            }

            if (state.Stage == Stage.WAITING)
            {
                Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)0, (byte)0));
            }

            if (state.Stage == Stage.INSTALLING)
            {
                Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)147, (byte)217));
            }

            if (state.Stage == Stage.UPDATING)
            {
                Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)147, (byte)217));
            }

            if (state.Stage == Stage.DONE)
            {
                Foreground = new SolidColorBrush(Color.FromRgb((byte)0, (byte)144, (byte)0));
            }

            if (state.Stage == Stage.ERROR)
            {
                base.Content = string.Concat(state.PreviousStage.ToString(), " ", Stage.ERROR.ToString());
                Foreground = new SolidColorBrush(Color.FromRgb((byte)144, (byte)0, (byte)0));
            }
        }
    }
}