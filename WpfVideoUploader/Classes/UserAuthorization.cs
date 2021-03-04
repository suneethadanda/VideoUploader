using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfVideoUploader
{
    public static  class UserAuthorization
    {
        public static string InventoryAuthorization { get; set; }
        public static string NonInventoryAuthorization { get; set; }
        public static string BatchUploadAuth { get; set; }

        public static string ExtractAuth { get; set; }
        public static string RemoteUploadAuth { get; set; }
        public static string totalVehicles {get; set;}
        public static string  Currentpage {get; set;}
        public static string  Totalpages {get; set;}
    }



    

}
