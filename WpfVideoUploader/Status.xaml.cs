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
using System.IO;
using CAVEditLib;
using System.Xml.Linq;
using System.Windows.Threading;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using System.Data;


using System.Threading.Tasks;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for Status.xaml
    /// </summary>
    public partial class Status : Window
    {

        #region Fields
        //ListViewItem lvitem = null;
        private const string LIBAV_PATH = "\\LibAV";
        private const string CAVEDIT_LIB_NAME = "CAVEditLib.dll";
        public string strpath;
        // TODO - -test these FTP related Variables
        FtpWebRequest ftp = null;
        FileStream fst = null;
        // ExtractImage ObjExtract = new ExtractImage();
        private OutputOptions mOptionForm = null;
        private ICAVConverter mConverter = null;
        private VehicleInfo _oVehicleInfo = null;
        private Status _oStatus = null;
        private CAVEditLib.CThreadPriority mThreadPriority = CThreadPriority.ctpLower;
        private int mThreadCount = 1;

        bool _uploadStarted = false;
        bool _encoding = false;
        bool _firstLoad = true;
        bool _firstTime = true;
        bool _isFinished = true;
        bool _isException = false;

        bool uploadCanceled = false;
        bool encodingCanceled = false;

        string strOutputFile = string.Empty;
        string strUploadedVehicleKey = string.Empty;
        string strUploadedInFile = string.Empty;

        string strUploadedOutFile = string.Empty;

        string strRoofTopNotify = string.Empty;
        string strVehicleKeyNotify = string.Empty;
        string strGuidNotify = string.Empty;
        string strVideoTitleNotify = string.Empty;
        string strVideoDescNotify = string.Empty;
        string strIsDefaultNotify = string.Empty;
        int strIsExtractNotify = 0;
        string strStockNotify = string.Empty;
        string strVINNotify = string.Empty;
        string strCategoryNotify = string.Empty;




        //private string _AddVideo = string.Empty;

        //public string AddVideo
        //{
        //    get
        //    {
        //        return _AddVideo;
        //    }
        //    set
        //    {
        //        _AddVideo = value;
        //    }
        //}
        //string strInputFileNameNotify = string.Empty;

        long progress = 0;
        int targetVideoWidth = 0;
        int targetVideoHeight = 0;
        VehicleInfoCollection encodingVehicleInfolist;
        VehicleInfoCollection uploadingVehicleInfoList;
        VehicleInfoCollection uploadedVehicleInfolist;

        VehicleInfoCollection ImageUploadingVehicleInfoList;
        VehicleInfoCollection ImageUploadedVehicleInfolist;

        private BackgroundWorker bgWorker;
        //private BackgroundWorker bgWorkerNew;
        DispatcherTimer tmrUpload = new DispatcherTimer();

        // This variable is added by sateesha 
        //KeyValuePair<int, string> PreviousUpdatedVideo;

        #endregion

        #region Properties
        public Status OStatus
        {
            get
            {
                return _oStatus;
            }
            set
            {
                _oStatus = value;
            }
        }

        public VehicleInfo OVehicleInfo
        {
            get
            {
                return _oVehicleInfo;
            }
            set
            {
                _oVehicleInfo = value;
            }
        }

        private Home _ohome = null;
        public Home OHome
        {
            get
            {
                return _ohome;
            }
            set
            {
                _ohome = value;
            }
        }
        #endregion

        ExtractImage objExtractImage = new ExtractImage();

        #region ConStructor
        public Status()
        {
            InitializeComponent();

            bgWorker = ((BackgroundWorker)this.FindResource("bgWorker"));
            //bgWorkerNew = ((BackgroundWorker)this.FindResource("bgWorkerNew"));
            tmrUpload.Interval = new TimeSpan(3000);
            tmrUpload.Tick += new EventHandler(tmrUpload_Tick);

            encodingVehicleInfolist = new VehicleInfoCollection();
            uploadingVehicleInfoList = new VehicleInfoCollection();
            uploadedVehicleInfolist = new VehicleInfoCollection();
            ImageUploadedVehicleInfolist = new VehicleInfoCollection();
            ImageUploadingVehicleInfoList = new VehicleInfoCollection();
            lvEncodingQueue.DataContext = encodingVehicleInfolist;
            lvUploading.DataContext = uploadingVehicleInfoList;
            lvVideoUploaded.DataContext = uploadedVehicleInfolist;
            //objTestPage.slvUploading.DataContext = uploadingVehicleInfoList;
            ImageObjects.UploadingList = ImageUploadingVehicleInfoList;
            //ImageObjects.UploadingList = uploadingVehicleInfoList;
            //objTestPage.slvVideoUploaded.DataContext = uploadedVehicleInfolist;
            ImageObjects.UploadedList = ImageUploadedVehicleInfolist;
            ImageObjects.Progress = 0;

        }
        #endregion

        #region Events

        private void Items_CurrentChanged(object sender, EventArgs e)
        {

        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Handled = true;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            int i = 0;
            int strProgress = 0;
            long currentDuration = 0;
            long totalDuration = 0;
            string message = "";
            _isFinished = false;
            _isException = false;
            int pro = 0;
            ICProgressInfo progressInfo = null;
            ICTerminateInfo terminateInfo = null;
            //Vehicle obj = new Vehicle();

            try
            {

                if (lvEncodingQueue.Items.Count > 0)
                {
                    Dispatcher.Invoke(new Action(() => lblVehicleKeyValue.Content = encodingVehicleInfolist[0].VehicleKey));

                }

                Dispatcher.Invoke(new Action(() => lblInputFileNameValue.Content = System.IO.Path.GetFileName(mConverter.OutputOptions.FileName)));
                Thread.Sleep(2000);

                if (mConverter == null || mThreadCount <= 0)
                {
                    return;
                }

                while (!_isFinished)
                {
                    Thread.Sleep(1000);

                    progressInfo = mConverter.get_ProgressInfo(i);
                    terminateInfo = mConverter.get_TerminateInfo(i);

                    //currentDuration = progressInfo.CurrentDuration;
                    totalDuration = progressInfo.TotalDuration;
                    if (totalDuration > 0)
                    {
                        strProgress = Convert.ToInt32((currentDuration * 100 / totalDuration));

                        bgWorker.ReportProgress(strProgress);

                        if (strProgress < 0 || strProgress >= 99)
                        {
                            bgWorker.ReportProgress(100);
                        }
                    }
                    else
                    {
                        pro = pro + 5;
                        
                        bgWorker.ReportProgress(pro);
                    }
                    _isFinished = terminateInfo.Finished;
                    _isException = terminateInfo.Exception;
                    
                    message = terminateInfo.ExceptionMsg;
                    if(_isFinished)
                        bgWorker.ReportProgress(100);
                    if (_isException)
                        break;
                }

                if (_isException)
                {
                    bgWorker.ReportProgress(10);
                   
                    Common.WriteEventLog("bgWorker_DoWork: " + message, "Error");
                    GetEncodingVideo(mConverter.InputOptions.FileName, mConverter.OutputOptions.FileName);
                    _isFinished = true;
                    bgWorker.ReportProgress(100);
                    //MessageBox.Show("Error while encoding video please contact support team");
                    //OStatus.RemoveEncodingQueueItem(OVehicleInfo.VehicleKey);
                    //return;
                }

                if (_isFinished)
                {
                    Dispatcher.Invoke(new Action(() => lblVehicleKeyValue.Content = ""));
                    Dispatcher.Invoke(new Action(() => lblInputFileNameValue.Content = ""));

                    //OStatus.AddUploadingQueue(OVehicleInfo);
                    OStatus.AddUploadingQueue(encodingVehicleInfolist[0]);

                    if (lvUploading.Items.Count > 0)
                    {
                        //ListViewItem lvItem=null;
                        //Dispatcher.Invoke(new Action(() => lvItem = lvUploading.Items[0] as ListViewItem));
                        //Dispatcher.Invoke(new Action(() =>lvItem.Foreground=Brushes.Blue));
                    }


                    //if (OVehicleInfo.VideoType == "Normal")
                    //{
                        OStatus.RemoveEncodingQueueItem(OVehicleInfo.VehicleKey);
                    //}


                }

            }
            catch (Exception ex)
            {
                //Common.WriteLog("bgWorker_DoWork: " + ex.Message);
                Common.WriteEventLog("bgWorker_DoWork: " + ex.Message, "Error");
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

                pbConvertProgress.Value = e.ProgressPercentage;




            }
            catch (Exception ex)
            {
                //Common.WriteLog("bgWorker_ProgressChanged: " + ex.Message);
                Common.WriteEventLog("bgWorker_ProgressChanged: " + ex.Message, "Error");
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                pbConvertProgress.Value = 0;
                mConverter = null;

                tmrUpload.Start();

                Thread.Sleep(500);

                if (!e.Cancelled)
                {
                    if (lvEncodingQueue.Items.Count > 0)
                    {

                        BeginEncoding();

                    }
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("bgWorker_RunWorkerCompleted: " + ex.Message);
                Common.WriteEventLog("bgWorker_RunWorkerCompleted: " + ex.Message, "Error");
            }
        }

        // ///// <summary>
        // ///// Start uploading using timer
        // ///// </summary>
        // ///// <param name="sender"></param>
        // ///// <param name="e"></param>
        private void tmrUpload_Tick(object sender, EventArgs e)
        {
            try
            {

                if (!_uploadStarted && lvUploading.Items.Count > 0)
                {
                    Thread th = new Thread(new ThreadStart(StartUpload));
                    th.IsBackground = true;
                    th.Start();
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("tmrUpload_Tick: " + ex.Message);
                Common.WriteEventLog("tmrUpload_Tick: " + ex.Message, "Error");
            }
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvEncodingQueue.Items.Count == 0 &&
                    lvUploading.Items.Count == 0 &&
                    lvVideoUploaded.Items.Count == 0)
                {
                    return;
                }

                //Common.WriteLog("Info: btnClearAll_Click: Clearing all the lists");
                Common.WriteEventLog("btnClearAll_Click: Clearing all the lists: ", "Information");
                MessageBoxResult msgResult = MessageBox.Show(ResourceTxt.ClearAllListsMessage, "Clear Items", MessageBoxButton.YesNo);

                if (msgResult == MessageBoxResult.No)
                {
                    return;
                }

                if (encodingVehicleInfolist.Count > 0)
                {
                    encodingCanceled = true;
                    ClearLists.DeleteItemsFromList(ref encodingVehicleInfolist, false);
                    lvEncodingQueue.Items.Refresh();

                }

                if (uploadingVehicleInfoList.Count > 0)
                {
                    uploadCanceled = true;
                    ResetProgressBar();
                    ClearLists.DeleteItemsFromList(ref uploadingVehicleInfoList, true);
                    try
                    {
                        ClearLists.DeleteItemsFromList(ref ImageUploadingVehicleInfoList, true);
                    }
                    catch (Exception ex)
                    { Common.WriteEventLog("StartUpload:" + ex.Message, "Error"); }
                    lvUploading.Items.Refresh();
                    //objTestPage.slvUploading.Items.Refresh();
                }

                if (uploadedVehicleInfolist.Count > 0)
                {
                    ClearLists.DeleteItemsFromList(ref uploadedVehicleInfolist, false);
                    ClearLists.DeleteItemsFromList(ref ImageUploadedVehicleInfolist, false);
                    lvVideoUploaded.Items.Refresh();
                    //objTestPage.slvVideoUploaded.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("Status_btnClearAll_Click:" + ex.Message);
                Common.WriteEventLog("Status_btnClearAll_Click: " + ex.Message, "Error");
            }
        }


        private void upLoadingChkBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (uploadingVehicleInfoList[0].IsSelected)
                    uploadingVehicleInfoList[0].IsSelected = false;
                lvUploading.Items.Refresh();
            }
            catch { }
            //objTestPage.slvUploading.Items.Refresh();
        }

        private void btnDelUploading_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = uploadingVehicleInfoList.Count; i > 1; i--)
                {
                    if (uploadingVehicleInfoList[i - 1].IsSelected)
                    {
                        RemoveRecordFromFile(uploadingVehicleInfoList[i - 1].OutputFileName);
                        uploadingVehicleInfoList.Remove(uploadingVehicleInfoList[i - 1]);

                        ImageObjects.UploadingList.Remove(ImageObjects.UploadingList[i - 1]);

                    }
                }
            }
            catch (Exception ex) { Common.WriteEventLog("btnDelUploading_Click: " + ex.Message, "Error"); }
            lvUploading.Items.Refresh();
            //objExtractImage.slvUploading.Items.Refresh();
            //objExtractImage.slvUploading.ItemsSource = uploadingVehicleInfoList;

        }

        private void encodingCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (encodingVehicleInfolist[0].IsSelected)
                encodingVehicleInfolist[0].IsSelected = false;
            lvEncodingQueue.Items.Refresh();
        }

        private void btnDelEncoding_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = encodingVehicleInfolist.Count; i > 1; i--)
                {
                    if (encodingVehicleInfolist[i - 1].IsSelected)
                    {
                        encodingVehicleInfolist.Remove(uploadingVehicleInfoList[i - 1]);
                    }
                }
                lvEncodingQueue.Items.Refresh();
            }
            catch { }
        }

        #endregion


        public void AddEncodingQueue(VehicleInfo poVehicleInfo)
        {
            try
            {
                    encodingVehicleInfolist.Add(poVehicleInfo);
                    lvEncodingQueue.Items.Refresh();
                
                if (!_encoding)
                {
                    //if (poVehicleInfo.VideoType == "Inventory" || poVehicleInfo.VideoType == "NonInventory")
                    //{
                    //    Dispatcher.Invoke(new Action(() => this.label1.Visibility = Visibility.Hidden));
                    //}
                    //else
                    //{
                    //    Dispatcher.Invoke(new Action(() => this.label1.Visibility = Visibility.Visible));
                    //}
                    BeginEncoding();


                }





                //objExtractImage.Show();
                //objExtractImage.Hide();
            }
            catch (Exception ex)
            {
                //Common.WriteLog("AddEncodingQueue: " + ex.Message);
                Common.WriteEventLog("AddEncodingQueue: " + ex.Message, "Error");
            }
        }

        public void RemoveEncodingQueueItem(string keyItem)
        {
            int countItems = 0;

            try
            {
                countItems = encodingVehicleInfolist.Count;
                //Dispatcher.Invoke(new Action(() => countItems = lvEncodingQueue.Items.Count));
                if (countItems > 0)
                {
                    encodingVehicleInfolist.RemoveAt(0);

                    Dispatcher.Invoke(new Action(() => lvEncodingQueue.Items.Refresh()));
                    //Dispatcher.Invoke(new Action(() => lvEncodingQueue.Items.RemoveAt(0)));
                }

                Dispatcher.Invoke(new Action(() => countItems = lvEncodingQueue.Items.Count));
                if (countItems > 0)
                {
                    // Dispatcher.Invoke(new Action(() => lvEncodingQueue.ItemTemplate);
                }

                if (lvEncodingQueue.Items.Count == 0)
                {
                    _encoding = false;
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("RemoveEncodingQueueItem: " + ex.Message);
                Common.WriteEventLog("RemoveEncodingQueueItem: " + ex.Message, "Error");
            }
        }

        public void RemoveUploadingQueueItem()
        {
            int countItems = 0;
            string strOutFileName = string.Empty;
            string strExtract = "False";
            try
            {
                Dispatcher.Invoke(new Action(() => countItems = lvUploading.Items.Count));
                if (countItems > 0)
                {
                    Dispatcher.Invoke(new Action(() => strOutFileName = uploadingVehicleInfoList[0].OutputFileName));
                    strExtract = Convert.ToString(uploadingVehicleInfoList[0].IsExtract);
                    uploadingVehicleInfoList.RemoveAt(0);
                    Dispatcher.Invoke(new Action(() => lvUploading.Items.Refresh()));
                    try
                    {
                        if (ImageObjects.UploadingList.Count > 0 && strExtract.Equals("True"))
                        {
                            ImageObjects.UploadingList.RemoveAt(0);
                            
                        }
                    }
                    catch (Exception ex) { Common.WriteEventLog("RemoveUploadingQueueItem-Extract: " + ex.Message, "Error"); }
                    //objTestPage.slvUploading.Items.Refresh();
                    //ListViewItem lvItem = null;
                    //Dispatcher.Invoke(new Action(() => lvItem = lvUploading.Items[0] as ListViewItem));
                    //Dispatcher.Invoke(new Action(() => lvItem.Foreground = Brushes.Blue));

                    // Remove from XML file also
                    RemoveRecordFromFile(strOutFileName);
                }

                Dispatcher.Invoke(new Action(() => countItems = lvUploading.Items.Count));
                if (countItems > 0)
                {
                    //Dispatcher.Invoke(new Action(() => lvUploading.Items..BackColor = Color.YellowGreen));
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("RemoveUploadingQueueItem: " + ex.Message);
                Common.WriteEventLog("RemoveUploadingQueueItem: " + ex.Message, "Error");
            }
        }

        // /// <summary>
        // /// Remove the Uploaded file from XML
        // /// </summary>
        // /// <param name="OutputFileName"></param>
        private void RemoveRecordFromFile(string OutputFileName)
        {
            try
            {
                string strUploadFile = Common.UploadRecordFile;

                if (string.IsNullOrEmpty(strUploadFile))
                {
                    //Common.WriteLog("RemoveRecordFromFile: failed to get the Upload Record file name");
                    Common.WriteEventLog("RemoveRecordFromFile: failed to get the Upload Record file name: ", "Information");
                    return;
                }

                if (!File.Exists(strUploadFile))
                {
                    //Common.WriteLog("RemoveRecordFromFile: Upload Record file " + strUploadFile + " not found");
                    Common.WriteEventLog("RemoveRecordFromFile: Upload Record file " + strUploadFile + " not found", "Information");
                    return;
                }

                XElement doc = XElement.Load(strUploadFile);

                var result = from videos in doc.Descendants("File") where videos.Element("OutputFileName").Value == OutputFileName select videos;

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
        }

        public void AddUploadingQueue(VehicleInfo poVehicleInfo)
        {
            //ListViewItem lvItem = null;

            try
            {
                if (lvEncodingQueue.Items.Count > 0)
                {
                    uploadingVehicleInfoList.Add(poVehicleInfo);
                    //if (poVehicleInfo.VideoType == "Inventory" || poVehicleInfo.VideoType == "NonInventory")
                    //{
                    //    Dispatcher.Invoke(new Action(() => this.label3.Visibility = Visibility.Hidden));
                    //}
                    //else
                    //{
                    //    Dispatcher.Invoke(new Action(() => this.label3.Visibility = Visibility.Visible));
                    //}

                    if (Convert.ToString(poVehicleInfo.IsExtract).Equals("True"))
                    {
                        ImageUploadingVehicleInfoList.Add(poVehicleInfo);
                    }

                }
                


                Dispatcher.Invoke(new Action(() => lvUploading.Items.Refresh()));

                AddRecordToFile();

                //AddVideo = "";

            }
            catch (Exception ex)
            {
                lblUploadError.Visibility = Visibility.Visible;
                lblUploadError.Content = ResourceTxt.UploadError_AddQ;

                //Common.WriteLog("AddUploadingQueue: " + ex.Message);
                Common.WriteEventLog("AddUploadingQueue: " + ex.Message, "Error");
            }
        }

        // /// <summary>
        // /// Add Records from if there is any pending file to upload from previous Run
        // /// </summary>
        public void AddUploadingQueueFromFile()
        {
            try
            {
                // Call this only once
                if (_firstLoad)
                {
                    _firstLoad = false;

                    string strUploadFile = Common.UploadRecordFile;

                    if (!File.Exists(strUploadFile))
                        return;

                    XElement doc = XElement.Load(strUploadFile);
                    //ListViewItem lvItem = null;

                    var pending = from videos in doc.Descendants("File") select videos;

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
                        if(xElePending.Element(XNAME.VIN)!=null)
                        strVIN = xElePending.Element(XNAME.VIN).Value;

                        string VType="";
                        if (xElePending.Element(XNAME.VIDEOTYPE) != null)
                        {
                             VType= xElePending.Element(XNAME.VIDEOTYPE).Value;
                        }



                        VehicleInfo uploadingVInfo = new VehicleInfo();
                        uploadingVInfo.VehicleKey = strVehicleKey;
                        uploadingVInfo.InputFileName = strSourceFile;
                        uploadingVInfo.OutputFileName = strOutFileName;
                        uploadingVInfo.RooftopKey = strRoofTop;
                        uploadingVInfo.VideoTitle = strVideoTitle;
                        uploadingVInfo.Description = strDesc;
                        uploadingVInfo.IsDefault = strIsDefault;
                        uploadingVInfo.IsExtract = strIsExtract;
                        uploadingVInfo.VIN = strVIN;
                        uploadingVInfo.VideoType = VType;
                        if (strStock != "")
                            uploadingVInfo.Stock = strStock;
                        uploadingVehicleInfoList.Add(uploadingVInfo);
                        lvUploading.Items.Refresh();
                        if (strIsExtract.Equals("True"))
                        {
                            ImageUploadingVehicleInfoList.Add(uploadingVInfo);
                        }


                        objExtractImage.slvUploading.Items.Refresh();

                    }

                    tmrUpload.Start();
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("AddUploadingQueueFromFile: " + ex.Message);
                Common.WriteEventLog("AddUploadingQueueFromFile: " + ex.Message, "Error");
                lblUploadError.Visibility = Visibility.Visible;

                lblUploadError.Content = ResourceTxt.UploadError_FileLoad;
            }
        }

        // /// <summary>
        // /// Add the Uploading file to XML
        // /// </summary>
        private void AddRecordToFile()
        {
            try
            {
                string strUploadFile = Common.UploadRecordFile;

                if (!File.Exists(strUploadFile))
                {
                    XmlDocument XDoc = new XmlDocument();
                    XmlElement XElemRoot = XDoc.CreateElement(XNAME.UPLOADING_FILE);
                    XDoc.AppendChild(XElemRoot);
                    XDoc.Save(strUploadFile);
                }

                XElement doc = XElement.Load(strUploadFile);


                doc.Add(
                new XElement(XNAME.FILE,
                new XElement(XNAME.VEHICLE_KEY, encodingVehicleInfolist[0].VehicleKey),
                new XElement(XNAME.SOURCE_FILE_NAME, encodingVehicleInfolist[0].InputFileName),
                new XElement(XNAME.OUTPUT_FILE_NAME, encodingVehicleInfolist[0].OutputFileName),
                new XElement(XNAME.ROOF_TOP_KEY, encodingVehicleInfolist[0].RooftopKey),      // Add Rooftop, Title, Desc
                new XElement(XNAME.VIDEO_TITLE, encodingVehicleInfolist[0].VideoTitle),
                new XElement(XNAME.DESCRIPTION, encodingVehicleInfolist[0].Description),
                new XElement(XNAME.IS_DEFAULT, encodingVehicleInfolist[0].IsDefault),
                new XElement(XNAME.IS_EXTRACT, encodingVehicleInfolist[0].IsExtract),
                new XElement(XNAME.Stock, encodingVehicleInfolist[0].Stock),
                new XElement(XNAME.VIN,encodingVehicleInfolist[0].VIN),
                new XElement(XNAME.VIDEOTYPE,encodingVehicleInfolist[0].VideoType)                                                                                                                                      
                ));

                doc.Save(strUploadFile);
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("AddRecordToFile: " + ex.Message);
                //Common.WriteLog("AddRecordToFile: " + ex.Message);
                Common.WriteEventLog("AddRecordToFile: " + ex.Message, "Error");
            }
        }

        private void AddExtractedVideoTofile()
        {
            try
            {
                string strExtractedFile = Common.ExtractedImagesFile;

                if (!File.Exists(strExtractedFile))
                {
                    XmlDocument XDoc = new XmlDocument();
                    XmlElement XElemRoot = XDoc.CreateElement(XNAME.ExTRACTED_VIDEOS);
                    XDoc.AppendChild(XElemRoot);
                    XDoc.Save(strExtractedFile);
                }

                XElement doc = XElement.Load(strExtractedFile);


                doc.Add(
                new XElement(XNAME.FILE,
                new XElement(XNAME.VEHICLE_KEY, uploadingVehicleInfoList[0].VehicleKey),
                new XElement(XNAME.SOURCE_FILE_NAME, uploadingVehicleInfoList[0].InputFileName),
                new XElement(XNAME.OUTPUT_FILE_NAME, uploadingVehicleInfoList[0].OutputFileName),
                new XElement(XNAME.ROOF_TOP_KEY, uploadingVehicleInfoList[0].RooftopKey),      // Add Rooftop, Title, Desc
                new XElement(XNAME.VIDEO_TITLE, uploadingVehicleInfoList[0].VideoTitle),
                new XElement(XNAME.DESCRIPTION, uploadingVehicleInfoList[0].Description),
                new XElement(XNAME.IS_DEFAULT, uploadingVehicleInfoList[0].IsDefault),
                new XElement(XNAME.IS_EXTRACT, uploadingVehicleInfoList[0].IsExtract),
                new XElement(XNAME.Stock, uploadingVehicleInfoList[0].Stock),
                new XElement(XNAME.VIN, uploadingVehicleInfoList[0].VIN),
                new XElement(XNAME.VIDEOTYPE, uploadingVehicleInfoList[0].VideoType),
                new XElement(XNAME.EXPORTEDSTATUS,"Not Exported")
                ));

                doc.Save(strExtractedFile);
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("AddRecordToFile: " + ex.Message);
                //Common.WriteLog("AddRecordToFile: " + ex.Message);
                Common.WriteEventLog("AddRecordToFile: " + ex.Message, "Error");
            }
        }
        

        public void AddUploadedQueue(VehicleInfo poVehicleInfo)
        {
            // ListViewItem lvItem = null;

            uploadedVehicleInfolist.Add(poVehicleInfo);
            try
            {
                if (Convert.ToString(poVehicleInfo.IsExtract).Equals("True"))
                    ImageObjects.UploadedList.Add(poVehicleInfo);
            }
            catch { }
            // lvVideoUploaded.Items.Add(obj.VehicleKey.Contains(Content.ToString));
            try
            {
                Dispatcher.Invoke(new Action(() => lvVideoUploaded.Items.Refresh()));
                Dispatcher.Invoke(new Action(() => objExtractImage.slvVideoUploaded.Items.Refresh()));
                Dispatcher.Invoke(new Action(() => objExtractImage.slvUploading.Items.Refresh()));

                //Dispatcher.Invoke(new Action(() => lvVideoUploaded.Items.Add(lvUploading.Items.Add(INDEX.INPUT_FILE_NAME).ToString())));
                //Dispatcher. Invoke(new Action(() =>lvVideoUploaded.Items.Add(lvUploading.Items.Add(INDEX.OUTPUT_FILE_NAME).ToString())));
            }
            catch (Exception ex)
            {
                //Common.WriteLog("AddUploadedQueue: " + ex.Message);
                Common.WriteEventLog("AddUploadedQueue: " + ex.Message, "Error");
            }

        }

        public void BeginEncoding()
        {
            _encoding = true;

            try
            {
                ICTerminateInfo terminateInfo = null;
                mOptionForm = new OutputOptions(1);

                if (!OpenConverter(true))
                {
                    //Common.WriteLog("BeginEncoding: " + ResourceTxt.ErrorOpeningConverter);
                    Common.WriteEventLog("BeginEncoding: " + ResourceTxt.ErrorOpeningConverter, "Information");
                    return;
                }

                string Aseed = ResourceTxt.Aseed;
                string ALiscenceKey = ResourceTxt.ALiscenceKey;

                mConverter.SetLicenseKey(Aseed, ALiscenceKey);
                mOptionForm.SetConverter(mConverter);

                VehicleInfo vInfo = encodingVehicleInfolist[0];
                //VehicleInfo vInfo = OVehicleInfo;
                string strInputFileName = vInfo.InputFileName;//lvEncodingQueue.Items.Contains(INDEX.INPUT_FILE_NAME).ToString();
                string strOutputFileName = vInfo.OutputFileName;//lvEncodingQueue.Items[INDEX.OUTPUT_FILE_NAME].ToString();

                //suneetha commented
               // targetVideoWidth = Convert.ToInt32(vInfo.TargetVideoWidth);//lvEncodingQueue.Items.Add((INDEX.VIDEO_WIDTH).ToString());
             //  targetVideoHeight = Convert.ToInt32(vInfo.TargetVideoHeight);//lvEncodingQueue.Items.Add((INDEX.VIDEO_HEIGHT).ToString());//
                                                                            //suneetha commented out


               

               


                AddTask(strInputFileName);
                StartConversion();

                terminateInfo = mConverter.get_TerminateInfo(0);
                if (terminateInfo != null)
                {
                    int taskIndex = terminateInfo.TaskIndex;
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("BeginEncoding:" + ex.Message);
                Common.WriteEventLog("BeginEncoding: " + ex.Message, "Error");
            }
        }

        public void VideoConverter()
        {

            OVehicleInfo.Converter = mConverter;
            //vehiInfo.Converter = mConverter;
            // Add pending uploadable files from the last cycle to Uploading queue
            AddUploadingQueueFromFile();
            //OStatus.AddEncodingQueue(vehiInfo);
            OStatus.AddEncodingQueue(OVehicleInfo);
            //OStatus.Refresh();
        }

        private bool OpenConverter(bool first)
        {
            try
            {              
                mConverter = new CAVEditLib.CAVConverter();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                if (first)
                {
                    if (ex.ErrorCode == -2147221164)
                    {
                        if (!RegisterCAVEditLib())
                        {
                            return false;
                        }
                        return OpenConverter(false);
                    }
                    else
                    {
                        //Common.WriteLog("OpenConverter:" + ex.Message);
                        Common.WriteEventLog("OpenConverter: " + ex.Message, "Error");
                        return false;
                    }
                }
                else
                {
                    //Common.WriteLog("OpenConverter:" + ex.Message);
                    Common.WriteEventLog("OpenConverter: " + ex.Message, "Error");
                    return false;
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("OpenConverter: " + ResourceTxt.ErrorOpeningConverter + ex.Message);
                Common.WriteEventLog("OpenConverter: " + ResourceTxt.ErrorOpeningConverter + ex.Message, "Error");
                return false;
            }

            return true;
        }

        private void AddTask(string FileName)
        {
            try
            {
                if (mConverter == null)
                {
                    return;
                }
                //objExtractImage.Show();
                //objExtractImage.Hide();
                if (!mConverter.AVLibLoaded())
                {
                    string path = null;

                    path = System.Windows.Forms.Application.StartupPath;
                    path += LIBAV_PATH;

                    if (!mConverter.LoadAVLib(path))
                    {
                        // Common.WriteLog("AddTask: " + ResourceTxt.ErrorAddingTask);
                        Common.WriteEventLog("AddTask: " + ResourceTxt.ErrorAddingTask, "Warning");
                        return;
                    }
                }

                if (!mConverter.AVPrope.LoadFile(FileName, ""))
                {
                    //Common.WriteLog("AddTask: " + ResourceTxt.ErrorFileLoading);

                    //Common.WriteLog("AddTask: " + ResourceTxt.EncodingErrorMsg1 + " " + FileName + ResourceTxt.EncodingErrorMsg2);
                    Common.WriteEventLog("AddTask: " + ResourceTxt.EncodingErrorMsg1 + " " + FileName + ResourceTxt.EncodingErrorMsg2, "Warning");
                    Common.WriteEventLog("AddTask: " + mConverter.AVPrope.LastErrMsg, "Warning");
                    // Common.WriteLog("AddTask: " + mConverter.AVPrope.LastErrMsg);
                    lblError.Content = ResourceTxt.EncodingErrorMsg1 + " " + System.IO.Path.GetFileName(FileName) + ResourceTxt.EncodingErrorMsg2;
                    lblError.Visibility = Visibility.Visible;

                    RemoveEncodingQueueItem(lblVehicleKeyValue.Content.ToString());



                    return;
                }

                lblVehicleKeyValue.Content = string.Empty;
                //try
                //{
                mOptionForm.SetConverter(mConverter);

                mOptionForm.GetInputOptions();
                // pass width and Height to decide Padding and Cropping
                //suneethe aded here
                double srcWidth = Convert.ToDouble(mConverter.AVPrope.FirstVideoStreamInfo.Width);
                double srcHgt = Convert.ToDouble(mConverter.AVPrope.FirstVideoStreamInfo.Height);

                double aspRatio = srcWidth / srcHgt;

                int targetVideoWidth = 0;
                int targetVideoHeight = 0;
                if (aspRatio >= 1.6)
                {
                    targetVideoWidth = 960;
                    targetVideoHeight = 540;
                }
                else
                {
                    targetVideoWidth = 640;
                    targetVideoHeight = 480;
                }

              
               
               
               

                //suneetha added ended here
                mOptionForm.GetOutputOptions(targetVideoWidth, targetVideoHeight);

                AddFile(mConverter.InputOptions, mConverter.OutputOptions);

            }
            catch (Exception ex)
            {
                //Common.WriteLog("AddTask: " + ResourceTxt.ErrorFileLoading + ex.Message);
                Common.WriteEventLog("AddTask: " + ResourceTxt.ErrorFileLoading + ex.Message, "Error");
            }
            finally
            {
                mConverter.AVPrope.CloseFile();
            }
        }

        private void AddFile(ICInputOptions inputOptions, ICOutputOptions outputOptions)
        {
            int taskIndex = 0;

            if (mConverter == null || inputOptions == null || outputOptions == null)
            {
                return;
            }

            try
            {
                // TODO - verify the file name and Hard Coded file extension
                Vehicle obj = new Vehicle();



                string strOutputFileName = encodingVehicleInfolist[0].OutputFileName;
                string OutFileLocation = Common.GetVideoSettingInfo("OutputVideoLocation");

                if (string.IsNullOrEmpty(OutFileLocation))
                {
                    System.Windows.Forms.MessageBox.Show("Output video location is not correct.\nPlease check the settings and give the valid path");
                    return;
                }

                //strOutputFile = OutFileLocation + "\\" + strOutputFileName + ".flv";
                strOutputFile = OutFileLocation + "\\" + strOutputFileName + ".mp4";
                if (!File.Exists(strOutputFile))
                {
                    outputOptions.FileName = strOutputFile;

                    if (encodingVehicleInfolist[0].IsExtract.ToUpper() == "TRUE")
                    {
                        File.Copy(inputOptions.FileName, outputOptions.FileName, true);

                        uploadingVehicleInfoList.Add(encodingVehicleInfolist[0]);
                        lvUploading.Items.Refresh();

                        encodingVehicleInfolist.RemoveAt(0);
                        Dispatcher.Invoke(new Action(() => lvEncodingQueue.Items.Refresh()));
                        _encoding = false;
                    }                    
                    else
                    {
                        taskIndex = mConverter.AddTask(inputOptions.FileName, outputOptions.FileName);
                        if (taskIndex < 0)
                        {
                            //Common.WriteLog("AddFile: " + ResourceTxt.ErrorAddingTask);
                            Common.WriteEventLog("AddTask: " + ResourceTxt.ErrorAddingTask, "Warning");
                            return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //Common.WriteLog("AddFile: " + ex.Message);
                Common.WriteEventLog("AddTask: " + ex.Message, "Error");
            }
        }

        public void StartConversion()
        {
            try
            { 
                if (mConverter == null)
                {
                    return;
                }



                mConverter.ThreadPriority = mThreadPriority;
                mConverter.LogPath = Common.LogFile;
                
                mConverter.Start(mThreadCount);

                if (bgWorker.IsBusy)
                {
                    bgWorker.WorkerSupportsCancellation = true;
                }
                else
                {
                    bgWorker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("StartConversion: " + ex.ToString());
                Common.WriteEventLog("StartConversion: " + ex.Message, "Error");
            }
        }

        private bool RegisterCAVEditLib()
        {
            bool ret = true;
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                string libPath = System.Windows.Forms.Application.StartupPath + "\\" + CAVEDIT_LIB_NAME;
                string regsvr32Path = Environment.SystemDirectory + "\\regsvr32.exe";

                startInfo.Arguments = "\"" + libPath + "\"" + " /s";
                startInfo.FileName = regsvr32Path;

                // TODO - Check the alternate for this.
                // This required because to register the DLL, user needs admin rights
                // This is the one time process in the application life cycle 
                // or till the corresponding registry availabe
                startInfo.Verb = "runas";

                System.Diagnostics.Process regProcess = System.Diagnostics.Process.Start(startInfo);
                regProcess.WaitForExit();
            }
            catch (Exception e)
            {
                ret = false;
                Common.WriteEventLog("RegisterCAVEditLib: " + e.Message, "Error");
            }

            return ret;
        }

        private void StartUpload()
        {
            string strExtract = "False";
            try
            {
                if (!_uploadStarted)
                {
                    _uploadStarted = true;

                    while (lvUploading.Items.Count > 0)
                    {
                        string strFileToUpload = null;
                        string OutFileLocation = null;

                        //Dispatcher.Invoke(new Action(() => lvitem = lvUploading.ItemContainerGenerator.ContainerFromIndex(0) as ListViewItem));
                        //Dispatcher.Invoke(new Action(() =>lvitem.Foreground  = Brushes.Blue));

                        OutFileLocation = Common.GetVideoSettingInfo("OutputVideoLocation");

                        //Dispatcher.Invoke(new Action(() => strFileToUpload = OutFileLocation + "\\" + uploadingVehicleInfoList[0].OutputFileName + ".flv"));
                        Dispatcher.Invoke(new Action(() => strFileToUpload = OutFileLocation + "\\" + uploadingVehicleInfoList[0].OutputFileName + ".mp4"));


                        if (!UploadFile(strFileToUpload))
                        {
                            //break;
                            if (lvUploading.Items.Count > 0)
                            {
                                strExtract = Convert.ToString(uploadingVehicleInfoList[0].IsExtract);
                                uploadingVehicleInfoList.RemoveAt(0);
                                Dispatcher.Invoke(new Action(() => lvUploading.Items.Refresh()));
                                try
                                {
                                    if (ImageUploadingVehicleInfoList.Count > 0 && strExtract.Equals("True"))
                                        ImageUploadingVehicleInfoList.RemoveAt(0);
                                }
                                catch (Exception ex)
                                { Common.WriteEventLog("StartUpload:" + ex.Message, "Error"); }
                            }

                            Dispatcher.Invoke(new Action(() => lblUploadError.Visibility = Visibility.Visible));
                        }
                        else
                        {
                            if (!uploadCanceled)
                                RemoveRecord(strFileToUpload);
                        }
                    }
                    _uploadStarted = false;
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("StartUpload: " + ex.Message);
                Common.WriteEventLog("StartUpload: " + ex.Message, "Error");
                return;
            }
        }

        public bool UploadFile(string fileName)
        {
            //FileStream fst = null;
            Stream rest = null;
            //int loopCount = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime;
            TimeSpan duration;

            //Common.WriteLog("Uploading file: " + fileName);
            Common.WriteEventLog("Uploading file: " + fileName, "Information");
            try
            {
                Dispatcher.Invoke(new Action(() => lblUploadError.Visibility = Visibility.Hidden));

                Dispatcher.Invoke(new Action(() => lblUploadingVehiceKey.Content = uploadingVehicleInfoList[0].VehicleKey));
                Dispatcher.Invoke(new Action(() => lblUploadingFileName.Content = System.IO.Path.GetFileName(uploadingVehicleInfoList[0].OutputFileName)));
                try
                {
                    
                    ImageObjects.ExtractFile = System.IO.Path.GetFileName(uploadingVehicleInfoList[0].OutputFileName);
                   

                    ImageObjects.VehicleKey = System.IO.Path.GetFileName(uploadingVehicleInfoList[0].VehicleKey);

                }
                catch (Exception ex) { Common.WriteEventLog("UploadFile-Extract:" + ex.Message, "Error"); }
                string ftpPath = ResourceTxt.FTPPath;
                string userName = ResourceTxt.UserName;
                string password = ResourceTxt.Password;

                string flvFileName = System.IO.Path.GetFileName(fileName);


                uploadCanceled = false;

                if (!File.Exists(fileName))
                {
                    //RemoveRecord(FileName);
                    //Common.WriteLog("UploadFile: While uploading " + fileName + " was not found, so deleted from the Que also from XML file list");
                    Common.WriteEventLog("UploadFile: While uploading " + fileName + " was not found, so deleted from the Que also from XML file list", "Warning");
                    Dispatcher.Invoke(new Action(() => lblUploadError.Content = flvFileName + " " + ResourceTxt.FileNotFound));
                    Dispatcher.Invoke(new Action(() => lblUploadError.Visibility = Visibility.Visible));

                    return true;
                }

                //TODO check commented by sathish
                //string ftpfullpath = "ftp://" + ftpPath + @"/" + flvFileName;

                string ftpfullpath = ftpPath + @"/" + flvFileName;

                // TODO
                //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);

                if (!Common.IsConnectedToInternet())
                {
                    Dispatcher.Invoke(new Action(() => lblUploadError.Content = ResourceTxt.NoInternet));
                    Dispatcher.Invoke(new Action(() => lblUploadError.Visibility = Visibility.Visible));
                    return false;
                }

                ftp.Credentials = new NetworkCredential(userName, password);
                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.UsePassive = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;
                ftp.Timeout = Timeout.Infinite;

                fst = new FileStream(fileName, FileMode.Open);

                long fileLenght = fst.Length;
                double fileLenthInMb = System.Math.Round((double)(fst.Length / 1024), 4);
                Dispatcher.Invoke(new Action(() => lblUploadingFileSize.Content = fileLenthInMb.ToString() + "KB"));

                const int bufferLength = 2048;//1024;
                byte[] buffer = new byte[bufferLength];
                long count = 0;
                int readBytes = 0;

                rest = ftp.GetRequestStream();


                Dispatcher.Invoke(new Action(() => lblUploadError.Visibility = Visibility.Hidden));

                // TODO - use using method for stream

                do
                {
                    if (count < fst.Length)
                    {
                        endTime = DateTime.Now;
                        duration = endTime - startTime;
                        if ((long)duration.Milliseconds != 0)
                        {
                            long inASec = (long)1000 / (long)duration.Milliseconds;
                            long uploadSpeed = (long)Math.Round((double)64 * inASec, 2);
                            Dispatcher.Invoke(new Action(() => lblSpeed.Content = uploadSpeed.ToString() + "kbps"));
                        }
                    }
                    readBytes = fst.Read(buffer, 0, bufferLength);
                    rest.Write(buffer, 0, readBytes);
                    count += readBytes;
                    double uploadedSize = Math.Round((double)(count / 1024), 4);
                    Dispatcher.Invoke(new Action(() => lblCompUploadingSize.Content = uploadedSize.ToString() + "KB"));
                    progress = (count * 100) / fileLenght;
                    startTime = DateTime.Now;
                    Dispatcher.Invoke(new Action(() => this.pbUploadProgress.Value = Convert.ToInt32(progress)));
                    ImageObjects.Progress = Convert.ToInt32(progress);

                }
                while (readBytes != 0 && !uploadCanceled);

               // Dispatcher.Invoke(new Action(() => AddVideo = ""));
                Dispatcher.Invoke(new Action(() => lblUploadingFileSize.Content = "0KB"));
                Dispatcher.Invoke(new Action(() => lblCompUploadingSize.Content = "0KB"));
                Dispatcher.Invoke(new Action(() => lblSpeed.Content = "0kbps"));
                progress = 0;
                ImageObjects.Progress = 0;
                _firstTime = true;

                if (uploadCanceled)
                {
                    //Common.WriteLog("Info: UploadFile: Uploading Canceled");
                    Common.WriteEventLog("UploadFile: Uploading Canceled: ", "Information");
                    ResetProgressBar();

                    return true;
                }


                //if (OVehicleInfo.IsExtract == "True")
                //{

                if (uploadingVehicleInfoList[0] != null)
                {
                    if (uploadingVehicleInfoList[0].IsExtract == "True")
                    {
                        if (!string.IsNullOrEmpty(uploadingVehicleInfoList[0].InputFileName) && File.Exists(uploadingVehicleInfoList[0].InputFileName))
                        {

                            GetVideoThumbnail(uploadingVehicleInfoList[0].InputFileName, flvFileName, 20);
                            AddExtractedVideoTofile();
                        }
                        else
                        {
                            GetVideoThumbnail(Common.GetVideoSettingInfo("OutputVideoLocation") + flvFileName, flvFileName, 20);
                            AddExtractedVideoTofile();
                        }
                        //}
                    }
                }
                return true;
            }
            catch (InsufficientMemoryException ex)
            {
                Dispatcher.Invoke(new Action(() => lblUploadError.Content = ResourceTxt.FTPUriError));

                if (_firstTime)
                {
                    Common.WriteLog("UploadFile: Insufficient Memory Exception, " + ex.Message);
                    Common.WriteEventLog("UploadFile: Insufficient Memory Exception: " + ex.Message, "Error");
                    _firstTime = false;
                }

                return false;
            }
            catch (System.UriFormatException ex)
            {
                Dispatcher.Invoke(new Action(() => lblUploadError.Content = ResourceTxt.FTPUriError));

                if (_firstTime)
                {
                    Common.WriteLog("UploadFile: URI Format Exception, " + ex.Message);
                    Common.WriteEventLog("UploadFile: URI Format Exception: " + ex.Message, "Error");
                    _firstTime = false;
                }

                return false;
            }
            catch (System.Net.WebException ex)
            {
                Dispatcher.Invoke(new Action(() => lblUploadError.Content = ResourceTxt.UnableToConnect));

                if (_firstTime)
                {
                    //Common.WriteLog("UploadFile: Web Exception, " + ex.Message);
                    Common.WriteEventLog("UploadFile: Web Exception, " + ex.Message, "Error");
                    _firstTime = false;
                }

                return false;
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(new Action(() => lblUploadError.Content = ResourceTxt.UploadError));

                if (_firstTime)
                {
                    //Common.WriteLog("UploadFile: " + ex.Message);
                    Common.WriteEventLog("UploadFile: " + ex.Message, "Error");
                    _firstTime = false;
                }

                return false;
            }
            finally
            {

                if (fst != null)
                {
                    fst.Close();
                    fst = null;
                }

                if (rest != null)
                {
                    rest.Close();
                    rest = null;
                }
            }
        }

        private void RemoveRecord(string FileName)
        {
            try
            {
                // Update Pending Flag
                if (uploadingVehicleInfoList[0].VideoType == "Normal")
                {
                    Common.OHome.UpdatePendingField(uploadingVehicleInfoList[0].VehicleKey);//lvUploading.Items.Add(INDEX.VEHICLE_KEY).ToString());
                //}
               // Common.OHome.UpdatePendingField(uploadingVehicleInfoList[0].OutputFileName);
                // Notify Server
                //if (uploadingVehicleInfoList[0].VideoType == "Normal")
                //{

                    GetDataToNotify();
                    NotifyCreateVideo();
                    NotifyUploadVideo();
                }
                if (uploadingVehicleInfoList[0].VideoType == "Inventory")
                {

                    GetInvetoryDataToNotify();
                    NotifyInventoryUploadVideo();
                }

                if (uploadingVehicleInfoList[0].VideoType == "NonInventory")
                {
                    GetNonInventoryDataToNotify();
                    NotifyNonInventoryUploadVideo();
                }

                ResetProgressBar();

                AddUploadedQueue(uploadingVehicleInfoList[0]);
                RemoveUploadingQueueItem();

                RemoveOutputFile(FileName);

            }
            catch (Exception ex)
            {
                //Common.WriteLog("RemoveRecord: " + ex.Message);
                Common.WriteEventLog("RemoveRecord: " + ex.Message, "Error");
            }
        }


        private void GetInvetoryDataToNotify()
        {
            strRoofTopNotify = uploadingVehicleInfoList[0].RooftopKey;

            strGuidNotify = System.IO.Path.GetFileNameWithoutExtension(uploadingVehicleInfoList[0].OutputFileName);
            strVideoTitleNotify = uploadingVehicleInfoList[0].VideoTitle;
            strVideoDescNotify = uploadingVehicleInfoList[0].Description;
            strStockNotify = uploadingVehicleInfoList[0].Stock;
            strVINNotify = uploadingVehicleInfoList[0].VIN;
            strIsDefaultNotify = uploadingVehicleInfoList[0].IsDefault;
            if(uploadingVehicleInfoList[0].IsExtract=="True")
                strIsExtractNotify = 1;
            else
                strIsExtractNotify = 0;
        }


        private void GetNonInventoryDataToNotify()
        {
            strRoofTopNotify = uploadingVehicleInfoList[0].RooftopKey;
            strGuidNotify = System.IO.Path.GetFileNameWithoutExtension(uploadingVehicleInfoList[0].OutputFileName);
            strStockNotify = uploadingVehicleInfoList[0].Stock;
            strVideoTitleNotify = uploadingVehicleInfoList[0].VideoTitle;
            strVideoDescNotify = uploadingVehicleInfoList[0].Description;
            strCategoryNotify = uploadingVehicleInfoList[0].Categories;
            strIsDefaultNotify = uploadingVehicleInfoList[0].IsDefault;
            if (uploadingVehicleInfoList[0].IsExtract == "True")
                strIsExtractNotify = 1;
            else
                strIsExtractNotify = 0;
        }


        private bool NotifyNonInventoryUploadVideo()
        {
            try
            {
            // "ACTION=UPLOADVIDEO"
            /*
            http://admin.flickfusion.net/ava/php/appxml.php?
            rooftop_key=[ROOFTOP_KEY]&ACTION=UPLOADVIDEO&VEHKEY=[VEHICLE_KEY]
            &VIDEOGUID=[VIDEOGUID]&NAME=[VIDEONAME]&DEFAULT=[DEFAULT_CHECKBOX]&DESC=[VIDEO_DESC]
            */

                http://admin.flickfusion.net/ava/php/iphonexml_dev.php"

                //?ACTION=UPLOADADVIDEO
                //&rooftop_key=0F502E5E-64A0-4C38-9242-4028B1227DDF
                //&NON_INV_VID=1
                //&VIDEOGUID=34D65848-7500-FA37-EC22-87989D3FCC9D
                //&STOCK=12345
                //&NAME=AD+Video
                //&DESC=This+is+test+video
                //&CATEGORY=Service|Parts


                string postData = "&ACTION=UPLOADADVIDEO";
                postData += "&rooftop_key=" + strRoofTopNotify;
                postData += "&NON_INV_VID=1";
                postData += "&VIDEOGUID=" + strGuidNotify;
                postData += "&STOCK=" + strStockNotify;
                postData += "&NAME=" + strVideoTitleNotify;
                postData += "&DESC=" + strVideoDescNotify;
                postData += "&CATEGORY=" + strCategoryNotify;
                postData += "&DEFAULT=" + strIsDefaultNotify;
                postData += "&EXTRACTIMAGES=" + strIsExtractNotify;

                ////postData += ("&VEHKEY=" + strVehicleKeyNotify);
                //postData += ("&VIDEOGUID=" + strGuidNotify);
                //postData += ("&STOCK=" + strStockNotify);
                //postData += ("VIN=" + strVINNotify);
                //postData += ("&NAME=" + strVideoTitleNotify);
                ////postData += ("&DEFAULT=" + strIsDefaultNotify);
                //postData += ("&DESC=" + strVideoDescNotify);

                // Call the server related URL with all the above querystring  
                return (NotifyServer(postData));
            }
            catch (Exception ex)
            {
                //Common.WriteLog("NotifyUploadVideo: " + ex.Message);
                Common.WriteEventLog("NotifyUploadVideo: " + ex.Message, "Error");
                return false;
            }
        }


        private bool NotifyInventoryUploadVideo()
        {
            try
            {
                // "ACTION=UPLOADVIDEO"
                /*
                http://admin.flickfusion.net/ava/php/appxml.php?
                rooftop_key=[ROOFTOP_KEY]&ACTION=UPLOADVIDEO&VEHKEY=[VEHICLE_KEY]
                &VIDEOGUID=[VIDEOGUID]&NAME=[VIDEONAME]&DEFAULT=[DEFAULT_CHECKBOX]&DESC=[VIDEO_DESC]
                */

                //?ACTION=UPLOADADVIDEO&rooftop_key=0F502E5E-64A0-4C38-9242-4028B1227DDF&
                //INV_VID=1&VIDEOGUID=34D65848-7500-FA37-EC22-87989D3FCC9D&STOCK=12345
                //&VIN=1234567&NAME=AD+Video&DESC=This+is+test+video


                string postData = "&ACTION=UPLOADADVIDEO";
                postData += "&rooftop_key=" + strRoofTopNotify;
                postData += "&INV_VID=1";
                postData += "&VIDEOGUID=" + strGuidNotify;
                postData += "&STOCK=" + strStockNotify;
                postData += "&VIN=" + strVINNotify;
                postData += "&NAME=" + strVideoTitleNotify;
                postData += "&DESC=" + strVideoDescNotify;
                postData += "&DEFAULT=" + strIsDefaultNotify;
                postData += "&EXTRACTIMAGES=" + strIsExtractNotify;

                ////postData += ("&VEHKEY=" + strVehicleKeyNotify);
                //postData += ("&VIDEOGUID=" + strGuidNotify);
                //postData += ("&STOCK=" + strStockNotify);
                //postData += ("VIN=" + strVINNotify);
                //postData += ("&NAME=" + strVideoTitleNotify);
                ////postData += ("&DEFAULT=" + strIsDefaultNotify);
                //postData += ("&DESC=" + strVideoDescNotify);

                // Call the server related URL with all the above querystring  
                return (NotifyServer(postData));
            }
            catch (Exception ex)
            {
                //Common.WriteLog("NotifyUploadVideo: " + ex.Message);
                Common.WriteEventLog("NotifyUploadVideo: " + ex.Message, "Error");
                return false;
            }
        }


        private void ResetProgressBar()
        {
            Dispatcher.Invoke(new Action(() => this.pbUploadProgress.Value = 0));
            Dispatcher.Invoke(new Action(() => lblUploadingVehiceKey.Content = ""));
            Dispatcher.Invoke(new Action(() => lblUploadingFileName.Content = ""));
            ImageObjects.Progress = 0;
        }

        private void GetDataToNotify()
        {
            strRoofTopNotify = uploadingVehicleInfoList[0].RooftopKey;
            strVehicleKeyNotify = uploadingVehicleInfoList[0].VehicleKey;
            strGuidNotify = System.IO.Path.GetFileNameWithoutExtension(uploadingVehicleInfoList[0].OutputFileName);
            strVideoTitleNotify = uploadingVehicleInfoList[0].VideoTitle;
            strVideoDescNotify = uploadingVehicleInfoList[0].Description;
            strIsDefaultNotify = uploadingVehicleInfoList[0].IsDefault;
            if(uploadingVehicleInfoList[0].IsExtract=="True")
                strIsExtractNotify = 1;
            else
                strIsExtractNotify = 0;
            strStockNotify = uploadingVehicleInfoList[0].Stock;

        }

        private bool NotifyCreateVideo()
        {
            try
            {
                // "ACTION=CREATEVIDEO"
                /*
                http://admin.flickfusion.net/ava/php/appxml.php?
                ACTION=CREATEVIDEO&rooftop_key=[ROOFTOP_KEY]&VEHKEY=[VEHICLEKEY]
                &VIDEOGUID=[VIDEOGUID]&NAME=[VIDEO_NAME]&DESC=[VIDEO_DESC]
                */

                //var encoding = new ASCIIEncoding();
                string postData = "ACTION=CREATEVIDEO";

                postData += ("&rooftop_key=" + strRoofTopNotify);
                postData += ("&VEHKEY=" + strVehicleKeyNotify);
                postData += ("&VIDEOGUID=" + strGuidNotify);
                postData += ("&NAME=" + strVideoTitleNotify);
                postData += ("&DESC=" + strVideoDescNotify);

                // Call the server related URL with all the above querystring  
                return (NotifyServer(postData));
            }
            catch (Exception ex)
            {
                //Common.WriteLog("NotifyCreateVideo: " + ex.Message);
                Common.WriteEventLog("NotifyCreateVideo: " + ex.Message, "Error");
                return false;
            }
        }

        private bool NotifyUploadVideo()
        {
            try
            {
                // "ACTION=UPLOADVIDEO"
                /*
                http://admin.flickfusion.net/ava/php/appxml.php?
                rooftop_key=[ROOFTOP_KEY]&ACTION=UPLOADVIDEO&VEHKEY=[VEHICLE_KEY]
                &VIDEOGUID=[VIDEOGUID]&NAME=[VIDEONAME]&DEFAULT=[DEFAULT_CHECKBOX]&DESC=[VIDEO_DESC]
                */


                //?ACTION=UPLOADADVIDEO&rooftop_key=0F502E5E-64A0-4C38-9242-4028B1227DDF&
                //INV_VID=1&VIDEOGUID=34D65848-7500-FA37-EC22-87989D3FCC9D&STOCK=12345
                //&VIN=1234567&NAME=AD+Video&DESC=This+is+test+video


                string postData = "rooftop_key=" + strRoofTopNotify + "&ACTION=UPLOADVIDEO";

                postData += ("&VEHKEY=" + strVehicleKeyNotify);
                postData += ("&VIDEOGUID=" + strGuidNotify);
                postData += ("&NAME=" + strVideoTitleNotify);
                postData += ("&DEFAULT=" + strIsDefaultNotify);
                postData += ("&DESC=" + strVideoDescNotify);
                postData += "&EXTRACTIMAGES=" + strIsExtractNotify;
                // Call the server related URL with all the above querystring  
                return (NotifyServer(postData));
            }
            catch (Exception ex)
            {
                //Common.WriteLog("NotifyUploadVideo: " + ex.Message);
                Common.WriteEventLog("NotifyUploadVideo: " + ex.Message, "Error");
                return false;
            }
        }

        private bool NotifyServer(string postData)
        {
            bool _failed = false;

            // Call the server related URL with all the above querystring 
            string serverData = string.Empty;
            serverData = Common.GetServerData(postData);

            // TODO -remove this log
            //Common.WriteLog("Notify server: " + postData);
            //Common.WriteLog("Server response: " + serverData);
            Common.WriteEventLog("Server response sucess ", "Information");
            if (string.IsNullOrEmpty(serverData))
                return false;

            if (serverData.Equals("WebError"))
            {
                lblError.Content = ResourceTxt.GetServerData_WebError;
                _failed = true;
            }
            else if (serverData.Equals("Error"))
            {
                lblError.Content = ResourceTxt.GetServerData_Error;
                _failed = true;
            }

            if (_failed)
            {
                lblError.Visibility = Visibility.Visible;

                return false;
            }

            return true;
        }

        /// <summary>
        /// If the user selected to delete the Output File in settings, 
        /// then delete that file from the output video location
        /// </summary>
        private void RemoveOutputFile(string OutputFile)
        {
            try
            {
                if (File.Exists(OutputFile))
                {
                    string strStoreVideo = Common.GetVideoSettingInfo("StoreEncodedVideo");
                    if (!string.IsNullOrEmpty(strStoreVideo))
                    {
                        if (strStoreVideo.ToLower() == "false")
                        {
                            File.Delete(OutputFile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("RemoveOutputFile: " + ex.Message);
                Common.WriteEventLog("RemoveOutputFile: " + ex.Message, "Error");
            }
        }

        internal void TerminateUploading()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                //Common.WriteLog("Status.TerminateUploading:" + ex.Message);
                Common.WriteEventLog("Status.TerminateUploading:" + ex.Message, "Error");
            }
        }

        private void lblUploadingVehiceKey_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            MessageBox.Show("dasdasd");
        }

        private void lblUploadingVehiceKey_TextInput(object sender, TextCompositionEventArgs e)
        {
            MessageBox.Show("dasdasd");
        }

        public void GetVideoThumbnail(string path, string saveThumbnailTo, int seconds)
        {
            try
            {

                string strFolder = Common.ImageExtract;
                strFolder = strFolder + "//" + saveThumbnailTo.Replace(".mp4", "");
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }
                saveThumbnailTo = strFolder + "//" + saveThumbnailTo.Replace(".mp4", "");
                string parameters = " -i '" + path + "'  -vf fps=fps=1/0.3 -f image2 '" + saveThumbnailTo + "$%04d.jpg'";
                parameters = parameters.Replace("'", "\"");
                var processInfo = new System.Diagnostics.ProcessStartInfo();
                processInfo.FileName = "ffmpeg.exe";
                processInfo.Arguments = parameters;
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;

                using (var process = new System.Diagnostics.Process())
                {
                    process.StartInfo = processInfo;
                    process.Start();
                    process.WaitForExit();
                }

            }
            catch (Exception ex)
            {
                //Common.WriteLog("AddFile: " + ex.Message);
                Common.WriteEventLog("GetVideoThumbnail: " + ex.Message, "Error");
            }
        }



        public void GetEncodingVideo(string ipath,string opath)
        {
            try
            {

               
                Thread.Sleep(1000);
                bgWorker.ReportProgress(50);
                //string parameters = string.Format("-i {0} -r 24 {1}", ipath, opath);
                string parameters = " -i '" + ipath + "' -r 24 '" + opath + "'";
                parameters = parameters.Replace("'", "\"");
                var processInfo = new System.Diagnostics.ProcessStartInfo();
                processInfo.FileName = "ffmpeg.exe";
                processInfo.Arguments = parameters;
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;

                using (var process = new System.Diagnostics.Process())
                {
                    process.StartInfo = processInfo;
                    process.Start();
                    Thread.Sleep(1000);

                    bgWorker.ReportProgress(75);
                    TimeSpan time = process.TotalProcessorTime;
                    //process.TotalProcessorTime
                    
                    process.WaitForExit();

                    bgWorker.ReportProgress(90);
                }

            }
            catch (Exception ex)
            {
              
                Common.WriteEventLog("Encoding Video: " + ex.Message, "Error");
            }
        }
       





    }
}
