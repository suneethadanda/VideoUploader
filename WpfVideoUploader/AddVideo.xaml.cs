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
using System.Threading;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using MultiSelectComboBox;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Configuration;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for AddVideo.xaml
    /// </summary>
    public partial class AddVideo : Window
    {
        private string _vehiclekey = string.Empty;
        private string _vehicleIndex = string.Empty;
        private string _videoGUID = string.Empty;
        private bool _isBrowserselected = false;
        private bool _isInventoryselected = false;
        public string VideoFileName = string.Empty;
        //Status oStatus = new Status();
        private ExtractImage _OextractImage = null;
        private string _RooftopKey = string.Empty;
        // private Status _oStatus = null;
        XDocument xDoc = null;
        string temp = string.Empty;
        // Login ObjLogin;
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

        string width = string.Empty;
        //string Index = string.Empty;
        string serverData = string.Empty;
        private string _CateGory = "";
        public string Category
        {
            get { return _CateGory; }
            set { _CateGory = value; }
        }

        private Status _oStatus = null;
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

        public ExtractImage OextractImage
        {
            get
            {
                return _OextractImage;
            }
            set
            {
                _OextractImage = value;
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

        public string RooftopKey
        {
            get
            {
                return _RooftopKey;
            }
            set
            {
                _RooftopKey = value;
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

        Dictionary<string, object> category = null;

        private string _Xflag = string.Empty;
        public string Xflag
        {
            get
            {
                return _Xflag;
            }
            set
            {
                _Xflag = value;
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


        private string _inventory = string.Empty;
        public string Inventory
        {
            get
            {
                return _inventory;
            }
            set
            {
                _inventory = value;
            }
        }


        private string _nonInventory = string.Empty;
        public string NonInventory
        {
            get
            {
                return _nonInventory;
            }
            set
            {
                _nonInventory = value;
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

        // Home OHome = new Home();
        private string _VIN = string.Empty;
        public string VIN
        {
            get
            {
                return _VIN;
            }
            set
            {
                _VIN = value;
            }
        }


        private string _VideoType = string.Empty;
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


        private Dictionary<string, object> _items;
        private Dictionary<string, object> _selectedItems;


        public Dictionary<string, object> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;

            }
        }

        public Dictionary<string, object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;

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
        public bool isInventoryselected
        {
            get
            {
                return _isInventoryselected;
            }
            set
            {
                _isInventoryselected = value;
            }
        }

        public AddVideo(string roofTop)
        {
            isBrowserselected = true;
            isInventoryselected = true;
            InitializeComponent();
            Items = new Dictionary<string, object>();

            this.RooftopKey = roofTop;

            Items.Add("Email", "Email");
            Items.Add("Inventory", "Inventory");
            Items.Add("Lead", "Lead");
            Items.Add("Parts", "Parts");
            Items.Add("Service", "Service");
            Items.Add("Testimonials", "Testimonials");
            
            SelectedItems = new Dictionary<string, object>();
            
           
            MultiSelectCombo.ItemsSource = Items;
            MultiSelectCombo.SelectedItems = SelectedItems;
            this.DataContext = this;

            string defaulton = Common.GetDefaultCheckValue("DefaultVideoAllwaysOn");
            if (!string.IsNullOrEmpty(defaulton))
            {
                DefaultAllwayson = Convert.ToBoolean(defaulton);
                if (DefaultAllwayson)
                    chkDefaultVideo.IsChecked = true;
            }

        }

     

        private void txtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp = "";
                if (!string.IsNullOrEmpty(txtTitle.Text))
                {
                     Regex objAlphaPattern = new Regex(@"[\s?a-zA-Z0-9-]$");
                    //Regex rgx = new Regex("[a-zA-Z0-9-\\s ]");
                    //Regex objAlphaPattern = new Regex(@"^[0 - 9A - Za - z] + $");
                    bool sts = objAlphaPattern.IsMatch(txtTitle.Text); 

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

        //public bool IsAlphaNumeric(String strToCheck)
        //{
        //    //Regex objAlphaNumericPattern = new Regex(@"[^a-zA-Z0-9\s]");
        //    //return !objAlphaNumericPattern.IsMatch(strToCheck);
        //    return true;
        //}

        private void chkDefaultVideo_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void chkDefaultVideo_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ChkExtractImages_Unchecked(object sender, RoutedEventArgs e)
        {
            if (UserAuthorization.ExtractAuth != "0")
            {
                lblMessage.Visibility = Visibility.Hidden;


            }


        }

        private void ChkExtractImages_Checked(object sender, RoutedEventArgs e)
        {
            if (UserAuthorization.ExtractAuth == "0")
            {
                lblMessage.Visibility = Visibility.Visible;
                lblMessage.Content = new TextBlock() { Text = "The Extract Images is not enabled.Please call the Admin to enable it.if You want to Proceed Uncheck Extract Video", TextWrapping = TextWrapping.Wrap };
                              
            }
            
           
        }
        
        
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                lblVideoTitleError.Content = "Please enter a video title";
                lblVideoTitleError.Visibility = Visibility.Visible;
                return;
            }
            else if (ChkExtractImages.IsChecked.ToString()=="True")
            {
                if (UserAuthorization.ExtractAuth == "0")
                {
                    lblMessage.Visibility = Visibility.Visible;
                    lblMessage.Content = new TextBlock() { Text = "The Extract Images is not enabled.Please call the Admin to enable it.if You want to Proceed Uncheck Extract Video", TextWrapping = TextWrapping.Wrap };
                    lblVideoTitleError.Visibility = Visibility.Hidden;
                    return;
                }
            }
            else if (string.IsNullOrEmpty(txtFileName.Text))
            {
                lblMessage.Visibility = Visibility.Visible;
                lblMessage.Content = new TextBlock() { Text = "Please browse Video file", TextWrapping = TextWrapping.Wrap };
                lblVideoTitleError.Visibility = Visibility.Hidden;
                return;
            }

            // btnNext.Visibility = Visibility.Hidden;
            VideoTitle = txtTitle.Text;
            VideoDescription = txtDescription.Text;
            VideoHeight = "360";
            VideoWidth = "960";
            Stock = txtStock.Text;


            //VehicleKey = Guid.NewGuid().ToString("D");

            if (rdBInventory.IsChecked == true)
            {

                IsDefault = chkDefaultVideo.IsChecked.ToString();
                IsExtract = ChkExtractImages.IsChecked.ToString();
                VIN = txtVIN.Text;
                VideoGUID = GetInventoryVideoGuid(RooftopKey);
                // GetVideoGuid(RooftopKey, VehicleKey);
                VideoType = "Inventory";

                if (string.IsNullOrEmpty(txtStock.Text) && string.IsNullOrEmpty(txtVIN.Text))
                {
                    lblVideoTitleError.Content = "Please enter either Stock or VIN";
                    lblVideoTitleError.Visibility = Visibility.Visible;
                    return;
                }       

            }
            VideoFileName = txtFileName.Text;


            if (rdBNonInventory.IsChecked == true)
            {
                category = MultiSelectCombo.SelectedItems;
                string[] keys = category.Keys.ToArray();

                string cate = "";
                foreach (string item in keys)
                {
                    cate += item + ",";
                }
                VideoType = "NonInventory";
                Category = cate.TrimEnd(',');
                VideoGUID = GetNonINventoryVideoGuid(RooftopKey);
                chkDefaultVideo.IsChecked = false;
                ChkExtractImages.IsChecked = false;
                IsDefault = "False";
                IsExtract = "False";
                if (string.IsNullOrEmpty(Category))
                {
                    lblVideoTitleError.Content = "Please Select Category";
                    lblVideoTitleError.Visibility = Visibility.Visible;
                    return;
                }

                if (!string.IsNullOrEmpty(txtStock.Text))
                {
                    Stock = txtStock.Text;
                }
                else
                {
                    Stock = "No Stock";
                }
            }






            GenerateAddVideoInfoXml();
            ImageObjects.ExtractStaus = true;

            this.Close();




        }

        public string GetNonINventoryVideoGuid(string rooftopKey)
        {
            XmlNodeReader reader = null;
            string videoGUID = null;

            try
            {
                //var encoding = new ASCIIEncoding();
                var postData = "ACTION=CREATEADVIDEO";

                postData += ("&rooftop_key=" + rooftopKey);
                postData += ("&NON_INV_VID=" + 1);

                string serverData = Common.GetServerData(postData);

                if (string.IsNullOrEmpty(serverData))
                {
                    Common.WriteEventLog("GetNonINventoryVideoGuid: No Data found", "Warning");
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


        public string GetInventoryVideoGuid(string rooftopKey)
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


        public void GenerateAddVideoInfoXml()
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


                XmlAttribute CategoryAttr = doc.CreateAttribute("Category");
                CategoryAttr.Value = Convert.ToString(Category);
                vehicleNode.Attributes.Append(CategoryAttr);

                XmlAttribute VINAttr = doc.CreateAttribute("VIN");
                VINAttr.Value = Convert.ToString(VIN);
                vehicleNode.Attributes.Append(VINAttr);


                XmlAttribute VideoTypeAttr = doc.CreateAttribute("VideoType");
                VideoTypeAttr.Value = Convert.ToString(VideoType);
                vehicleNode.Attributes.Append(VINAttr);



                if (rdBInventory.IsChecked == true)
                {
                    VideoTypeAttr.Value = "Inventory";
                }
                if (rdBNonInventory.IsChecked == true)
                {
                    VideoTypeAttr.Value = "NonInventory";
                }
                vehicleNode.Attributes.Append(VideoTypeAttr);




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




        private string GetVehicleKey(string piStock)
        {

            try
            {

                int pageCountinit = 1;
                var postData = "ACTION=VEHICLELIST";
                postData += ("&rooftop_key=" + RooftopKey);
                postData += ("&PAGE=" + pageCountinit.ToString());
                try
                {
                    if (!Common.IsConnectedToInternet())
                    {
                        lblMessage.Visibility = Visibility.Visible;
                        lblMessage.Content = ResourceTxt.NoInternet;

                        return "";
                    }

                    serverData = Common.GetServerData(postData);


                    //  string serverData = Common.GetServerData(postData);
                    xDoc = XDocument.Parse(serverData);


                    XmlNodeReader reader = null;
                    IEnumerable<XElement> childList = from book in xDoc.Root.Elements("vehicle")
                                                      where book.Attribute("stock").Value.ToLower().Contains(piStock.ToLower().ToString())
                                                      select book;
                    StringBuilder sb = new StringBuilder();


                    var myArray = childList.XmlSerializeAll().ToArray();

                    for (int i = 0; i < myArray.Count(); i++)
                    {
                        sb = sb.Append(myArray[i].ToString());
                    }
                    string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";

                    string strResult = sb.ToString();

                    strResult = "<Vehicles>" + strResult.ToString();
                    strResult += "</Vehicles>";
                    strResult = strResult.Replace(xml, "");


                    XmlDocument xDocNew = new XmlDocument();
                    xDocNew.LoadXml(strResult);

                    reader = new XmlNodeReader(xDocNew);
                    //while (reader.Read())
                    //{
                    //XmlNode node = xDocNew.ReadNode(reader);
                    //var attr = node.InnerXml;

                    foreach (XmlNode node in xDocNew.SelectNodes("Vehicles"))
                    {
                        VehicleKey = node.FirstChild.Attributes["vehiclekey"].Value;
                        VideoHeight = node.FirstChild.Attributes["h"].Value;
                        VideoWidth = node.FirstChild.Attributes["w"].Value;
                        VehicleIndex = node.FirstChild.Attributes["index"].Value;
                    }




                    return VehicleKey;

                }
                catch (Exception ex)
                {
                    //Common.WriteLog("FindRecord: " + ex.Message);
                    Common.WriteEventLog("FindRecord:" + ex.Message, "Error");
                    return "";

                }
            }

            catch { return ""; }
        }






        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //rdBInventory.IsChecked = true;
            GetAuthenticatedata(RooftopKey);
            
            MultiSelectCombo.Visibility = Visibility.Hidden;
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        //Login logObj = new Login();
        private void rdBNonInventory_Checked(object sender, RoutedEventArgs e)
        {

            if (UserAuthorization.NonInventoryAuthorization == "1")
            {
                chkDefaultVideo.Visibility = Visibility.Hidden;
                ChkExtractImages.Visibility = Visibility.Hidden;

                chkDefaultVideo.IsChecked = false;
                ChkExtractImages.IsChecked = false;

                txtVIN.Text = string.Empty;
                txtStock.Text = string.Empty;

                lblVIN.Visibility = Visibility.Hidden;
                txtVIN.Visibility = Visibility.Hidden;


                lblStock.Visibility = Visibility.Visible;
                txtStock.Visibility = Visibility.Visible;
                MultiSelectCombo.Visibility = Visibility.Visible;

                //cmbCategories.Visibility = Visibility.Visible;
                lblCategory.Visibility = Visibility.Visible;

                //txtFileName.Visibility = Visibility.Visible;
                //txtFileName.Text = string.Empty;

                //btnAddVideo.Visibility = Visibility.Visible;

                if (rdbBrowse.IsChecked == true)
                {
                    txtFileName.Visibility = Visibility.Visible;
                    btnAddVideo.Visibility = Visibility.Visible;
                    txtFileName.Text = string.Empty;
                }
                if (rdbRecord.IsChecked == true)
                {
                    btnRecord.Visibility = Visibility.Visible;
                }

                lblMessage.Content = string.Empty;
                //lblMessage.Foreground = Brushes.Red;
                lblMessage.Visibility = Visibility.Hidden;
            }
            else
            {
                lblMessage.Content = ResourceTxt.UnAuthorizedNonInvetory;
                lblMessage.Foreground = Brushes.Red;
                lblMessage.Visibility = Visibility.Visible;


                chkDefaultVideo.Visibility = Visibility.Hidden;
                ChkExtractImages.Visibility = Visibility.Hidden;

                lblCategory.Visibility = Visibility.Hidden;
                MultiSelectCombo.Visibility = Visibility.Hidden;
                txtStock.Text = string.Empty;
                lblStock.Visibility = Visibility.Hidden;
                txtStock.Visibility = Visibility.Hidden;

                lblVIN.Visibility = Visibility.Hidden;
                txtVIN.Visibility = Visibility.Hidden;

                txtFileName.Visibility = Visibility.Hidden;
                txtFileName.Text = string.Empty;

                btnAddVideo.Visibility = Visibility.Hidden;


                btnRecord.Visibility = Visibility.Hidden;
                btnNext.Visibility = Visibility.Hidden;
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
            UserAuthorization.InventoryAuthorization = node.Attributes["recordvideo"].Value;
            UserAuthorization.NonInventoryAuthorization = node.Attributes["recordadvideo"].Value;
            UserAuthorization.ExtractAuth = node.Attributes["extractimages"].Value;
            //xDoc.LoadXml(serverData);
            //GetRooftopValues(serverData);
        }



        private void rdBInventory_Checked(object sender, RoutedEventArgs e)
        {

            if (UserAuthorization.InventoryAuthorization == "1")
            {
                chkDefaultVideo.Visibility = Visibility.Visible;
                ChkExtractImages.Visibility = Visibility.Visible;

                lblStock.Visibility = Visibility.Visible;
                txtStock.Visibility = Visibility.Visible;
                txtStock.Text = string.Empty;
                lblVIN.Visibility = Visibility.Visible;
                txtVIN.Visibility = Visibility.Visible;
                if (rdbBrowse.IsChecked == true)
                {
                    txtFileName.Visibility = Visibility.Visible;
                    btnAddVideo.Visibility = Visibility.Visible;
                    txtFileName.Text = string.Empty;
                }
                if (rdbRecord.IsChecked == true)
                {
                    btnRecord.Visibility = Visibility.Visible;
                }
                // btnNext.Visibility = Visibility.Visible;

                MultiSelectCombo.Visibility = Visibility.Hidden;
                lblCategory.Visibility = Visibility.Hidden;


                lblMessage.Content = string.Empty;
                //lblMessage.Foreground = Brushes.Red;
                lblMessage.Visibility = Visibility.Hidden;
            }
            else
            {


                lblMessage.Content = ResourceTxt.UnAuthorizedInvetory;
                lblMessage.Foreground = Brushes.Red;
                lblMessage.Visibility = Visibility.Visible;




                chkDefaultVideo.Visibility = Visibility.Hidden;
                ChkExtractImages.Visibility = Visibility.Hidden;

                lblStock.Visibility = Visibility.Hidden;
                txtStock.Visibility = Visibility.Hidden;
                txtStock.Text = string.Empty;
                lblVIN.Visibility = Visibility.Hidden;
                txtVIN.Visibility = Visibility.Hidden;

                MultiSelectCombo.Visibility = Visibility.Hidden;

                lblCategory.Visibility = Visibility.Hidden;

                txtFileName.Visibility = Visibility.Hidden;
                txtFileName.Text = string.Empty;
                btnAddVideo.Visibility = Visibility.Hidden;

                btnNext.Visibility = Visibility.Hidden;

                btnRecord.Visibility = Visibility.Hidden;
            }




        }

        private void txtStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp = "";
                if (!string.IsNullOrEmpty(txtStock.Text))
                {
                    
                    Regex objAlphaPattern = new Regex(@"^[a-zA-Z0-9-]*$");
                    bool sts = objAlphaPattern.IsMatch(txtStock.Text); ;
                
                    if (!sts)
                    {
                        txtStock.Text = txtStock.Text.Remove(txtStock.Text.Length - 1, 1);
                    }

                    txtStock.Text = txtStock.Text.ToUpper();
                    txtStock.CaretIndex = txtStock.Text.Length;


                }


            }
            catch { }

        }

        private void txtVIN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp = "";
                if (!string.IsNullOrEmpty(txtVIN.Text))
                {
                    if (!Char.IsLetterOrDigit(txtVIN.Text[txtVIN.Text.Length - 1]))
                    {
                        //string temp = string.Empty;
                        for (int i = 0; i < txtVIN.Text.Length - 1; i++)
                        {
                            temp += txtVIN.Text[i];
                        }

                        txtVIN.Text = temp;
                    }

                    txtVIN.CaretIndex = txtVIN.Text.Length;
                }
            }
            catch { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        private void rdbRecord_Checked(object sender, RoutedEventArgs e)
        {
            rdBNonInventory.Visibility = Visibility.Visible;
            rdBInventory.Visibility = Visibility.Visible;

            if (rdBInventory.IsChecked == true || rdBNonInventory.IsChecked == true)
            {
                btnRecord.Visibility = Visibility.Visible;
            }

            txtFileName.Visibility = Visibility.Hidden;
            btnAddVideo.Visibility = Visibility.Hidden;
            txtFileName.Text = string.Empty;
        }

        private void rdbBrowse_Checked(object sender, RoutedEventArgs e)
        {
            rdBNonInventory.Visibility = Visibility.Visible;
            rdBInventory.Visibility = Visibility.Visible;

            if (rdBInventory.IsChecked == true || rdBNonInventory.IsChecked == true)
            {
                txtFileName.Visibility = Visibility.Visible;
                btnAddVideo.Visibility = Visibility.Visible;
                txtFileName.Text = string.Empty;
            }
            if(btnRecord!=null)
                btnRecord.Visibility = Visibility.Hidden;
        }

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    lblVideoTitleError.Content = "Please enter a video title";
                    lblVideoTitleError.Visibility = Visibility.Visible;
                    return;
                }


                // btnNext.Visibility = Visibility.Hidden;
                VideoTitle = txtTitle.Text;
                VideoDescription = txtDescription.Text;
                VideoHeight = "360";
                VideoWidth = "640";
                Stock = txtStock.Text;


                //VehicleKey = Guid.NewGuid().ToString("D");

                if (rdBInventory.IsChecked == true)
                {

                    IsDefault = chkDefaultVideo.IsChecked.ToString();
                    IsExtract = ChkExtractImages.IsChecked.ToString();
                    VIN = txtVIN.Text;
                    VideoGUID = GetInventoryVideoGuid(RooftopKey);
                    // GetVideoGuid(RooftopKey, VehicleKey);
                    VideoType = "Inventory";

                    if (string.IsNullOrEmpty(txtStock.Text))
                    {
                        lblVideoTitleError.Content = "Please enter a Stock";
                        lblVideoTitleError.Visibility = Visibility.Visible;
                        return;
                    }

                    if (string.IsNullOrEmpty(txtVIN.Text))
                    {
                        lblVideoTitleError.Content = "Please Enter VIN#";
                        lblVideoTitleError.Visibility = Visibility.Visible;
                        return;
                    }

                }



                if (rdBNonInventory.IsChecked == true)
                {
                    category = MultiSelectCombo.SelectedItems;
                    string[] keys = category.Keys.ToArray();

                    string cate = "";
                    foreach (string item in keys)
                    {
                        cate += item + ",";
                    }
                    VideoType = "NonInventory";
                    Category = cate.TrimEnd(',');
                    VideoGUID = GetNonINventoryVideoGuid(RooftopKey);
                    chkDefaultVideo.IsChecked = false;
                    ChkExtractImages.IsChecked = false;
                    IsDefault = "False";
                    IsExtract = "False";
                    if (string.IsNullOrEmpty(Category))
                    {
                        lblVideoTitleError.Content = "Please Select Category";
                        lblVideoTitleError.Visibility = Visibility.Visible;
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtStock.Text))
                    {
                        Stock = txtStock.Text;
                    }
                    else
                    {
                        Stock = "No Stock";
                    }
                }
                lblVideoTitleError.Visibility = Visibility.Hidden;

                //RecordVideoForm objRecord = new RecordVideoForm();
                // RecordVideoNew objRecord1 = new RecordVideoNew();
                EE4Test.frmEE4WebCam objRecord = new EE4Test.frmEE4WebCam();
                objRecord.ShowDialog();

                VideoFileName = objRecord.fileName;
                if (!string.IsNullOrEmpty(VideoFileName))
                {
                    GenerateAddVideoInfoXml();
                    ImageObjects.ExtractStaus = true;
                    
                    this.Hide();
                    this.Close();
                }
                else
                {
                    lblVideoTitleError.Content = "Please record video";
                    lblVideoTitleError.Visibility = Visibility.Visible;
                    return;
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

        private void txtTitle_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            




        }

        private void txtStock_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
         
        }

        private void txtVIN_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
         
        }




    }
}
