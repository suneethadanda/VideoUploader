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
using System.Net;
using System.ComponentModel;
using System.Diagnostics;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for UpdateApplication.xaml
    /// </summary>
    public partial class UpdateApplication : Window
    {
        public UpdateApplication()
        {
            InitializeComponent();
            DownloadLatestSolution();
        }
        static string tempFullPath = string.Empty;
        static string appFullPath = string.Empty;

        void DownloadLatestSolution()
        {
            try
            {
                lblMessage.Visibility = Visibility.Visible;
                lblMessage.Content = ResourceTxt.DownloadLatestMessage;

                // Get the latest exe and download to the temporary folder
                string tempDir = System.IO.Path.GetTempPath();
                tempFullPath = tempDir + "\\" + ResourceTxt.InstallerName;

                string url = ResourceTxt.ApplicationUpdateURL + "//" + ResourceTxt.InstallerName;
                // @"http://localhost/VideoUploader/VideoUploaderInstaller.msi";

                WebClient client = new WebClient();
                
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(url), tempFullPath);

                // Re-Install Current VideoUploader exe with the latest exe
                // do this in client_DownloadFileCompleted function
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        static void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                // Start executing downloaded latest application
                ProcessStartInfo prUpdate = new ProcessStartInfo(tempFullPath);
                Process.Start(prUpdate);


                // Close the current VideoUploader application
                Process[] pArry = Process.GetProcesses();
                foreach (Process p in pArry)
                {
                    string s = p.ProcessName;
                    if (s.CompareTo("VideoUploader") == 0)
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
