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
    /// Interaction logic for EnlargeImage.xaml
    /// </summary>
    public partial class EnlargeImage : Window
    {
        private List<ViewImages.ClsImages> lstImages;
        private int ImageNumber;
        private string ImagePath;
        DispatcherTimer timer;
        int ctr = -1;
       
        public EnlargeImage()
        {
            InitializeComponent();
        }

        public EnlargeImage(List<ViewImages.ClsImages> DopDownSelectedItems, int ImageNumber)
        {
            InitializeComponent();
            this.lstImages = DopDownSelectedItems;
            this.ImageNumber = ImageNumber;
            EnableTimer();

        }

        public EnlargeImage(List<ViewImages.ClsImages> DropDownSelectedItems, int ImageNumber, string Imagepath)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.lstImages = DropDownSelectedItems;
            this.ImageNumber = ImageNumber;
            this.ImagePath = Imagepath;
            EnableTimer();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EnableTimer()
        {
            try
            {
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 2);
                timer.Tick += new EventHandler(timer_Tick);
            }
            catch { }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                ctr++;
                if (ctr > lstImages.Count())
                {
                    ctr = 0;
                }
                PlaySlideShow(ctr);
            }
            catch { }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ctr = -1;
           
            PlaySlideShow(ctr);
            lstViewSelectedImages.ItemsSource = lstImages;
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            string Imagefilename = ImagePath;
            image.UriSource = new Uri(Imagefilename);
            image.EndInit();
            ImgforSelected.Source = image;
            ImgforSelected.Stretch = Stretch.Uniform;
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
                string Imagefilename = ((ctr < lstImages.Count()) ? lstImages[ctr].image.ToString() : lstImages[ctr - 1].image.ToString());
                image.UriSource = new Uri(Imagefilename);
                image.EndInit();
                ImgforSelected.Source = image;
                ImgforSelected.Stretch = Stretch.Uniform;
                StausPbar.Maximum = lstImages.Count();
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
                    ctr = lstImages.Count();
                }
                PlaySlideShow(ctr);
            }
            catch { }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr++;
                if (ctr > lstImages.Count())
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
                ctr = lstImages.Count();
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
                btnnPreviuos.Visibility = (btnnPreviuos.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                butnNext.Visibility = (butnNext.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
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
                string Imagefilename = lstImages[SelectedIndex].image;
                image.UriSource = new Uri(Imagefilename);
                image.EndInit();
                ImgforSelected.Source = image;
                ImgforSelected.Stretch = Stretch.Uniform;
                ctr = SelectedIndex;
                StausPbar.Value = ctr;
            }
            catch { }
        }
        //private void btnClose_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr++;
                if (ctr > lstImages.Count())
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
                    ctr = lstImages.Count();
                }
                PlaySlideShow(ctr);
            }
            catch { }
        }


        /// <summary>
        /// to start slide show
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                timer.IsEnabled = true;
               
                btnPlay.Visibility = (btnPlay.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnPause.Visibility = (btnPause.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;


                //btnFirst.Visibility = (btnFirst.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnnPreviuos.Visibility = (btnnPreviuos.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                butnNext.Visibility = (butnNext.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                //btnLast.Visibility = (btnLast.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
            }
            catch { }
        }

        /// <summary>
        /// to stop slide show
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.IsEnabled = false;
                btnPlay.Visibility = (btnPlay.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnPause.Visibility = (btnPause.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;

                //btnFirst.Visibility = (btnFirst.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnnPreviuos.Visibility = (btnnPreviuos.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                butnNext.Visibility = (butnNext.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                // btnLast.Visibility = (btnLast.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
            }
            catch { }
        }

        
    }
}

