using System.Windows;
using System.Windows.Input;

namespace WpfVideoUploader
{
    partial class HomePageStyle
    {
        private void Hyperlink_RequestNavigate(object sender, RoutedEventArgs e)
        {
            Common.OHome.UpdateStatus();
        }

        private void lnkText_MouseEnter(object sender, MouseEventArgs e)
        {
            Common.OHome.Cursor = Cursors.Hand;
        }

        private void lnkText_MouseLeave(object sender, MouseEventArgs e)
        {
            Common.OHome.Cursor = Cursors.Arrow;
        }
    }
}
