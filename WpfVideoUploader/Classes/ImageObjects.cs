using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfVideoUploader
{
    public static class ImageObjects
    {
        private static IList<VehicleInfo> _UplodingList;
        private static IList<VehicleInfo> _UplodedList;
        private static string _ExtractFileName;
        private static string _VehicleKey;
        private static string _Stock;
        private static string _VIN;

        private static Int32 _Progress;
        private static bool _ExtStatus;
        public static IList<VehicleInfo> UploadingList
        {
            set { _UplodingList = value; }
            get { return _UplodingList; }
        }
        public static IList<VehicleInfo> UploadedList
        {
            set { _UplodedList = value; }
            get { return _UplodedList; }
        }
        public static string ExtractFile
        {
            set { _ExtractFileName = value; }
            get { return _ExtractFileName; }
        }


        public static ExportImages exportImages
        {
            set;
            get;
        }

        public static VehicleInfoCollection ImageExportingVideoList;

        //public static bool _ExportStarted ;
        private static bool _exportstarted;
        public static bool _ExportStarted
        {
            set { _exportstarted = value; }
            get { return _exportstarted; }
        }

        private static int _imageExportprogress;
        public static int imageExportprogress
        {
            set { _imageExportprogress = value; }
            get { return _imageExportprogress; }
        }

        //public static string rooftop
        //{
        //    get;
        //    set;
        //}
        //public static string numberOfImages;

        public static string VehicleKey
        {
            set { _VehicleKey = value; }
            get { return _VehicleKey; }
        }
        public static Int32 Progress
        {
            set { _Progress = value; }
            get { return _Progress; }
        }
        public static bool ExtractStaus
        {
            set { _ExtStatus = value; }
            get { return _ExtStatus; }
        }

        public static string Stock
        {
            set { _Stock = value; }
            get { return _Stock; }
        }


        public static string VIN
        {
            set { _VIN = value; }
            get { return _VIN; }
        }
    }
}
