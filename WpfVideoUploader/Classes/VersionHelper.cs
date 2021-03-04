using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.Windows;
using System.Threading;
using System.Windows.Forms;

namespace WpfVideoUploader
{
    public class VersionHelper
    {

        public void DownloadFileFTP()
        {

            string inputfilepath = Common.SetupFile; //DownloadsFolder + ResourceTxt.SetupFile; //Common.SetupFile; //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ResourceTxt.AppName + "\\") + "\\VideoUploader.msi";
            //string ftphost = ResourceTxt.VersionFTPHost;
            string ftphost = ResourceTxt.VersionFTPProductionHost;
            string ftpfilepath = ResourceTxt.VersionFTPFolderPath + ResourceTxt.SetupFile;
            string ftpfullpath = ftphost + ftpfilepath;
            using (WebClient request = new WebClient())
            {
               // request.Credentials = new NetworkCredential(ResourceTxt.VersionFTPUserName, ResourceTxt.VersionFTPPassword);
                request.Credentials = new NetworkCredential(ResourceTxt.VersionFTPProductionUserName, ResourceTxt.VersionFTPProductionPassword);
                byte[] fileData = request.DownloadData(ftpfullpath);
                if (File.Exists(inputfilepath))
                    File.Delete(inputfilepath);

                using (FileStream file = File.Create(inputfilepath))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }
            }

        }

        /// <summary>
        /// Method for to get latest version of the application
        /// </summary>
        /// <returns></returns>
        public string CheckLatestVersion()
        {
            string strResult = "0";
           // string ftphost = ResourceTxt.VersionFTPHost;
            string ftphost = ResourceTxt.VersionFTPProductionHost;
            string ftpfilepath = ResourceTxt.VersionFTPFolderPath + ResourceTxt.VersionFile; //"VersionFile.xml";
            string inputfilepath = Common.VersionFile; //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ResourceTxt.AppName + "\\") + "\\VersionFile.xml";
            string ftpfullpath = ftphost + ftpfilepath;
            using (WebClient request = new WebClient())
            {
                //request.Credentials = new NetworkCredential(ResourceTxt.VersionFTPUserName, ResourceTxt.VersionFTPPassword);
                request.Credentials = new NetworkCredential(ResourceTxt.VersionFTPProductionUserName, ResourceTxt.VersionFTPProductionPassword);
                byte[] fileData = request.DownloadData(ftpfullpath);
                if (File.Exists(inputfilepath))
                    File.Delete(inputfilepath);
                using (FileStream file = File.Create(inputfilepath))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }
            }
            if (File.Exists(inputfilepath))
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(inputfilepath);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    strResult = Convert.ToString(ds.Tables[0].Rows[0]["VersionNo"]);
            }
            if (File.Exists(inputfilepath))
                File.Delete(inputfilepath);
            return strResult;
        }


        public void RemoveOldInstallnewVersion()
        {
            try
            {
                string InstallFile = Common.SetupFile;  // location of setup file        
                string BatName = "c:\\windows\\temp\\update.bat";// name of the .bat file that does the uninstall/install

                // if install file is not available skip the whole process           
                if (!File.Exists(InstallFile)) return;
                try
                {
                    string BatFile = "";
                    string old_dir = Directory.GetCurrentDirectory();
                    BatFile += "@echo off\r\n";
                    BatFile += "ping localhost -n 2\r\n";
                    BatFile += "C:\\WINDOWS\\system32\\msiexec.exe /x " + ResourceTxt.ProductCode + " /qr \r\n";
                    BatFile += "C:\\WINDOWS\\system32\\msiexec.exe /i \"" + InstallFile + "\" /qr\r\n";
                    BatFile += "cd \"" + old_dir + "\"\r\n";
                    BatFile += "start \"\" " + Environment.CommandLine + "\r\n";
                    StreamWriter sw = new StreamWriter(BatName);
                    sw.Write(BatFile);
                    sw.Close();

                    // executes the batch file
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                    psi.FileName = BatName;
                    psi.Arguments = "";
                    psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo = psi;
                    p.Start();
                }
                catch { }
            }
            catch { }


        }
    }
}
