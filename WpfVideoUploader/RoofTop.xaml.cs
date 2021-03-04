﻿using System;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Windows.Threading;
using System.Threading;

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for RoofTop.xaml
    /// </summary>
    public partial class RoofTop : Window
    {
        #region Fields
        private string _loggedInUser = string.Empty;
        private BackgroundWorker backgroundWorker;

        public bool IsBackgroudBusy { get; set; }

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

        private Dictionary<string, string> _roofTopValues = null;

        public Dictionary<string, string> RooftopValues
        {
            get
            {
                return _roofTopValues;
            }
            set
            {
                _roofTopValues = value;
            }
        }

        private Home _oHome = null;

        public Home OHome
        {
            get { return _oHome; }
            set { _oHome = value; }
        }
        #endregion

        //##################################################################################################

        #region Constructor

        public RoofTop()
        {
            InitializeComponent();
            cmbRoofTop.Focus();
            backgroundWorker =
                            ((BackgroundWorker)this.FindResource("backgroundWorker"));
        }

        #endregion

        //##################################################################################################

        #region Events

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string RoofTopKey = string.Empty;
                string RoofTopText = string.Empty;

                if (cmbRoofTop.SelectedItem != null && cmbRoofTop.SelectedItem.ToString().Trim() != ResourceTxt.RoofTopSelect)
                {
                    btnNext.IsEnabled = false;
                    lblLoading.Content = "Loading.......Please wait";
                    lblLoading.Refresh();
                    this.Refresh();
                    WpfVideoUploader.ComboBoxItem cItem = (WpfVideoUploader.ComboBoxItem)cmbRoofTop.SelectedItem;
                    if (!string.IsNullOrEmpty(cItem.Value))
                    {
                        RoofTopKey = cItem.Value.ToString();
                        RoofTopText = cItem.Text.ToString();

                        Home obMainWindow = new Home();
                        OHome = obMainWindow;
                        OHome.HRooftop = this;
                        OHome.RooftopKey = RoofTopKey;
                        OHome.RooftopText = RoofTopText;
                        OHome.PopulateRooftop(RooftopValues);
                        OHome.LoggedInUser = LoggedInUser;
                        Spinner.Visibility = Visibility.Visible;
                        btnNext.Visibility = Visibility.Hidden;
                        backgroundWorker.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("RoofTop btnNext_Click: " + ex.Message);
                Common.WriteEventLog("RoofTop btnNext_Click: " + ex.Message, "Error");
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #endregion

        //#################################################################################################

        #region PublicMethods

        public void PopulateRooftop(Dictionary<string, string> listRooftop)
        {
            try
            {
                cmbRoofTop.Items.Add(ResourceTxt.RoofTopSelect);
                cmbRoofTop.SelectedItem = ResourceTxt.RoofTopSelect;
                foreach (KeyValuePair<string, string> kvp in listRooftop)
                {
                    WpfVideoUploader.ComboBoxItem cItem = new WpfVideoUploader.ComboBoxItem(kvp.Key, kvp.Value);
                    cmbRoofTop.Items.Add(cItem);
                }
            }
            catch (Exception ex)
            {
                //Common.WriteLog("PopulateRoofTop: " + ResourceTxt.ErrorPopulatingRoofTop + ex.Message);
                Common.WriteEventLog("PopulateRoofTop: " + ResourceTxt.ErrorPopulatingRoofTop + ex.Message, "Error");
            }
        }

        #endregion

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.IsBackgroudBusy = true;
            OHome.GetVehicleDataFromServer();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Close();
            OHome.PopulateVehicleData();
            OHome.Show();
            this.IsBackgroudBusy = false;
        }

        //#################################################################################################
      
    }

    /// <summary>
    /// This class is used to update UI
    /// As there is norefresh methodfor controls
    /// </summary>
    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate() { };

        public static void Refresh(this DispatcherObject uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}
