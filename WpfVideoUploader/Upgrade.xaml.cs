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
using System.Xml;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Net;
using System.Diagnostics;


namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for Upgrade.xaml
    /// </summary>
    public partial class Upgrade : Window
    {
        private RoofTop oRoofTop = null;
        public Dictionary<string, string> lstRootTop;
        VersionHelper objVhelper = null;

        private BackgroundWorker backgroundWorker;
        public string username = string.Empty;
        public Upgrade()
        {
            InitializeComponent();
            backgroundWorker =
                            ((BackgroundWorker)this.FindResource("backgroundWorker"));
            objVhelper = new VersionHelper();
        }

        public Upgrade(string _username, string _Password)
        {
            InitializeComponent();

            backgroundWorker =
                            ((BackgroundWorker)this.FindResource("backgroundWorker"));
            objVhelper = new VersionHelper();
            oRoofTop = new RoofTop();
            this.username = _username;
            var postData = "ACTION=LOGIN";
            postData += ("&USER=" + _username);
            postData += ("&PWD=" + _Password);

            string serverData = string.Empty;
            serverData = Common.GetServerData(postData);
            GetRooftopValues(serverData);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            lblUpgradeMessage.Visibility = Visibility.Visible;
            lblUpgradeMessage.Content = "A new version of this app is available. Would you like" + "\n" + "to upgrade now?"; //ResourceTxt.MsgNewVersion;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                //setupSpiiner.Visibility = Visibility.Visible;
                //Dispatcher.Invoke(new Action(() => btnLogin.IsEnabled = false));

                backgroundWorker.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                //btnLogin.IsEnabled = true;
                // setupSpiiner.Visibility = Visibility.Hidden;
                Common.WriteEventLog(ex.Message, "Error");
            }
        }

        private void btnNO_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            oRoofTop.PopulateRooftop(lstRootTop);
            oRoofTop.RooftopValues = lstRootTop;
            oRoofTop.LoggedInUser = username;
            oRoofTop.ShowDialog();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            oRoofTop.PopulateRooftop(lstRootTop);
            oRoofTop.RooftopValues = lstRootTop;
            oRoofTop.LoggedInUser = username;
            oRoofTop.ShowDialog();
        }

        public void GetRooftopValues(string xmlDoc)
        {
            XmlReader reader = null;
            lstRootTop = new Dictionary<string, string>();
            string strKey = null;
            string strValue = null;
            try
            {
                using (reader = XmlReader.Create(new StringReader(xmlDoc)))
                {
                    while (reader.Read())
                    {
                        reader.ReadToFollowing("rooftop");
                        reader.MoveToFirstAttribute();
                        strKey = reader.Value;
                        reader.Read();
                        strValue = reader.Value;
                        if (!string.IsNullOrEmpty(strValue))
                        {
                            lstRootTop.Add(strKey, strValue);
                        }
                        strKey = null;
                        strValue = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("GetRoofTopValues: " + ex.Message);
                Common.WriteEventLog("GetRoofTopValues: " + ex.Message, "Error");
            }
            finally
            {
                // Do the necessary clean up.
                if (reader != null)
                    reader.Close();
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => Spinner.Visibility = Visibility.Visible));
            Dispatcher.Invoke(new Action(() => btnClose.IsEnabled = false));
            Dispatcher.Invoke(new Action(() => btnYes.Visibility = Visibility.Hidden));
            Dispatcher.Invoke(new Action(() => btnNO.Visibility = Visibility.Hidden));
            Dispatcher.Invoke(new Action(() => lblUpgradeMessage.Content = "Downloading.... please wait!   It will take few minutes " + "\n" + "to download and install."));
            objVhelper.DownloadFileFTP();
            objVhelper.RemoveOldInstallnewVersion();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ExitApplication();
        }

        private void ExitApplication()
        {
            //exit the app.
            try
            {
                System.Windows.Application.Current.Shutdown();
            }
            catch { }
        }


    }
}
