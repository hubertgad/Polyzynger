using System.Collections.Generic;
using System.Windows;

namespace PolyzyngerUI.Controls
{
    class AVCheckBox : CustomCheckBox
    {
        private static List<AVCheckBox> _aVCheckBoxes = new List<AVCheckBox>();

        public AVCheckBox()
        {
            _aVCheckBoxes.Add(this);
            Checked += AVCheckBox_Checked;
        }

        private void AVCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var checkBox in _aVCheckBoxes)
            {
                if (checkBox != this)
                {
                    checkBox.IsChecked = false;
                }
            }
        }
    }
}