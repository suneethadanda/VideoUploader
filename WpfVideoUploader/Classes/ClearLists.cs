using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WpfVideoUploader
{
    public static class ClearLists
    {
        public static bool DeleteItemsFromList(ref VehicleInfoCollection itemList, bool deleteFromFile)
        {
            bool isItemDeleted = false;

            try
            {
                if (deleteFromFile)
                {
                    foreach (VehicleInfo toDelete in itemList)
                    {
                        UploadFileHelper.RemoveRecordFromFile(toDelete.OutputFileName);
                    }
                }
                itemList.Clear();
                isItemDeleted = true;
            }
            catch (Exception ex)
            {
                Common.WriteLog("DeleteItemsFromList: " + ex.Message);
                isItemDeleted = true;
            }

            return isItemDeleted;
        }
    }
}
