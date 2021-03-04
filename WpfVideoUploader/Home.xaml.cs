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
using CAVEditLib;
using System.Xml.Linq;

using System.IO;
using System.Data;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {

    //    //#####################################################################################
    //    #region Fields

    //    private RoofTop roofTop;
    //    private Dictionary<string, int> listMake;
    //    private Dictionary<string, string> listModel;
    //    private ICAVConverter mConverter = null;
    //    private string strMakeSelected = string.Empty;
    //    private string strModelSelected = string.Empty;
    //    private bool bgWorkerBusy;
    //    Status oStatus = new Status();
    //    //ExportImages oExport = new ExportImages();
    //    ExtractImage oExtractStatus = new ExtractImage();
    //    //BatchUpload objBatch = new BatchUpload();
    //    string serverData = string.Empty;
    //    bool isGridView = true;
    //    XDocument xDoc = null;
    //    VehicleList vehicleList;// = new VehicleList();
    //    private BackgroundWorker backgroundWorker;
    //    private BackgroundWorker listLoaderThread;

    //    #endregion

    //    //#####################################################################################

    //    #region Properties

    //    public RoofTop HRooftop
    //    {
    //        get { return roofTop; }
    //        set { roofTop = value; }
    //    }

    //    private string _loggedInUser = string.Empty;
    //    public string LoggedInUser
    //    {
    //        get
    //        {
    //            return _loggedInUser;
    //        }
    //        set
    //        {
    //            _loggedInUser = value;
    //        }
    //    }

    //    private string _roofTopKey = string.Empty;
    //    public string RooftopKey
    //    {
    //        get
    //        {
    //            return _roofTopKey;
    //        }
    //        set
    //        {
    //            _roofTopKey = value;
    //        }
    //    }

    //    private string _roofTopText = string.Empty;
    //    public string RooftopText
    //    {
    //        get
    //        {
    //            return _roofTopText;
    //        }
    //        set
    //        {
    //            _roofTopText = value;
    //        }
    //    }

    //    private string _Stock = string.Empty;
    //    public string Stock
    //    {
    //        get
    //        {
    //            return _Stock;
    //        }
    //        set
    //        {
    //            _Stock = value;
    //        }
    //    }



    //    #endregion

    //    //#####################################################################################

    //    #region Constructor
    //    public Home()
    //    {
    //        vehicleList = new VehicleList();
    //        InitializeComponent();
    //        dtGrid.CanUserAddRows = false;
    //        Common.OHome = this;
    //        backgroundWorker =
    //                        ((BackgroundWorker)this.FindResource("backgroundWorker"));
    //        listLoaderThread =
    //                        ((BackgroundWorker)this.FindResource("listLoaderThread"));
    //    }
    //    #endregion

    //    //#####################################################################################

    //    #region Events

    //    private void Window_Loaded(object sender, RoutedEventArgs e)
    //    {
    //        GetAuthenticatedata(RooftopKey);
    //        if (UserAuthorization.BatchUploadAuth == "0")
    //        {
    //            btnBatchUpload.Visibility = Visibility.Hidden;
    //        }
    //        PopulateVehicleMake(listMake);

    //        cmbMake.Text = ResourceTxt.VehicleMakeAll;
    //        cmbModel.Text = ResourceTxt.VehicleModelAll;
    //        lblLoggedUser.Content = LoggedInUser;
    //        dtGrid.ItemsSource = vehicleList;
    //        listiew.ItemsSource = vehicleList;
    //        dtGrid.Visibility = Visibility.Hidden;
            
    //    }
        

    //    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    //    {
    //        try
    //        {
    //            this.DragMove();
    //        }
    //        catch { }
    //    }

    //    private void btnClose_Click(object sender, RoutedEventArgs e)
    //    {
    //        Alert objalert = new Alert(ResourceTxt.sure);
    //        objalert.ShowDialog();
    //        if (objalert.close)
    //        {
    //            System.Windows.Application.Current.Shutdown();

    //        }


    //    }

    //    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    //    {
    //        this.WindowState = WindowState.Minimized;
    //    }

    //    private void Image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    //    {
    //        Image imgage = (Image)sender;
    //        popUpImag.Source = imgage.Source;
    //        imagePopUp.IsOpen = true;
    //        imagePopUp.StaysOpen = false;
    //    }

    //    private void Image_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    //    {
    //        imagePopUp.IsOpen = false;
    //        imagePopUp.StaysOpen = false;
    //    }

    //    private void btnToggle_Click(object sender, RoutedEventArgs e)
    //    {
    //        if (dtGrid.Visibility == Visibility.Visible)
    //        {
    //            listiew.Visibility = Visibility.Visible;
    //            dtGrid.Visibility = Visibility.Hidden;
    //            listiew.Items.Refresh();
    //            isGridView = false;
    //            return;
    //        }
    //        dtGrid.Visibility = Visibility.Visible;
    //        listiew.Visibility = Visibility.Hidden;
    //        dtGrid.Items.Refresh();
    //        isGridView = true;

    //    }

    //    private void chkPendingVideo_Click(object sender, RoutedEventArgs e)
    //    {
    //        chkPendingVideo.Refresh();
    //        vehicleList.Clear();
    //        listiew.Items.Refresh();
    //        dtGrid.Items.Refresh();
    //        PopulateVehicleData();
    //    }

    //    private void cmbMake_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //        try
    //        {
    //            if (bgWorkerBusy)
    //                return;

    //            xDoc = XDocument.Parse(serverData);

    //            if (cmbMake.Items.Count == 0)
    //                return;

    //            if (cmbMake.SelectedItem == null)
    //                return;

    //            strMakeSelected = cmbMake.SelectedItem.ToString();
    //            PopulateVehicleModel(strMakeSelected);

    //            string strResult = string.Empty;

    //            if (strMakeSelected != ResourceTxt.VehicleMakeAll)
    //            {
    //                var records = from book in xDoc.Root.Elements("vehicle")
    //                              where (string)book.Attribute("make") == strMakeSelected.ToString()
    //                              select book;

    //                if (chkPendingVideo.IsChecked == true)
    //                {
    //                    records = from book in xDoc.Root.Elements("vehicle")
    //                              where (string)book.Attribute("make") == strMakeSelected.ToString() &&
    //                                    (string)book.Attribute("Pending") == "1"
    //                              select book;
    //                }

    //                foreach (var record in records)
    //                {
    //                    strResult = strResult + record;
    //                }
    //            }
    //            else
    //            {
    //                var records = from book in xDoc.Root.Elements("vehicle")
    //                              select book;

    //                if (chkPendingVideo.IsChecked == true)
    //                {
    //                    records = from book in xDoc.Root.Elements("vehicle")
    //                              where (string)book.Attribute("Pending") == "1"
    //                              select book;
    //                }

    //                foreach (var record in records)
    //                {
    //                    strResult = strResult + record;
    //                }
    //            }

    //            if (!string.IsNullOrEmpty(strResult) && HRooftop.IsBackgroudBusy == false)
    //            {
    //                strResult = "<Vehicles>" + strResult;
    //                strResult += "</Vehicles>";
    //                listLoaderThread.RunWorkerAsync(strResult);
    //                //BindVehicleData(strResult);
    //            }
    //            else
    //            {
    //                listiew.Items.Clear();
    //                dtGrid.Items.Clear();
    //                listiew.Refresh();
    //                dtGrid.Refresh();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("Home.cmbMake_SelectionChanged:" + ex.Message);
    //            Common.WriteEventLog("Home.cmbMake_SelectionChanged: " + ex.Message, "Error");
    //        }
    //    }

    //    private void cmbModel_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //        try
    //        {
    //            xDoc = XDocument.Parse(serverData);

    //            if (cmbModel.SelectedItem == null && cmbModel.Text == ResourceTxt.VehicleModelAll)
    //                return;

    //            if (strModelSelected == cmbModel.SelectedItem.ToString())
    //                return;

    //            strModelSelected = cmbModel.SelectedItem.ToString();
    //            if (cmbModel.Items.Count == 0)
    //                return;

    //            string strMake = cmbMake.SelectedItem.ToString();
    //            string strResult = string.Empty;

    //            if (strModelSelected != ResourceTxt.VehicleModelAll)
    //            {
    //                var records = from book in xDoc.Root.Elements("vehicle")
    //                              where (string)book.Attribute("model") == strModelSelected.ToString()
    //                              select book;

    //                if (chkPendingVideo.HasContent)
    //                {
    //                    records = from book in xDoc.Root.Elements("vehicle")
    //                              where (string)book.Attribute("model") == strModelSelected.ToString() &&
    //                                    (string)book.Attribute("Pending") == "1"
    //                              select book;
    //                }

    //                foreach (var record in records)
    //                {
    //                    strResult = strResult + record;
    //                }
    //            }
    //            else
    //            {
    //                var records = from book in xDoc.Root.Elements("vehicle")
    //                              where (string)book.Attribute("make") == strMake.ToString()
    //                              select book;

    //                if (chkPendingVideo.HasContent)
    //                {
    //                    records = from book in xDoc.Root.Elements("vehicle")
    //                              where (string)book.Attribute("make") == strModelSelected.ToString() &&
    //                                    (string)book.Attribute("Pending") == "1"
    //                              select book;
    //                }

    //                foreach (var record in records)
    //                {
    //                    strResult = strResult + record;
    //                }
    //            }

    //            if (!string.IsNullOrEmpty(strResult))
    //            {
    //                strResult = "<Vehicles>" + strResult;
    //                strResult += "</Vehicles>";
    //                listLoaderThread.RunWorkerAsync(strResult);
    //                //BindVehicleData(strResult);
    //            }
    //            else
    //            {
    //                listiew.Items.Clear();
    //                dtGrid.Items.Clear();
    //                listiew.Refresh();
    //                dtGrid.Refresh();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("cmbModel_SelectedIndexChanged: " + ex.Message);
    //            Common.WriteEventLog("Home.cmbModel_SelectedIndexChanged: " + ex.Message, "Error");
    //        }

    //    }

    //    private void btnLogout_Click(object sender, RoutedEventArgs e)
    //    {
    //        try
    //        {

    //            Alert objalert = new Alert(ResourceTxt.SureLogout);
    //            objalert.ShowDialog();
    //            if (objalert.close)
    //            {
    //                foreach (Window frm in System.Windows.Application.Current.Windows)
    //                {
    //                    if (frm is Status)
    //                    {
    //                        frm.Close();
    //                        break;
    //                    }
    //                }
    //                Process.Start(Application.ResourceAssembly.Location);
    //                Application.Current.Shutdown();

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("btnLogout_Click: " + ex.Message);
    //            Common.WriteEventLog("btnLogout_Click: " + ex.Message, "Error");
    //        }
    //    }

    //    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    //    {
    //        //MessageBoxResult result = MessageBox.Show(ResourceTxt.sure, "Close", MessageBoxButton.YesNo, MessageBoxImage.Warning);
    //        //if (result == MessageBoxResult.Yes)
    //        //{
    //        try
    //        {
    //            if (mConverter != null)
    //            {
    //                mConverter.ClearTasks();
    //            }
    //            if (null != this.oStatus)
    //            {
    //                oStatus.Close();
    //            }

    //            foreach (Window frm in Application.Current.Windows)
    //            {
    //                if (frm is Status)
    //                {
    //                    frm.Close();
    //                    break;
    //                }
    //            }

    //            Application.Current.Shutdown();
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("Home_FormClosing: " + ex.Message);
    //            Common.WriteEventLog("Home_FormClosing: " + ex.Message, "Error");
    //        }
    //        //}
    //    }

    //    private void btnRefresh_Click(object sender, RoutedEventArgs e)
    //    {
    //        if (bgWorkerBusy == true)
    //            return;

    //        listiew.Visibility = Visibility.Hidden;
    //        dtGrid.Visibility = Visibility.Hidden;
    //        ReloadDataFromServer();
    //    }

    //    private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
    //    {
    //        Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;
    //        UploadStatusListQueue(selecedVehicle);
    //    }

    //    private void btnSearch_Click(object sender, RoutedEventArgs e)
    //    {
    //        vehicleList.Clear();
    //        listiew.Items.Refresh();
    //        dtGrid.Items.Refresh();
    //        if (!string.IsNullOrEmpty(txtStock.Text))
    //        {
    //            FindRecord(txtStock.Text);
    //            return;
    //        }
    //        PopulateVehicleData();

    //    }

    //    private void btnSettings_Click(object sender, RoutedEventArgs e)
    //    {

    //        LogDetails objLog = new LogDetails();

    //        objLog.ShowDialog();
    //        if (objLog.IsActive)
    //            objLog.Visibility = Visibility.Visible;


    //    }

    //    private void btnAboutUs_Click(object sender, RoutedEventArgs e)
    //    {
    //        AboutUs oAboutUs = new AboutUs();
    //        oAboutUs.ShowDialog();
    //    }

    //    private void btnQueue_Click(object sender, RoutedEventArgs e)
    //    {
    //        try
    //        {
    //            // Add pending uploadable files from the last cycle to Uploading queue
    //            oStatus.AddUploadingQueueFromFile();

    //            oStatus.Show();
    //            oStatus.Focus();
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("btnQueue_Click: " + ex.Message);
    //            Common.WriteEventLog("btnQueue_Click: " + ex.Message, "Error");
    //        }
    //    }

    //    private void btnReload_Click(object sender, RoutedEventArgs e)
    //    {
    //        txtStock.Text = string.Empty;
    //    }


    //    private void lnkText_MouseEnter(object sender, MouseEventArgs e)
    //    {
    //        this.Cursor = Cursors.Hand;
    //    }

    //    private void lnkText_MouseLeave(object sender, MouseEventArgs e)
    //    {
    //        this.Cursor = Cursors.Arrow;
    //    }

    //    #endregion

    //    //#####################################################################################

    //    #region PrivateMethods

    //    public void PopulateVehicleMake(Dictionary<string, int> listVehicleMake)
    //    {
    //        try
    //        {
    //            cmbMake.Items.Clear();
    //            cmbMake.Items.Add(ResourceTxt.VehicleMakeAll);

    //            foreach (KeyValuePair<string, int> kvp in listVehicleMake)
    //            {
    //                cmbMake.Items.Add(kvp.Key);
    //            }
    //            PopulateVehicleModel(ResourceTxt.VehicleMakeAll);
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("PopulateVehicleMake: " + ex.Message);
    //            Common.WriteEventLog("PopulateVehicleMake: " + ex.Message, "Error");
    //        }
    //    }

    //    public void PopulateVehicleModel(string makeValue)
    //    {
    //        try
    //        {
    //            cmbModel.Items.Clear();
    //            cmbModel.Items.Add(ResourceTxt.VehicleModelAll);
    //            foreach (KeyValuePair<string, string> kvp in listModel)
    //            {
    //                if (kvp.Value == makeValue)
    //                {
    //                    cmbModel.Items.Add(kvp.Key);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("PopulateVehicleModel: " + ex.Message);
    //            Common.WriteEventLog("PopulateVehicleModel: " + ex.Message, "Error");
    //        }
    //    }

    //    public void PopulateRooftop(Dictionary<string, string> listRooftop)
    //    {
    //        try
    //        {
    //            cmbRoofTop.Items.Add(ResourceTxt.RoofTopSelect);
    //            foreach (KeyValuePair<string, string> kvp in listRooftop)
    //            {
    //                WpfVideoUploader.ComboBoxItem cItem = new WpfVideoUploader.ComboBoxItem(kvp.Key, kvp.Value);
    //                cmbRoofTop.Items.Add(cItem);
    //            }
    //            cmbRoofTop.Text = RooftopText;
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("PopulateRoofTop:" + ex.Message);
    //            Common.WriteEventLog("PopulateRoofTop: " + ex.Message, "Error");
    //        }
    //    }

    //    public void GetVehicleDataFromServer()
    //    {
    //        //var encoding = new ASCIIEncoding();
    //        int pageCountinit = 1;
    //        var postData = "ACTION=VEHICLELIST";
    //        postData += ("&rooftop_key=" + RooftopKey);
    //        postData += ("&PAGE=" + pageCountinit.ToString());
    //        try
    //        {
    //            if (!Common.IsConnectedToInternet())
    //            {
    //                lblError.Visibility = Visibility.Visible;
    //                lblError.Content = ResourceTxt.NoInternet;

    //                return;
    //            }

    //            serverData = Common.GetServerData(postData);

    //            string totalpages = "totalpages=";

    //            int totalpgindex = serverData.IndexOf(totalpages);
    //            string pagecount = serverData.Substring(totalpgindex + totalpages.Length, 5);
    //            pagecount = pagecount.Replace('"', ' ');
    //            pagecount = pagecount.Replace('p', ' ');

    //            int noOfPages = Convert.ToInt32(pagecount.Trim());

    //            var postDataNew = "ACTION=VEHICLELIST";
    //            postDataNew += ("&rooftop_key=" + RooftopKey);
    //            serverData = string.Empty;
    //            for (int i = 1; i <= noOfPages; i++)
    //            {
    //                postDataNew += ("&PAGE=" + i.ToString());
    //                var serverDataNew = Common.GetServerData(postDataNew);
    //                if (string.IsNullOrEmpty(serverDataNew))
    //                {
    //                    continue;
    //                }
    //                int index = serverDataNew.IndexOf("<vehicle");
    //                if (index == -1)
    //                {
    //                    continue;
    //                }

    //                serverDataNew = serverDataNew.Remove(0, serverDataNew.IndexOf("<vehicle"));
    //                int x = serverDataNew.IndexOf("</search>");
    //                serverDataNew = serverDataNew.Remove(x, (serverDataNew.Length - x));
    //                serverData += serverDataNew;
    //            }

    //            if (string.IsNullOrEmpty(serverData))
    //            {
    //                return;
    //            }

    //            if (serverData.Equals("WebError"))
    //            {
    //                lblError.Visibility = Visibility.Visible;
    //                lblError.Content = ResourceTxt.GetServerData_WebError;
    //                return;
    //            }
    //            else if (serverData.Equals("Error") || serverData.Contains("Fatal error"))
    //            {
    //                lblError.Visibility = Visibility.Visible;
    //                lblError.Content = ResourceTxt.GetServerData_Error;
    //                return;
    //            }

    //            int dataIndex = serverData.IndexOf("<vehicle");
    //            if (dataIndex == -1)
    //            {
    //                lblNoOfRecordsValue.Content = ResourceTxt.NoRecordsFound;
    //                listiew.Visibility = Visibility.Hidden;
    //                dtGrid.Visibility = Visibility.Hidden;
    //                return;
    //            }

    //            serverData = "<Vehicles>" + serverData;
    //            serverData += "</Vehicles>";

    //            //PopulateVehicleData();
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("GetVehicleDataFromServer: " + ex.Message);
    //            Common.WriteEventLog("GetVehicleDataFromServer: " + ex.Message, "Error");
    //            return;
    //        }
    //    }

    //    public void PopulateVehicleData()
    //    {
    //        try
    //        {
    //            string strRecords = string.Empty;
    //            string strCurrentModel = string.Empty;
    //            string strCurrentMake = string.Empty;

    //            if (chkPendingVideo.IsChecked == true)
    //            {
    //                if (cmbModel.Items.Count == 0 && cmbMake.Items.Count == 0)
    //                {
    //                    strRecords = GetPendingRecordOnly(serverData);
    //                }
    //                else
    //                {
    //                    strCurrentModel = cmbModel.SelectedItem.ToString();
    //                    strCurrentMake = cmbMake.SelectedItem.ToString();

    //                    strRecords = XmlDataQueryHelper.GetPendingRecordsOnCheck(serverData, strCurrentModel, strCurrentMake, lblNoOfRecordsValue);
    //                }
    //            }
    //            else
    //            {
    //                if (cmbModel.Items.Count == 0 && cmbMake.Items.Count == 0)
    //                {
    //                    strRecords = serverData;
    //                }
    //                else if ((cmbModel.SelectedItem.ToString() != ResourceTxt.VehicleModelAll) || (cmbMake.SelectedItem.ToString() != ResourceTxt.VehicleMakeAll))
    //                {
    //                    strCurrentModel = cmbModel.SelectedItem.ToString();
    //                    strCurrentMake = cmbMake.SelectedItem.ToString();
    //                    strRecords = XmlDataQueryHelper.GetPendingRecordsOnUNCheck(serverData, strCurrentModel, strCurrentMake, lblNoOfRecordsValue);
    //                }
    //                else
    //                {
    //                    strRecords = serverData;
    //                }
    //            }

    //            if (string.IsNullOrEmpty(strRecords))
    //            {
    //                //Common.WriteLog("Record Not found, Check the XML data");
    //                Common.WriteEventLog("Record Not found, Check the XML data", "Warning");
    //                return;
    //            }
    //            listMake = new Dictionary<string, int>();
    //            listModel = new Dictionary<string, string>();

    //            listLoaderThread.RunWorkerAsync(strRecords);
    //            GetMakeModel();
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("PopulateVehicleData: " + ex.Message);
    //            Common.WriteEventLog("PopulateVehicleData" + ex.Message, "Error");
    //        }
    //    }

    //    private static string GetPendingRecordOnly(string serverXmlData)
    //    {
    //        try
    //        {
    //            XDocument xDocAll = XDocument.Parse(serverXmlData);

    //            var records = from vehicleData in xDocAll.Root.Elements("vehicle")
    //                          where (string)vehicleData.Attribute("Pending") == "1"
    //                          select vehicleData;

    //            XDocument xDocResults = null;
    //            string strResult = string.Empty;

    //            foreach (var record in records)
    //            {
    //                xDocResults = new XDocument(record);
    //                strResult += xDocResults.ToString();
    //            }

    //            strResult = "<Vehicles>" + strResult;
    //            strResult += "</Vehicles>";

    //            return strResult;
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("GetPendingRecordOnly: " + ex.Message);
    //            Common.WriteEventLog("GetPendingRecordOnly:" + ex.Message, "Error");
    //            return null;
    //        }
    //    }

    //    private void GetMakeModel()
    //    {
    //        try
    //        {
    //            XDocument doc = XDocument.Parse(serverData);
    //            var records = from book in doc.Root.Elements("vehicle")
    //                          select book;

    //            int indexMake = 0;

    //            foreach (var book in records)
    //            {
    //                // See if Dictionary contains this make
    //                string makeValue = book.Attribute("make").Value;
    //                if (!listMake.ContainsKey(makeValue) && !string.IsNullOrEmpty(makeValue))
    //                {
    //                    listMake.Add(makeValue, indexMake);
    //                    indexMake++;
    //                }

    //                // See if Dictionary contains this make
    //                string modelValue = book.Attribute("model").Value;
    //                if (!listModel.ContainsKey(modelValue) && !string.IsNullOrEmpty(modelValue))
    //                {
    //                    listModel.Add(modelValue, makeValue);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("GetMakeModel: " + ex.Message);
    //            Common.WriteEventLog("GetMakeModel:" + ex.Message, "Error");
    //        }
    //    }

    //    internal void UpdateStatus()
    //    {
    //        Vehicle selecedVehicle = (Vehicle)listiew.SelectedItem;
    //        UploadStatusListQueue(selecedVehicle);
    //    }

    //    private void UploadStatusListQueue(Vehicle selecedVehicle)
    //    {
    //        try
    //        {
    //            string strGUID = selecedVehicle.VehicleKey;
    //            string vehicleIndex = selecedVehicle.Index;

    //            VideoInfo vInfo = new VideoInfo();
    //            vInfo.RooftopKey = RooftopKey;
    //            vInfo.OStatus = oStatus;


    //            vInfo.Stock = selecedVehicle.Stock;


    //            vInfo.VehicleKey = strGUID;
    //            vInfo.VehicleIndex = vehicleIndex;
    //            vInfo.ShowDialog();

    //            string strInputFileName = vInfo.VideoFileName;
    //            string strOutputFileName = vInfo.VideoGUID;

    //            if (string.IsNullOrEmpty(strInputFileName))
    //                return;

    //            lblMessage.Visibility = Visibility.Visible;
    //            string strMessage = "Video file " + System.IO.Path.GetFileName(vInfo.VideoFileName) + "\n added to the queue.";
    //            strMessage += "\n";
    //            strMessage += ResourceTxt.VideoAddMessage;
    //            lblMessage.Content = strMessage;

    //            VehicleInfo oVehicleInfo = new VehicleInfo();
    //            oVehicleInfo.VehicleKey = strGUID;
    //            oVehicleInfo.InputFileName = strInputFileName;
    //            oVehicleInfo.OutputFileName = strOutputFileName;
    //            oVehicleInfo.TargetVideoHeight = selecedVehicle.Height;
    //            oVehicleInfo.TargetVideoWidth = selecedVehicle.Width;
    //            oVehicleInfo.RooftopKey = RooftopKey;
    //            oVehicleInfo.Stock = selecedVehicle.Stock;
    //            oVehicleInfo.VideoTitle = vInfo.VideoTitle;
    //            oVehicleInfo.Description = vInfo.VideoDescription;
    //            oVehicleInfo.IsDefault = vInfo.IsDefault;
    //            oVehicleInfo.IsExtract = vInfo.IsExtract;
    //            oVehicleInfo.NumberOfImages = vInfo.NumberOfImages;
    //            oVehicleInfo.SaveLocation = vInfo.saveLocation;
    //            // oVehicleInfo.Stock = vInfo.Stock;
    //            oVehicleInfo.VIN = strOutputFileName;
    //            oVehicleInfo.VideoType = vInfo.VideoType;
    //            oStatus.OVehicleInfo = oVehicleInfo;


    //            oStatus.OStatus = oStatus;

    //            if (strInputFileName != null && !string.IsNullOrEmpty(oVehicleInfo.InputFileName))
    //            {
    //                oStatus.VideoConverter();
    //            }

    //            //Show Status Queue for first time only
    //            if (!oStatus.IsVisible)
    //            {
    //                vInfo.OStatus.OHome = this;
    //                vInfo.OStatus.Show();
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("lnkAddVideo_LinkClicked: " + ex.Message);
    //            Common.WriteEventLog("lnkAddVideo_LinkClicked:" + ex.Message, "Error");
    //        }
    //    }

    //    private void FindRecord(string piStock)
    //    {

    //        try
    //        {

    //            IEnumerable<XElement> childList = from book in xDoc.Root.Elements("vehicle")
    //                                              where book.Attribute("stock").Value.ToLower().Contains(piStock.ToLower().ToString())
    //                                              select book;
    //            StringBuilder sb = new StringBuilder();


    //            var myArray = childList.XmlSerializeAll().ToArray();

    //            for (int i = 0; i < myArray.Count(); i++)
    //            {
    //                sb = sb.Append(myArray[i].ToString());
    //            }
    //            string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";

    //            string strResult = sb.ToString();

    //            strResult = "<Vehicles>" + strResult.ToString();
    //            strResult += "</Vehicles>";
    //            strResult = strResult.Replace(xml, "");

    //            listLoaderThread.RunWorkerAsync(strResult);
    //            //BindVehicleData(strResult);
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("FindRecord: " + ex.Message);
    //            Common.WriteEventLog("FindRecord:" + ex.Message, "Error");
    //        }
    //    }

    //    private void BindVehicleData(string pstrServerData)
    //    {
    //        try
    //        {
    //            Dispatcher.Invoke(new Action(() => Spinner.Visibility = Visibility.Visible));
    //            vehicleList.Clear();
    //            MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(pstrServerData));
    //            DataSet dataSet = new DataSet();
    //            dataSet.ReadXml(xmlStream);

    //            DataView dataView = new DataView(dataSet.Tables["Vehicle"]);
    //            if (dataView.Count > 0)
    //            {
    //                Dispatcher.Invoke(new Action(() => lblNoOfRecordsValue.Content = dataView.Count.ToString()));
    //                foreach (DataRow dataRow in dataSet.Tables["Vehicle"].Rows)
    //                {
    //                    Vehicle vehicle = new Vehicle();
    //                    vehicle.Height = Convert.ToString(dataRow["h"]);
    //                    vehicle.Image = Convert.ToString(dataRow["image"]);
    //                    vehicle.Index = Convert.ToString(dataRow["index"]);
    //                    vehicle.ListingType = Convert.ToString(dataRow["listing_type"]);
    //                    vehicle.Make = Convert.ToString(dataRow["make"]);
    //                    vehicle.Mileage = Convert.ToString(dataRow["mileage"]);
    //                    vehicle.Model = Convert.ToString(dataRow["model"]);
    //                    vehicle.Pending = Convert.ToString(dataRow["Pending"]);
    //                    vehicle.Stock = Convert.ToString(dataRow["stock"]);
    //                    vehicle.TotalViews = Convert.ToString(dataRow["totalviews"]);
    //                    vehicle.Trim = Convert.ToString(dataRow["trim"]);
    //                    vehicle.VehicleKey = Convert.ToString(dataRow["vehiclekey"]);
    //                    vehicle.Videos = "Add Video(" + Convert.ToString(dataRow["videos"]) + ")";
    //                    vehicle.Width = Convert.ToString(dataRow["w"]);
    //                    vehicle.Year = Convert.ToString(dataRow["year"]);
    //                    if (Convert.ToInt32(dataRow["Pending"]) == 1)
    //                        vehicle.VideoImage = @"/WpfVideoUploader;component/Images/video_red.png";
    //                    else
    //                        vehicle.VideoImage = @"/WpfVideoUploader;component/Images/video_green.png";
    //                    vehicleList.Add(vehicle);
    //                }
    //            }
    //            else
    //            {
    //                Dispatcher.Invoke(new Action(() => dtGrid.Visibility = Visibility.Hidden));
    //                Dispatcher.Invoke(new Action(() => lblNoOfRecordsValue.Content = ResourceTxt.NoRecordsFound));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("BindVehicleData: " + ex.Message);
    //            Common.WriteEventLog("BindVehicleData:" + ex.Message, "Error");
    //        }
    //    }

    //    private void ReloadDataFromServer()
    //    {
    //        try
    //        {
    //            WpfVideoUploader.ComboBoxItem cItem = (WpfVideoUploader.ComboBoxItem)cmbRoofTop.Items[cmbRoofTop.SelectedIndex];
    //            string strRoofTop = cItem.Value.ToString();
    //            lblNoOfRecords.Visibility = Visibility.Hidden;
    //            lblNoOfRecordsValue.Visibility = Visibility.Hidden;
    //            lblMessage.Visibility = Visibility.Hidden;
    //            RooftopKey = strRoofTop;
    //            Spinner.Visibility = Visibility.Visible;
    //            dtGrid.Visibility = Visibility.Hidden;
    //            listiew.Visibility = Visibility.Hidden;
    //            //code to start spinner
    //            backgroundWorker.RunWorkerAsync();
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("ReloadDataFromServer: " + ex.Message);
    //            Common.WriteEventLog("ReloadDataFromServer:" + ex.Message, "Error");
    //        }
    //    }

    //    /// <summary>
    //    /// Update the Pending attribute value for the matching vehiclekey if Pending is true
    //    /// </summary>
    //    /// <param name="VehicleKey"></param>
    //    public void UpdatePendingField(string vehicleKey)
    //    {
    //        try
    //        {


    //            XmlTextReader xmlTxtServerData = new XmlTextReader(new StringReader(serverData));
    //            XElement xServerElement = XElement.Load(xmlTxtServerData);
    //            XDocument xDocResults = null;

    //            var result = from videos in xServerElement.Descendants("vehicle") where videos.Attribute("vehiclekey").Value == vehicleKey && videos.Attribute("Pending").Value == "1" select videos;

    //            // Don't do anything if there is no matching vehiclekey with Pending
    //            if (result.Count() == 0)
    //                return;

    //            result = from videos in xServerElement.Descendants("vehicle") select videos;
    //            StringBuilder strResult = new StringBuilder();
    //            foreach (XElement xEle in result.ToList())
    //            {
    //                if (xEle.Attribute("vehiclekey").Value.Equals(vehicleKey))
    //                    xEle.Attribute("Pending").Value = "0";

    //                xDocResults = new XDocument(xEle);
    //                strResult.Append(xDocResults.ToString());
    //            }

    //            strResult.Insert(0, "<Vehicles>");
    //            strResult.Append("</Vehicles>");

    //            serverData = string.Empty;
    //            serverData = strResult.ToString();

    //            PopulateVehicleData();
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("UpdatePendingFlag: " + ex.Message);
    //            Common.WriteEventLog("UpdatePendingFlag:" + ex.Message, "Error");
    //        }
    //    }

    //    #endregion

    //    private void cmbRoofTop_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //        try
    //        {
    //            ComboBoxItem cItem = (ComboBoxItem)cmbRoofTop.Items[cmbRoofTop.SelectedIndex];
    //            string strRoofTop = cItem.Value.ToString();

    //            if (strRoofTop != RooftopKey && bgWorkerBusy == false)
    //            {
    //                ReloadDataFromServer();
    //                GetAuthenticatedata(strRoofTop);
    //                if (UserAuthorization.BatchUploadAuth == "0")
    //                {
    //                    btnBatchUpload.Visibility = Visibility.Hidden;
    //                }
    //                else
    //                {
    //                    btnBatchUpload.Visibility = Visibility.Visible;
    //                }
    //                //PopulateVehicleData();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("cmbRoofTop_SelectedIndexChanged: " + ex.Message);
    //            Common.WriteEventLog("cmbRoofTop_SelectedIndexChanged:" + ex.Message, "Error");
    //        }
    //    }

    //    private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    //    {
    //        //Common.WriteLog("INFO: BackgroundWorker_DoWork started");
    //        Common.WriteEventLog("BackgroundWorker_DoWork started:", "Information");
    //        bgWorkerBusy = true;
    //        GetVehicleDataFromServer();
    //        //Common.WriteLog("INFO: BackgroundWorker_DoWork End");
    //        Common.WriteEventLog("BackgroundWorker_DoWork End:", "Information");
    //    }

    //    private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    //    {
    //        //Common.WriteLog("INFO: BackgroundWorker_RunWorkerCompleted started");
    //        Common.WriteEventLog("BackgroundWorker_RunWorkerCompleted started:", "Information");
    //        PopulateVehicleMake(listMake);
    //        cmbMake.Text = ResourceTxt.VehicleMakeAll;
    //        cmbModel.Text = ResourceTxt.VehicleModelAll;
    //        lblNoOfRecords.Visibility = Visibility.Visible;
    //        lblNoOfRecordsValue.Visibility = Visibility.Visible;
    //        Spinner.Visibility = Visibility.Hidden;
    //        PopulateVehicleData();
    //        bgWorkerBusy = false;
    //        //Common.WriteLog("INFO: BackgroundWorker_RunWorkerCompleted End");
    //        Common.WriteEventLog("BackgroundWorker_RunWorkerCompleted End:", "Information");
    //    }

    //    private void listLoaderThread_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    //    {
    //        //Common.WriteLog("INFO: listLoaderThread_DoWork Started");
    //        Common.WriteEventLog("listLoaderThread_DoWork Started:", "Information");
    //        string serverData = (string)e.Argument;
    //        BindVehicleData(serverData);
    //        //Common.WriteLog("INFO: listLoaderThread_DoWork End");
    //        Common.WriteEventLog("listLoaderThread_DoWork End:", "Information");
    //    }

    //    private void listLoaderThread_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    //    {
    //        //Common.WriteLog("INFO: listLoaderThread_RunWorkerCompleted Started");
    //        Common.WriteEventLog("listLoaderThread_RunWorkerCompleted Started:", "Information");
    //        Spinner.Visibility = Visibility.Hidden;
    //        if (isGridView)
    //        {
    //            dtGrid.Items.Refresh();
    //            dtGrid.Visibility = Visibility.Visible;
    //            listiew.Visibility = Visibility.Hidden;
    //        }
    //        else
    //        {
    //            dtGrid.Visibility = Visibility.Hidden;
    //            listiew.Visibility = Visibility.Visible;
    //            listiew.Items.Refresh();
    //        }
    //        //cmbModel.Text = ResourceTxt.VehicleModelAll;
    //        //Common.WriteLog("INFO: listLoaderThread_RunWorkerCompleted Completed");
    //        Common.WriteEventLog("listLoaderThread_RunWorkerCompleted Completed:", "Information");
    //    }

    //    private void DataGridTemplateColumn_MouseUp(object sender, MouseButtonEventArgs e)
    //    {
    //        //  MessageBox.Show("asdsasadsad");
    //        Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;
    //        UploadStatusListQueue(selecedVehicle);
    //    }

    //    private void btnImageQueue_Click(object sender, RoutedEventArgs e)
    //    {

    //        try
    //        {

    //            oExtractStatus.Show();
    //            oExtractStatus.Focus();
    //        }
    //        catch
    //        {
    //            //oExtractStatus.Show();
    //        }

    //        // var exists = Application.Current.Windows.Cast<ViewImages>().SingleOrDefault(w=>w.IsActive);
    //    }



    //    private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    //    {
    //        Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;
    //        UploadStatusListQueue(selecedVehicle);
    //    }

    //    private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    //    {
    //        Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;
    //        UploadStatusListQueue(selecedVehicle);
    //    }

    //    private void Hyperlink_Click(object sender, RoutedEventArgs e)
    //    {
    //        //Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;
    //        //UploadStatusListQueue(selecedVehicle);
    //    }

    //    private void btnAddVideo_Click(object sender, RoutedEventArgs e)
    //    {
    //        try
    //        {
    //            //Vehicle selectedVehicle = new Vehicle();
    //            UpdatestatuslistQueue();
    //        }
    //        catch { }






    //    }



    //    private void UpdatestatuslistQueue()
    //    {
    //        AddVideo AddvInfo = new AddVideo(RooftopKey);

    //        AddvInfo.OStatus = oStatus;



    //        AddvInfo.ShowDialog();

    //        string strInputFileName = AddvInfo.VideoFileName;
    //        string strOutputFileName = AddvInfo.VideoGUID;

    //        if (string.IsNullOrEmpty(strInputFileName))
    //            return;

    //        lblMessage.Visibility = Visibility.Visible;
    //        string strMessage = "Video file " + System.IO.Path.GetFileName(AddvInfo.VideoFileName) + "\n added to the queue.";
    //        strMessage += "\n";
    //        strMessage += ResourceTxt.VideoAddMessage;
    //        lblMessage.Content = strMessage;

    //        VehicleInfo oVehicleInfo = new VehicleInfo();
    //        oVehicleInfo.VehicleKey = AddvInfo.VehicleKey;
    //        oVehicleInfo.InputFileName = strInputFileName;
    //        oVehicleInfo.OutputFileName = strOutputFileName;

    //        oVehicleInfo.TargetVideoHeight = AddvInfo.VideoHeight;
    //        oVehicleInfo.TargetVideoWidth = AddvInfo.VideoWidth;

    //        oVehicleInfo.RooftopKey = RooftopKey;

    //        if (!string.IsNullOrEmpty(AddvInfo.Stock))
    //        {
    //            oVehicleInfo.Stock = AddvInfo.Stock;
    //        }
    //        else
    //        {
    //            oVehicleInfo.Stock = "No Stock";
    //        }

    //        oVehicleInfo.VideoTitle = AddvInfo.VideoTitle;
    //        oVehicleInfo.Description = AddvInfo.VideoDescription;
    //        oVehicleInfo.IsDefault = AddvInfo.IsDefault;
    //        oVehicleInfo.IsExtract = AddvInfo.IsExtract;
    //        oVehicleInfo.VIN = AddvInfo.VIN;
    //        oVehicleInfo.SaveLocation = AddvInfo.saveLocation;
    //        oVehicleInfo.Categories = AddvInfo.Category;
    //        oVehicleInfo.NumberOfImages = AddvInfo.NumberOfImages;
    //        oVehicleInfo.VideoType = AddvInfo.VideoType; 
    //        //string duration = oStatus.OStatus.GetVideoDuration(oVehicleInfo.InputFileName);
    //        oStatus.OVehicleInfo = oVehicleInfo;

    //        oStatus.OStatus = oStatus;

    //        if (strInputFileName != null && !string.IsNullOrEmpty(oVehicleInfo.InputFileName))
    //        {
    //            oStatus.VideoConverter();
    //        }

    //        //Show Status Queue for first time only
    //        if (!oStatus.IsVisible)
    //        {
    //            AddvInfo.OStatus.OHome = this;
    //            AddvInfo.OStatus.Show();

    //        }
    //    }

    //    private void btnExportQueue_Click(object sender, RoutedEventArgs e)
    //    {
    //        //oExport.AddCurrentExportingQueueFromFile();

    //        //oExport.Show();
    //        //oExport.Focus();
    //    }

    //    private void GetAuthenticatedata(string rooftop)
    //    {
    //        XmlReader reader = null;

    //        var postData = "ACTION=CREATEADVIDEO";

    //        postData += ("&rooftop_key=" + rooftop);

    //        XmlDocument xDoc = new XmlDocument();

    //        string serverData = Common.GetServerData(postData);
    //        xDoc.LoadXml(serverData);
    //        reader = new XmlNodeReader(xDoc);

    //        XmlNode node = xDoc.SelectSingleNode("search");

    //        UserAuthorization.ExtractAuth = node.Attributes["extractimages"].Value;
    //        UserAuthorization.RemoteUploadAuth = node.Attributes["remoteupload"].Value;
    //        UserAuthorization.InventoryAuthorization = node.Attributes["recordvideo"].Value;
    //        UserAuthorization.NonInventoryAuthorization = node.Attributes["recordadvideo"].Value;
    //        UserAuthorization.BatchUploadAuth=node.Attributes["batchupload"].Value;
    //        //xDoc.LoadXml(serverData);
    //        //GetRooftopValues(serverData);
    //    }

    //    private void btnBatchUpload_Click(object sender, RoutedEventArgs e)
    //    {

    //        Vehicle selectedVehicle = new Vehicle();
    //        BatchUploadQueue(selectedVehicle);

           
    //    }


    //    private void BatchUploadQueue(Vehicle selectedVehicle)
    //    {
    //        BatchUpload objBatch = new BatchUpload(RooftopKey);
    //        objBatch.OStatus = oStatus;
    //        objBatch.ShowDialog();

    //        if (objBatch.SaveLocation == "Select" || objBatch.FileName == "Select" || !objBatch.Upload)
    //            return;


    //        lblMessage.Visibility = Visibility.Visible;
    //        string strMessage = "Batch Videos added to the queue.";
    //        strMessage += "\n";
    //        strMessage += ResourceTxt.VideoAddMessage;
    //        lblMessage.Content = strMessage;



    //        for (int Vcount = 0; Vcount < objBatch.SelectedvideosList.Count(); Vcount++)
    //        {
    //            VehicleInfo oVehicleInfo = new VehicleInfo();
    //            oVehicleInfo.VehicleKey = objBatch.VehicleKey; ;
    //            oVehicleInfo.InputFileName = objBatch.VideoFileName;
    //            oVehicleInfo.OutputFileName = objBatch.GetVideoGuid(RooftopKey);
    //            oVehicleInfo.TargetVideoHeight = objBatch.VideoHeight;
    //            oVehicleInfo.TargetVideoWidth = objBatch.VideoWidth;
    //            oVehicleInfo.RooftopKey = RooftopKey;
    //            oVehicleInfo.Stock = "No Stock#(Batch Upload)";
    //            oVehicleInfo.VehicleKey = objBatch.VehicleKey;
    //            oVehicleInfo.NumberOfImages = objBatch.NumberOfImages;
    //            oVehicleInfo.InputFileName = objBatch.SelectedvideosList[Vcount].videopath;
    //            oVehicleInfo.VideoTitle = "";
    //            oVehicleInfo.Description = "";
    //            oVehicleInfo.IsDefault = "";

    //            oVehicleInfo.IsExtract = objBatch.IsExtract;
    //            oVehicleInfo.SaveLocation = objBatch.SaveLocation;
    //            // oVehicleInfo.Stock = vInfo.Stock;
    //            oVehicleInfo.VIN = "No VIN#(Batch Upload)";
    //            oVehicleInfo.VideoType = objBatch.VideoType;
    //            // oStatus.OVehicleInfo = new VehicleInfo();
    //            oStatus.OVehicleInfo = oVehicleInfo;

    //            oStatus.OStatus = oStatus;

    //            if (oVehicleInfo.InputFileName != null && !string.IsNullOrEmpty(oVehicleInfo.InputFileName))
    //            {
    //                oStatus.VideoConverter();
    //            }

    //            GenerateBatchVideosInfoXml(oVehicleInfo);
    //            //Show Status Queue for first time only
    //            if (!oStatus.IsVisible)
    //            {
    //                objBatch.OStatus.OHome = this;
    //                objBatch.OStatus.Show();
    //            }


    //        }


    //    }


    //    private void GenerateBatchVideosInfoXml(VehicleInfo oVehicleInfo)
    //    {

    //        try
    //        {
    //            string xmlPath = Common.VideoInfoFile;
    //            XmlDocument doc = null;
    //            XmlNode vehiclesNode = null;

    //            if (File.Exists(xmlPath))
    //            {
    //                doc = new XmlDocument();
    //                doc.Load(xmlPath);

    //                vehiclesNode = doc.SelectSingleNode("Vehicles");
    //            }
    //            else
    //            {
    //                doc = new XmlDocument();
    //                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
    //                doc.AppendChild(docNode);

    //                vehiclesNode = doc.CreateElement("Vehicles");
    //                doc.AppendChild(vehiclesNode);
    //            }

    //            XmlNode vehicleNode = doc.CreateElement("Vehicle");
    //            XmlAttribute indexAttr = doc.CreateAttribute("index");
    //            indexAttr.Value = "";
    //            vehicleNode.Attributes.Append(indexAttr);

    //            XmlAttribute keyAttribute = doc.CreateAttribute("Key");
    //            //keyAttribute.Value = "No Vehicle Key";
    //            keyAttribute.Value = "";
    //            vehicleNode.Attributes.Append(keyAttribute);

    //            XmlAttribute titleAttribute = doc.CreateAttribute("Title");
    //            titleAttribute.Value = "";
    //            vehicleNode.Attributes.Append(titleAttribute);

    //            XmlAttribute descAttribute = doc.CreateAttribute("Description");
    //            descAttribute.Value = "";
    //            vehicleNode.Attributes.Append(descAttribute);



    //            XmlAttribute VideoGUIDArttr = doc.CreateAttribute("VideoGUID");
    //            VideoGUIDArttr.Value = oVehicleInfo.OutputFileName;
    //            vehicleNode.Attributes.Append(VideoGUIDArttr);

    //            XmlAttribute VideoTypeArttr = doc.CreateAttribute("VideoType");
    //            VideoTypeArttr.Value = oVehicleInfo.VideoType;
    //            vehicleNode.Attributes.Append(VideoTypeArttr);

    //            XmlAttribute defVideoAttr = doc.CreateAttribute("DefaultVideo");
    //            defVideoAttr.Value = "";
    //            vehicleNode.Attributes.Append(defVideoAttr);

    //            XmlAttribute NofImagesArttr = doc.CreateAttribute("NumberOfImages");
    //            NofImagesArttr.Value = oVehicleInfo.NumberOfImages;
    //            vehicleNode.Attributes.Append(NofImagesArttr);


    //            XmlAttribute saveLocationArttr = doc.CreateAttribute("saveLocation");
    //            saveLocationArttr.Value = oVehicleInfo.SaveLocation;
    //            vehicleNode.Attributes.Append(saveLocationArttr);


    //            XmlAttribute IsExtractImages = doc.CreateAttribute("IsExtractImages");
    //            if (oVehicleInfo.IsExtract == "True")
    //            {
    //                IsExtractImages.Value = "True";
    //            }
    //            else
    //            {
    //                IsExtractImages.Value = "False";
    //            }


    //            XmlAttribute StockAttr = doc.CreateAttribute("Stock");
    //            StockAttr.Value = "No Stock#(Batch Upload)";
    //            vehicleNode.Attributes.Append(StockAttr);



    //            XmlAttribute InputVideoLocationAttr = doc.CreateAttribute("InputVideoLocation");
    //            InputVideoLocationAttr.Value = oVehicleInfo.InputFileName;
    //            vehicleNode.Attributes.Append(InputVideoLocationAttr);

    //            vehicleNode.Attributes.Append(IsExtractImages);

    //            vehiclesNode.AppendChild(vehicleNode);
    //            doc.Save(xmlPath);
    //        }
    //        catch (Exception ex)
    //        {
    //            //Common.WriteLog("Error occurred while Generating Vedio Information XML");
    //            //Common.WriteLog(ex.Message);
    //            Common.WriteEventLog("Error occurred while Generating Vedio Information XML: " + ex.Message, "Error");
    //        }
    //    }



    //    //#####################################################################################
    //}


        //#####################################################################################
        #region Fields

        private RoofTop roofTop;
        private Dictionary<string, int> listMake;
        private Dictionary<string, string> listModel;
        private ICAVConverter mConverter = null;
        private string strMakeSelected = string.Empty;
        private string strModelSelected = string.Empty;
        private bool bgWorkerBusy;
        Status oStatus = new Status();
        ExtractImage oExtractStatus = new ExtractImage();
        string serverData = string.Empty;
        bool isGridView = true;
        XDocument xDoc = null;
        VehicleList vehicleList;// = new VehicleList();
        private BackgroundWorker backgroundWorker;
        private BackgroundWorker listLoaderThread;

        #endregion

        //#####################################################################################

        #region Properties

        public RoofTop HRooftop
        {
            get { return roofTop; }
            set { roofTop = value; }
        }

        private string _loggedInUser = string.Empty;
        public string LoggedInUser
        {
            get
            {
                return _loggedInUser;
            }
            set
            {
                _loggedInUser = value;
            }
        }

        private string _roofTopKey = string.Empty;
        public string RooftopKey
        {
            get
            {
                return _roofTopKey;
            }
            set
            {
                _roofTopKey = value;
            }
        }

        private string _roofTopText = string.Empty;
        public string RooftopText
        {
            get
            {
                return _roofTopText;
            }
            set
            {
                _roofTopText = value;
            }
        }

        private string _Stock = string.Empty;
        public string Stock
        {
            get
            {
                return _Stock;
            }
            set
            {
                _Stock = value;
            }
        }



        #endregion

        //#####################################################################################

        #region Constructor
        public Home()
        {
            vehicleList = new VehicleList();
            InitializeComponent();
            dtGrid.CanUserAddRows = false;
            Common.OHome = this;
            backgroundWorker =
                            ((BackgroundWorker)this.FindResource("backgroundWorker"));
            listLoaderThread =
                            ((BackgroundWorker)this.FindResource("listLoaderThread"));
        }
        #endregion

        //#####################################################################################

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //PopulateVehicleMake(listMake);

            //cmbMake.Text = ResourceTxt.VehicleMakeAll;
            //cmbModel.Text = ResourceTxt.VehicleModelAll;
            //lblLoggedUser.Content = LoggedInUser;
            //dtGrid.ItemsSource = vehicleList;
            //listiew.ItemsSource = vehicleList;
            //dtGrid.Visibility = Visibility.Hidden;


            GetAuthenticatedata(RooftopKey);
            if (UserAuthorization.BatchUploadAuth == "0")
            {
                btnBatchUpload.Visibility = Visibility.Hidden;
            }
            PopulateVehicleMake(listMake);

            cmbMake.Text = ResourceTxt.VehicleMakeAll;
            cmbModel.Text = ResourceTxt.VehicleModelAll;
            lblLoggedUser.Content = LoggedInUser;
            dtGrid.ItemsSource = vehicleList;
            listiew.ItemsSource = vehicleList;
            dtGrid.Visibility = Visibility.Hidden;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Alert objalert = new Alert(ResourceTxt.sure);
            objalert.ShowDialog();
            if (objalert.close)
            {
                System.Windows.Application.Current.Shutdown();

            }
        }
        private void btnAppsettings_Click(object sender, RoutedEventArgs e)
        {
            AppSettings objappsettings = new AppSettings();
            objappsettings.ShowDialog();
            if (objappsettings.close)
            {
                objappsettings.Close();

            }
        }
        

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Image imgage = (Image)sender;
            popUpImag.Source = imgage.Source;
            imagePopUp.IsOpen = true;
            imagePopUp.StaysOpen = false;
        }

        private void BatchUploadQueue(Vehicle selectedVehicle)
        {
            BatchUpload objBatch = new BatchUpload(RooftopKey);
            objBatch.OStatus = oStatus;
            objBatch.ShowDialog();

            if (objBatch.SaveLocation == "Select" || objBatch.FileName == "Select" || !objBatch.Upload)
                return;


            lblMessage.Visibility = Visibility.Visible;
            string strMessage = "Batch Videos added to the queue.";
            strMessage += "\n";
            strMessage += ResourceTxt.VideoAddMessage;
            lblMessage.Content = strMessage;



            for (int Vcount = 0; Vcount < objBatch.SelectedvideosList.Count(); Vcount++)
            {
                VehicleInfo oVehicleInfo = new VehicleInfo();
                oVehicleInfo.VehicleKey = objBatch.VehicleKey; ;
                oVehicleInfo.InputFileName = objBatch.VideoFileName;
                oVehicleInfo.OutputFileName = objBatch.GetVideoGuid(RooftopKey);
                oVehicleInfo.TargetVideoHeight = objBatch.VideoHeight;
                oVehicleInfo.TargetVideoWidth = objBatch.VideoWidth;
                oVehicleInfo.RooftopKey = RooftopKey;
                oVehicleInfo.Stock = "No Stock#(Batch Upload)";
                oVehicleInfo.VehicleKey = objBatch.VehicleKey;
                oVehicleInfo.NumberOfImages = objBatch.NumberOfImages;
                oVehicleInfo.InputFileName = objBatch.SelectedvideosList[Vcount].videopath;
                oVehicleInfo.VideoTitle = "";
                oVehicleInfo.Description = "";
                oVehicleInfo.IsDefault = "";

                oVehicleInfo.IsExtract = objBatch.IsExtract;
                oVehicleInfo.SaveLocation = objBatch.SaveLocation;
                // oVehicleInfo.Stock = vInfo.Stock;
                oVehicleInfo.VIN = "No VIN#(Batch Upload)";
                oVehicleInfo.VideoType = objBatch.VideoType;
                // oStatus.OVehicleInfo = new VehicleInfo();
                oStatus.OVehicleInfo = oVehicleInfo;

                oStatus.OStatus = oStatus;

                if (oVehicleInfo.InputFileName != null && !string.IsNullOrEmpty(oVehicleInfo.InputFileName))
                {
                    oStatus.VideoConverter();
                }

                GenerateBatchVideosInfoXml(oVehicleInfo);
                //Show Status Queue for first time only
                if (!oStatus.IsVisible)
                {
                    objBatch.OStatus.OHome = this;
                    objBatch.OStatus.Show();
                }


            }


        }

        private void Image_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imagePopUp.IsOpen = false;
            imagePopUp.StaysOpen = false;
        }

        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {
            if (dtGrid.Visibility == Visibility.Visible)
            {
                listiew.Visibility = Visibility.Visible;
                dtGrid.Visibility = Visibility.Hidden;
                listiew.Items.Refresh();
                isGridView = false;
                return;
            }
            dtGrid.Visibility = Visibility.Visible;
            listiew.Visibility = Visibility.Hidden;
            dtGrid.Items.Refresh();
            isGridView = true;

        }

        private void chkPendingVideo_Click(object sender, RoutedEventArgs e)
        {
            chkPendingVideo.Refresh();
            vehicleList.Clear();
            listiew.Items.Refresh();
            dtGrid.Items.Refresh();
            PopulateVehicleData();
        }
        private void GenerateBatchVideosInfoXml(VehicleInfo oVehicleInfo)
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
                indexAttr.Value = "";
                vehicleNode.Attributes.Append(indexAttr);

                XmlAttribute keyAttribute = doc.CreateAttribute("Key");
                //keyAttribute.Value = "No Vehicle Key";
                keyAttribute.Value = "";
                vehicleNode.Attributes.Append(keyAttribute);

                XmlAttribute titleAttribute = doc.CreateAttribute("Title");
                titleAttribute.Value = "";
                vehicleNode.Attributes.Append(titleAttribute);

                XmlAttribute descAttribute = doc.CreateAttribute("Description");
                descAttribute.Value = "";
                vehicleNode.Attributes.Append(descAttribute);



                XmlAttribute VideoGUIDArttr = doc.CreateAttribute("VideoGUID");
                VideoGUIDArttr.Value = oVehicleInfo.OutputFileName;
                vehicleNode.Attributes.Append(VideoGUIDArttr);

                XmlAttribute VideoTypeArttr = doc.CreateAttribute("VideoType");
                VideoTypeArttr.Value = oVehicleInfo.VideoType;
                vehicleNode.Attributes.Append(VideoTypeArttr);

                XmlAttribute defVideoAttr = doc.CreateAttribute("DefaultVideo");
                defVideoAttr.Value = "";
                vehicleNode.Attributes.Append(defVideoAttr);

                XmlAttribute NofImagesArttr = doc.CreateAttribute("NumberOfImages");
                NofImagesArttr.Value = oVehicleInfo.NumberOfImages;
                vehicleNode.Attributes.Append(NofImagesArttr);


                XmlAttribute saveLocationArttr = doc.CreateAttribute("saveLocation");
                saveLocationArttr.Value = oVehicleInfo.SaveLocation;
                vehicleNode.Attributes.Append(saveLocationArttr);


                XmlAttribute IsExtractImages = doc.CreateAttribute("IsExtractImages");
                if (oVehicleInfo.IsExtract == "True")
                {
                    IsExtractImages.Value = "True";
                }
                else
                {
                    IsExtractImages.Value = "False";
                }


                XmlAttribute StockAttr = doc.CreateAttribute("Stock");
                StockAttr.Value = "No Stock#(Batch Upload)";
                vehicleNode.Attributes.Append(StockAttr);



                XmlAttribute InputVideoLocationAttr = doc.CreateAttribute("InputVideoLocation");
                InputVideoLocationAttr.Value = oVehicleInfo.InputFileName;
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
            UserAuthorization.BatchUploadAuth = node.Attributes["batchupload"].Value;
            //xDoc.LoadXml(serverData);
            //GetRooftopValues(serverData);
        }

        private void cmbMake_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (bgWorkerBusy)
                    return;

                xDoc = XDocument.Parse(serverData);

                if (cmbMake.Items.Count == 0)
                    return;

                if (cmbMake.SelectedItem == null)
                    return;

                strMakeSelected = cmbMake.SelectedItem.ToString();
                PopulateVehicleModel(strMakeSelected);

                string strResult = string.Empty;

                if (strMakeSelected != ResourceTxt.VehicleMakeAll)
                {
                    var records = from book in xDoc.Root.Elements("vehicle")
                                  where (string)book.Attribute("make") == strMakeSelected.ToString()
                                  select book;

                    if (chkPendingVideo.IsChecked == true)
                    {
                        records = from book in xDoc.Root.Elements("vehicle")
                                  where (string)book.Attribute("make") == strMakeSelected.ToString() &&
                                        (string)book.Attribute("Pending") == "1"
                                  select book;
                    }

                    foreach (var record in records)
                    {
                        strResult = strResult + record;
                    }
                }
                else
                {
                    var records = from book in xDoc.Root.Elements("vehicle")
                                  select book;

                    if (chkPendingVideo.IsChecked == true)
                    {
                        records = from book in xDoc.Root.Elements("vehicle")
                                  where (string)book.Attribute("Pending") == "1"
                                  select book;
                    }

                    foreach (var record in records)
                    {
                        strResult = strResult + record;
                    }
                }

                if (!string.IsNullOrEmpty(strResult) && HRooftop.IsBackgroudBusy == false)
                {
                    strResult = "<Vehicles>" + strResult;
                    strResult += "</Vehicles>";
                    listLoaderThread.RunWorkerAsync(strResult);
                    //BindVehicleData(strResult);
                }
                else
                {
                    listiew.Items.Clear();
                    dtGrid.Items.Clear();
                    listiew.Refresh();
                    dtGrid.Refresh();
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("Home.cmbMake_SelectionChanged:" + ex.Message);
                Common.WriteEventLog("Home.cmbMake_SelectionChanged: " + ex.Message, "Error");
            }
        }

        private void cmbModel_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                xDoc = XDocument.Parse(serverData);

                if (cmbModel.SelectedItem == null && cmbModel.Text == ResourceTxt.VehicleModelAll)
                    return;

                if (strModelSelected == cmbModel.SelectedItem.ToString())
                    return;

                strModelSelected = cmbModel.SelectedItem.ToString();
                if (cmbModel.Items.Count == 0)
                    return;

                string strMake = cmbMake.SelectedItem.ToString();
                string strResult = string.Empty;

                if (strModelSelected != ResourceTxt.VehicleModelAll)
                {
                    var records = from book in xDoc.Root.Elements("vehicle")
                                  where (string)book.Attribute("model") == strModelSelected.ToString()
                                  select book;

                    if (chkPendingVideo.HasContent)
                    {
                        records = from book in xDoc.Root.Elements("vehicle")
                                  where (string)book.Attribute("model") == strModelSelected.ToString() &&
                                        (string)book.Attribute("Pending") == "1"
                                  select book;
                    }

                    foreach (var record in records)
                    {
                        strResult = strResult + record;
                    }
                }
                else
                {
                    var records = from book in xDoc.Root.Elements("vehicle")
                                  where (string)book.Attribute("make") == strMake.ToString()
                                  select book;

                    if (chkPendingVideo.HasContent)
                    {
                        records = from book in xDoc.Root.Elements("vehicle")
                                  where (string)book.Attribute("make") == strModelSelected.ToString() &&
                                        (string)book.Attribute("Pending") == "1"
                                  select book;
                    }

                    foreach (var record in records)
                    {
                        strResult = strResult + record;
                    }
                }

                if (!string.IsNullOrEmpty(strResult))
                {
                    strResult = "<Vehicles>" + strResult;
                    strResult += "</Vehicles>";
                    listLoaderThread.RunWorkerAsync(strResult);
                    //BindVehicleData(strResult);
                }
                else
                {
                    listiew.Items.Clear();
                    dtGrid.Items.Clear();
                    listiew.Refresh();
                    dtGrid.Refresh();
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("cmbModel_SelectedIndexChanged: " + ex.Message);
                Common.WriteEventLog("Home.cmbModel_SelectedIndexChanged: " + ex.Message, "Error");
            }

        }

        private void btnBatchUpload_Click(object sender, RoutedEventArgs e)
        {

            Vehicle selectedVehicle = new Vehicle();
            BatchUploadQueue(selectedVehicle);


        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MessageBoxResult result = MessageBox.Show("are you sure to LogOut?", "LogOut", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (Window frm in System.Windows.Application.Current.Windows)
                    {
                        if (frm is Status)
                        {
                            frm.Close();
                            break;
                        }
                    }
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("btnLogout_Click: " + ex.Message);
                Common.WriteEventLog("btnLogout_Click: " + ex.Message, "Error");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show(ResourceTxt.sure, "Close", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            //if (result == MessageBoxResult.Yes)
            //{
            try
            {
                if (mConverter != null)
                {
                    mConverter.ClearTasks();
                }
                if (null != this.oStatus)
                {
                    oStatus.Close();
                }

                foreach (Window frm in Application.Current.Windows)
                {
                    if (frm is Status)
                    {
                        frm.Close();
                        break;
                    }
                }

                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                //Common.WriteLog("Home_FormClosing: " + ex.Message);
                Common.WriteEventLog("Home_FormClosing: " + ex.Message, "Error");
            }
            //}
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (bgWorkerBusy == true)
                return;

            listiew.Visibility = Visibility.Hidden;
            dtGrid.Visibility = Visibility.Hidden;
            ReloadDataFromServer();
        }

        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;
            UploadStatusListQueue(selecedVehicle);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            vehicleList.Clear();
            listiew.Items.Refresh();
            dtGrid.Items.Refresh();
            if (!string.IsNullOrEmpty(txtStock.Text))
            {
                FindRecord(txtStock.Text);
                return;
            }
            PopulateVehicleData();

        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {

            LogDetails objLog = new LogDetails();

            objLog.ShowDialog();
            if (objLog.IsActive)
                objLog.Visibility = Visibility.Visible;


        }

        private void btnAboutUs_Click(object sender, RoutedEventArgs e)
        {
            AboutUs oAboutUs = new AboutUs();
            oAboutUs.ShowDialog();
        }

        private void btnQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Add pending uploadable files from the last cycle to Uploading queue
                oStatus.AddUploadingQueueFromFile();

                oStatus.Show();
                oStatus.Focus();
            }
            catch (Exception ex)
            {
                //Common.WriteLog("btnQueue_Click: " + ex.Message);
                Common.WriteEventLog("btnQueue_Click: " + ex.Message, "Error");
            }
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            txtStock.Text = string.Empty;
        }


        private void lnkText_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void lnkText_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        #endregion

        //#####################################################################################

        #region PrivateMethods

        public void PopulateVehicleMake(Dictionary<string, int> listVehicleMake)
        {
            try
            {
                cmbMake.Items.Clear();
                cmbMake.Items.Add(ResourceTxt.VehicleMakeAll);

                foreach (KeyValuePair<string, int> kvp in listVehicleMake)
                {
                    cmbMake.Items.Add(kvp.Key);
                }
                PopulateVehicleModel(ResourceTxt.VehicleMakeAll);
            }
            catch (Exception ex)
            {
                //Common.WriteLog("PopulateVehicleMake: " + ex.Message);
                Common.WriteEventLog("PopulateVehicleMake: " + ex.Message, "Error");
            }
        }

        public void PopulateVehicleModel(string makeValue)
        {
            try
            {
                cmbModel.Items.Clear();
                cmbModel.Items.Add(ResourceTxt.VehicleModelAll);
                foreach (KeyValuePair<string, string> kvp in listModel)
                {
                    if (kvp.Value == makeValue)
                    {
                        cmbModel.Items.Add(kvp.Key);
                    }
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("PopulateVehicleModel: " + ex.Message);
                Common.WriteEventLog("PopulateVehicleModel: " + ex.Message, "Error");
            }
        }

        public void PopulateRooftop(Dictionary<string, string> listRooftop)
        {
            try
            {
                cmbRoofTop.Items.Add(ResourceTxt.RoofTopSelect);
                foreach (KeyValuePair<string, string> kvp in listRooftop)
                {
                    WpfVideoUploader.ComboBoxItem cItem = new WpfVideoUploader.ComboBoxItem(kvp.Key, kvp.Value);
                    cmbRoofTop.Items.Add(cItem);
                }
                cmbRoofTop.Text = RooftopText;
            }
            catch (Exception ex)
            {
                //Common.WriteLog("PopulateRoofTop:" + ex.Message);
                Common.WriteEventLog("PopulateRoofTop: " + ex.Message, "Error");
            }
        }

        public void GetVehicleDataFromServer()
        {
            //var encoding = new ASCIIEncoding();
            int pageCountinit = 1;
            var postData = "ACTION=VEHICLELIST";
            postData += ("&rooftop_key=" + RooftopKey);
            postData += ("&PAGE=" + pageCountinit.ToString());
            try
            {
                if (!Common.IsConnectedToInternet())
                {
                    lblError.Visibility = Visibility.Visible;
                    lblError.Content = ResourceTxt.NoInternet;

                    return;
                }

                serverData = Common.GetServerData(postData);

                string totalpages = "totalpages=";

                int totalpgindex = serverData.IndexOf(totalpages);
                string pagecount = serverData.Substring(totalpgindex + totalpages.Length, 5);
                pagecount = pagecount.Replace('"', ' ');
                pagecount = pagecount.Replace('p', ' ');

                int noOfPages = Convert.ToInt32(pagecount.Trim());

                var postDataNew = "ACTION=VEHICLELIST";
                postDataNew += ("&rooftop_key=" + RooftopKey);
                serverData = string.Empty;
                for (int i = 1; i <= noOfPages; i++)
                {
                    postDataNew += ("&PAGE=" + i.ToString());
                    var serverDataNew = Common.GetServerData(postDataNew);
                    if (string.IsNullOrEmpty(serverDataNew))
                    {
                        continue;
                    }
                    int index = serverDataNew.IndexOf("<vehicle");
                    if (index == -1)
                    {
                        continue;
                    }

                    serverDataNew = serverDataNew.Remove(0, serverDataNew.IndexOf("<vehicle"));
                    int x = serverDataNew.IndexOf("</search>");
                    serverDataNew = serverDataNew.Remove(x, (serverDataNew.Length - x));
                    serverData += serverDataNew;
                }

                if (string.IsNullOrEmpty(serverData))
                {
                    return;
                }

                if (serverData.Equals("WebError"))
                {
                    lblError.Visibility = Visibility.Visible;
                    lblError.Content = ResourceTxt.GetServerData_WebError;
                    return;
                }
                else if (serverData.Equals("Error") || serverData.Contains("Fatal error"))
                {
                    lblError.Visibility = Visibility.Visible;
                    lblError.Content = ResourceTxt.GetServerData_Error;
                    return;
                }

                int dataIndex = serverData.IndexOf("<vehicle");
                if (dataIndex == -1)
                {
                    lblNoOfRecordsValue.Content = ResourceTxt.NoRecordsFound;
                    listiew.Visibility = Visibility.Hidden;
                    dtGrid.Visibility = Visibility.Hidden;
                    return;
                }

                serverData = "<Vehicles>" + serverData;
                serverData += "</Vehicles>";

                //PopulateVehicleData();
            }
            catch (Exception ex)
            {
                //Common.WriteLog("GetVehicleDataFromServer: " + ex.Message);
                Common.WriteEventLog("GetVehicleDataFromServer: " + ex.Message, "Error");
                return;
            }
        }

        public void PopulateVehicleData()
        {
            try
            {
                string strRecords = string.Empty;
                string strCurrentModel = string.Empty;
                string strCurrentMake = string.Empty;

                if (chkPendingVideo.IsChecked == true)
                {
                    if (cmbModel.Items.Count == 0 && cmbMake.Items.Count == 0)
                    {
                        strRecords = GetPendingRecordOnly(serverData);
                    }
                    else
                    {
                        strCurrentModel = cmbModel.SelectedItem.ToString();
                        strCurrentMake = cmbMake.SelectedItem.ToString();

                        strRecords = XmlDataQueryHelper.GetPendingRecordsOnCheck(serverData, strCurrentModel, strCurrentMake, lblNoOfRecordsValue);
                    }
                }
                else
                {
                    if (cmbModel.Items.Count == 0 && cmbMake.Items.Count == 0)
                    {
                        strRecords = serverData;
                    }
                    else if ((cmbModel.SelectedItem.ToString() != ResourceTxt.VehicleModelAll) || (cmbMake.SelectedItem.ToString() != ResourceTxt.VehicleMakeAll))
                    {
                        strCurrentModel = cmbModel.SelectedItem.ToString();
                        strCurrentMake = cmbMake.SelectedItem.ToString();
                        strRecords = XmlDataQueryHelper.GetPendingRecordsOnUNCheck(serverData, strCurrentModel, strCurrentMake, lblNoOfRecordsValue);
                    }
                    else
                    {
                        strRecords = serverData;
                    }
                }

                if (string.IsNullOrEmpty(strRecords))
                {
                    //Common.WriteLog("Record Not found, Check the XML data");
                    Common.WriteEventLog("Record Not found, Check the XML data", "Warning");
                    return;
                }
                listMake = new Dictionary<string, int>();
                listModel = new Dictionary<string, string>();

                listLoaderThread.RunWorkerAsync(strRecords);
                GetMakeModel();
            }
            catch (Exception ex)
            {
                //Common.WriteLog("PopulateVehicleData: " + ex.Message);
                Common.WriteEventLog("PopulateVehicleData" + ex.Message, "Error");
            }
        }

        private static string GetPendingRecordOnly(string serverXmlData)
        {
            try
            {
                XDocument xDocAll = XDocument.Parse(serverXmlData);

                var records = from vehicleData in xDocAll.Root.Elements("vehicle")
                              where (string)vehicleData.Attribute("Pending") == "1"
                              select vehicleData;

                XDocument xDocResults = null;
                string strResult = string.Empty;

                foreach (var record in records)
                {
                    xDocResults = new XDocument(record);
                    strResult += xDocResults.ToString();
                }

                strResult = "<Vehicles>" + strResult;
                strResult += "</Vehicles>";

                return strResult;
            }
            catch (Exception ex)
            {
                //Common.WriteLog("GetPendingRecordOnly: " + ex.Message);
                Common.WriteEventLog("GetPendingRecordOnly:" + ex.Message, "Error");
                return null;
            }
        }

        private void GetMakeModel()
        {
            try
            {
                XDocument doc = XDocument.Parse(serverData);
                var records = from book in doc.Root.Elements("vehicle")
                              select book;

                int indexMake = 0;

                foreach (var book in records)
                {
                    // See if Dictionary contains this make
                    string makeValue = book.Attribute("make").Value;
                    if (!listMake.ContainsKey(makeValue) && !string.IsNullOrEmpty(makeValue))
                    {
                        listMake.Add(makeValue, indexMake);
                        indexMake++;
                    }

                    // See if Dictionary contains this make
                    string modelValue = book.Attribute("model").Value;
                    if (!listModel.ContainsKey(modelValue) && !string.IsNullOrEmpty(modelValue))
                    {
                        listModel.Add(modelValue, makeValue);
                    }
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("GetMakeModel: " + ex.Message);
                Common.WriteEventLog("GetMakeModel:" + ex.Message, "Error");
            }
        }

        internal void UpdateStatus()
        {
            Vehicle selecedVehicle = (Vehicle)listiew.SelectedItem;
            UploadStatusListQueue(selecedVehicle);
        }

        private void UploadStatusListQueue(Vehicle selecedVehicle)
        {
            try
            {
                string strGUID = selecedVehicle.VehicleKey;
                string vehicleIndex = selecedVehicle.Index;

                VideoInfo vInfo = new VideoInfo();
                vInfo.RooftopKey = RooftopKey;
                vInfo.OStatus = oStatus;


                vInfo.Stock = selecedVehicle.Stock;


                vInfo.VehicleKey = strGUID;
                vInfo.VehicleIndex = vehicleIndex;
                vInfo.ShowDialog();

                string strInputFileName = vInfo.VideoFileName;
                string strOutputFileName = vInfo.VideoGUID;

                if (string.IsNullOrEmpty(strInputFileName))
                    return;

                lblMessage.Visibility = Visibility.Visible;
                string strMessage = "Video file " + System.IO.Path.GetFileName(vInfo.VideoFileName) + "\n added to the queue.";
                strMessage += "\n";
                strMessage += ResourceTxt.VideoAddMessage;
                lblMessage.Content = strMessage;





                VehicleInfo oVehicleInfo = new VehicleInfo();


                //oStatus.OVehicleInfo = oVehicleInfo;

                oVehicleInfo.VehicleKey = strGUID;
                oVehicleInfo.InputFileName = strInputFileName;
                oVehicleInfo.OutputFileName = strOutputFileName;
                oVehicleInfo.TargetVideoHeight = selecedVehicle.Height;
                oVehicleInfo.TargetVideoWidth = selecedVehicle.Width;
                oVehicleInfo.RooftopKey = RooftopKey;
                oVehicleInfo.Stock = selecedVehicle.Stock;
                oVehicleInfo.VideoTitle = vInfo.VideoTitle;
                oVehicleInfo.Description = vInfo.VideoDescription;
                oVehicleInfo.IsDefault = vInfo.IsDefault;
                oVehicleInfo.IsExtract = vInfo.IsExtract;
                // oVehicleInfo.Stock = vInfo.Stock;
                oVehicleInfo.VIN = strOutputFileName;
                oVehicleInfo.VideoType = vInfo.VideoType;
                oStatus.OVehicleInfo = oVehicleInfo;


                oStatus.OStatus = oStatus;

                if (strInputFileName != null)
                {
                    oStatus.VideoConverter();
                }

                //Show Status Queue for first time only
                if (!oStatus.IsVisible)
                {
                    vInfo.OStatus.OHome = this;
                    vInfo.OStatus.Show();
                }

            }
            catch (Exception ex)
            {
                //Common.WriteLog("lnkAddVideo_LinkClicked: " + ex.Message);
                Common.WriteEventLog("lnkAddVideo_LinkClicked:" + ex.Message, "Error");
            }
        }

        private void FindRecord(string piStock)
        {

            try
            {

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

                listLoaderThread.RunWorkerAsync(strResult);
                //BindVehicleData(strResult);
            }
            catch (Exception ex)
            {
                //Common.WriteLog("FindRecord: " + ex.Message);
                Common.WriteEventLog("FindRecord:" + ex.Message, "Error");
            }
        }

        private void BindVehicleData(string pstrServerData)
        {
            try
            {
                Dispatcher.Invoke(new Action(() => Spinner.Visibility = Visibility.Visible));
                vehicleList.Clear();
                MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(pstrServerData));
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(xmlStream);

                DataView dataView = new DataView(dataSet.Tables["Vehicle"]);
                if (dataView.Count > 0)
                {
                    Dispatcher.Invoke(new Action(() => lblNoOfRecordsValue.Content = dataView.Count.ToString()));
                    foreach (DataRow dataRow in dataSet.Tables["Vehicle"].Rows)
                    {
                        Vehicle vehicle = new Vehicle();
                        vehicle.Height = Convert.ToString(dataRow["h"]);
                        vehicle.Image = Convert.ToString(dataRow["image"]);
                        vehicle.Index = Convert.ToString(dataRow["index"]);
                        vehicle.ListingType = Convert.ToString(dataRow["listing_type"]);
                        vehicle.Make = Convert.ToString(dataRow["make"]);
                        vehicle.Mileage = Convert.ToString(dataRow["mileage"]);
                        vehicle.Model = Convert.ToString(dataRow["model"]);
                        vehicle.Pending = Convert.ToString(dataRow["Pending"]);
                        vehicle.Stock = Convert.ToString(dataRow["stock"]);
                        vehicle.TotalViews = Convert.ToString(dataRow["totalviews"]);
                        vehicle.Trim = Convert.ToString(dataRow["trim"]);
                        vehicle.VehicleKey = Convert.ToString(dataRow["vehiclekey"]);
                        vehicle.Videos = "Add Video(" + Convert.ToString(dataRow["videos"]) + ")";
                        vehicle.Width = Convert.ToString(dataRow["w"]);
                        vehicle.Year = Convert.ToString(dataRow["year"]);
                        if (Convert.ToInt32(dataRow["Pending"]) == 1)
                            vehicle.VideoImage = @"/WpfVideoUploader;component/Images/video_red.png";
                        else
                            vehicle.VideoImage = @"/WpfVideoUploader;component/Images/video_green.png";
                        vehicleList.Add(vehicle);
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => dtGrid.Visibility = Visibility.Hidden));
                    Dispatcher.Invoke(new Action(() => lblNoOfRecordsValue.Content = ResourceTxt.NoRecordsFound));
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("BindVehicleData: " + ex.Message);
                Common.WriteEventLog("BindVehicleData:" + ex.Message, "Error");
            }
        }

        private void ReloadDataFromServer()
        {
            try
            {
                WpfVideoUploader.ComboBoxItem cItem = (WpfVideoUploader.ComboBoxItem)cmbRoofTop.Items[cmbRoofTop.SelectedIndex];
                string strRoofTop = cItem.Value.ToString();
                lblNoOfRecords.Visibility = Visibility.Hidden;
                lblNoOfRecordsValue.Visibility = Visibility.Hidden;
                lblMessage.Visibility = Visibility.Hidden;
                RooftopKey = strRoofTop;
                Spinner.Visibility = Visibility.Visible;
                dtGrid.Visibility = Visibility.Hidden;
                listiew.Visibility = Visibility.Hidden;
                //code to start spinner
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Common.WriteLog("ReloadDataFromServer: " + ex.Message);
                Common.WriteEventLog("ReloadDataFromServer:" + ex.Message, "Error");
            }
        }

        /// <summary>
        /// Update the Pending attribute value for the matching vehiclekey if Pending is true
        /// </summary>
        /// <param name="VehicleKey"></param>
        public void UpdatePendingField(string vehicleKey)
        {
            try
            {


                XmlTextReader xmlTxtServerData = new XmlTextReader(new StringReader(serverData));
                XElement xServerElement = XElement.Load(xmlTxtServerData);
                XDocument xDocResults = null;

                var result = from videos in xServerElement.Descendants("vehicle") where videos.Attribute("vehiclekey").Value == vehicleKey && videos.Attribute("Pending").Value == "1" select videos;

                // Don't do anything if there is no matching vehiclekey with Pending
                if (result.Count() == 0)
                    return;

                result = from videos in xServerElement.Descendants("vehicle") select videos;
                StringBuilder strResult = new StringBuilder();
                foreach (XElement xEle in result.ToList())
                {
                    if (xEle.Attribute("vehiclekey").Value.Equals(vehicleKey))
                        xEle.Attribute("Pending").Value = "0";

                    xDocResults = new XDocument(xEle);
                    strResult.Append(xDocResults.ToString());
                }

                strResult.Insert(0, "<Vehicles>");
                strResult.Append("</Vehicles>");

                serverData = string.Empty;
                serverData = strResult.ToString();

                PopulateVehicleData();
            }
            catch (Exception ex)
            {
                //Common.WriteLog("UpdatePendingFlag: " + ex.Message);
                Common.WriteEventLog("UpdatePendingFlag:" + ex.Message, "Error");
            }
        }

        #endregion

        private void cmbRoofTop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    ComboBoxItem cItem = (ComboBoxItem)cmbRoofTop.Items[cmbRoofTop.SelectedIndex];
            //    string strRoofTop = cItem.Value.ToString();

            //    if (strRoofTop != RooftopKey && bgWorkerBusy == false)
            //    {
            //        ReloadDataFromServer();
            //        //PopulateVehicleData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //Common.WriteLog("cmbRoofTop_SelectedIndexChanged: " + ex.Message);
            //    Common.WriteEventLog("cmbRoofTop_SelectedIndexChanged:" + ex.Message, "Error");
            //}

             try
             {
                 ComboBoxItem cItem = (ComboBoxItem)cmbRoofTop.Items[cmbRoofTop.SelectedIndex];
                 string strRoofTop = cItem.Value.ToString();

                 if (strRoofTop != RooftopKey && bgWorkerBusy == false)
                 {
                     ReloadDataFromServer();
                     GetAuthenticatedata(strRoofTop);
                     if (UserAuthorization.BatchUploadAuth == "0")
                     {
                         btnBatchUpload.Visibility = Visibility.Hidden;
                     }
                     else
                     {
                         btnBatchUpload.Visibility = Visibility.Visible;
                     }
                     //PopulateVehicleData();
                 }
             }
             catch (Exception ex)
             {
                 //Common.WriteLog("cmbRoofTop_SelectedIndexChanged: " + ex.Message);
                 Common.WriteEventLog("cmbRoofTop_SelectedIndexChanged:" + ex.Message, "Error");
             }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //Common.WriteLog("INFO: BackgroundWorker_DoWork started");
            Common.WriteEventLog("BackgroundWorker_DoWork started:", "Information");
            bgWorkerBusy = true;
            GetVehicleDataFromServer();
            //Common.WriteLog("INFO: BackgroundWorker_DoWork End");
            Common.WriteEventLog("BackgroundWorker_DoWork End:", "Information");
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Common.WriteLog("INFO: BackgroundWorker_RunWorkerCompleted started");
            Common.WriteEventLog("BackgroundWorker_RunWorkerCompleted started:", "Information");
            PopulateVehicleMake(listMake);
            cmbMake.Text = ResourceTxt.VehicleMakeAll;
            cmbModel.Text = ResourceTxt.VehicleModelAll;
            lblNoOfRecords.Visibility = Visibility.Visible;
            lblNoOfRecordsValue.Visibility = Visibility.Visible;
            Spinner.Visibility = Visibility.Hidden;
            PopulateVehicleData();
            bgWorkerBusy = false;
            //Common.WriteLog("INFO: BackgroundWorker_RunWorkerCompleted End");
            Common.WriteEventLog("BackgroundWorker_RunWorkerCompleted End:", "Information");
        }

        private void listLoaderThread_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //Common.WriteLog("INFO: listLoaderThread_DoWork Started");
            Common.WriteEventLog("listLoaderThread_DoWork Started:", "Information");
            string serverData = (string)e.Argument;
            BindVehicleData(serverData);
            //Common.WriteLog("INFO: listLoaderThread_DoWork End");
            Common.WriteEventLog("listLoaderThread_DoWork End:", "Information");
        }

        private void listLoaderThread_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Common.WriteLog("INFO: listLoaderThread_RunWorkerCompleted Started");
            Common.WriteEventLog("listLoaderThread_RunWorkerCompleted Started:", "Information");
            Spinner.Visibility = Visibility.Hidden;
            if (isGridView)
            {
                dtGrid.Items.Refresh();
                dtGrid.Visibility = Visibility.Visible;
                listiew.Visibility = Visibility.Hidden;
            }
            else
            {
                dtGrid.Visibility = Visibility.Hidden;
                listiew.Visibility = Visibility.Visible;
                listiew.Items.Refresh();
            }
            //cmbModel.Text = ResourceTxt.VehicleModelAll;
            //Common.WriteLog("INFO: listLoaderThread_RunWorkerCompleted Completed");
            Common.WriteEventLog("listLoaderThread_RunWorkerCompleted Completed:", "Information");
        }

        private void DataGridTemplateColumn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //  MessageBox.Show("asdsasadsad");
            Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;
            UploadStatusListQueue(selecedVehicle);
        }

        private void btnImageQueue_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                oExtractStatus.Show();
                oExtractStatus.Focus();
            }
            catch
            {
                //oExtractStatus.Show();
            }

            // var exists = Application.Current.Windows.Cast<ViewImages>().SingleOrDefault(w=>w.IsActive);
        }



        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;
            UploadStatusListQueue(selecedVehicle);
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;
            UploadStatusListQueue(selecedVehicle);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            //Vehicle selecedVehicle = (Vehicle)dtGrid.SelectedItem;

            //UploadStatusListQueue(selecedVehicle);
        }

        private void btnAddVideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Vehicle selectedVehicle = new Vehicle();
                UpdatestatuslistQueue(selectedVehicle);
            }
            catch(Exception ex) { string error = ex.Message; }






        }



        public void UpdatestatuslistQueue(Vehicle selectedVehicle)
        { 
            AddVideo AddvInfo = new AddVideo(RooftopKey); 
            AddvInfo.OStatus = oStatus; 
            AddvInfo.ShowDialog(); 
            string strInputFileName = AddvInfo.VideoFileName;
            string strOutputFileName = AddvInfo.VideoGUID;

            if (string.IsNullOrEmpty(strInputFileName))
                return;

            lblMessage.Visibility = Visibility.Visible;
            string strMessage = "Video file " + System.IO.Path.GetFileName(AddvInfo.VideoFileName) + "\n added to the queue.";
            strMessage += "\n";
            strMessage += ResourceTxt.VideoAddMessage;
            lblMessage.Content = strMessage;

            VehicleInfo oVehicleInfo = new VehicleInfo();
            oVehicleInfo.VehicleKey = AddvInfo.VehicleKey;
            oVehicleInfo.InputFileName = strInputFileName;
            oVehicleInfo.OutputFileName = strOutputFileName;

            oVehicleInfo.TargetVideoHeight = AddvInfo.VideoHeight;
            oVehicleInfo.TargetVideoWidth = AddvInfo.VideoWidth;

            oVehicleInfo.RooftopKey = RooftopKey;

            if (!string.IsNullOrEmpty(AddvInfo.Stock))
            {
                oVehicleInfo.Stock = AddvInfo.Stock;
            }
            else
            {
                oVehicleInfo.Stock = "No Stock";
            }

            oVehicleInfo.VideoTitle = AddvInfo.VideoTitle;
            oVehicleInfo.Description = AddvInfo.VideoDescription;
            oVehicleInfo.IsDefault = AddvInfo.IsDefault;
            oVehicleInfo.IsExtract = AddvInfo.IsExtract;
            oVehicleInfo.VIN = AddvInfo.VIN;
            oVehicleInfo.Categories = AddvInfo.Category;

            oVehicleInfo.VideoType = AddvInfo.VideoType;



            oStatus.OVehicleInfo = oVehicleInfo;

            oStatus.OStatus = oStatus;

            if (strInputFileName != null)
            {
                oStatus.VideoConverter();
            }

            //Show Status Queue for first time only
            if (!oStatus.IsVisible)
            {
                AddvInfo.OStatus.OHome = this;
                AddvInfo.OStatus.Show();

            }
        }






        //#####################################################################################
    }
}
