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
    /// Interaction logic for Alert.xaml
    /// </summary>
    public partial class Alert : Window
    {
        public string message = string.Empty;
        public bool close=false;
        public Alert(string alertmessage)
        {
            InitializeComponent();
            this.message = alertmessage;
            lblMessage.Content = message;
            lblMessage.Visibility = Visibility.Visible;
            close = false;
        }

        public Alert()
        {
            InitializeComponent();
            lblMessage.Content = message;
           
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            close = true;
            this.Hide();
           
        }

        private void btnNO_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

       
    }
}
