using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class AppSettings : Window
    {
        public string message = string.Empty;
        public bool close=false;
        public AppSettings(string alertmessage)
        {
            InitializeComponent();
            this.message = alertmessage;           
            close = false;
        }

        public AppSettings()
        {
            InitializeComponent();
            bool isenabled = false;
            string settingsvalue = Common.GetDefaultCheckValue("DefaultVideoAllwaysOn");
            if (settingsvalue != null)
                 isenabled = Convert.ToBoolean(settingsvalue);
            if (isenabled)
                chkDefaultVideo.IsChecked = true;
            else
                chkDefaultVideo.IsChecked = false;

        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            close = true;
            if (chkDefaultVideo.IsChecked.Value)
                Common.SetDefaultCheckValue("DefaultVideoAllwaysOn", "True");
            else
                Common.SetDefaultCheckValue("DefaultVideoAllwaysOn", "False");

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

        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static void SetSetting(string key, string value)
        {
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }


    }
}
