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
using System.Diagnostics;
using System.Net;
using System.IO;
using CAVEditLib;
using System.Xml.Linq;
using System.Windows.Threading;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using System.Data;



namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for ViewImages.xaml
    /// </summary>
    public partial class ViewImages : Window
    {
        public class ClsImages
        {

            public string title { get; set; }
            public string image { get; set; }
            public bool isChecked { get; set; }
        }

        //public ExportImages obJExport = new ExportImages();

        ExportImages obJExportImagesNew =null;
        //ViewSelectedImages OViewSelected = new ViewSelectedImages();
        List<ClsImages> lstCheckedImages = new List<ClsImages>();
        string strDestination = string.Empty;
        private string _ImageName = string.Empty;

        private int ImageIndex = 0;
        private int itemIndex = 0;
        //private ExportImages obJExportImages = new ExportImages ();
        private string ReplaceImagepath;
        private bool IsReplace = false;
        private string ddlSelect;

        private Home _home = null;

        public Home OHome
        {
            get
            {
                return _home;
            }
            set
            {
                _home = value;
            }
        }
        public string ImageName
        {
            get { return _ImageName; }
            set { _ImageName = value; }
        }
        string ImageNum = string.Empty;

        public string stock;
        public string VIN;

        public string rooftop
        {
            get;
            set;
        }
        public List<ClsImages> lstViewImages { get; set; }
        public List<ClsImages> NewlstViewImages { get; set; }
        List<ClsImages> DropDownSelectedItems { get; set; }


        public ViewImages()
        {
           
            InitializeComponent();
            Getauthdata(rooftop);
         
        }
        /// <summary>
        /// getting Video name fro Image Extraction Status screen
        /// </summary>
        /// <param name="ImgName"></param>
        public ViewImages(string ImgName)
        {
            InitializeComponent();
            Getauthdata(rooftop);
            ImageName = ImgName;
        }

        public ViewImages(string ImageName, string stock, string VIN)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            //if (Directory.Exists(Common.ImageExtract + "\\" + ImageName))
            //{
           
            this.ImageName = ImageName;
            this.stock = stock;
            this.VIN = VIN;
            Getauthdata(rooftop);
            //}
            //else
            //{
            //    //MessageBox.Show("You have already removed this Images");
            //    //this.Close();
            //}
        }


        /// <summary>
        /// getting Replace Image index and and new Image  from Replace Image screen
        /// </summary>
        /// <param name="ImageName"></param>
        /// <param name="ImageIndex"></param>
        /// <param name="filename"></param>
        /// <param name="IsReplace"></param>
        /// <param name="NewlstViewImages"></param>
        /// <param name="lstViewImages"></param>
        /// <param name="ddlSelect"></param>

        public void LoadImages(string ImageName, int ImageIndex, string filename, bool IsReplace, List<ViewImages.ClsImages> NewlstViewImages, List<ViewImages.ClsImages> lstViewImages, string ddlSelect, string stock, string VIN, int itemIndex,string rooftopKey)
        {

            this.ImageName = ImageName;
            this.ImageIndex = ImageIndex;
            this.ReplaceImagepath = filename;
            this.IsReplace = IsReplace;
            this.NewlstViewImages = NewlstViewImages;
            this.lstViewImages = lstViewImages;
            this.ddlSelect = ddlSelect;
            this.stock = stock;
            this.VIN = VIN;
            this.rooftop = rooftopKey;
            this.itemIndex = itemIndex;
            Getauthdata(rooftop);
        }

        /// <summary>
        /// binding Images to grid on window load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillImages();
           
        }

        private void Getauthdata(string rooftop)
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
        
        
        }

        /// <summary>
        /// for loading Images with Image Replacement 
        /// </summary>
        private void FillImages()
        {
            try
            {
                if (!IsReplace)
                {
                    string strFolder = Common.ImageExtract;
                    strFolder = strFolder + "\\" + ImageName;
                    //if (File.Exists(strFolder))
                    //{
                    lstViewImages = GetViewImages(strFolder);
                    lstExtractedImages.ItemsSource = lstViewImages;
                    // lstExtractedImages.DataContext = lstViewImages;
                    NewlstViewImages = lstViewImages;
                    cmbNumberOfImages.SelectedIndex = 1;
                    //}
                }
                else
                {

                    NewlstViewImages[ImageIndex-1].image = ReplaceImagepath;
                    lstViewImages[itemIndex].image = ReplaceImagepath;
                    lstExtractedImages.ItemsSource = lstViewImages;
                    //lstExtractedImages.DataContext = lstViewImages;
                    cmbNumberOfImages.SelectedIndex = Convert.ToInt32(ddlSelect);
                    IsReplace = false;
                }
            }

            catch (Exception ex)
            {
                //Common.WriteLog("NotifyUploadVideo: " + ex.Message);
                Common.WriteEventLog("LoadImages to View Images screen: " + ex.Message, "Error");

            }
        }
        /// <summary>
        /// Getting Extracted Images from defined path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>

        public List<ClsImages> GetViewImages(string path)
        {

            try
            {

                var images = Directory.GetFiles(path, "*.jpg");
                lstViewImages = new List<ClsImages>();
                for (int i = 0; i < images.Count(); i++)
                {
                    if (images[i].Contains(ImageName + "$"))
                    {
                        //using (FileStream stream = new FileStream(images[i], FileMode.Open, FileAccess.Read))
                        //{
                        ImageNum = images[i].Split('$')[1].ToString().TrimEnd('.', 'j', 'p', 'g');
                        lstViewImages.Add(new ClsImages { image = images[i], title = "IMAGE" + ImageNum, isChecked = true });
                        // }
                    }
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("NotifyUploadVideo: " + ex.Message);
                Common.WriteEventLog("Get View Images: " + ex.Message, "Error");

            }
            return (List<ClsImages>)lstViewImages;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    //if (this.lstExtractedImages.Items.Count > 0)
            //    //{
            //    //    //for (int i = 0; i < this.lstExtractedImages.Items.Count; i++)
            //    //    //{
            //    //        //lstExtractedImages.Items.Dispatcher.DisableProcessing();
            //    //    //}

            //    //    //this.lstExtractedImages.Items.Clear();
            //    //}
            //    lstViewImages = new List<ClsImages>();
            //    lstExtractedImages.ItemsSource = lstViewImages;


            //    if (NewlstViewImages.Count() > 0)
            //    {
            //        NewlstViewImages.Clear();
            //    }
            //    if (DropDownSelectedItems.Count() > 0)
            //    {
            //        DropDownSelectedItems.Clear();
            //    }

            //    //if (lstExtractedImages.Items.Count > 0)
            //    //{
            //    //    lstExtractedImages.Items.Clear();
            //    //}
            //    if (lstCheckedImages.Count > 0)
            //    {
            //        lstCheckedImages.Clear();
            //    }

            //}
            //catch
            //{
            //    //MessageBox.Show("btnClose_Click: " + ex.Message);
            //}
            e.Handled = true;
            this.Hide();
        }
        /// <summary>
        /// sending vlues for getting Images for Replacing selected image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClsImages item = (ClsImages)(sender as System.Windows.Controls.Button).DataContext;
                //item.
                itemIndex = lstExtractedImages.Items.IndexOf(item);
                ImageNum = item.title.Split('E')[1].ToString();
                int ImageIndex = Convert.ToInt32(ImageNum) - 1;
                ddlSelect = Convert.ToString(cmbNumberOfImages.SelectedIndex);
                ReplaceImage objReplaceImages = new ReplaceImage(ImageName, ImageIndex, NewlstViewImages, DropDownSelectedItems, ddlSelect, this, stock, VIN, itemIndex, rooftop);
               // cmbNumberOfImages.SelectedIndex = Convert.ToInt32(ddlSelect);
                objReplaceImages.ShowDialog();
                //objReplaceImages.Show();
                //objReplaceImages.Focus();
            }
            catch (Exception ex)
            {
                //Common.WriteLog("NotifyUploadVideo: " + ex.Message);
                Common.WriteEventLog("btnReplace_Click: " + ex.Message, "Error");

            }
        }
        /// <summary>
        ///Getting Images on dropdown selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNumberOfImages_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!IsReplace)
                {
                    DropDownSelectedItems = new List<ClsImages>();
                    //Images = new List<ClsImages>();
                    if (cmbNumberOfImages.SelectedItem.ToString().Split(':')[1].ToString().TrimStart() != "All")
                    {
                        int totalNumberOfImages = NewlstViewImages.Count();
                        int NumberOfRequiredImages = Convert.ToInt32(cmbNumberOfImages.SelectedItem.ToString().Split(':')[1].ToString().TrimStart());

                        if (totalNumberOfImages >= NumberOfRequiredImages)
                        {
                            int requiredImageIndex = (totalNumberOfImages / NumberOfRequiredImages);
                            int requiredImage = requiredImageIndex;
                            if (totalNumberOfImages > 0)
                            {

                                for (int i = 0; i < totalNumberOfImages; i++)
                                {

                                    if (DropDownSelectedItems.Count() < NumberOfRequiredImages)
                                    {

                                        requiredImage = i * requiredImageIndex;
                                        if (requiredImage < NewlstViewImages.Count())
                                        {

                                            ImageNum = NewlstViewImages[requiredImage].title.Split('E')[1];
                                            //ViewImageIndex = i;
                                            DropDownSelectedItems.Add(new ClsImages { image = NewlstViewImages[requiredImage].image, title = "IMAGE" + ImageNum, isChecked = NewlstViewImages[requiredImage].isChecked });
                                            //Images.Add(new ClsImages { image = NewlstViewImages[requiredImage].image, title = "IMAGE" + i, isChecked = NewlstViewImages[requiredImage].isChecked });
                                        }


                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                            }


                            lstExtractedImages.ItemsSource = DropDownSelectedItems;
                        }

                        else
                        {
                            DropDownSelectedItems = NewlstViewImages;
                            lstExtractedImages.ItemsSource = DropDownSelectedItems;
                        }
                    }
                    else
                    {
                        DropDownSelectedItems = lstViewImages;
                        lstExtractedImages.ItemsSource = DropDownSelectedItems;
                    }
                }
                else
                {

                    DropDownSelectedItems = lstViewImages;
                    lstExtractedImages.ItemsSource = DropDownSelectedItems;
                }
            }

            catch (Exception ex)
            {
                DropDownSelectedItems = NewlstViewImages;
                lstExtractedImages.ItemsSource = DropDownSelectedItems;


                //Common.WriteLog("NotifyUploadVideo: " + ex.Message);
                Common.WriteEventLog("cmbNumberOfImages_SelectedIndexChanged: " + ex.Message, "Error");


            }
        }

        /// <summary>
        /// getting checked images from ViewImages on dropdown selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewSelectd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                CheckedImages();
                if (lstCheckedImages.Count() > 0)
                {
                    ViewSelectedImages ObjViewSelected = new ViewSelectedImages(lstCheckedImages);
                    ObjViewSelected.ShowDialog();
                    ObjViewSelected.Focus();
                }
                else
                {
                    Alert1 objAlert = new Alert1(ResourceTxt.SelectImage);
                    objAlert.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Common.WriteEventLog("btnViewSelectd_Click: " + ex.Message, "Error");


            }
        }
        /// <summary>
        /// exporting/saving selected images to FTP server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.Close();
                CheckedImages();
                if (lstCheckedImages.Count() != 0)
                {
                    //obJExportImages.Close();
                    if (ImageObjects.ImageExportingVideoList == null)
                        ImageObjects.ImageExportingVideoList = new VehicleInfoCollection();
                    //if (obJExportImagesNew.lstCheckedImages == null)

                    //if (obJExportImagesNew == null)
                    //    obJExportImagesNew = new ExportImages();
                    if (ImageObjects.exportImages == null)
                        ImageObjects.exportImages = new ExportImages();
                   
                   

                    if (VIN != "")
                    {
                        //obJExportImagesNew.VIN = VIN;
                        ImageObjects.exportImages.VIN = VIN;
                    }
                    else
                    {
                        //obJExportImagesNew.VIN = ImageName;
                        ImageObjects.exportImages.VIN = ImageName;
                    }
                    //obJExportImagesNew.stock = stock;
                    ImageObjects.exportImages.stock = stock;
                    //obJExportImagesNew.OutFileName = ImageName;
                    ImageObjects.exportImages.OutFileName = ImageName;
                    //obJExportImagesNew.lstCheckedImages = lstCheckedImages;
                    ImageObjects.exportImages.lstCheckedImages = lstCheckedImages;
                    //obJExportImagesNew.RoofTopKey = rooftop;
                    ImageObjects.exportImages.RoofTopKey = rooftop;
                    // ImageObjects.ImageExportingVideoList.Add(new VehicleInfo { VIN = VIN, OutputFileName = ImageName, Stock = stock });
                    //ExportImages obJExportImagesNew = new ExportImages(ImageName, stock, lstCheckedImages, VIN);
                    ///obJExportImagesNew.AddCurrentExportingQueueFromFile();
                    //AddCurrentExportingImagesTofile();
                    //obJExportImagesNew.ShowDialog();
                    ImageObjects.exportImages.ShowDialog();
                    //obJExportImagesNew.Focus();
                    //this.Visibility = Visibility.Hidden;
                    //obJExportImages.Show();
                    this.Close();
                }


                else
                {
                    Alert1 objAlert = new Alert1(ResourceTxt.SelectImage);
                    objAlert.ShowDialog();
                }
            }
            catch { this.Close(); }



        }

        private void AddCurrentExportingImagesTofile()
        {
            try
            {
                string strExportingFile = Common.CurrentExporting;

                if (!File.Exists(strExportingFile))
                {
                    XmlDocument XDoc = new XmlDocument();
                    XmlElement XElemRoot = XDoc.CreateElement(XNAME.CURRENT_EXPORTING);
                    XDoc.AppendChild(XElemRoot);
                    XDoc.Save(strExportingFile);
                }

                XElement doc = XElement.Load(strExportingFile);


                doc.Add(
                new XElement(XNAME.FILE,
                new XElement(XNAME.VEHICLE_KEY, ""),
                new XElement(XNAME.SOURCE_FILE_NAME, ""),
                new XElement(XNAME.OUTPUT_FILE_NAME, ImageName),
                new XElement(XNAME.ROOF_TOP_KEY, ""),      // Add Rooftop, Title, Desc
                new XElement(XNAME.VIDEO_TITLE, ""),
                new XElement(XNAME.DESCRIPTION, ""),
                new XElement(XNAME.IS_DEFAULT, ""),
                new XElement(XNAME.IS_EXTRACT, ""),
                new XElement(XNAME.Stock, stock),
                new XElement(XNAME.VIN, VIN),
                new XElement(XNAME.VIDEOTYPE, "")));

                doc.Save(strExportingFile);
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("AddRecordToFile: " + ex.Message);
                //Common.WriteLog("AddRecordToFile: " + ex.Message);
                Common.WriteEventLog("AddCurrentExportingImagesTofile: " + ex.Message, "Error");
            }
        }
       
        private void CheckedImages()
        {
            try
            {
                lstCheckedImages.Clear();
                for (int i = 0; i < DropDownSelectedItems.Count(); i++)
                {
                    if (DropDownSelectedItems[i].image.Contains(ImageName + "$"))
                    {
                        if (DropDownSelectedItems[i].isChecked)
                        {
                            ImageNum = DropDownSelectedItems[i].title.Split('E')[1].ToString();
                            lstCheckedImages.Add(new ClsImages { image = DropDownSelectedItems[i].image, title = "IMAGE" + ImageNum, isChecked = DropDownSelectedItems[i].isChecked });

                        }
                        //else
                        //{
                        //    ImageNum = DropDownSelectedItems[i].title.Split('E')[1].ToString();
                        //    lstCheckedImages.Add(new ClsImages { image = DropDownSelectedItems[i].image, title = "IMAGE" + ImageNum, isChecked = false });
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteEventLog("CheckedImages: " + ex.Message, "Error");
            }

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void chkSelectImage_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                ClsImages item = (ClsImages)(sender as System.Windows.Controls.CheckBox).DataContext;
                item.isChecked = true;

            }
            catch (Exception ex)
            {
                Common.WriteEventLog("chkSelectImage_Checked: " + ex.Message, "Error");
            }
        }

        private void chkSelectImage_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ClsImages item = (ClsImages)(sender as System.Windows.Controls.CheckBox).DataContext;
                item.isChecked = false;
            }
            catch (Exception ex)
            {
                Common.WriteEventLog("chkSelectImage_UnChecked: " + ex.Message, "Error");
            }
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                ClsImages item = (ClsImages)(sender as System.Windows.Controls.Button).DataContext;

                BitmapImage bi = new BitmapImage(new Uri(item.image));
                int ImageNumber = Convert.ToInt32(item.title.Split('E')[1].ToString());
                EnlargeImage ObjEnlageImage = new EnlargeImage(DropDownSelectedItems, ImageNumber, item.image);
                ObjEnlageImage.ShowDialog();
                //ObjEnlageImage.Show();
            }
            catch (Exception ex)
            {
                lblMessge.Visibility = Visibility.Visible;
                lblMessge.Content = "Error Occured";


                Common.WriteEventLog("btnMaximize_Click: " + ex.Message, "Error");


            }
        }


        public void SaveToServer(List<ClsImages> lstCheckedImages, string prefix)
        {
            try
            {
                #region save to local
                int count = 0;


                strDestination = Common.DestinationtoSave + "\\" + prefix;

                if (!Directory.Exists(strDestination))
                {
                    strDestination += "\\";
                    Directory.CreateDirectory(strDestination);
                }
                else
                {
                    string[] strArrFolders = Directory.GetDirectories(Common.DestinationtoSave);


                    foreach (string folder in strArrFolders)
                    {

                        string[] folderParts = folder.Split('\\');

                        if (folderParts[7].Contains(prefix))
                        {
                            count = count + 1;
                        }
                    }
                    strDestination = strDestination + "_" + count;
                    strDestination += "\\";
                    Directory.CreateDirectory(strDestination);
                }

                string[] toBeSavedfiles = new string[lstCheckedImages.Count()];
                string imageNumberWithExtention = string.Empty;
                for (int i = 0; i < lstCheckedImages.Count(); i++)
                {
                    toBeSavedfiles[i] = lstCheckedImages[i].image;
                    //imageNumberWithExtention = toBeSavedfiles[i].Split('$')[1].ToString();
                    imageNumberWithExtention = (i + 1) + ".jpg";

                    try
                    {
                        if (!string.IsNullOrEmpty(strDestination + prefix + "_" + imageNumberWithExtention))
                        {
                            if (File.Exists(strDestination + prefix + "_" + imageNumberWithExtention))
                            {
                                File.Delete(strDestination + prefix + "_" + imageNumberWithExtention);
                            }
                            System.IO.File.Copy(toBeSavedfiles[i], strDestination + prefix + "_" + imageNumberWithExtention);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Common.WriteLog("NotifyUploadVideo: " + ex.Message);
                        Common.WriteEventLog("SavetoServer: " + ex.Message, "Error");

                    }
                }
                OpenSavedImages();
                this.Visibility=Visibility.Hidden;
                #endregion




            }

            catch (Exception ex)
            {
                //Common.WriteLog("NotifyUploadVideo: " + ex.Message);
                Common.WriteEventLog("SavetoServer: " + ex.Message, "Error");

            }
        }




        /// <summary>
        /// method for timer with time span 5 seconds
        /// </summary>
        private void StartOpenTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3d);
            timer.Tick += TimerTick;
            timer.Start();
        }

        /// <summary>
        /// timer event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            OpenSavedImages();
        }

        /// <summary>
        /// method for opening Saved Images
        /// </summary>
        public void OpenSavedImages()
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = strDestination;
            process.Start();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }
    }







}

