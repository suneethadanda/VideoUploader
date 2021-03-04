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
using System.IO;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for AboutUs.xaml
    /// </summary>
    public partial class AboutUs : Window
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DateTime buildDate = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime;
            
            string strDateSuffix = (buildDate.Day == 1 || buildDate.Day == 21 || buildDate.Day == 31) ? "st" :
                (buildDate.Day == 2 || buildDate.Day == 22) ? "nd" :
                (buildDate.Day == 3 || buildDate.Day == 23) ? "rd" : "th";
            lblBuildDate.Content += buildDate.ToString("MMMM") + " ";
            lblBuildDate.Content += buildDate.ToString("dd") + strDateSuffix + " ";
            lblBuildDate.Content += buildDate.ToString("yyyy");

        }
    }
}
