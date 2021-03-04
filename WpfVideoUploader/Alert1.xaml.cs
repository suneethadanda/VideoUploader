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
    /// Interaction logic for Alert1.xaml
    /// </summary>
    public partial class Alert1 : Window
    {
       

        public Alert1(string message)
        {
            InitializeComponent();
            lblMessage.Content = message;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
