using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
 
namespace WpfVideoUploader
{
    public static class Common
    {
        public static Home OHome { get; set; }


        //Creating the extern function.
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// Connects to the FTP Server and get the required data
        /// </summary>
        /// <param name="pstrPostData"></param>
        /// <returns></returns>
        public static string GetServerData(string postData)
        {
            string result = null;
            try
            {
                var encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(postData);
                //var myRequest = (HttpWebRequest)WebRequest.Create(ResourceTxt.DevRequestUri);
                var myRequest = (HttpWebRequest)WebRequest.Create(ResourceTxt.ProductionRequestURI);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length; 
                //myRequest.Timeout = -1; 
                var newStream = myRequest.GetRequestStream(); 
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                var response = myRequest.GetResponse();
                //myRequest.KeepAlive = false;
                var responseStream = response.GetResponseStream(); 
                var responseReader = new StreamReader(responseStream); 
                result = responseReader.ReadToEnd();
            }
            catch (System.Net.WebException ex)
            {
                Common.WriteLog("GetServerData: " + ex.Message);
                return "WebError";
            }
            catch (Exception ex)
            {
                Common.WriteLog("GetServerData: " + ex.Message);
                return "Error";
            }

            return result;
        }





        public static string LogFile
        {
            get { return (Common.GetFileName(ResourceTxt.LogFile)); }
        }
        //cva
        public static string VersionFile
        {
            get { return (Common.GetFileName(ResourceTxt.VersionFile)); }
        }
        public static string SetupFile
        {
            get { return (Common.GetFileName(ResourceTxt.SetupFile)); }
        }
        public static string EventFile
        {
            get { return (Common.GetEventFileName(ResourceTxt.EventFile)); }
        }
        public static string SettingsFile
        {
            get { return (Common.GetFileName(ResourceTxt.SettingsFile)); }
        }

        public static string VedioFolder
        {
            get { return (Common.GetFolderPathInfo(ResourceTxt.AppName)); }
        }

        public static string UploadRecordFile
        {
            get { return (Common.GetFileName(ResourceTxt.UploadRecordFile)); }
        }

        public static string VideoInfoFile
        {
            get { return (Common.GetFileName(ResourceTxt.VideoInfoXmlFile)); }
        }


        public static string ExtractedImagesFile
        {
            get
            {
                return (Common.GetFileName(ResourceTxt.ExTractedVideosInfo));
            }
        }

        public static string CurrentExporting
        {
            get
            {
                return (Common.GetFileName(ResourceTxt.CurrentExporting));
            }
        }

        public static string AddVideoInfoFile
        {
            get { return (Common.GetFileName(ResourceTxt.AddVideoInfoXmlFile)); }
        }

        public static string ImageInfofile
        {
            get { return (Common.GetFileName(ResourceTxt.ImageInfoXmlFile)); }
        }

        public static string EventFileXls
        {
            get { return (Common.GetAppNamePath(ResourceTxt.LogFileXls)); }
        }

        //Get the Image extract folder path
        public static string ImageExtract
        {
            get { return (GetExtractFolder(ResourceTxt.ImageExtract)); }
        }


        public static string RecordedVideos
        {
            get { return (GetRecordedVideosFolder(ResourceTxt.RecordedVideos)); }
        }
        public static string DestinationtoSave
        {
            get { return (GetDestinationFolder(ResourceTxt.DestinationtoSave)); }
        }


        /// <summary>
        /// Get the requested file name from the folder where all the Application related files are stored.
        /// If the folder dosn't exist, then creates the folder
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        private static string GetFileName(string File)
        {
            string strAppName = ResourceTxt.AppName;
            string strFileName = strAppName + "\\" + File;

            DirectoryInfo dirUserData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), strAppName + "\\")))
            {
                dirUserData.CreateSubdirectory(strAppName);
            }

            var strFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), strFileName);

            return strFile;
        }
        private static string GetFolderPathInfo(string strPath)
        {
            string strAppName = ResourceTxt.AppName;
            string strFileName = strPath;
            DirectoryInfo dirUserData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            if (!Directory.Exists(strFileName))
            {
                dirUserData.CreateSubdirectory(strFileName);
            }
            strFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), strFileName);

            return strFileName;
        }
        private static string GetAppNamePath(string strPath)
        {
            string strAppName = ResourceTxt.AppName;
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), strAppName + "\\") + strPath;
        }
        //cva
        /// <summary>
        /// To get the file path of event log file 
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        private static string GetEventFileName(string strFile)
        {
            string strAppName = ResourceTxt.AppName;
            string strFileName = strAppName + "\\" + strFile;
            string strReturnFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), strFileName);
            if (!File.Exists(strReturnFile))
            {
                string xmlPath = strReturnFile;
                XmlDocument xmlDoc = null;
                xmlDoc = new XmlDocument();
                XmlNode settigsNode = null;
                XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(docNode);

                settigsNode = xmlDoc.CreateElement("EventLog");
                xmlDoc.AppendChild(settigsNode);
                xmlDoc.Save(xmlPath);
            }
            return strReturnFile;
        }
        public static string GetVideoSettingInfo(string videoLocation)
        {
            string xmlPath = Common.SettingsFile;
            string strDefaulePath = string.Empty;

            if (File.Exists(xmlPath))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(xmlPath);
                XmlNode node;
                node = myXmlDocument.DocumentElement[videoLocation];

                if (node.Name == videoLocation)
                {
                    strDefaulePath = node.InnerText.ToString();
                }
            }

            return strDefaulePath;
        }

        public static string GetDefaultCheckValue(string DefaultNode)
        {
            string xmlPath = Common.SettingsFile;
            string strDefaulePath = string.Empty;

            if (File.Exists(xmlPath))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(xmlPath);
                XmlNode node;
                node = myXmlDocument.DocumentElement[DefaultNode];

                if ((node != null) && node.Name == DefaultNode)
                {
                    strDefaulePath = node.InnerText.ToString();
                }
                else
                    strDefaulePath = "False";
            }

            return strDefaulePath;
        }
        public static bool SetDefaultCheckValue(string DefaultNode,string value)
        {
            string xmlPath = Common.SettingsFile;
            bool result = false;

            if (File.Exists(xmlPath))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(xmlPath);
                XmlNode node;
                node = myXmlDocument.DocumentElement[DefaultNode];

                if ((node != null) && node.Name == DefaultNode)
                {
                    node.InnerText = value;
                    myXmlDocument.Save(xmlPath);
                    result = true;
                }
                else
                {
                    XmlNode root = myXmlDocument.DocumentElement;
                    XmlElement elem = myXmlDocument.CreateElement(DefaultNode);
                    // defaultnode = myXmlDocument.CreateElement("DefaultVideoAllwaysOn");
                    elem.InnerText = value;
                    root.AppendChild(elem);
                    myXmlDocument.Save(xmlPath);
                }
            }

            return result;
        }

        private static string GetExtractFolder(string strFile)
        {
            string strFileName = GetAppNamePath(ResourceTxt.ImageExtract);
            string strReturnFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), strFileName);
            if (!Directory.Exists(strFileName))
            {
                Directory.CreateDirectory(strFileName);
            }
            return strReturnFile;
        }

        private static string GetRecordedVideosFolder(string strFile)
        {
            string strFileName = GetAppNamePath(ResourceTxt.RecordedVideos);
            string strReturnFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), strFileName);
            if (!Directory.Exists(strFileName))
            {
                Directory.CreateDirectory(strFileName);
            }
            return strReturnFile;
        }


        private static string GetDestinationFolder(string strFile)
        {
            string strFileName = GetAppNamePath(ResourceTxt.DestinationtoSave);
            string strReturnFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), strFileName);
            if (!Directory.Exists(strFileName))
            {
                Directory.CreateDirectory(strFileName);
            }
            return strReturnFile;
        }



        private static string CreateFolderForImageExtract(string strFolder)
        {
            string strReturn = ImageExtract;
            strReturn = Path.Combine(strReturn, "//" + strFolder);
            if (!Directory.Exists(strReturn))
            {
                Directory.CreateDirectory(strReturn);
            }
            return strReturn;
        }


        private static string CreateFolderForSaveImage(string strFolder)
        {
            string strReturn = DestinationtoSave;
            strReturn = Path.Combine(strReturn, "//" + strFolder);
            if (!Directory.Exists(strReturn))
            {
                Directory.CreateDirectory(strReturn);
            }
            return strReturn;
        }

        public static void WriteLog(string logText)
        {
            try
            {
                // Create a writer and open the file:
                StreamWriter log;
                string logFile = LogFile;
                if (!File.Exists(logFile))
                    log = new StreamWriter(logFile);
                else
                    log = File.AppendText(logFile);
                log.WriteLine(DateTime.Now + "|" + logText);
                log.Close();
            }
            catch (System.UnauthorizedAccessException)
            {
                //MessageBox.Show(ResourceTxt.AccessDeniedError, "Access denied");

                Alert1 objalert = new Alert1(ResourceTxt.AccessDeniedError);
                objalert.ShowDialog();


            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// Write Event log
        /// </summary>
        /// <param name="logText"></param>
        public static void WriteEventLog(string strMessage, string strType)
        {
            try
            {

                string logFile = EventFile;

                if (File.Exists(logFile))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(logFile);
                    XmlNode node;
                    node = xmlDoc.DocumentElement;
                    XmlNode OutputVideoNode = xmlDoc.CreateElement("EventData");
                    XmlNode dateNode = xmlDoc.CreateElement("CreatedData");
                    dateNode.InnerText = DateTime.Now.ToString();
                    XmlNode typeNode = xmlDoc.CreateElement("EventType");
                    typeNode.InnerText = strType;
                    XmlNode MsgNode = xmlDoc.CreateElement("Message");
                    MsgNode.InnerText = strMessage;
                    XmlNode VerNode = xmlDoc.CreateElement("Version");
                    VerNode.InnerText = ResourceTxt.CurrentVersion;
                    OutputVideoNode.AppendChild(dateNode);
                    OutputVideoNode.AppendChild(typeNode);
                    OutputVideoNode.AppendChild(MsgNode);
                    OutputVideoNode.AppendChild(VerNode);
                    node.AppendChild(OutputVideoNode);
                    xmlDoc.Save(logFile);
                }

            }
            catch (System.UnauthorizedAccessException)
            {
                // MessageBox.Show(ResourceTxt.AccessDeniedError, "Access denied");
                Alert objalert = new Alert(ResourceTxt.AccessDeniedError);
                objalert.ShowDialog();
                if (objalert.close)
                {
                    //System.Windows.Application.Current.Shutdown();

                }
            }
            catch
            {
                return;
            }
        }
        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        // Taken from MyUtil
        public static int StrToNumberWithDefValue(string value, int def)
        {
            if (value == null)
                return def;
            int result = int.Parse(value);
            return result > 0 ? result : def;
        }
    }


    public class ComboBoxItem
    {
        public string Value;
        public string Text;

        public ComboBoxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
