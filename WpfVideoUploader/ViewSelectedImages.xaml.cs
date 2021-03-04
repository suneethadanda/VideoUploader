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

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for ViewSelectedImages.xaml
    /// </summary>
    public partial class ViewSelectedImages : Window
    {
        private string _ImageName = string.Empty;
        public string ImageName
        {
            get { return _ImageName; }
            set { _ImageName = value; }
        }

        public List<ViewImages.ClsImages> lstSelectedImages { get; set; }
        DispatcherTimer timer;
        int ctr = 0;
        public ViewSelectedImages()
        {
            InitializeComponent();
            EnableTimer();
        }
        /// <summary>
        /// Getting list of checked images from ViewImages screen
        /// </summary>
        /// <param name="lstCheckedImages"></param>
        public ViewSelectedImages(List<ViewImages.ClsImages> lstCheckedImages)
        {
            InitializeComponent();
            lstSelectedImages = lstCheckedImages;
            EnableTimer();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void EnableTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(timer_Tick);
        }
        void timer_Tick(object sender, EventArgs e)
        {
            ctr++;
            if (ctr > lstSelectedImages.Count())
            {
                ctr = 0;
            }
            PlaySlideShow(ctr);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ctr = 0;
            PlaySlideShow(ctr);
            lstViewSelectedImages.ItemsSource = lstSelectedImages;
        }

        /// <summary>
        /// show images navigation as user required (like click on Next or Prevous buttons)
        /// </summary>
        /// <param name="ctr"></param>
        private void PlaySlideShow(int ctr)
        {
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                string Imagefilename = ((ctr < lstSelectedImages.Count()) ? lstSelectedImages[ctr].image.ToString() : lstSelectedImages[ctr - 1].image.ToString());
                image.UriSource = new Uri(Imagefilename);
                image.EndInit();
                ImgforSelected.Source = image;
                ImgforSelected.Stretch = Stretch.Uniform;
                StausPbar.Maximum = lstSelectedImages.Count();
                StausPbar.Value = ctr;
            }
            catch { }
        }
        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr = 0;
                PlaySlideShow(ctr);
            }
            catch { }

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr--;
                if (ctr < 0)
                {
                    ctr = lstSelectedImages.Count();
                }
                PlaySlideShow(ctr);
            }
            catch{ }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr++;
                if (ctr > lstSelectedImages.Count())
                {
                    ctr = 0;
                }
                PlaySlideShow(ctr);
            }
            catch { }
        }
        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr = lstSelectedImages.Count();
                PlaySlideShow(ctr);
            }
            catch { }
        }

        /// <summary>
        /// for Images Slide show
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.IsEnabled = chkAutoPlay.IsChecked.Value;
                btnFirst.Visibility = (btnFirst.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnnPrevious.Visibility = (btnnPrevious.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnnNext.Visibility = (btnnNext.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnLast.Visibility = (btnLast.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
            }
            catch { }
            
        }
        /// <summary>
        /// to display selected item of listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void lstViewSelectedImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                int SelectedIndex = lstViewSelectedImages.SelectedIndex;
                string Imagefilename = lstSelectedImages[SelectedIndex].image;
                image.UriSource = new Uri(Imagefilename);
                image.EndInit();
                ImgforSelected.Source = image;
                ImgforSelected.Stretch = Stretch.Uniform;
                ctr = SelectedIndex;
                StausPbar.Value = ctr;
            }
            catch { }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr++;
                if (ctr > lstSelectedImages.Count())
                {
                    ctr = 0;
                }
                PlaySlideShow(ctr);
            }
            catch { }
        }
        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnPrevoius1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr--;
                if (ctr < 0)
                {
                    ctr = lstSelectedImages.Count();
                }
                PlaySlideShow(ctr);
            }
            catch { }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                timer.IsEnabled = true;

                btnPlay.Visibility = (btnPlay.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnPause.Visibility = (btnPause.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                
                
                //btnFirst.Visibility = (btnFirst.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnnPrevious.Visibility = (btnnPrevious.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnnNext.Visibility = (btnnNext.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                // btnLast.Visibility = (btnLast.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
            }
            catch { }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.IsEnabled = false;

                btnPlay.Visibility = (btnPlay.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnPause.Visibility = (btnPause.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;

                //btnFirst.Visibility = (btnFirst.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnnPrevious.Visibility = (btnnPrevious.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnnNext.Visibility = (btnnNext.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                //btnLast.Visibility = (btnLast.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
            }
            catch { }
        }
    }
}
