using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WpfVideoUploader
{
    public static class INDEX
    {
        public const int VEHICLE_KEY = 0;
        public const int INPUT_FILE_NAME = 1;
        public const int OUTPUT_FILE_NAME = 2;
        public const int ROOFTOP_KEY = 3;
        public const int VIDEO_TITLE = 4;
        public const int VIDEO_DESCRIPTION = 5;
        public const int VIDEO_ISDEFAULT = 6;
        public const int VIDEO_WIDTH = 7;
        public const int VIDEO_HEIGHT = 8;
    }

    public static class XNAME
    {
        public const string UPLOADING_FILE = "UplodingFile";
        public const string FILE = "File";
        public const string VEHICLE_KEY = "VehicleKey";
        public const string SOURCE_FILE_NAME = "SourceFileName";
        public const string OUTPUT_FILE_NAME = "OutputFileName";
        public const string ROOF_TOP_KEY = "RooftopKey";
        public const string VIDEO_TITLE = "VideoTitle";
        public const string DESCRIPTION = "Description";
        public const string IS_DEFAULT = "IsDefault";
        public const string VIN = "VIN";
        public const string VIDEOTYPE = "VIDEOTYPE";
        public const string ExTRACTED_VIDEOS = "EXTRACEDVIDEOS";
        public const string EXPORTEDSTATUS = "EXPORTEDSTATUS";
        public const string CURRENT_EXPORTING = "CURRENTEXPORTING";
        public const string NUMBER_OF_IMAGES = "NUMBEROFIMAGES";
        public const string SAVE_LOCATION = "SAVELOCATION";
        // For getting latest version
        public const string VERSION_NUMBER = "versionNo";
        public const string DOWNLOAD_LOCATION = "downloadLocation";

        public const string IS_EXTRACT = "IsExtract";

        public const string Stock = "Stock";
    }
    public static class testCll
{
      public static string GetInnerXml(this XElement element)
        {
            var reader = element.CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
        }
}
}
