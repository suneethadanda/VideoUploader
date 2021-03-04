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

using System.Xml.Linq;
using System.Windows.Threading;
using System.Xml;
using System.ComponentModel;
using System.Threading;
using System.Data;
namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for ReplaceImage.xaml
    /// </summary>
    public partial class ReplaceImage : Window
    {

        public class ClsImages
        {
            public string title { get; set; }
            public string image { get; set; }
            public bool isChecked { get; set; }
        }
        public IList<ClsImages> lstReplaceImages { get; set; }
        public IList<ClsImages> lstTempImages { get; set; }
        ViewImages objViewImages = new ViewImages();
        private ViewImages objimg;
        DispatcherTimer timer;
        int ctr = 0;
        private string ImageName;
        private int ImageNumber;
        private int ImageIndex;
        private int ItemIndex;
        private bool IsReplace = false;
        private List<ViewImages.ClsImages> NewlstViewImages;
        private List<ViewImages.ClsImages> lstViewImages;
        private string ddlSelect;

        private string rooftopkey = "";
        public ReplaceImage()
        {
            InitializeComponent();
            EnableTimer();

        }

        private string stock;
        private string VIN;
        public ReplaceImage(string ImageName, int ImgNum, int ImgIndex)
        {
            InitializeComponent();
            this.ImageNumber = ImgNum;
            this.ImageName = ImageName;
            this.ImageIndex = ImgIndex;

            EnableTimer();
        }
        public ReplaceImage(string ImageName, int ImgIndex, List<ViewImages.ClsImages> NewlstViewImages, List<ViewImages.ClsImages> lstViewImages, string ddlSelect, ViewImages parenform, string stock, string VIN, int itemIndex,string rooftopkey)
        {
            InitializeComponent();
            objimg = parenform;
            this.ImageName = ImageName;
            this.ImageIndex = ImgIndex;
            this.ImageNumber = ImgIndex + 1;
            this.NewlstViewImages = NewlstViewImages;
            this.lstViewImages = lstViewImages;
            this.ddlSelect = ddlSelect;
            this.stock = stock;
            this.VIN = VIN;
            this.ItemIndex = itemIndex;
            this.rooftopkey = rooftopkey;
            EnableTimer();
        }

        private void EnableTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(timer_Tick);
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            ctr++;
            if (ctr > lstReplaceImages.Count())
            {
                ctr = 0;
            }
            PlaySlideShow(ctr);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ctr = 0;
            // PlaySlideShow(ctr);
            string strFolder = Common.ImageExtract;
            strFolder = strFolder + "\\" + ImageName;
            lstTempImages = GetImagesForReplace(strFolder, ImageNumber);
            if (lstTempImages != null)
            {
                lstImagesForReplace.ItemsSource = lstTempImages;

                BitmapImage image = new BitmapImage();
                image.BeginInit();

                string filename = (lstTempImages[0].image.ToString());
                image.UriSource = new Uri(filename);
                image.EndInit();
                ImgforReplace.Source = image;
                ImgforReplace.Stretch = Stretch.Uniform;
            }
        }
        private List<ClsImages> GetImagesForReplace(string path, int SelectedImgNumber)
        {
            var images = Directory.GetFiles(path, "*.jpg");
            lstReplaceImages = new List<ClsImages>();
            for (int i = 0; i < images.Count(); i++)
            {
                if (images[i].Contains(ImageName + "$"))
                {
                    int ReplaceImgNum = Convert.ToInt32(images[i].Split('$')[1].ToString().TrimEnd('.', 'j', 'p', 'g'));
                    if (ReplaceImgNum >= SelectedImgNumber - 11 && ReplaceImgNum <= SelectedImgNumber + 11)
                    {
                        lstReplaceImages.Add(new ClsImages { image = images[i], title = "IMAGE" + ReplaceImgNum, isChecked = true });
                    }
                }
            }
            return (List<ClsImages>)lstReplaceImages;

            #region Discussion
            //var images = Directory.GetFiles(path, "*.jpg");
            //lstReplaceImages = new List<ClsImages>();
            //for (int i = 0; i < images.Count(); i++)
            //{
            //    if (images[i].Contains(ImageName + "$"))
            //    {
            //        int ImgNum = Convert.ToInt32(images[i].Split('$')[1].ToString().TrimEnd('.', 'j', 'p', 'g'));
            //        if (ImgNum <= (2 * SelectedImgNumber))
            //        {
            //            lstReplaceImages.Add(new ClsImages { image = images[i], title = "IMAGE" + ImgNum, isChecked = true });
            //        }
            //    }
            //}
            //return (List<ClsImages>)lstReplaceImages;

            #endregion
        }
        private void PlaySlideShow(int ctr)
        {
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                string Imagefilename = ((ctr < lstReplaceImages.Count()) ? lstReplaceImages[ctr].image.ToString() : lstReplaceImages[ctr - 1].image.ToString());
                image.UriSource = new Uri(Imagefilename);
                image.EndInit();
                ImgforReplace.Source = image;
                ImgforReplace.Stretch = Stretch.Uniform;
                StatusPbar.Maximum = lstReplaceImages.Count();
                StatusPbar.Value = ctr;
            }

            catch { }
        }
        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr = 1;
                PlaySlideShow(ctr);
            }
            catch { };
        }



        private void chkAutoPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.IsEnabled = chkAutoPlay.IsChecked.Value;
                //btnFirst.Visibility = (btnFirst.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnPrevious.Visibility = (btnPrevious.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnNext.Visibility = (btnNext.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                //btnLast.Visibility = (btnLast.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
            }
            catch { }

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr--;
                if (ctr < 1)
                {
                    ctr = lstReplaceImages.Count();
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
                if (ctr > lstReplaceImages.Count())
                {
                    ctr = 1;
                }
                PlaySlideShow(ctr);
            }
            catch { }
        }
        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr = lstReplaceImages.Count();
                PlaySlideShow(ctr);
            }
            catch { }
        }
        /// <summary>
        /// to display selected item of listbox
        /// </summary>
        /// <param name="sender"></param>
        private void lstImagesForReplace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                string Imagefilename = lstReplaceImages[Convert.ToInt32(lstImagesForReplace.SelectedIndex)].image;
                image.UriSource = new Uri(Imagefilename);
                image.EndInit();
                ImgforReplace.Source = image;
                ImgforReplace.Stretch = Stretch.Uniform;
                ctr = Convert.ToInt32(lstImagesForReplace.SelectedIndex);
                StatusPbar.Value = ctr;
            }
            catch { }
        }
        private void btnCnacel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnRepalceImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ctr >= 0)
                {
                    string filename = lstReplaceImages[ctr].image;
                    //int index = lstImagesForReplace.SelectedIndex;
                    IsReplace = true;
                    objimg.Close();
                    objViewImages.LoadImages(ImageName, ImageIndex, filename, IsReplace, NewlstViewImages, lstViewImages, ddlSelect, stock, VIN, ItemIndex,rooftopkey);
                    objViewImages.Show();
                    this.Close();
                }
            }
            catch { }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnNext_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr++;
                if (ctr > lstReplaceImages.Count())
                {
                    ctr = 0;
                }
                PlaySlideShow(ctr);
            }
            catch { }
        }
        private void btnPrevious_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ctr--;
                if (ctr < 0)
                {
                    ctr = lstReplaceImages.Count();
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
                btnPrevious.Visibility = (btnPrevious.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnNext.Visibility = (btnNext.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                //btnLast.Visibility = (btnLast.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
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
                btnPrevious.Visibility = (btnPrevious.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                btnNext.Visibility = (btnNext.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
                //btnLast.Visibility = (btnLast.IsVisible == true) ? Visibility.Hidden : Visibility.Visible;
            }
            catch { }
        }


    }
}
