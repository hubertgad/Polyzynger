using System.Windows;
using System.Windows.Controls;

namespace PolyzyngerUI.Controls
{
    class CustomCheckBox : CheckBox
    {
        public CustomCheckBox()
        {
            Style = TryFindResource("CustomCheckBox") as Style;
        }
    }
}