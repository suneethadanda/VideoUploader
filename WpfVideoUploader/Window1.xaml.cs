using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //var window = (Window)((FrameworkElement)sender).TemplatedParent;
            //window.WindowState = WindowState.Minimized;
            //this.WindowState = WindowState.Maximized;
            //var window = (Window)((FrameworkElement)sender).TemplatedParent;
            //window.DragMove();

        }
    }
}
