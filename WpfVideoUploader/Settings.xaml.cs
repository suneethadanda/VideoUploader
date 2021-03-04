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
using System.Windows.Forms;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            LoadSettings();
        }
        public void LoadSettings()
        {
            try
            {
                string strAppName = ResourceTxt.AppName;
                string strSettingFile = Common.SettingsFile;
                if (!File.Exists(strSettingFile))
                {
                    DirectoryInfo dirUserData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), strAppName + "\\");
                    if (!Directory.Exists(path))
                        dirUserData.CreateSubdirectory(strAppName);

                    txtOutFileLocation.Text = path;
                    txtVideoLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                else
                {
                    string strPath = Common.VedioFolder;
                    DirectoryInfo dirUserData = new DirectoryInfo(Environment.CurrentDirectory);
               
                    if (!Directory.Exists(strPath))
                        System.IO.Directory.CreateDirectory(strPath);

                    if (File.Exists(strSettingFile))
                    {
                        XmlDocument myXmlDocument = new XmlDocument();
                        myXmlDocument.Load(strSettingFile);
                        XmlNode node;
                        node = myXmlDocument.DocumentElement;

                        foreach (XmlNode childnode in node.ChildNodes)
                        {
                            if (childnode.Name == "OutputVideoLocation")
                                txtOutFileLocation.Text = childnode.InnerText.ToString();
                            if (childnode.Name == "InputVideoLocation")
                                txtVideoLocation.Text = childnode.InnerText.ToString();
                            if (childnode.Name == "StoreEncodedVideo")
                                chkVideoStore.IsChecked = Convert.ToBoolean(childnode.InnerText.ToString());
                        }
                    }
                }
                SaveSettings();
                this.Close();
            }
            catch (Exception ex)
            {
                Common.WriteEventLog("LoadSettings: Error occurred while loading the setting \n" + ex.Message,"Error");
                System.Windows.MessageBox.Show("Problem in loading the setting");
            }
        }

        public void SaveSettings()
        {
            string xmlPath = Common.SettingsFile;
            XmlDocument xmlDoc = null;

            if (File.Exists(xmlPath))
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode node;
                node = xmlDoc.DocumentElement;
                string strAppName = ResourceTxt.AppName;
                string strPath=Common.VedioFolder;
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    if (node2.Name == "OutputVideoLocation")
                    {
                        //node2.InnerText = txtOutFileLocation.Text.ToString();
                        node2.InnerText = strPath;
                    }
                    if (node2.Name == "InputVideoLocation")
                    {
                        node2.InnerText = strPath;
                        //node2.InnerText = txtVideoLocation.Text.ToString();
                    }
                    if (node2.Name == "StoreEncodedVideo")
                    {
                        node2.InnerText = chkVideoStore.IsChecked.ToString();
                    }
                }
            }
            else
            {
                xmlDoc = new XmlDocument();
                XmlNode settigsNode = null;
                XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(docNode);

                settigsNode = xmlDoc.CreateElement("Settings");
                xmlDoc.AppendChild(settigsNode);

                XmlNode OutputVideoNode = xmlDoc.CreateElement("OutputVideoLocation");
                OutputVideoNode.InnerText = txtOutFileLocation.Text.ToString();
                settigsNode.AppendChild(OutputVideoNode);

                XmlNode InputVideoLocationNode = xmlDoc.CreateElement("InputVideoLocation");
                InputVideoLocationNode.InnerText = txtVideoLocation.Text.ToString();
                settigsNode.AppendChild(InputVideoLocationNode);

                XmlNode StoreEncodedVideoNode = xmlDoc.CreateElement("StoreEncodedVideo");
                StoreEncodedVideoNode.InnerText = chkVideoStore.IsChecked.ToString();
                settigsNode.AppendChild(StoreEncodedVideoNode);
            }

            xmlDoc.Save(xmlPath);
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveSettings();
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Problem in Setting creation.");
                //Common.WriteLog("SaveSettings:" + ex.Message);
                Common.WriteEventLog("SaveSettings:" + ex.Message, "Error");
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnVideoBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtVideoLocation.Text = dlg.SelectedPath;
            }
        }

        private void btnOutFileLocation_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult dlgResult = dlg.ShowDialog();
            if (dlgResult==System.Windows.Forms.DialogResult.OK)
            {
                txtOutFileLocation.Text = dlg.SelectedPath;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
