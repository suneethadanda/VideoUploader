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
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for ExportImages.xaml
    /// </summary>
    public partial class ExportImages : Window
    {
        private string prefix = string.Empty;

        public string VIN;
        public string stock;
        bool _firstLoad = true;
        public string RoofTopKey
        { get; set; }
        Alert objalert = new Alert();
        public List<ViewImages.ClsImages> lstCheckedImages;
        VehicleInfo ImageExportingVideoList;
        DispatcherTimer tmrExport = new DispatcherTimer();
        BackgroundWorker backgroundWorker;
        //string ftpPath = string.Empty;
        //string userName = string.Empty;
        //string password = string.Empty;
        //string folder = string.Empty;

        ViewImages objViewImages = new ViewImages();
        string temp = string.Empty;

        //public static bool _ExportStarted = false;
        public int Progress
        {
            get;

            set;

        }
        public string OutFileName
        {
            get;

            set;

        }

        public ExportImages()
        {
            InitializeComponent();
            //if (ImageObjects.ImageExportingVideoList.Count > 0 && ImageObjects._ExportStarted)
            //{
            //    //try
            //    //{
            //    //    //Dispatcher.Invoke(new Action(() => pbExportImages.Value = ImageObjects.imageExportprogress));
            //    //}
            //    //catch { }
            //    //try
            //    //{
            //    //    Dispatcher.Invoke(new Action(() => lvExportingQueue.DataContext = ImageObjects.ImageExportingVideoList));
            //    //}
            //    //catch { }
            //}


            tmrExport.Interval = new TimeSpan(3000);
            tmrExport.Tick += new EventHandler(tmrExport_Tick);

            backgroundWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));

        }



        private void tmrExport_Tick(object sender, EventArgs e)
        {
            try
            {
                Dispatcher.Invoke(new Action(() => pbExportImages.Value = ImageObjects.imageExportprogress));
                Dispatcher.Invoke(new Action(() => txtBlockProgress.Text = ImageObjects.imageExportprogress+"%"));
                
                if (!ImageObjects._ExportStarted && ImageObjects.ImageExportingVideoList.Count > 0)
                {
                    ImageObjects._ExportStarted = true;
                    Dispatcher.Invoke(new Action(() => lblRoofTopKey.Content=ImageObjects.ImageExportingVideoList[0].RooftopKey));
                    Dispatcher.Invoke(new Action(() => lblVideoGuid.Content = ImageObjects.ImageExportingVideoList[0].OutputFileName));
                    //Dispatcher.Invoke(new Action(() => lblMessge.Content = "Exporting in Progress.How ever you can close the window " + "\n" + " and continue using the application"));//ResourceTxt.noPrefix;
                    Thread th = new Thread(new ThreadStart(exporttoFTP));
                    th.IsBackground = true;
                    th.Start();
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("tmrUpload_Tick: " + ex.Message);
                Common.WriteEventLog("tmrExport_Tick: " + ex.Message, "Error");
            }
        }

        public ExportImages(string ImageName, string stock, List<ViewImages.ClsImages> lstCheckedImages, string VIN)
        {

            InitializeComponent();
            //currentExportingList = new VehicleInfoCollection();
            ImageExportingVideoList = new VehicleInfo();
            //lvExportingQueue.DataContext = ImageObjects.ImageExportingVideoList;
            tmrExport.Interval = new TimeSpan(3000);
            tmrExport.Tick += new EventHandler(tmrExport_Tick);

            //backgroundWorker =((BackgroundWorker)this.FindResource("backgroundWorker"));
            //secondbackgroundWorker =((BackgroundWorker)this.FindResource("secondbackgroundWorker"));
            if (VIN != "")
            {
                this.VIN = VIN;
            }
            else
            {
                this.VIN = ImageName;
            }

            OutFileName = ImageName;
            this.stock = stock;
            this.lstCheckedImages = lstCheckedImages;

        }



        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //int count = 0;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //bool accept = false;
            if (rdBtnCustom.IsChecked == true || rdBtnStock.IsChecked == true || rdBtnVIN.IsChecked == true || rbtPushToServer.IsChecked == true)
            {
                lblMessge.Content = "";
                if (rdBtnCustom.IsChecked == true)
                {
                    if (!string.IsNullOrEmpty(txtCustomSuffix.Text))
                    {

                        prefix = txtCustomSuffix.Text;

                    }
                    else
                    {
                        lblMessge.Content = ResourceTxt.noPrefix;
                        lblMessge.Foreground = Brushes.Red;
                        lblMessge.Visibility = Visibility.Visible;
                        return;
                    }
                }
                try
                {
                    //lblMessge.Content = "Exporting in Progress.How ever you can close the window " + "\n" + " and continue using the application";//ResourceTxt.noPrefix;
                    //lblMessge.Foreground = Brushes.Red;
                    //Spinner.Visibility = Visibility.Visible;

                    if (rbtPushToServer.IsChecked == true)
                    {
                        // prefix = OutFileName;
                        grdStatus.Visibility = Visibility.Visible;
                        AddToExportingQueue();
                        rbtPushToServer.IsChecked = false;
                        if (ImageObjects.ImageExportingVideoList.Count > 1)
                        {
                           
                            Alert1 objAlert1 = new Alert1("Images Added to Exporting Queue");
                            objAlert1.ShowDialog();
                        }
                        tmrExport.Start();

                    }
                    else
                    {
                        try
                        {
                            backgroundWorker.RunWorkerAsync();
                        }
                        catch { }
                    }





                }
                catch
                {
                    lblMessge.Content = ResourceTxt.Error;
                }

            }
            else
            {
                lblMessge.Content = ResourceTxt.ExportWarnig;
                lblMessge.Foreground = Brushes.Red;
                lblMessge.Visibility = Visibility.Visible;
                return;
            }
        }
        private void AddToExportingQueue()
        {
            VehicleInfo objImageExportVehicleInfo = new VehicleInfo();
            objImageExportVehicleInfo.OutputFileName = OutFileName;
            objImageExportVehicleInfo.VIN = VIN;
            objImageExportVehicleInfo.Stock = stock;
            objImageExportVehicleInfo.RooftopKey = RoofTopKey;
            ImageObjects.ImageExportingVideoList.Add(objImageExportVehicleInfo);
            lvExportingQueue.DataContext = ImageObjects.ImageExportingVideoList;
            lvExportingQueue.Items.Refresh();
        }

        #region extra
        //private void AddCurrentExportingImagesTofile()
        //{
        //    try
        //    {
        //        string strExportingFile = Common.CurrentExporting;

        //        if (!File.Exists(strExportingFile))
        //        {
        //            XmlDocument XDoc = new XmlDocument();
        //            XmlElement XElemRoot = XDoc.CreateElement(XNAME.CURRENT_EXPORTING);
        //            XDoc.AppendChild(XElemRoot);
        //            XDoc.Save(strExportingFile);
        //        }

        //        XElement doc = XElement.Load(strExportingFile);


        //        doc.Add(
        //        new XElement(XNAME.FILE,
        //        new XElement(XNAME.VEHICLE_KEY, ImageExportingVideoList.VehicleKey),
        //        new XElement(XNAME.SOURCE_FILE_NAME, ImageExportingVideoList.InputFileName),
        //        new XElement(XNAME.OUTPUT_FILE_NAME, ImageExportingVideoList.OutputFileName),
        //        new XElement(XNAME.ROOF_TOP_KEY, ImageExportingVideoList.RooftopKey),      // Add Rooftop, Title, Desc
        //        new XElement(XNAME.VIDEO_TITLE, ImageExportingVideoList.VideoTitle),
        //        new XElement(XNAME.DESCRIPTION, ImageExportingVideoList.Description),
        //        new XElement(XNAME.IS_DEFAULT, ImageExportingVideoList.IsDefault),
        //        new XElement(XNAME.IS_EXTRACT, ImageExportingVideoList.IsExtract),
        //        new XElement(XNAME.Stock, ImageExportingVideoList.Stock),
        //        new XElement(XNAME.VIN, ImageExportingVideoList.VIN),
        //        new XElement(XNAME.VIDEOTYPE, ImageExportingVideoList.VideoType)));

        //        doc.Save(strExportingFile);
        //    }
        //    catch (Exception ex)
        //    {
        //        //System.Windows.Forms.MessageBox.Show("AddRecordToFile: " + ex.Message);
        //        //Common.WriteLog("AddRecordToFile: " + ex.Message);
        //        Common.WriteEventLog("AddCurrentExportingImagesTofile: " + ex.Message, "Error");
        //    }
        //}

        #endregion


        public void AddCurrentExportingQueueFromFile()
        {
            try
            {
                // Call this only once
                if (_firstLoad)
                {
                    _firstLoad = false;

                    string strExportFile = Common.CurrentExporting;

                    if (!File.Exists(strExportFile))
                        return;

                    XElement doc = XElement.Load(strExportFile);
                    //ListViewItem lvItem = null;

                    var pending = from Images in doc.Descendants("File") select Images;

                    foreach (XElement xElePending in pending.ToList())
                    {
                        string strVehicleKey = xElePending.Element(XNAME.VEHICLE_KEY).Value;
                        string strSourceFile = xElePending.Element(XNAME.SOURCE_FILE_NAME).Value;
                        string strOutFileName = xElePending.Element(XNAME.OUTPUT_FILE_NAME).Value;

                        // Add Rooftop, Title and Desc also
                        string strRoofTop = xElePending.Element(XNAME.ROOF_TOP_KEY).Value;
                        string strVideoTitle = xElePending.Element(XNAME.VIDEO_TITLE).Value;
                        string strDesc = xElePending.Element(XNAME.DESCRIPTION).Value;
                        string strIsDefault = xElePending.Element(XNAME.IS_DEFAULT).Value;
                        string strIsExtract = "False";
                        if (xElePending.Element(XNAME.IS_EXTRACT) != null)
                            strIsExtract = xElePending.Element(XNAME.IS_EXTRACT).Value;

                        string strStock = xElePending.Element(XNAME.Stock).Value;

                        string strVIN = string.Empty;
                        if (xElePending.Element(XNAME.VIN) != null)
                            strVIN = xElePending.Element(XNAME.VIN).Value;

                        string VType = "";
                        if (xElePending.Element(XNAME.VIDEOTYPE) != null)
                        {
                            VType = xElePending.Element(XNAME.VIDEOTYPE).Value;
                        }



                        VehicleInfo ExportingVInfo = new VehicleInfo();
                        ExportingVInfo.VehicleKey = strVehicleKey;
                        ExportingVInfo.InputFileName = strSourceFile;
                        ExportingVInfo.OutputFileName = strOutFileName;
                        ExportingVInfo.RooftopKey = strRoofTop;
                        ExportingVInfo.VideoTitle = strVideoTitle;
                        ExportingVInfo.Description = strDesc;
                        ExportingVInfo.IsDefault = strIsDefault;
                        ExportingVInfo.IsExtract = strIsExtract;
                        ExportingVInfo.VIN = strVIN;
                        ExportingVInfo.VideoType = VType;
                        if (strStock != "")
                            ExportingVInfo.Stock = strStock;

                        ImageObjects.ImageExportingVideoList.Add(ExportingVInfo);


                    }

                    tmrExport.Start();
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("AddUploadingQueueFromFile: " + ex.Message);
                Common.WriteEventLog("AddCurrentExportingQueueFromFile: " + ex.Message, "Error");

            }
        }


        //public int getfolderCount(string foldername)
        //{

        //    try
        //    {
        //        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ResourceTxt.FTPPath);
        //        ftpRequest.Credentials = new NetworkCredential(ResourceTxt.UserName, ResourceTxt.Password);
        //        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
        //        FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
        //        StreamReader streamReader = new StreamReader(response.GetResponseStream());

        //        List<string> directories = new List<string>();

        //        string line = streamReader.ReadLine();
        //        int foldercount = 0;
        //        while (!string.IsNullOrEmpty(line))
        //        {
        //            directories.Add(line);
        //            line = streamReader.ReadLine();
        //        }

        //        foldercount = directories.Where(a => a.Contains(foldername)).Count();
        //        streamReader.Close();
        //        return foldercount;
        //    }
        //    catch { return 0; }

        //}


        private void exporttoFTP()
        {
            try
            {

                //Dispatcher.Invoke(new Action(() => lblMessge.Content = "Exporting in Progress.How ever you can close the window " + "\n" + " and continue using the application"));
                //_ExportStarted = true;
                if (ImageObjects.ImageExportingVideoList.Count() > 0)
                {


                    #region old
                    //bool accept = false;
                    //int foldCount = getfolderCount(ImageObjects.ImageExportingVideoList[0].OutputFileName);

                    //if (foldCount == 0)
                    //{
                    //    folder = ImageObjects.ImageExportingVideoList[0].OutputFileName;
                    //    accept = CreateDirectoryinFtp(ImageObjects.ImageExportingVideoList[0].OutputFileName);
                    //}
                    //else
                    //{
                    //    folder = ImageObjects.ImageExportingVideoList[0].OutputFileName + "_" + foldCount;
                    #endregion
                    try
                    {
                        CreateDirectoryinFtp(ImageObjects.ImageExportingVideoList[0].OutputFileName);
                    }
                    catch { }
                    uploadImagestoFTP(ImageObjects.ImageExportingVideoList[0].OutputFileName);
                    // Dispatcher.Invoke(new Action(() => Spinner.Visibility = Visibility.Hidden));
                    // Dispatcher.Invoke(new Action(() => lblMessge.Content = "Successfully exported"));



                }
            }
            catch
            { }
        }



        private void uploadImagestoFTP(string foldername)
        {
            try
            {
                for (int imagecount = 0; imagecount < lstCheckedImages.Count(); imagecount++)
                {
                    try
                    {
                        //string path = ftpPath + "/" + ImageObjects.ImageExportingVideoList[0].OutputFileName + "//" + ImageObjects.ImageExportingVideoList[0].OutputFileName + "_" + imagecount + ".jpg";
                        FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(ResourceTxt.FTPPath + "/" + foldername + "//" + foldername + "_" + (imagecount + 1) + ".jpg");
                        ftpReq.UseBinary = true;
                        ftpReq.KeepAlive = false;
                        ftpReq.Timeout = -1;
                        ftpReq.UsePassive = true;
                        ftpReq.UseBinary = true;
                        ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
                        ftpReq.Credentials = new NetworkCredential(ResourceTxt.UserName, ResourceTxt.Password);

                        byte[] b = File.ReadAllBytes(lstCheckedImages[imagecount].image);

                        ftpReq.ContentLength = b.Length;
                        using (Stream stream = ftpReq.GetRequestStream())
                        {
                            try
                            {
                                stream.Write(b, 0, b.Length);
                            }
                            catch { }
                        }
                        FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();
                        ImageObjects.imageExportprogress = (imagecount * 100) / lstCheckedImages.Count();
                        //Dispatcher.Invoke(new Action(() => pbExportImages.Value = ImageObjects.imageExportprogress));
                    }
                    catch { }
                }


                NotifyExportedImagesToServer();
                UpadateExtractedVideosXml(ImageObjects.ImageExportingVideoList[0].OutputFileName);
                ImageObjects.ImageExportingVideoList.RemoveAt(0);

                Dispatcher.Invoke(new Action(() => lvExportingQueue.Items.Refresh()));
                Dispatcher.Invoke(new Action(() => pbExportImages.Value = 0));

                Dispatcher.Invoke(new Action(() => lblRoofTopKey.Content = ""));
                Dispatcher.Invoke(new Action(() => lblVideoGuid.Content = ""));

                Dispatcher.Invoke(new Action(() => ImageObjects.imageExportprogress = 0));
                Dispatcher.Invoke(new Action(() => txtBlockProgress.Text=""));

                Dispatcher.Invoke(new Action(() => objalert.lblMessage.Content = "You have selected " + lstCheckedImages.Count + " images." + "\n" + "Would you like to delete Remaining images?" + "\n" + "from your Computer to save space?"));
                Dispatcher.Invoke(new Action(() => objalert.ShowDialog()));

                if (objalert.close)
                {
                    RemoveExtractedVideoFromFile();
                }
                ImageObjects._ExportStarted = false;

                //if (this.IsVisible)
                //{
                //    Dispatcher.Invoke(new Action(() => this.Visibility = Visibility.Hidden));
                //}

                //this.Hide();

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                ImageObjects._ExportStarted = false;
            }


        }


        private bool NotifyExportedImagesToServer()
        {
            string postData = "ACTION=UPLOADIMAGES&rooftop_key=" + ImageObjects.ImageExportingVideoList[0].RooftopKey + "&VIDEOGUID=" + ImageObjects.ImageExportingVideoList[0].OutputFileName + "";
            return (NotifyServer(postData));
        }

        private bool NotifyServer(string postData)
        {
            bool _failed = false;

            // Call the server related URL with all the above querystring 
            string serverData = string.Empty;
            serverData = Common.GetServerData(postData);

            // TODO -remove this log
            Common.WriteLog("Notify server: " + postData);
            Common.WriteLog("Server response: " + serverData);
            //Common.WriteEventLog("Server response sucess ", "Images Uploading Information");
            if (string.IsNullOrEmpty(serverData))
                return false;

            if (serverData.Equals("WebError"))
            {
                // lblError.Content = ResourceTxt.GetServerData_WebError;
                _failed = true;
            }
            else if (serverData.Equals("Error"))
            {
                //lblError.Content = ResourceTxt.GetServerData_Error;
                _failed = true;
            }

            if (_failed)
            {
                //lblError.Visibility = Visibility.Visible;

                return false;
            }

            return true;
        }

        public bool CreateDirectoryinFtp(string directory)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ResourceTxt.FTPPath + directory);
            request.Credentials = new NetworkCredential(ResourceTxt.UserName, ResourceTxt.Password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            //Console.WriteLine("Getting the response");
            try
            {
                request.Method = WebRequestMethods.Ftp.MakeDirectory;

                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch { return false; }

        }


        /// <summary>
        /// timer for showing exporting status
        /// </summary>


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //e.Handled = true;
            e.Handled = true;
            this.Hide();
            //this.Visibility = Visibility.Hidden;

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void rdBtnCustom_Checked(object sender, RoutedEventArgs e)
        {
            if (!ImageObjects._ExportStarted && ImageObjects.ImageExportingVideoList.Count == 0)
            {
                grdStatus.Visibility = Visibility.Hidden;
            }
            // lblMessge.Visibility = Visibility.Hidden;
            txtCustomSuffix.Visibility = Visibility.Visible;
            txtCustomSuffix.Focus();
            //lblMessge.Content = string.Empty;
            // lblMessge.Visibility = Visibility.Hidden;
        }
        private void rdBtnVIN_Checked(object sender, RoutedEventArgs e)
        {

            prefix = VIN;
            txtCustomSuffix.Text = string.Empty;
            txtCustomSuffix.Visibility = Visibility.Hidden;
            if (!ImageObjects._ExportStarted && ImageObjects.ImageExportingVideoList.Count == 0)
            {
                grdStatus.Visibility = Visibility.Hidden;
            }
            //lblMessge.Content = string.Empty;
            //lblMessge.Visibility = Visibility.Hidden;
        }
        private void rdBtnStock_Checked(object sender, RoutedEventArgs e)
        {
            prefix = stock;
            txtCustomSuffix.Text = string.Empty;
            txtCustomSuffix.Visibility = Visibility.Hidden;
            if (!ImageObjects._ExportStarted && ImageObjects.ImageExportingVideoList.Count == 0)
            {
                grdStatus.Visibility = Visibility.Hidden;
            }
            //lblMessge.Content = string.Empty;
            //lblMessge.Visibility = Visibility.Hidden;
        }

        //public bool IsAlphaNumeric(String strToCheck)
        //{
        //    Regex objAlphaNumericPattern = new Regex(@"[^a-zA-Z0-9_\s]");
        //    return !objAlphaNumericPattern.IsMatch(strToCheck);
        //}
        private void txtCustomSuffix_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp = "";
                if (!string.IsNullOrEmpty(txtCustomSuffix.Text))
                {
                    if (!Char.IsLetterOrDigit(txtCustomSuffix.Text[txtCustomSuffix.Text.Length - 1]))
                    {
                        //string temp = string.Empty;
                        for (int i = 0; i < txtCustomSuffix.Text.Length - 1; i++)
                        {
                            temp += txtCustomSuffix.Text[i];
                        }

                        txtCustomSuffix.Text = temp;
                    }
                    //txtTitle.Text = txtTitle.Text.ToUpper();
                    txtCustomSuffix.CaretIndex = txtCustomSuffix.Text.Length;

                }


            }
            catch { }
        }



        private void UpadateExtractedVideosXml(string filename)
        {
            string strExtractedFile = Common.ExtractedImagesFile;

            if (!File.Exists(strExtractedFile))
                return;

            XDocument xmlDoc = XDocument.Load(strExtractedFile);
            var items = (from item in xmlDoc.Descendants("File") where item.Element("OutputFileName").Value == filename select item).ToList();
            foreach (var item in items)
            {
                item.Element("EXPORTEDSTATUS").Value = "Exported";

            }
            xmlDoc.Save(strExtractedFile);


        }



        private void RemoveExtractedVideoFromFile()
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

            RemovefromSystem();
        }


        private void RemovefromSystem()
        {
            string strLocalPath = Common.ImageExtract + "\\" + OutFileName;

            if (Directory.Exists(strLocalPath))
            {
                DirectoryInfo objfolder = new DirectoryInfo(strLocalPath);
                foreach (FileInfo fi in objfolder.GetFiles())
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch { }
                }
                //this.Close();
            }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //Dispatcher.Invoke(new Action(() => Spinner.Visibility = Visibility.Visible));
            objViewImages.SaveToServer(lstCheckedImages, prefix);
            UpadateExtractedVideosXml(OutFileName);
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => Spinner.Visibility = Visibility.Hidden));
            Dispatcher.Invoke(new Action(() => lblMessge.Content = "Successfully exported"));
            Alert objalert = new Alert("You have selected " + lstCheckedImages.Count + " images." + "\n" + "Would you like to delete Remaining images?" + "\n" + "from your Computer to save space?");
            objalert.ShowDialog();

            if (objalert.close)
            {
                RemoveExtractedVideoFromFile();
            }

            this.Visibility = Visibility.Hidden;

        }

        private void BackgroundWorker_DoWork_1(object sender, DoWorkEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => lblMessge.Content = "Deleting......"));
            ImageObjects.ImageExportingVideoList.Remove(ImageObjects.ImageExportingVideoList[0]);
            //Dispatcher.Invoke(new Action(() =>));
            //RemoveExportedImagesFromFile(ImageObjects.ImageExportingVideoList[0].OutputFileName);
            //RemoveExtractedVideoFromFile();
            //UpadateExtractedVideosXml();
        }

        private void BackgroundWorker_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            //e.Handled = true;
        }

        private void rbtPushToServer_Checked(object sender, RoutedEventArgs e)
        {
            if (UserAuthorization.RemoteUploadAuth != "0" && UserAuthorization.RemoteUploadAuth != null)
            {
                prefix = OutFileName;
                txtCustomSuffix.Text = string.Empty;
                txtCustomSuffix.Visibility = Visibility.Hidden;
                //lblMessge.Content = string.Empty;
                //lblMessge.Visibility = Visibility.Hidden;
            }
            
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBarExport.Value = e.ProgressPercentage;
        }

        private void rbtPushToServer_Click(object sender, RoutedEventArgs e)
        {
            txtCustomSuffix.Text = string.Empty;
            txtCustomSuffix.Visibility = Visibility.Hidden;

            if (UserAuthorization.RemoteUploadAuth == "0" || UserAuthorization.RemoteUploadAuth == null)
            {
                //rbtPushToServer.IsEnabled = false;
                rbtPushToServer.IsChecked = false;

                Alert1 objAlert = new Alert1("Remote upload feature is disabled. Please " + "\n" + "contact your Reseller to enable Remote " + "\n" + "upload");
                objAlert.ShowDialog();
            }
        }
    }
}
