using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
//using System.Windows.Forms;
using System.Windows.Controls;


namespace WpfVideoUploader
{
    public static class XmlDataQueryHelper
    {
        public static string GetPendingRecordsOnCheck(string serverXmlData, string currentModel, string currentMake, ContentControl labelNoOfRecords)
        {
            try
            {
                XDocument xDocAll = XDocument.Parse(serverXmlData);

                var records = from vehicleData in xDocAll.Root.Elements("vehicle")
                              where (string)vehicleData.Attribute("Pending") == "1"
                              select vehicleData;

                /* July 27*/
                if (currentModel != ResourceTxt.VehicleModelAll)
                {
                    records = from vehicleData in xDocAll.Root.Elements("vehicle")
                              where (string)vehicleData.Attribute("Pending") == "1" && (string)vehicleData.Attribute("model") == currentModel
                              select vehicleData;
                }

                if (currentMake != ResourceTxt.VehicleMakeAll)
                {
                    records = from vehicleData in xDocAll.Root.Elements("vehicle")
                              where (string)vehicleData.Attribute("Pending") == "1" && (string)vehicleData.Attribute("make") == currentMake
                              select vehicleData;
                }

                if ((currentModel != ResourceTxt.VehicleModelAll) && (currentMake != ResourceTxt.VehicleMakeAll))
                {
                    records = from vehicleData in xDocAll.Root.Elements("vehicle")
                              where (string)vehicleData.Attribute("Pending") == "1" &&
                              (string)vehicleData.Attribute("model") == currentModel &&
                              (string)vehicleData.Attribute("make") == currentMake
                              select vehicleData;
                }
                /*....*/

                XDocument xDocResults = null;
                string strResult = string.Empty;

                foreach (var record in records)
                {
                    xDocResults = new XDocument(record);
                    strResult += xDocResults.ToString();
                }

                int noOfRecords = records.Count();
                labelNoOfRecords.Content = noOfRecords.ToString();

                strResult = "<Vehicles>" + strResult;
                strResult += "</Vehicles>";

                return strResult;
            }
            catch (Exception ex)
            {
                Common.WriteLog("GetPendingRecordsOnCheck: " + ex.Message);
                return null;
            }
        }

        public static string GetPendingRecordsOnUNCheck(string serverXmlData, string currentModel, string currentMake, ContentControl labelNoOfRecords)
        {
            try
            {
                XDocument xDocAll = XDocument.Parse(serverXmlData);

                var records = from vehicleData in xDocAll.Root.Elements("vehicle")
                              where (string)vehicleData.Attribute("model") == currentModel ||
                              (string)vehicleData.Attribute("make") == currentMake
                              select vehicleData;

                /* July 27*/
                if (currentModel != ResourceTxt.VehicleModelAll)
                {
                    records = from vehicleData in xDocAll.Root.Elements("vehicle")
                              where (string)vehicleData.Attribute("model") == currentModel
                              select vehicleData;
                }

                if (currentMake != ResourceTxt.VehicleMakeAll)
                {
                    records = from vehicleData in xDocAll.Root.Elements("vehicle")
                              where (string)vehicleData.Attribute("make") == currentMake
                              select vehicleData;
                }

                if ((currentModel != ResourceTxt.VehicleModelAll) && (currentMake != ResourceTxt.VehicleMakeAll))
                {
                    records = from vehicleData in xDocAll.Root.Elements("vehicle")
                              where (string)vehicleData.Attribute("model") == currentModel &&
                              (string)vehicleData.Attribute("make") == currentMake
                              select vehicleData;
                }
                /*....*/

                XDocument xDocResults = null;
                string strResult = string.Empty;

                foreach (var record in records)
                {
                    xDocResults = new XDocument(record);
                    strResult += xDocResults.ToString();
                }

                int noOfRecords = records.Count();
                labelNoOfRecords.Content = noOfRecords.ToString();

                strResult = "<Vehicles>" + strResult;
                strResult += "</Vehicles>";

                return strResult;
            }
            catch (Exception ex)
            {
                Common.WriteLog("GetPendingRecordsOnCheck: " + ex.Message);
                return null;
            }
        }
    }
}
