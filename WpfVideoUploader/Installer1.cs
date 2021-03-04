using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.IO;
using System.Windows;
using System.Diagnostics;
using System.Reflection;



namespace WpfVideoUploader
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        public Installer1()
        {
           
            InitializeComponent();
        }
        public override void Install(IDictionary stateSaver)
        {

            base.Install(stateSaver);
        }


        public override void Commit(IDictionary savedState)
        {

            base.Commit(savedState);
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }






        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            try
            {
                string settingsPath = Common.SettingsFile;
                string uploadRecordFilePath = Common.UploadRecordFile;
                string videoInfoFilePath = Common.VideoInfoFile;
                string strAddVideoInfoFile = Common.AddVideoInfoFile;
                string logFilePath = Common.LogFile;
                string eventFile = Common.EventFile;
                string setupFile = Common.SetupFile;
                string versionFile = Common.VersionFile;
                string strLog = Common.EventFileXls;
                string strExtractFolder = Common.ImageExtract;
                string strSvaedImagesFolder = Common.DestinationtoSave;

                string strRecordedVideosFolder = Common.RecordedVideos;

                string strExtraVideoInfoFile = Common.ExtractedImagesFile;
                string strVideofolder = Common.VedioFolder;
                string appfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + ResourceTxt.AppName;
                //MessageBox.Show(strVideofolder);

                //if (File.Exists(setupFile))
                //    File.Delete(setupFile);

                if (File.Exists(versionFile))
                    File.Delete(versionFile);


                //MessageBoxResult msgRemoveData = MessageBox.Show(ResourceTxt.UninstalMessage, ResourceTxt.UnInstalCaption, MessageBoxButton.YesNo, MessageBoxImage.Information);
                //if (msgRemoveData == MessageBoxResult.No)
                //{
                //    MessageBoxResult msgRemoveConfirm = MessageBox.Show(ResourceTxt.UninstalMessageConfirm, ResourceTxt.UnInstalCaptionConfirm, MessageBoxButton.YesNo, MessageBoxImage.Question);
                //    if (msgRemoveConfirm == MessageBoxResult.Yes)
                //    {

                        //if (File.Exists(strExtraVideoInfoFile))
                        //{

                        //    File.Delete(strExtraVideoInfoFile);
                        //}


                        //if (File.Exists(settingsPath))
                        //    File.Delete(settingsPath);

                        //if (File.Exists(uploadRecordFilePath))
                        //    File.Delete(uploadRecordFilePath);

                        //if (File.Exists(strAddVideoInfoFile))
                        //    File.Delete(strAddVideoInfoFile);


                        //if (File.Exists(logFilePath))
                        //    File.Delete(logFilePath);

                        //if (File.Exists(eventFile))
                        //    File.Delete(eventFile);

                        //if (File.Exists(strLog))
                        //    File.Delete(strLog);

                        //if (File.Exists(videoInfoFilePath))
                        //    File.Delete(videoInfoFilePath);

                        //if (Directory.Exists(strExtractFolder))
                        //{
                        //    if (Directory.EnumerateDirectories(strExtractFolder).Count() > 0)
                        //    {
                        //        DirectoryInfo objfolder = new DirectoryInfo(strExtractFolder);
                        //        foreach (DirectoryInfo dir in objfolder.GetDirectories())
                        //        {
                        //            foreach (FileInfo fi in dir.GetFiles())
                        //            {
                        //                fi.Delete();

                        //            }
                        //            dir.Delete();
                        //        }


                        //    }
                        //    Directory.Delete(strExtractFolder);
                        //}




                        //if (Directory.Exists(strSvaedImagesFolder))
                        //{
                        //    if (Directory.GetDirectories(strSvaedImagesFolder).Count() > 0)
                        //    {

                        //        DirectoryInfo objfolder = new DirectoryInfo(strSvaedImagesFolder);
                        //        foreach (DirectoryInfo dir in objfolder.GetDirectories())
                        //        {
                        //            foreach (FileInfo fi in dir.GetFiles())
                        //            {
                        //                fi.Delete();

                        //            }


                        //            dir.Delete();
                        //        }
                        //    }
                        //    Directory.Delete(strSvaedImagesFolder);
                        //}




                        ////MessageBox.Show("ok1");
                        //if (Directory.Exists(strVideofolder))
                        //{

                        //    if (Directory.EnumerateFiles(strVideofolder).Count() > 0)
                        //    {
                        //        DirectoryInfo objfolder = new DirectoryInfo(strVideofolder);
                        //        foreach (FileInfo fi in objfolder.GetFiles())
                        //        {

                        //            fi.Delete();
                        //        }
                        //    }

                        //    Directory.Delete(strVideofolder);
                        //}



                        //// MessageBox.Show("ok3");
                        //if (Directory.Exists(strRecordedVideosFolder))
                        //{
                        //    if (Directory.GetFiles(strRecordedVideosFolder).Count() > 0)
                        //    {
                        //        DirectoryInfo objfolder = new DirectoryInfo(strRecordedVideosFolder);
                        //        foreach (FileInfo fi in objfolder.GetFiles())
                        //        {
                        //            //MessageBox.Show("Success");
                        //            fi.Delete();
                        //        }

                        //    }
                        //    Directory.Delete(strRecordedVideosFolder);
                        //}




                        ////MessageBox.Show("ok2");

                        //if (Directory.Exists(appfolder))
                        //{
                        //    if (Directory.EnumerateDirectories(appfolder).Count() > 0)
                        //    {
                        //        DirectoryInfo objfolder = new DirectoryInfo(appfolder);
                        //        foreach (DirectoryInfo dir in objfolder.GetDirectories())
                        //        {
                        //            foreach (DirectoryInfo innerdir in dir.GetDirectories())
                        //            {

                        //                foreach (FileInfo fi in innerdir.GetFiles())
                        //                {
                        //                    fi.Delete();
                        //                }
                        //                innerdir.Delete();
                        //            }

                        //            dir.Delete();
                        //        }
                        //    }
                        //    Directory.Delete(appfolder);
                        //}






                //    }
                //}



            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }


    }
}
