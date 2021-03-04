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
using System.IO;
using System.Xml;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for BatchUpload.xaml
    /// </summary>
    public partial class BatchUpload : Window
    {

        private string _vehiclekey = string.Empty;
        private string _vehicleIndex = string.Empty;
        private string _videoGUID = string.Empty;
       
        public string VideoFileName = string.Empty;

        public string RooftopKey = string.Empty;
        private Status _oStatus = null;

        

        public string SaveLocation
        {
            get;
            set;
        }
        public string FileName
        {
            get;
            set;
        }
        public bool Upload = false;



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
        private string _NumberOfImages;
        public string NumberOfImages
        {
            get
            {
                return _NumberOfImages;
            }
            set
            {
                _NumberOfImages = value;
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

        //private string _stock = string.Empty;
        //public string Stock
        //{
        //    get
        //    {
        //        return _stock;
        //    }
        //    set
        //    {
        //        _stock = value;
        //    }
        //}
        private string _VideoHeight = string.Empty;
        public string VideoHeight
        {
            get
            {
                return _VideoHeight;
            }
            set
            {
                _VideoHeight = value;
            }
        }

        private string _VideoWidth = string.Empty;
        public string VideoWidth
        {
            get
            {
                return _VideoWidth;
            }
            set
            {
                _VideoWidth = value;
            }
        }


        public class Selectedvideos
        {
            public string videopath { get; set; }
        }

        //private Status _oStatus = null;
        //public Status OStatus
        //{
        //    get
        //    {
        //        return _oStatus;
        //    }
        //    set
        //    {
        //        _oStatus = value;
        //    }
        //}

        private string _vGuid = null;
        public string videoGuid
        {
            get
            {
                return _vGuid;
            }
            set
            {
                _vGuid = value;
            }
        }


        string rooftop = string.Empty;
        Status oStatus = new Status();
        public List<Selectedvideos> SelectedvideosList { get; set; }
        //List<Selectedvideos> SelectedvideosList;
        public BatchUpload()
        {
            InitializeComponent();
            SelectedvideosList = new List<Selectedvideos>();

        }

        public BatchUpload(string rooftop)
        {
            InitializeComponent();
            SelectedvideosList = new List<Selectedvideos>();
            this.rooftop = rooftop;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAuthenticatedata(rooftop);



            //cmbRoofTop.Items.Add(new ComboBoxItem("1", "Stock#"));
            //cmbRoofTop.Items.Add(new ComboBoxItem("2", "VIN#"));
            cmbFileName.Text = ResourceTxt.SelectFileName;

            cmbNofImages.Text = ResourceTxt.SelectFileName;
            //comboBox1.Items.Add(new ComboBoxItem("1", "Local"));
            //comboBox1.Items.Add(new ComboBoxItem("2", "Remote Server"));

            CmbSaveLocation.Text = ResourceTxt.SelectFileName;

        }

        private void btnAddVideos_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                OpenFileDialog ofDialog = new OpenFileDialog();
                ofDialog.Title = ResourceTxt.selectVideoFile;
                ofDialog.InitializeLifetimeService();
                ofDialog.Multiselect = true;
                string strInitialDir = Common.GetVideoSettingInfo("InputVideoLocation");


                ofDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                ofDialog.Title = "Select Videos";
                ofDialog.RestoreDirectory = true;
                string filename = "";


                txtFileName.Text = ofDialog.FileName;


                DialogResult dr = ofDialog.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {


                    Selectedvideos objSelectVid = new Selectedvideos();
                    foreach (String file in ofDialog.FileNames)
                    {
                        try
                        {
                            filename += file + ",";
                            objSelectVid.videopath = file;
                            SelectedvideosList.Add(new Selectedvideos { videopath = file });

                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }

                filename = filename.TrimEnd(',');
                //lvSelectedVideos.Items.Clear();
                txtFileName.Text = filename;
                lvSelectedVideos.DataContext = SelectedvideosList;
                lvSelectedVideos.Items.Refresh();
                if (lvSelectedVideos.Items.Count > 0)
                {
                    GrpSelectedVideos.Visibility = Visibility.Visible;
                    btnUpload.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Common.WriteEventLog(ex.Message, "Error");
            }
        }

        public string GetVideoGuid(string rooftopKey)
        {
            XmlNodeReader reader = null;
            string videoGUID = null;

            try
            {
                //var encoding = new ASCIIEncoding();
                var postData = "ACTION=CREATEADVIDEO";

                postData += ("&rooftop_key=" + rooftopKey);
                postData += ("&INV_VID=" + 1);

                string serverData = Common.GetServerData(postData);

                if (string.IsNullOrEmpty(serverData))
                {
                    Common.WriteEventLog("GetINventoryVideoGuid: No Data found", "Warning");
                    //Common.WriteLog("GetVideoGUID: No Data found");
                    return null;
                }

                if (serverData.Equals("WebError"))
                {
                    //lblError.Content = ResourceTxt.GetServerData_WebError;
                    return null;
                }
                else if (serverData.Equals("Error"))
                {
                    //lblError.Content = ResourceTxt.GetServerData_Error;
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void cmbRoofTop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbFileName.Text == "--Select--")
                {
                    lblMessage.Content = "Select File Name";
                    lblMessage.Visibility = Visibility.Visible;
                    lblMessage.Foreground = Brushes.Red;
                    return;
                }

                SaveLocation = "";

                if (ChkExtractImages.IsChecked == true)
                {
                    if (cmbNofImages.Text == "--Select--")
                    {
                        lblMessage.Content = "Select number of Images";
                        lblMessage.Visibility = Visibility.Visible;
                        lblMessage.Foreground = Brushes.Red;
                        return;
                    }

                    if (CmbSaveLocation.Text == "--Select--")
                    {

                        lblMessage.Content = "Select Save Location";
                        lblMessage.Visibility = Visibility.Visible;
                        lblMessage.Foreground = Brushes.Red;
                        return;
                    }
                    SaveLocation = CmbSaveLocation.Text;
                }

                if (SelectedvideosList.Count == 0)
                {
                    lblMessage.Content = "Select Videos to upload";
                    lblMessage.Visibility = Visibility.Visible;
                    lblMessage.Foreground = Brushes.Red;
                    return;
                }

                lblMessage.Content = "";
                lblMessage.Visibility = Visibility.Hidden;
                Upload = true;

                //VideoGUID = GetVideoGuid(rooftop);
               // VehicleKey = "No Vehicle Key";
                VehicleKey = "";
                IsExtract = ChkExtractImages.IsChecked.ToString();
                VideoHeight = "360";
                VideoWidth = "640";
                VideoFileName = cmbFileName.Text;
               
                VideoType = "Batch";
                //VehicleKey = "No Vehicle Key";
                VehicleKey = "";
                NumberOfImages = cmbNofImages.Text;
                ImageObjects.ExtractStaus = true;
                // GenerateBatchVideosInfoXml();
                this.Close();



            }
            catch
            { };
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Selectedvideos itemSelected = (Selectedvideos)(sender as Hyperlink).DataContext;
            SelectedvideosList.Remove(itemSelected);
            lvSelectedVideos.DataContext = SelectedvideosList;
            lvSelectedVideos.Items.Refresh();

            txtFileName.Text = txtFileName.Text.Replace(",,", "") + ",";
            txtFileName.Text = txtFileName.Text.Replace(itemSelected.videopath + ",", "");



            if (SelectedvideosList.Count == 0 && lvSelectedVideos.Items.Count == 0)
            {
                //txtFileName.Text = "";
                GrpSelectedVideos.Visibility = Visibility.Hidden;
                btnUpload.Visibility = Visibility.Hidden;

            }

        }

        private void GetAuthenticatedata(string rooftop)
        {
            XmlReader reader = null;

            var postData = "ACTION=CREATEADVIDEO";

            postData += ("&rooftop_key=" + rooftop);

            XmlDocument xDoc = new XmlDocument();

            string serverData = Common.GetServerData(postData);
            xDoc.LoadXml(serverData);
            reader = new XmlNodeReader(xDoc);

            XmlNode node = xDoc.SelectSingleNode("search");

            UserAuthorization.ExtractAuth = node.Attributes["extractimages"].Value;
            UserAuthorization.RemoteUploadAuth = node.Attributes["remoteupload"].Value;
            UserAuthorization.InventoryAuthorization = node.Attributes["recordvideo"].Value;
            UserAuthorization.NonInventoryAuthorization = node.Attributes["recordadvideo"].Value;

            //xDoc.LoadXml(serverData);
            //GetRooftopValues(serverData);
        }


        

        private void ChkExtractImages_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbNofImages.Visibility = Visibility.Hidden;
            cmbNofImages.Text = ResourceTxt.SelectFileName;
        }

        private void ChkExtractImages_Click(object sender, RoutedEventArgs e)
        {
            if (UserAuthorization.ExtractAuth == "0")
            {
                ChkExtractImages.IsChecked = false;
                Alert1 objAlert = new Alert1("Extract Images feature is disabled. Please " + "\n" + "contact your Reseller to enable Extraction " + "\n" + "of Images");
                objAlert.ShowDialog();
            }
        }

        private void CmbSaveLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserAuthorization.RemoteUploadAuth=="0" && CmbSaveLocation.SelectedIndex == 2)
            {
                 
                Alert1 objAlert = new Alert1("Remote upload feature is disabled. Please " + "\n" + "contact your Reseller to enable Remote " + "\n" + "upload");
                objAlert.ShowDialog();
                CmbSaveLocation.SelectedIndex = 0;
            }
        }

        private void CmbSaveLocation_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
          
            if (CmbSaveLocation.Text == "Remote Server")
            {

              
            }
        }

        private void ChkExtractImages_Checked(object sender, RoutedEventArgs e)
        {
            cmbNofImages.Visibility = Visibility;
        }
    }
}
