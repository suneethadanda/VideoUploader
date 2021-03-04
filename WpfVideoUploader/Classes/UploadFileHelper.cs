﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using WpfVideoUploader;

namespace WpfVideoUploader
{
    public static class UploadFileHelper
    {
        /// <summary>
        /// Remove the Uploaded file from XML
        /// </summary>
        /// <param name="OutputFileName"></param>
        public static void RemoveRecordFromFile(string outputFileName)
        {
            try
            {
                string strUploadFile = Common.UploadRecordFile;

                if (string.IsNullOrEmpty(strUploadFile))
                {
                    Common.WriteLog("RemoveRecordFromFile: failed to get the Upload Record file name");
                    return;
                }

                if (!File.Exists(strUploadFile))
                {
                    Common.WriteLog("RemoveRecordFromFile: Upload Record file " + strUploadFile + " not found");
                    return;
                }

                XElement doc = XElement.Load(strUploadFile);

                var result = from videos in doc.Descendants("File") where videos.Element("OutputFileName").Value == outputFileName select videos;

                foreach (XElement xEle in result.ToList())
                {
                    xEle.Remove();
                }

                doc.Save(strUploadFile);
            }
            catch (Exception ex)
            {
                Common.WriteLog("RemoveRecordFromFile: " + ResourceTxt.RemovingXMLFile + ex.Message);
            }
        }
    }
}
