using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CAVEditLib;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;
using System.Net.Mail;
using System.Net;
using System.Windows.Controls.Primitives;
using Outlook = Microsoft.Office.Interop.Outlook; 

namespace WpfVideoUploader
{
    /// <summary>
    /// Interaction logic for LogDetails.xaml
    /// </summary>
    public partial class LogDetails : Window
    {
        DataTable dtable = new DataTable();
        public LogDetails()
        {
            InitializeComponent();
        }

        #region WindowLoad event
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSet dsData = new DataSet();
                dsData.ReadXml(Common.EventFile, XmlReadMode.InferSchema);
                grdlogDetils.ItemsSource = dsData.Tables[0].DefaultView;
                dtable = dsData.Tables[0];
                frmDate.Text = DateTime.Now.ToShortDateString();
                toDate.Text = DateTime.Now.ToShortDateString();
                cmbLogType.Items.Clear();
                cmbLogType.Items.Add("All");
                cmbLogType.Items.Add("Information");
                cmbLogType.Items.Add("Warning");
                cmbLogType.Items.Add("Error");
                cmbLogType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Common.WriteEventLog("LogDetails_LogDetailsLoad:" + ex.Message, "Error");
            }
        }
        #endregion

        #region Close Button
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = false;
            this.Close();
        }
        #endregion

        #region GetDetails
        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Common.EventFile, XmlReadMode.InferSchema);
                string strtodate = string.Empty,strfromDate=string.Empty;
                if (!string.IsNullOrEmpty(toDate.Text) && !string.IsNullOrEmpty(frmDate.Text))
                {
                    strfromDate = frmDate.Text;
                    strtodate = Convert.ToDateTime(toDate.Text).ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                }
                else
                {
                    strfromDate =DateTime.Now.ToShortDateString();
                    strtodate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                }
                    var results = from table1 in ds.Tables[0].AsEnumerable()
                                  where Convert.ToDateTime(table1.Field<string>("CreatedData")) >= Convert.ToDateTime(strfromDate) && Convert.ToDateTime(table1.Field<string>("CreatedData")) <= Convert.ToDateTime(strtodate)
                                  select new { CreatedData = table1.Field<string>("CreatedData"), EventType = table1.Field<string>("EventType"), Message = table1.Field<string>("Message"), Version = table1.Field<string>("Version") };
                
                if (cmbLogType.SelectedIndex != 0)
                     results = from table1 in ds.Tables[0].AsEnumerable()
                                  where Convert.ToDateTime(table1.Field<string>("CreatedData")) >= Convert.ToDateTime(strfromDate) && Convert.ToDateTime(table1.Field<string>("CreatedData")) <= Convert.ToDateTime(strtodate) && table1.Field<string>("EventType").Equals(cmbLogType.SelectedItem)
                                  select new { CreatedData = table1.Field<string>("CreatedData"), EventType = table1.Field<string>("EventType"), Message = table1.Field<string>("Message"), Version = table1.Field<string>("Version") };
                    grdlogDetils.ItemsSource = results;
                
                DataRow drow;
                dtable = new DataTable();
                if (dtable.Columns.Count == 0)
                {
                    dtable.Columns.Add("CreatedData");
                    dtable.Columns.Add("EventType");
                    dtable.Columns.Add("Message");
                    dtable.Columns.Add("Version");
                }
                foreach (var obj in results)
                {
                    drow = dtable.NewRow();
                    drow["CreatedData"] = obj.CreatedData;
                    drow["EventType"] = obj.EventType;
                    drow["Version"] = obj.Version;
                    drow["Message"] = obj.Message;
                    dtable.Rows.Add(drow);
                }

            }
            catch (Exception ex)
            {
                Common.WriteEventLog("LogDetails_btnReload_Click:" + ex.Message, "Error");
            }
        }
        #endregion

        #region Excel file Generation
        /// <summary>
        /// To Generate Excel file of grid data
        /// </summary>
        private void GenerateExcel(string strtype)
        {
            try
            {
                Spinner.Visibility = Visibility.Visible;
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 20;
                for (int icol = 1; icol < grdlogDetils.Columns.Count + 1; icol++)
                    ExcelApp.Cells[1, icol] = grdlogDetils.Columns[icol - 1].Header;
                for (int irow = 0; irow < dtable.Rows.Count - 1; irow++)
                    for (int icol = 0; icol < grdlogDetils.Columns.Count; icol++)
                        ExcelApp.Cells[irow + 2, icol + 1] = dtable.Rows[irow][icol];
                ExcelApp.Columns.AutoFit();
                string fileName = Common.EventFileXls;
                //if (File.Exists(fileName))
                //    File.Delete(fileName);
                ExcelApp.ActiveWorkbook.SaveCopyAs(fileName);
                ExcelApp.ActiveWorkbook.Saved = true;
                if (strtype.Equals("Automatic Mail"))
                    SendMail(fileName);
                else if (strtype.Equals("Attach to Outlook"))
                {
                    try
                    {
                        //out look
                        Outlook.Application objApp = new Outlook.Application();
                        Outlook.MailItem mail = null;
                        mail = (Outlook.MailItem)objApp.CreateItem(Outlook.OlItemType.olMailItem);
                        mail.Attachments.Add((object)fileName, Outlook.OlAttachmentType.olEmbeddeditem, 1, (object)"Attachment");
                        mail.To = ResourceTxt.MailTo;
                        mail.Display();
                        //end outlook
                        if (File.Exists(fileName))
                            File.Delete(fileName);
                    }
                    catch (Exception e)
                    {
                        Common.WriteEventLog("Outlook: " + e.Message, "Error");
                    }
                }
                else if (strtype.Equals("Save in Location"))
                {
                    try
                    {
                        System.Windows.Forms.SaveFileDialog opDialog = new System.Windows.Forms.SaveFileDialog();
                        opDialog.Filter = "Excel Files|*.xls;*.xlsx;";
                        opDialog.RestoreDirectory = true;
                        if (opDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                        {
                            Spinner.Visibility = Visibility.Hidden;
                            return;
                        }
                        ExcelApp.ActiveWorkbook.SaveCopyAs(opDialog.FileName);
                    }
                    catch (Exception exec)
                    { Common.WriteEventLog("SaveLocation: " + exec.Message, "Error"); }
                }
                ExcelApp.Quit();
                Spinner.Visibility = Visibility.Hidden;
            }
            catch (Exception ex) { Spinner.Visibility = Visibility.Hidden; Common.WriteEventLog("GenerateExcel: " + ex.Message, "Error"); }
            
        }
        
        #endregion

        #region Send Mail
        /// <summary>
        /// To send attachement mail
        /// </summary>
        /// <param name="filepath"></param>
        private bool SendMail(string filepath)
        {
            MailMessage msg = new MailMessage();
            Attachment attach = new Attachment(filepath);
            bool flag=true;

            try
            {
                var fromAddress = new MailAddress(ResourceTxt.MailFrom, "Admin");
                var toAddress = new MailAddress(ResourceTxt.MailTo, ResourceTxt.MailTo);
                string fromPassword = ResourceTxt.MailPassword;
                string subject = "EventLogFile";
                string body = "Hi,<br/>Please find the attached event log file";
                var smtp = new SmtpClient
                {
                    Host = ResourceTxt.MailHost,//smtp.gmail.com
                    Port = Convert.ToInt32(ResourceTxt.MailPort),//587
                    EnableSsl = Convert.ToBoolean(ResourceTxt.EnableSsl),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                var message = new MailMessage(fromAddress, toAddress);
                message.Subject = subject;
                message.Body = body;
                message.Priority = MailPriority.Normal;
                message.IsBodyHtml = true;
                message.Attachments.Add(attach);
                smtp.Send(message);
                smtp = null;
            }
            catch (Exception ex)
            { flag = false; //MessageBox.Show(ex.Message); 
            Spinner.Visibility = Visibility.Hidden;
                Common.WriteEventLog("SendMail: " + ex.Message, "Error");
            }
            attach = null;
            
            if(flag)
                MessageBox.Show(ResourceTxt.MSgMailSentSucess);
            else
                MessageBox.Show(ResourceTxt.MsgMailFailed, "Error", MessageBoxButton.OK);
            return flag;
        }
        #endregion

        #region Mail Button
        private void btnMail_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = true;
            //GenerateExcel();
        }
        #endregion
        private void rdbMail_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = false;
            GenerateExcel("Automatic Mail");
        }
        private void rdbOutlook_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = false;
            GenerateExcel("Attach to Outlook");
        }
        private void rdbLocation_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = false;
            GenerateExcel("Save in Location");
        }

        private void myPopup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            myPopup.IsOpen = false;
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            myPopup.IsOpen = false;
        }

        private void myPopup_Opened(object sender, EventArgs e)
        {

        }

       
        

       
    }
}
