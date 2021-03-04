using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfVideoUploader
{
    public class ImagesInfo
    {



        private string _ImageName = null;
        public string ImageName
        {
            get 
            {
                return _ImageName; 
            }
            set 
            { 
                _ImageName=value;
            }
        }
        private string _ImagePath = null;
        public string ImagePath
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                _ImagePath = value;
            }
        }
        private bool _IsChecked = true;
        public bool IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
            }

        }

    }
}
