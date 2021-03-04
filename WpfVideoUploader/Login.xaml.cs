using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Configuration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using System.Xml;
using System.IO;
using System.Diagnostics;

using System.ComponentModel;
using System.Drawing;
using System.Windows.Interop;
using System.Net;




namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        #region Fields
        private RoofTop oRoofTop = null;
        private Upgrade objUpgarde = null;
        // private bool _isUserValidated = false;
        string _userName = string.Empty;
        string _passWord = string.Empty;
        public Dictionary<string, string> lstRootTop;
        private BackgroundWorker backgroundWorker;

        private string _InventoryAuthorization = string.Empty;
        private string _NonInventoryAuthorization = string.Empty;

        public string InventoryAuthorization
        {
            get;
            set;
        }
        public string NonInventoryAuthorization
        {
            get;
            set;
        }



        #endregion

        //###################################################################################################################################

        #region Constructor
        public Login()
        {
            InitializeComponent();
            backgroundWorker =
                            ((BackgroundWorker)this.FindResource("backgroundWorker"));
            btnLogin.IsEnabled = false;

            oRoofTop = new RoofTop();
            if (!File.Exists(Common.SettingsFile))
                ShowSetting();
        }
        #endregion

        //##################################################################################################################################

        #region Events
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            Alert objalert = new Alert(ResourceTxt.sure);
            objalert.ShowDialog();
            if (objalert.close)
            {
                System.Windows.Application.Current.Shutdown();

            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Coment for time being
        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // each time something is typed into or removed from either textbox,
            //    we will check to see if anything is in both textboxes.
            // if both contain a value(other than spaces), the button is enabled.
            // if one or both boxes are blank, the button is disabled.

            IsCapsLock();

            if (txtPassword.Password.Trim().Length > 0 &&
                txtUserName.Text.Trim().Length > 0)
            {
                btnLogin.IsEnabled = true;
            }
            else
            {
                btnLogin.IsEnabled = false;
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = LoginUser(_userName, _passWord);
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool loginStatus = (bool)e.Result;

            if (loginStatus)
            {

                this.Close();

                VersionHelper obj = new VersionHelper();
                string strVer = obj.CheckLatestVersion();

                if (!string.IsNullOrEmpty(strVer) && !strVer.Equals("0") && Convert.ToDecimal(ResourceTxt.CurrentVersion.Replace(".", "")) < Convert.ToDecimal(strVer.Replace(".", "")))
                {
                    objUpgarde = new Upgrade(_userName, _passWord);
                    objUpgarde.ShowDialog();
                }
                else
                {
                    Spinner.Visibility = Visibility.Hidden;
                    oRoofTop.PopulateRooftop(lstRootTop);
                    oRoofTop.RooftopValues = lstRootTop;
                    oRoofTop.LoggedInUser = txtUserName.Text;
                    oRoofTop.Show();
                }
            }
            else
            {
                lblError.Visibility = Visibility.Visible;
                btnLogin.Visibility = Visibility.Visible;
                Spinner.Visibility = Visibility.Hidden;
                lblLoading.Visibility = Visibility.Hidden;
            }

        }

        private void btnLogin_Click(object sender, RoutedEventArgs args)
        {

            btnLogin.Visibility = Visibility.Hidden;
            lblError.Visibility = Visibility.Hidden;
            _userName = txtUserName.Text;
            _passWord = txtPassword.Password;
            Spinner.Visibility = Visibility.Visible;
            backgroundWorker.RunWorkerAsync();
        }
        #endregion

        //#################################################################################################################

        #region PrivateMethods
        private bool LoginUser(string userName, string passWord)
        {
            try
            {
                Dispatcher.Invoke(new Action(() => lblError.Visibility = Visibility.Hidden));

                if (Common.IsConnectedToInternet())
                {
                    Dispatcher.Invoke(new Action(() => btnLogin.IsEnabled = false));
                    Dispatcher.Invoke(new Action(() => lblLoading.Visibility = Visibility.Visible));

                    if (ValidateUser(userName, passWord))
                    {
                        Common.WriteLog("User " + userName + " Logged In");
                        //_isUserValidated = true;
                        ValidatePath();
                        Common.WriteEventLog("User " + userName + " Logged In", "Information");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //_isUserValidated = false;
                    Dispatcher.Invoke(new Action(() => lblError.Visibility = Visibility.Visible));
                    Dispatcher.Invoke(new Action(() => lblError.Content = ResourceTxt.NoInternet));
                    Common.WriteLog(ResourceTxt.NoInternet);
                    return false;
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("btnLogin_Click: " + ex.Message);
                Common.WriteEventLog("btnLogin_Click: " + ex.Message, "Error");
                return false;
            }
        }

        /// <summary>
        /// Validating User
        /// </summary>
        /// <param name="pstrUserName"></param>
        /// <param name="pstrPassword"></param>
        /// <returns></returns>
        private bool ValidateUser(string pstrUserName, string pstrPassword)
        {
            try
            {
                bool _failed = false;

                //var encoding = new ASCIIEncoding();
                var postData = "ACTION=LOGIN";
                postData += ("&USER=" + pstrUserName);
                postData += ("&PWD=" + pstrPassword);

                string serverData = string.Empty;
                serverData = Common.GetServerData(postData); 

                if (string.IsNullOrEmpty(serverData))
                    return false;

                if (serverData.Contains("InValid Login"))
                {
                    Dispatcher.Invoke(new Action(() => lblError.Content = ResourceTxt.InValidLogin));
                    _failed = true;
                }
                else if (serverData.Equals("WebError"))
                {
                    Dispatcher.Invoke(new Action(() => lblError.Content = ResourceTxt.GetServerData_WebError));
                    _failed = true;
                }
                else if (serverData.Equals("Error"))
                {
                    Dispatcher.Invoke(new Action(() => lblError.Content = ResourceTxt.GetServerData_Error));
                    _failed = true;
                }

                if (_failed)
                {
                    Dispatcher.Invoke(new Action(() => lblError.Visibility = Visibility.Visible));
                    Dispatcher.Invoke(new Action(() => btnLogin.IsEnabled = true));
                    return false;
                }

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(serverData);
                GetRooftopValues(serverData);


                //XmlDocument xDocUserDetails = new XmlDocument();
                //xDocUserDetails.LoadXml(serverData);

                XmlNode node = xDoc.SelectSingleNode("search");

                //UserAuthorization.InventoryAuthorization = node.Attributes["recordvideo"].Value;
                //UserAuthorization.NonInventoryAuthorization = node.Attributes["recordadvideo"].Value;



                return true;
            }
            catch (Exception ex)
            {
                //Common.WriteLog("ValidateUser: " + ex.Message);
                Common.WriteEventLog("ValidateUser: " + ex.Message, "Error");
                return false;
            }
        }


        public void GetRooftopValues(string xmlDoc)
        {
            XmlReader reader = null;
            lstRootTop = new Dictionary<string, string>();
            string strKey = null;
            string strValue = null;
            try
            {
                using (reader = XmlReader.Create(new StringReader(xmlDoc)))
                {
                    while (reader.Read())
                    {
                        reader.ReadToFollowing("rooftop");
                        reader.MoveToFirstAttribute();
                        strKey = reader.Value;
                        reader.Read();
                        strValue = reader.Value;
                        if (!string.IsNullOrEmpty(strValue))
                        {
                            lstRootTop.Add(strKey, strValue);
                        }
                        strKey = null;
                        strValue = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("GetRoofTopValues: " + ex.Message);
                Common.WriteEventLog("GetRoofTopValues: " + ex.Message, "Error");
            }
            finally
            {
                // Do the necessary clean up.
                if (reader != null)
                    reader.Close();
            }
        }

        private void ShowSetting()
        {
            try
            {
                Settings oSetting = new Settings();
                //oSetting.ShowDialog();
            }
            catch
            {
                // System.Windows.MessageBox.Show(ResourceTxt.CannotLoadSettings);
                Alert objalert = new Alert(ResourceTxt.CannotLoadSettings);
                objalert.btnNO.Visibility = Visibility.Visible;
                objalert.btnYes.Content = "Ok";
                objalert.ShowDialog();

                if (objalert.close)
                {
                    //System.Windows.Application.Current.Shutdown();

                }
            }
        }

        #endregion
        private void ValidatePath()
        {
            string strAppName = ResourceTxt.AppName;
            string strSettingFile = Common.SettingsFile;
            string strPath = Common.VedioFolder;
            if (!Directory.Exists(strPath))
            {
                System.IO.Directory.CreateDirectory(strPath);
                if (File.Exists(strSettingFile))
                {
                    XmlDocument xmlDoc = null;
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(strSettingFile);
                    XmlNode node;
                    node = xmlDoc.DocumentElement;
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name == "OutputVideoLocation")
                        {
                            node2.InnerText = strPath;
                        }
                    }
                    xmlDoc.Save(strSettingFile);
                }
            }


        }
        //############################################################################################

        ///-----Lateversion image starts-----
        /// <summary>
        ///  
        /// </summary>
        Bitmap _bitmap;
        BitmapSource _source;

        private BitmapSource GetSource()
        {
            if (_bitmap == null)
            {
                string path = Directory.GetCurrentDirectory();

                path = path.Replace("\\bin", "").Replace("\\Debug", "");
                // Check the path to the .gif file
                //_bitmap = new Bitmap(path+"\\Images\\NewVersion.gif");
                _bitmap = new Bitmap(path + "\\NewVersion.gif");
            }

            IntPtr handle = IntPtr.Zero;
            handle = _bitmap.GetHbitmap();

            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }




        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageObjects._ExportStarted = false;
                if (ImageObjects.ImageExportingVideoList == null)
                    ImageObjects.ImageExportingVideoList = new VehicleInfoCollection();

                string setupFile = Common.SetupFile;
                string strLog = Common.EventFileXls;
                if (File.Exists(setupFile))
                    File.Delete(setupFile);
                if (File.Exists(strLog))
                    File.Delete(strLog);

                //VersionHelper obj = new VersionHelper();
                //string strVer = obj.CheckLatestVersion();

                //if (!string.IsNullOrEmpty(strVer) && !strVer.Equals("0") && Convert.ToDecimal(ResourceTxt.CurrentVersion.Replace(".", "")) < Convert.ToDecimal(strVer.Replace(".", "")))
                //{

                //    imgNewVersion.Visibility = Visibility.Visible;
                //    _source = GetSource();
                //    imgNewVersion.Source = _source;
                //    ImageAnimator.Animate(_bitmap, OnFrameChanged);

                //}


                txtUserName.Focus();
            }
            catch (Exception ex)
            {
                Common.WriteEventLog(ex.Message, "Error");

            }
        }

        private void IsCapsLock()
        {
            if (Keyboard.IsKeyToggled(Key.CapsLock))
            {
                lblError.Content = ResourceTxt.CapsLock;
                lblError.Visibility = Visibility.Visible;

            }
            else
            {
                lblError.Content = "";
                lblError.Visibility = Visibility.Hidden;

            }
        }


        private void FrameUpdatedCallback()
        {
            ImageAnimator.UpdateFrames();

            if (_source != null)
            {
                _source.Freeze();
            }

            _source = GetSource();

            //imgNewVersion.Source = _source;
            InvalidateVisual();
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(FrameUpdatedCallback));
        }

        private void imgNewVersion_KeyDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(ResourceTxt.MsgNewVersion, "Notification", MessageBoxButton.YesNo);
                if (Convert.ToString(result) == "Yes")
                {
                    //setupSpiiner.Visibility = Visibility.Visible;
                    Dispatcher.Invoke(new Action(() => btnLogin.IsEnabled = false));
                    VersionHelper objVer = new VersionHelper();
                    objVer.DownloadFileFTP();
                }
            }
            catch (Exception ex)
            {
                btnLogin.IsEnabled = true;
                //setupSpiiner.Visibility = Visibility.Hidden;
                Common.WriteEventLog(ex.Message, "Error");
            }
        }






        ///-----Lateversion image Ends-----



    }
}
