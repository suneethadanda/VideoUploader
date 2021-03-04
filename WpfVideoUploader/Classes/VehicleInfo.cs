using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CAVEditLib;

namespace WpfVideoUploader
{
   public class VehicleInfo
    {
        public bool IsSelected { get; set; }
        private string _inputFileName = null;
        public string InputFileName
        {
            get
            {
                return _inputFileName;
            }
            set
            {
                _inputFileName = value;
            }
        }
        private string _outFileName = null;
        public string OutputFileName
        {
            get
            {
                return _outFileName;
            }
            set
            {
                _outFileName = value;
            }
        }
        private string _vehicleKey = null;
        public string VehicleKey
        {
            get
            {
                return _vehicleKey;
            }
            set
            {
                _vehicleKey = value;
            }
        }

        private string _targetVideoWidth = null;
        public string TargetVideoWidth
        {
            get
            {
                return _targetVideoWidth;
            }
            set
            {
                _targetVideoWidth = value;
            }
        }

        private string _targetVideoHeight = null;
        public string TargetVideoHeight
        {
            get
            {
                return _targetVideoHeight;
            }
            set
            {
                _targetVideoHeight = value;
            }
        }

       private string _rooftopKey = null;
       public string RooftopKey
       {
           get{
               return _rooftopKey;
           }
           set{
               _rooftopKey = value;
           }
       }

       private string _videoTitle;
       public string VideoTitle
       {
           get{
               return _videoTitle;
           }
           set{
               _videoTitle = value;
           }
       }
      
      

       private string _description;
       public string Description
       {
           get
           {
               return _description;
           }
           set
           {
               _description = value;
           }
       }

       private string _isDefault;
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

        private ICAVConverter _converter = null;
        public ICAVConverter Converter
        {
            get
            {
                return _converter;
            }
            set
            {
                _converter = value;
            }
        }
        private string _isExtract;
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
        private string _stock;
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


        private string _VIN;
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

        private string _Categories;
        public string Categories
        {
            get
            {
                return _Categories;
            }
            set
            {
                _Categories = value;
            }
        }


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

       private string _ExtractionStatus = "";

       public string ExtractionStatus
       {
           get
           {
               return _ExtractionStatus;
           }
           set
           {
               _ExtractionStatus = value;
           }
       }

       private string _NumberOfImages = "";
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


       private string _SaveLocation = "";
       public string SaveLocation
       {
           get
           {
               return _SaveLocation;
           }
           set
           {
               _SaveLocation = value;
           }
       }


    }
}
