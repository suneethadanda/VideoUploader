using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DirectX.Capture;
using DShowNET;
using System.IO;
using System.Windows;
using System.Threading;


using Splicer;
using Splicer.Timeline;
using Splicer.Renderer;

namespace WpfVideoUploader
{
    public partial class RecordVideoNew : Form
    {
        public string fileName;


        Capture capture = null;
        Filters filters = null;
        bool upload = false;
        int counter = 1;
        int count = 0;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        int deviceNumber = 0;

        public RecordVideoNew()
        {
            InitializeComponent();
            MaximizeBox = false;
            fileName = string.Empty;
            if (!Directory.Exists(Common.RecordedVideos))
            {
                Directory.CreateDirectory(Common.RecordedVideos);
            }
            fileName = Common.RecordedVideos + "\\" + Guid.NewGuid();
        }

        private void btnStartVideoCapture_Click(object sender, EventArgs e)
        {
            startOrStopCapturing(capture);
        } 

        void preview(int deviceNo)
        {
            try
            {
                capture = new Capture(filters.VideoInputDevices[deviceNo], filters.AudioInputDevices[0]);

                // capture.PreviewWindow = panel1;

                if (btnStartVideoCapture.Text == "STOP")
                {
                    counter++;
                    if (!capture.Cued)
                    {
                        capture.Filename = fileName;
                    }

                    capture.Cue();
                }

                //capture.Start();
            }
            catch { fileName = ""; }
        }


        private void RecordVideoNew_Load(object sender, EventArgs e)
        {
            try
            {
                filters = new Filters();

                if (filters.VideoInputDevices != null)
                {
                    try
                    {
                        preview(deviceNumber);
                    }
                    catch (Exception ex)
                    {
                        fileName = "";
                        System.Windows.Forms.MessageBox.Show("Maybe any other software is already using your WebCam.\n\n Error Message: \n\n" + ex.Message);
                    }
                }
                else
                {
                    btnStartVideoCapture.Enabled = false;
                    System.Windows.Forms.MessageBox.Show("No video device connected to your PC!");
                }

                timer.Interval = 600000; // 10 minutes!
                timer.Tick += (obj, evt) =>
                {
                    if (btnStartVideoCapture.Text == "STOP")
                    {
                        counter++;

                        if (capture != null && counter > 1)
                        {
                            // capture.Stop();
                            if (!capture.Cued)
                            {

                                capture.Filename = fileName;
                            }
                            capture.Cue();
                            capture.Start();
                        }
                    }
                };

                if (filters.VideoInputDevices != null)
                {
                    for (var i = 0; i < filters.VideoInputDevices.Count; i++)
                    {
                        var device = filters.VideoInputDevices[i];

                        var btn = new Button();

                        btn.Text = i.ToString();
                        btn.ForeColor = Color.White;
                        btn.BackColor = Color.DarkSlateBlue;
                        btn.Width = 25;

                        btn.Click += (obj, evt) =>
                        {
                            var thisButton = (Button)obj;

                            if (int.Parse(thisButton.Text) != deviceNumber)
                            {
                                if (capture != null)
                                {
                                    capture.Dispose();
                                    capture.Stop();
                                    capture.PreviewWindow = null;

                                }

                                deviceNumber = int.Parse(thisButton.Text);
                                preview(deviceNumber);
                            }
                        };

                        flowLayoutPanel1.Controls.Add(btn);
                    }
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("No Device found", "Information");
                fileName = "";
                this.Close();
                //this.Close();
            }
        }

        void startOrStopCapturing(Capture capture)
        {
            btnStartVideoCapture.Visible = false;

            if (capture != null)
            {
                capture.Stop();
            }
            if (timer.Enabled) timer.Stop();

            if (btnStartVideoCapture.Text == "START")
            {
                capture.PreviewWindow = panel1;
                btnStartVideoCapture.Text = "STOP";
                button1.Enabled = false;
                button2.Enabled = false;
                btnPause.Enabled = true;
                btnStartVideoCapture.BackColor = Color.Maroon;
                this.Text = "Recording......";
                try
                {
                    if (!capture.Cued)
                    {

                        capture.Filename = fileName + "_" + count + ".wmv";
                    }

                    capture.Cue();
                    capture.Start();

                    timer.Start();
                    count = count + 1;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error Message: \n\n" + ex.Message);
                }
            }
            else
            {
                // Paused = true;
                button1.Enabled = true;
                button2.Enabled = true;
                btnStartVideoCapture.Text = "START";
                capture.PreviewWindow = null;
                this.Text = "Record";
                //pause = false;
                btnPause.Enabled = false;
                btnStartVideoCapture.BackColor = Color.DarkSlateBlue;



                if (count != 0)
                {
                    using (ITimeline timeline = new DefaultTimeline())
                    {
                        IGroup group = timeline.AddVideoGroup(32, 640, 360);

                        var firstVideoClip = group.AddTrack().AddVideo(fileName + "_" + (count - 1) + ".wmv");
                       // var secondVideoClip = group.AddTrack().AddVideo(fileName + "_" + (count) + ".wmv", firstVideoClip.Duration);

                        using (AviFileRenderer renderer = new AviFileRenderer(timeline, fileName + ".wmv"))
                        {
                            renderer.Render();

                        }
                    }
                }

                if (File.Exists(fileName + "_" + (count - 1) + ".wmv"))
                {
                    File.Delete(fileName + "_" + (count - 1) + ".wmv");
                }
                if (File.Exists(fileName + "_" + (count) + ".wmv"))
                {
                    File.Delete(fileName + "_" + (count) + ".wmv");
                }


            }
            btnStartVideoCapture.Visible = true;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlayVideo objplay = new PlayVideo();
            //fileName = fileName + ".wmv";
            objplay.clipFile = fileName + ".wmv";
            objplay.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            upload = true;
            this.Hide();
        }


        // bool Paused = false;



        private void RecordVideoNew_Deactivate(object sender, EventArgs e)
        {
            //if (capture.Stopped)
            //{
            //    capture.Dispose();
            //}
            //else
            //{
            //    capture.Stop();
            //    capture.Dispose();
            //}


        }

        private void RecordVideoNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (capture != null)
                {
                    if (capture.Stopped)
                    {
                        capture.Dispose();
                    }
                    else
                    {
                        capture.Stop();
                        capture.Dispose();
                    }

                    if (!upload)
                    {
                        DialogResult dr = System.Windows.Forms.MessageBox.Show("Do you want to save Recorded Video?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No)
                        {
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                        }
                        fileName = "";

                    }
                }
            }
            catch { }
        }




        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Pause")
            {

                btnPause.Text = "Resume";
                btnPause.BackColor = Color.Maroon;
                button1.Enabled = false;
                button2.Enabled = false;
                btnStartVideoCapture.Enabled = false;

            }
            if (btnPause.Text == "Resume")
            {


                btnPause.Text = "Pause";
                btnStartVideoCapture.Enabled = true;
                btnPause.BackColor = Color.DarkSlateBlue;

            }
        }


    }
}
