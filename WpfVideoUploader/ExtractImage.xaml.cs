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
//using System.IO;
using CAVEditLib;
using System.Xml.Linq;
using System.Windows.Threading;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using System.Data;
using System.IO;
namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class ExtractImage : Window
    {
        DispatcherTimer tmrUpload = new DispatcherTimer();

        VehicleInfoCollection ExtractedVeVideosInfolist;
        // public bool _firstLoad = true;
        // public string stock = string.Empty;
        string strLocalPath = string.Empty;
        //bool extactFlag = true;
        DispatcherTimer tmStatus = new DispatcherTimer();
        public ExtractImage()
        {
            InitializeComponent();
            ExtractedVeVideosInfolist = new VehicleInfoCollection();
            tmrUpload.Interval = new TimeSpan(20000);
            tmrUpload.Tick += new EventHandler(tmrUpload_Tick);
            tmrUpload.IsEnabled = true;
            tmrUpload.Start();

            tmStatus.Interval = new TimeSpan(20000);
            tmStatus.Tick += new EventHandler(CheckStatus);

            tmStatus.Start();

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            //e.Handled = true;


            this.Close();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        /// <summary>
        /// binding data to Image Extraction status screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {

                BindExtractStatus();
                AddExtractedVideosFromFile();
            }
            catch (Exception ex)
            {
                Common.WriteEventLog("BindExtractStatus: " + ex.Message, "Error");
            }
        }

        public void BindExtractStatus()
        {
            try
            {

                slvUploading.DataContext = ImageObjects.UploadingList;
                slvVideoUploaded.DataContext = ImageObjects.UploadedList;
                lblUploadingFileName.Content = ImageObjects.ExtractFile;
                lblUploadingVehiceKey.Content = ImageObjects.VehicleKey;
                pbUploadProgress.Value = ImageObjects.Progress;

                //slvUploading.Items.Refresh();
                //slvVideoUploaded.Items.Refresh();
                tmrUpload.Interval = new TimeSpan(20000);
                tmrUpload.Tick += new EventHandler(tmrUpload_Tick);
                tmrUpload.IsEnabled = true;
                tmrUpload.Start();






            }
            catch (Exception ex)
            {
                Common.WriteEventLog("BindExtractStatus: " + ex.Message, "Error");
            }
        }





        /// <summary>
        /// timer event called in Window_Loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrUpload_Tick(object sender, EventArgs e)
        {
            try
            {

                if (slvUploading.Items.Count > 0)
                {

                    ImageObjects.ExtractStaus = false;
                    lblUploadingFileName.Content = ImageObjects.ExtractFile;
                    lblUploadingVehiceKey.Content = ImageObjects.VehicleKey;

                    pbUploadProgress.Value = ImageObjects.Progress;
                    slvVideoUploaded.DataContext = ImageObjects.UploadedList;
                    slvUploading.DataContext = ImageObjects.UploadingList;
                    slvUploading.Items.Refresh();
                    slvVideoUploaded.Items.Refresh();




                }
                if (slvUploading.Items.Count == 0)
                {
                    pbUploadProgress.Value = 0;
                    lblUploadingFileName.Content = "";
                    lblUploadingVehiceKey.Content = "";

                    if (!ImageObjects.ExtractStaus)
                    {

                        slvVideoUploaded.Items.Refresh();


                        tmrUpload.Stop();
                        tmrUpload.IsEnabled = false;


                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteEventLog("tmrUpload_Tick: " + ex.Message, "Error");
                //Common.WriteLog("tmrUpload_Tick: " + ex.Message);
                //Common.WriteEventLog("tmrUpload_Tick: " + ex.Message, "Error");
            }
        }

        /// <summary>
        /// view selected video Images and passing Video name to View Images Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Hyperlink clickedButton = (Hyperlink)sender;

                //ImageObjects.UploadedList.RemoveAt(slvVideoUploaded.SelectedIndex);
                //slvVideoUploaded.Items.Refresh();

                //clickedButton.NavigateUri
                //stock = Convert.ToString(clickedButton.NavigateUri);
                if (Directory.Exists(Common.ImageExtract + "\\" + clickedButton.TargetName))
                {
                    ViewImages objView = new ViewImages(clickedButton.TargetName, Convert.ToString(clickedButton.NavigateUri), Convert.ToString(clickedButton.Tag));

                    objView.ShowDialog();
                    //objView.Focus();
                }

                else
                {
                    MessageBox.Show("you have already removed selected Images");
                }

                //slvVideoUploaded.Items.Clear();
                AddExtractedVideosFromFile();
            }
            catch (Exception ex)
            {
                Common.WriteEventLog("Hyperlink_Click: " + ex.Message, "Error");
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            //this.Hide();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void AddExtractedVideosFromFile()
        {
            try
            {
                // Call this only once
                //if (_firstLoad)
                //{
                //    _firstLoad = false;
                ExtractedVeVideosInfolist.Clear();
                string strExtractedFile = Common.ExtractedImagesFile;

                if (File.Exists(strExtractedFile))
                {
                    //XmlDocument XDoc = new XmlDocument();
                    //XmlElement XElemRoot = XDoc.CreateElement(XNAME.ExTRACTED_VIDEOS);
                    //XDoc.AppendChild(XElemRoot);
                    //XDoc.Save(strExtractedFile);


                    XElement doc = XElement.Load(strExtractedFile);
                    //ListViewItem lvItem = null;

                    var pending = from videos in doc.Descendants("File") select videos;
                    //int i=0;
                    foreach (XElement xElePending in pending.ToList())
                    {

                        string strVehicleKey = "";
                        if (xElePending.Element(XNAME.VEHICLE_KEY) != null)
                            strVehicleKey = xElePending.Element(XNAME.VEHICLE_KEY).Value;

                        string strSourceFile = "";
                        if (xElePending.Element(XNAME.SOURCE_FILE_NAME) != null)
                            strSourceFile = xElePending.Element(XNAME.SOURCE_FILE_NAME).Value;

                        string strOutFileName = "";
                        if (xElePending.Element(XNAME.OUTPUT_FILE_NAME) != null)
                            strOutFileName = xElePending.Element(XNAME.OUTPUT_FILE_NAME).Value;

                        // Add Rooftop, Title and Desc also
                        string strRoofTop = "";
                        if (xElePending.Element(XNAME.ROOF_TOP_KEY) != null)
                            strRoofTop = xElePending.Element(XNAME.ROOF_TOP_KEY).Value;

                        string strVideoTitle = "";
                        if (xElePending.Element(XNAME.VIDEO_TITLE) != null)
                            strVideoTitle = xElePending.Element(XNAME.VIDEO_TITLE).Value;

                        string strDesc = "";
                        if (xElePending.Element(XNAME.DESCRIPTION) != null)
                            strDesc = xElePending.Element(XNAME.DESCRIPTION).Value;

                        string strIsDefault = "";
                        if (xElePending.Element(XNAME.IS_DEFAULT) != null)
                            strIsDefault = xElePending.Element(XNAME.IS_DEFAULT).Value;

                        string strIsExtract = "False";
                        if (xElePending.Element(XNAME.IS_EXTRACT) != null)
                            strIsExtract = xElePending.Element(XNAME.IS_EXTRACT).Value;
                        string strStock = "";
                        if (xElePending.Element(XNAME.Stock) != null)
                            strStock = xElePending.Element(XNAME.Stock).Value;

                        string strVIN = string.Empty;
                        if (xElePending.Element(XNAME.VIN) != null)
                            strVIN = xElePending.Element(XNAME.VIN).Value;

                        string VType = "";
                        if (xElePending.Element(XNAME.VIDEOTYPE) != null)
                        {
                            VType = xElePending.Element(XNAME.VIDEOTYPE).Value;
                        }


                        string strExportedStatus = "";
                        if (xElePending.Element(XNAME.EXPORTEDSTATUS) != null)
                            strExportedStatus = xElePending.Element(XNAME.EXPORTEDSTATUS).Value;


                        VehicleInfo ExtractedVInfo = new VehicleInfo();

                        if (strVehicleKey != "")
                            ExtractedVInfo.VehicleKey = strVehicleKey;

                        if (strSourceFile != "")
                            ExtractedVInfo.InputFileName = strSourceFile;
                        if (strOutFileName != "")
                            ExtractedVInfo.OutputFileName = strOutFileName;
                        if (strRoofTop != "")
                            ExtractedVInfo.RooftopKey = strRoofTop;
                        if (strVideoTitle != "")
                            ExtractedVInfo.VideoTitle = strVideoTitle;
                        if (strDesc != "")
                            ExtractedVInfo.Description = strDesc;
                        if (strIsDefault != "")
                            ExtractedVInfo.IsDefault = strIsDefault;
                        if (strIsExtract != "")
                            ExtractedVInfo.IsExtract = strIsExtract;
                        if (strVIN != "")
                            ExtractedVInfo.VIN = strVIN;
                        if (VType != "")
                            ExtractedVInfo.VideoType = VType;
                        if (strStock != "")
                            ExtractedVInfo.Stock = strStock;
                        if (strExportedStatus != "")
                            ExtractedVInfo.ExtractionStatus = strExportedStatus;
                        //if (slvExtractedVideo.Items.Count > 0)
                        //{
                        //    slvExtractedVideo.Items.RemoveAt(i);
                        //    i++;
                        //}
                        ExtractedVeVideosInfolist.Add(ExtractedVInfo);




                    }
                    if (ExtractedVeVideosInfolist != null && ExtractedVeVideosInfolist.Count > 0)
                    {

                        slvExtractedVideo.DataContext = ExtractedVeVideosInfolist;
                    }

                    slvExtractedVideo.Items.Refresh();
                }
            }



            //}
            catch (Exception ex)
            {
                //Common.WriteLog("AddUploadingQueueFromFile: " + ex.Message);
                Common.WriteEventLog("AddExtractedVideosFromFile: " + ex.Message, "Error");
                lblUploadError.Visibility = Visibility.Visible;

                lblUploadError.Content = ResourceTxt.UploadError_FileLoad;
            }
        }

        private void ExtractedVideosCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }


        private void RemoveExtractedVideoFromFile(string OutFileName)
        {
            try
            {
                string strUploadFile = Common.ExtractedImagesFile;

                if (string.IsNullOrEmpty(strUploadFile))
                {
                    //Common.WriteLog("RemoveRecordFromFile: failed to get the Upload Record file name");
                    Common.WriteEventLog("RemoveExtractedRecordFromFile: failed to get the Upload Record file name: ", "Information");
                    return;
                }

                if (!File.Exists(strUploadFile))
                {
                    //Common.WriteLog("RemoveRecordFromFile: Upload Record file " + strUploadFile + " not found");
                    Common.WriteEventLog("RemoveRecordFromFile: Extracted Record file " + strUploadFile + " not found", "Information");
                    return;
                }

                XElement doc = XElement.Load(strUploadFile);

                var result = from videos in doc.Descendants("File") where videos.Element("OutputFileName").Value == OutFileName select videos;

                foreach (XElement xEle in result.ToList())
                {
                    xEle.Remove();
                }

                doc.Save(strUploadFile);
            }
            catch (Exception ex)
            {
                //Common.WriteLog(ResourceTxt.RemovingXMLFile + ex.Message);
                Common.WriteEventLog(ResourceTxt.RemovingXMLFile + ex.Message, "Error");
            }

            // RemovefromSystem(OutFileName);
        }


        private void RemovefromSystem(string OutFileName)
        {
            strLocalPath = Common.ImageExtract + "\\" + OutFileName;

            try
            {
                if (Directory.Exists(strLocalPath))
                {
                    DirectoryInfo objfolder = new DirectoryInfo(strLocalPath);
                    foreach (FileInfo fi in objfolder.GetFiles())
                    {
                        try
                        {
                            fi.Delete();
                        }
                        catch
                        { 
                            //MessageBox.Show(ex.Message); 
                        }
                    }
                    objfolder.Delete();

                }
            }
            catch { }
        }

        private void btnDelExtractedVideos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < ExtractedVeVideosInfolist.Count; i++)
                {
                    if (ExtractedVeVideosInfolist[i].IsSelected)
                    {
                        RemovefromSystem(ExtractedVeVideosInfolist[i].OutputFileName);
                        RemoveExtractedVideoFromFile(ExtractedVeVideosInfolist[i].OutputFileName);
                        // ExtractedVeVideosInfolist.Remove(ExtractedVeVideosInfolist[i]);


                    }
                }
                slvExtractedVideo.Items.Refresh();
                AddExtractedVideosFromFile();
            }
            catch { }
        }


        private void CheckStatus(object sender, EventArgs e)
        {
            if (ImageObjects.ExtractStaus && !tmrUpload.IsEnabled)
            {
                tmrUpload.IsEnabled = true;
                tmrUpload.Start();
            }

        }


    }





}
