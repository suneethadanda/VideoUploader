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
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using WpfVideoUploader;
using System.Threading;
using CLEyeMulticam;
//using WPFMediaKit;
using System.Configuration;
using System.Windows.Interop;


namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for VideoInfo.xaml
    /// </summary>
    public partial class VideoInfo : Window
    {

        // _camera;
        private string _vehiclekey = string.Empty;
        private string _vehicleIndex = string.Empty;
        private string _videoGUID = string.Empty;
        public string VideoFileName = string.Empty;
        private bool _isBrowserselected = false;
        public string RooftopKey = string.Empty;
        private Status _oStatus = null;

        string temp = string.Empty;
        private string _VideoType = "";
        public string VideoType
        {
            get
            {
                return _VideoType;

            }
            set
            {
                _VideoType = value;
            }
        }

        public string VehicleKey
        {
            get
            {
                return _vehiclekey;
            }
            set
            {
                _vehiclekey = value;
            }
        }



        public string VehicleIndex
        {
            get
            {
                return _vehicleIndex;
            }
            set
            {
                _vehicleIndex = value;
            }
        }

        public string VideoGUID
        {
            get
            {
                return _videoGUID;
            }
            set
            {
                _videoGUID = value;
            }
        }

        private string _videoTitle = string.Empty;
        public string VideoTitle
        {
            get
            {
                return _videoTitle;
            }
            set
            {
                _videoTitle = value;
            }
        }

        private string _videoDesc = string.Empty;
        public string VideoDescription
        {
            get
            {
                return _videoDesc;
            }
            set
            {
                _videoDesc = value;
            }
        }

        private string _isDefault = string.Empty;
        public string IsDefault
        {
            get
            {
                return _isDefault;
            }
            set
            {
                _isDefault = value;
            }
        }

        //Webcam.Webcam.CLEyeCameraDevice(

        private string _isExtract = string.Empty;
        public string IsExtract
        {
            get
            {
                return _isExtract;
            }
            set
            {
                _isExtract = value;
            }
        }
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

        private string _stock = string.Empty;
        public string Stock
        {
            get
            {
                return _stock;
            }
            set
            {
                _stock = value;
            }
        }
        public bool isBrowserselected
        {
            get
            {
                return _isBrowserselected;
            }
            set
            {
                _isBrowserselected = value;
            }
        }
        private bool _DefaultAllwayson = false;
        public bool DefaultAllwayson
        {
            get
            {
                return _DefaultAllwayson;
            }
            set
            {
                _DefaultAllwayson = value;
            }
        }

        //private string _InputFileLocation = string.Empty;
        //public string InputFileLocation
        //{
        //    get
        //    {
        //        return _InputFileLocation;
        //    }
        //    set
        //    {
        //        _InputFileLocation =value ;
        //    }
        //}

        public VideoInfo()
        {
            isBrowserselected = true;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window_Loaded);
            this.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
            this.DataContext = this;

            string defaulton = Common.GetDefaultCheckValue("DefaultVideoAllwaysOn");
            if (!string.IsNullOrEmpty(defaulton))
            {
                DefaultAllwayson = Convert.ToBoolean(defaulton);
                if (DefaultAllwayson)
                    chkDefaultVideo.IsChecked = true;
            }
        }




        private void btnAddVideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                OpenFileDialog ofDialog = new OpenFileDialog();
                ofDialog.Title = ResourceTxt.selectVideoFile;
                ofDialog.InitializeLifetimeService();
                string strInitialDir = Common.GetVideoSettingInfo("InputVideoLocation");

                if (!string.IsNullOrEmpty(strInitialDir))
                {
                    ofDialog.InitialDirectory = strInitialDir;
                    if (!File.Exists(strInitialDir) && !Directory.Exists(strInitialDir)) // checking the directory exist or not
                        ofDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //defualt path for to take videos
                }
                else
                {
                    ofDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //defualt path for to take videos
                    // System.Windows.Forms.MessageBox.Show("Input Video Location not correct.\nPlease check the Settings and correct the Path");
                }
                ofDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                ofDialog.RestoreDirectory = true;

                if (ofDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                txtFileName.Text = ofDialog.FileName;
                SaveInputLocationPath(ofDialog.FileName);
                btnNext.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                Common.WriteEventLog(ex.Message, "Error");
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                lblVideoTitleError.Visibility = Visibility.Visible;
                return;
            }
            else if (ChkExtractImages.IsChecked.ToString() == "True")
            {
                if (UserAuthorization.ExtractAuth == "0")
                {
                    lblMessage.Visibility = Visibility.Visible;
                    lblMessage.Content = new TextBlock() { Text = "The Extract Images is not enabled.Please call the Admin to enable it.if You want to Proceed Uncheck Extract Video", TextWrapping = TextWrapping.Wrap }; ;
                    lblVideoTitleError.Visibility = Visibility.Hidden;
                    return;
                }
            }
            else if (string.IsNullOrEmpty(txtFileName.Text))
            {
                lblMessage.Visibility = Visibility.Visible;
                lblMessage.Content = new TextBlock() { Text = "Please slect Video file", TextWrapping = TextWrapping.Wrap };
                lblVideoTitleError.Visibility = Visibility.Hidden;
                return;
            }

            lblError.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Hidden;
            VideoType = "Normal";
            VideoGUID = GetVideoGuid(RooftopKey, VehicleKey);

            if (string.IsNullOrEmpty(VideoGUID))
            {
                lblError.Visibility = Visibility.Visible;
                btnNext.Visibility = Visibility.Visible; ;

                return;
            }

            VideoFileName = txtFileName.Text;

            VideoTitle = txtTitle.Text;
            VideoDescription = txtDescription.Text;
            IsDefault = chkDefaultVideo.IsChecked.ToString();


            IsExtract = ChkExtractImages.IsChecked.ToString();

            GenerateVideoInfoXml();
            ImageObjects.ExtractStaus = true;

            this.Close();
        }




        public string GetVideoGuid(string rooftopKey, string vehicleKey)
        {
            XmlNodeReader reader = null;
            string videoGUID = null;

            try
            {
                //var encoding = new ASCIIEncoding();
                var postData = "ACTION=CREATEVIDEO";

                postData += ("&rooftop_key=" + rooftopKey);
                postData += ("&VEHKEY=" + vehicleKey);

                string serverData = Common.GetServerData(postData);

                if (string.IsNullOrEmpty(serverData))
                {
                    Common.WriteEventLog("GetVideoGUID: No Data found", "Warning");
                    //Common.WriteLog("GetVideoGUID: No Data found");
                    return null;
                }

                if (serverData.Equals("WebError"))
                {
                    lblError.Content = ResourceTxt.GetServerData_WebError;
                    return null;
                }
                else if (serverData.Equals("Error"))
                {
                    lblError.Content = ResourceTxt.GetServerData_Error;
                    return null;
                }

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(serverData);

                reader = new XmlNodeReader(xDoc);

                // Read all the data in the XML document and display it.
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {   // Keep track of the element that the user is on.
                        if (reader.Name == "guid")
                        {
                            reader.Read();
                            if (reader.NodeType == XmlNodeType.Text)
                            {
                                videoGUID = reader.Value;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("GetVideoGUID: " + ex.Message);
                Common.WriteEventLog("GetVideoGUID: " + ex.Message, "Error");
                videoGUID = null;
            }
            finally
            {
                // Do the necessary clean up.
                if (reader != null)
                    reader.Close();
            }

            return videoGUID;
        }

        public void GenerateVideoInfoXml()
        {





            try
            {
                string xmlPath = Common.VideoInfoFile;
                XmlDocument doc = null;
                XmlNode vehiclesNode = null;

                if (File.Exists(xmlPath))
                {
                    doc = new XmlDocument();
                    doc.Load(xmlPath);

                    vehiclesNode = doc.SelectSingleNode("Vehicles");
                }
                else
                {
                    doc = new XmlDocument();
                    XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    doc.AppendChild(docNode);

                    vehiclesNode = doc.CreateElement("Vehicles");
                    doc.AppendChild(vehiclesNode);
                }

                XmlNode vehicleNode = doc.CreateElement("Vehicle");
                XmlAttribute indexAttr = doc.CreateAttribute("index");
                indexAttr.Value = VehicleIndex;
                vehicleNode.Attributes.Append(indexAttr);

                XmlAttribute keyAttribute = doc.CreateAttribute("Key");
                keyAttribute.Value = VehicleKey;
                vehicleNode.Attributes.Append(keyAttribute);

                XmlAttribute titleAttribute = doc.CreateAttribute("Title");
                titleAttribute.Value = txtTitle.Text;
                vehicleNode.Attributes.Append(titleAttribute);

                XmlAttribute descAttribute = doc.CreateAttribute("Description");
                descAttribute.Value = txtDescription.Text;
                vehicleNode.Attributes.Append(descAttribute);

                XmlAttribute VideoGUIDArttr = doc.CreateAttribute("VideoGUID");
                VideoGUIDArttr.Value = VideoGUID;
                vehicleNode.Attributes.Append(VideoGUIDArttr);

                XmlAttribute VideoTypeArttr = doc.CreateAttribute("VideoType");
                VideoTypeArttr.Value = VideoType;
                vehicleNode.Attributes.Append(VideoTypeArttr);

                XmlAttribute defVideoAttr = doc.CreateAttribute("DefaultVideo");
                if (chkDefaultVideo.IsChecked == true)
                {
                    defVideoAttr.Value = "True";
                }
                else
                {
                    defVideoAttr.Value = "False";
                }
                vehicleNode.Attributes.Append(defVideoAttr);


                XmlAttribute IsExtractImages = doc.CreateAttribute("IsExtractImages");
                if (ChkExtractImages.IsChecked == true)
                {
                    IsExtractImages.Value = "True";
                }
                else
                {
                    IsExtractImages.Value = "False";
                }






                XmlAttribute StockAttr = doc.CreateAttribute("Stock");
                StockAttr.Value = Stock;
                vehicleNode.Attributes.Append(StockAttr);



                XmlAttribute InputVideoLocationAttr = doc.CreateAttribute("InputVideoLocation");
                InputVideoLocationAttr.Value = VideoFileName;
                vehicleNode.Attributes.Append(InputVideoLocationAttr);

                vehicleNode.Attributes.Append(IsExtractImages);

                vehiclesNode.AppendChild(vehicleNode);
                doc.Save(xmlPath);
            }
            catch (Exception ex)
            {
                //Common.WriteLog("Error occurred while Generating Vedio Information XML");
                //Common.WriteLog(ex.Message);
                Common.WriteEventLog("Error occurred while Generating Vedio Information XML: " + ex.Message, "Error");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// To save recent open folder path in setting file
        /// </summary>
        /// <param name="strPath"></param>
        private void SaveInputLocationPath(string strPath)
        {
            string strSettingFile = Common.SettingsFile;
            if (File.Exists(strSettingFile))
            {
                XmlDocument xmlDoc = null;
                xmlDoc = new XmlDocument();
                xmlDoc.Load(strSettingFile);
                XmlNode node;
                node = xmlDoc.DocumentElement;
                foreach (XmlNode node2 in node.ChildNodes)
                    if (node2.Name == "InputVideoLocation")
                        node2.InnerText = strPath;
                xmlDoc.Save(strSettingFile);
            }
        }

        /// <summary>
        /// allows only alphanumerics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp = "";
                if (!string.IsNullOrEmpty(txtTitle.Text))
                {
                    Regex objAlphaPattern = new Regex(@"[\s?a-zA-Z0-9-]$");
                    bool sts = objAlphaPattern.IsMatch(txtTitle.Text); ;

                    if (!sts)
                    {
                        txtTitle.Text = txtTitle.Text.Remove(txtTitle.Text.Length - 1, 1);
                    }

                    txtTitle.Text = txtTitle.Text;
                    txtTitle.CaretIndex = txtTitle.Text.Length;
                }
            }
            catch { }



        }

        private void chkDefaultVideo_Checked(object sender, RoutedEventArgs e)
        {
           

        }

        private void chkDefaultVideo_Unchecked(object sender, RoutedEventArgs e)
        {

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTitle.Focus();

        }

        private void btnRecordVideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    lblVideoTitleError.Visibility = Visibility.Visible;
                    return;
                }

                VideoGUID = GetVideoGuid(RooftopKey, VehicleKey);


                if (string.IsNullOrEmpty(VideoGUID))
                {
                    lblError.Visibility = Visibility.Visible;
                    //btnNext.Visibility = Visibility.Visible;

                    return;
                }

                lblVideoTitleError.Visibility = Visibility.Hidden;
                lblError.Visibility = Visibility.Hidden;
                btnNext.Visibility = Visibility.Hidden;

                //RecordVideoForm ObjRecord = new RecordVideoForm();
                //RecordVideoNew ObjRecord = new RecordVideoNew();
                EE4Test.frmEE4WebCam ObjRecord = new EE4Test.frmEE4WebCam();
                ObjRecord.ShowDialog();
                if (!string.IsNullOrEmpty(ObjRecord.fileName))
                {
                    VideoFileName = ObjRecord.fileName;
                    VideoType = "Normal";
                    //VideoFileName = txtFileName.Text;

                    VideoTitle = txtTitle.Text;
                    VideoDescription = txtDescription.Text;
                    IsDefault = chkDefaultVideo.IsChecked.ToString();


                    IsExtract = ChkExtractImages.IsChecked.ToString();

                    GenerateVideoInfoXml();
                    ImageObjects.ExtractStaus = true;

                    ObjRecord.Hide();
                    this.Close();
                }





            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Alert1 objAlert = new Alert1(ex.InnerException.Message);
                    objAlert.ShowDialog();
                }
                else
                {
                    Alert1 objAlert = new Alert1(ex.Message);
                    objAlert.ShowDialog();
                }
            }
        }


        private void rbRecord_Checked(object sender, RoutedEventArgs e)
        {
            CanvasRecord.Visibility = Visibility.Visible;
            CanvasBrowse.Visibility = Visibility.Hidden;

            //btnNext.Visibility = Visibility.Visible;
        }

        private void rbBrowse_Checked(object sender, RoutedEventArgs e)
        {
            CanvasRecord.Visibility = Visibility.Hidden;
            CanvasBrowse.Visibility = Visibility.Visible;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void ChkExtractImages_Checked(object sender, RoutedEventArgs e)
        {
            if (UserAuthorization.ExtractAuth == "0")
            {
                lblMessage.Visibility = Visibility.Visible;
                lblMessage.Content = new TextBlock() { Text = "The Extract Images is not enabled.Please call the Admin to enable it.if You want to Proceed Uncheck Extract Video", TextWrapping = TextWrapping.Wrap };                 

            }
            

        }
        private void ChkExtractImages_Unchecked(object sender, RoutedEventArgs e)
        {
            if (UserAuthorization.ExtractAuth != "0")
            {
                lblMessage.Visibility = Visibility.Hidden;              
                

            }


        }
        

    }
}
