namespace EE4Test
{
    partial class frmEE4WebCam
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEE4WebCam));
            this.panelVideoPreview = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstVideoDevices = new System.Windows.Forms.ListBox();
            this.lstAudioDevices = new System.Windows.Forms.ListBox();
            this.btnStartStopRecording = new System.Windows.Forms.Button();
            this.lblVideoDeviceSelectedForPreview = new System.Windows.Forms.Label();
            this.lblAudioDeviceSelectedForPreview = new System.Windows.Forms.Label();
            this.btnGrabImage = new System.Windows.Forms.Button();
            this.checkBoxShowConfigDialog = new System.Windows.Forms.CheckBox();
            this.btnBroadcast = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRecordingTime = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnPauseResume = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelVideoPreview
            // 
            this.panelVideoPreview.Location = new System.Drawing.Point(374, 12);
            this.panelVideoPreview.Name = "panelVideoPreview";
            this.panelVideoPreview.Size = new System.Drawing.Size(320, 240);
            this.panelVideoPreview.TabIndex = 0;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(16, 382);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(94, 32);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 510);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1028, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // lstVideoDevices
            // 
            this.lstVideoDevices.FormattingEnabled = true;
            this.lstVideoDevices.Location = new System.Drawing.Point(16, 12);
            this.lstVideoDevices.Name = "lstVideoDevices";
            this.lstVideoDevices.Size = new System.Drawing.Size(338, 108);
            this.lstVideoDevices.TabIndex = 3;
            // 
            // lstAudioDevices
            // 
            this.lstAudioDevices.FormattingEnabled = true;
            this.lstAudioDevices.Location = new System.Drawing.Point(16, 126);
            this.lstAudioDevices.Name = "lstAudioDevices";
            this.lstAudioDevices.Size = new System.Drawing.Size(338, 108);
            this.lstAudioDevices.TabIndex = 4;
            // 
            // btnStartStopRecording
            // 
            this.btnStartStopRecording.Enabled = false;
            this.btnStartStopRecording.Location = new System.Drawing.Point(133, 240);
            this.btnStartStopRecording.Name = "btnStartStopRecording";
            this.btnStartStopRecording.Size = new System.Drawing.Size(94, 32);
            this.btnStartStopRecording.TabIndex = 5;
            this.btnStartStopRecording.Text = "Start Recording";
            this.btnStartStopRecording.UseVisualStyleBackColor = true;
            this.btnStartStopRecording.Click += new System.EventHandler(this.btnStartStopRecording_Click);
            // 
            // lblVideoDeviceSelectedForPreview
            // 
            this.lblVideoDeviceSelectedForPreview.AutoSize = true;
            this.lblVideoDeviceSelectedForPreview.Location = new System.Drawing.Point(16, 318);
            this.lblVideoDeviceSelectedForPreview.Name = "lblVideoDeviceSelectedForPreview";
            this.lblVideoDeviceSelectedForPreview.Size = new System.Drawing.Size(173, 13);
            this.lblVideoDeviceSelectedForPreview.TabIndex = 6;
            this.lblVideoDeviceSelectedForPreview.Text = "lblVideoDeviceSelectedForPreview";
            // 
            // lblAudioDeviceSelectedForPreview
            // 
            this.lblAudioDeviceSelectedForPreview.AutoSize = true;
            this.lblAudioDeviceSelectedForPreview.Location = new System.Drawing.Point(16, 342);
            this.lblAudioDeviceSelectedForPreview.Name = "lblAudioDeviceSelectedForPreview";
            this.lblAudioDeviceSelectedForPreview.Size = new System.Drawing.Size(173, 13);
            this.lblAudioDeviceSelectedForPreview.TabIndex = 7;
            this.lblAudioDeviceSelectedForPreview.Text = "lblAudioDeviceSelectedForPreview";
            // 
            // btnGrabImage
            // 
            this.btnGrabImage.Enabled = false;
            this.btnGrabImage.Location = new System.Drawing.Point(250, 240);
            this.btnGrabImage.Name = "btnGrabImage";
            this.btnGrabImage.Size = new System.Drawing.Size(94, 32);
            this.btnGrabImage.TabIndex = 8;
            this.btnGrabImage.Text = "Grab Image";
            this.btnGrabImage.UseVisualStyleBackColor = true;
            this.btnGrabImage.Click += new System.EventHandler(this.cmdGrabImage_Click);
            // 
            // checkBoxShowConfigDialog
            // 
            this.checkBoxShowConfigDialog.AutoSize = true;
            this.checkBoxShowConfigDialog.Location = new System.Drawing.Point(16, 284);
            this.checkBoxShowConfigDialog.Name = "checkBoxShowConfigDialog";
            this.checkBoxShowConfigDialog.Size = new System.Drawing.Size(226, 17);
            this.checkBoxShowConfigDialog.TabIndex = 9;
            this.checkBoxShowConfigDialog.Text = "Show configuration dialogs before preview";
            this.checkBoxShowConfigDialog.UseVisualStyleBackColor = true;
            this.checkBoxShowConfigDialog.Visible = false;
            // 
            // btnBroadcast
            // 
            this.btnBroadcast.Location = new System.Drawing.Point(16, 420);
            this.btnBroadcast.Name = "btnBroadcast";
            this.btnBroadcast.Size = new System.Drawing.Size(94, 32);
            this.btnBroadcast.TabIndex = 10;
            this.btnBroadcast.Text = "Broadcast";
            this.btnBroadcast.UseVisualStyleBackColor = true;
            this.btnBroadcast.Click += new System.EventHandler(this.Broadcast_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Location = new System.Drawing.Point(250, 240);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(94, 32);
            this.btnPlay.TabIndex = 11;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Time:";
            // 
            // lblRecordingTime
            // 
            this.lblRecordingTime.AutoSize = true;
            this.lblRecordingTime.Location = new System.Drawing.Point(52, 285);
            this.lblRecordingTime.Name = "lblRecordingTime";
            this.lblRecordingTime.Size = new System.Drawing.Size(10, 13);
            this.lblRecordingTime.TabIndex = 13;
            this.lblRecordingTime.Text = " ";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(33, 240);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(94, 32);
            this.btnUpload.TabIndex = 14;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnPauseResume
            // 
            this.btnPauseResume.Location = new System.Drawing.Point(250, 299);
            this.btnPauseResume.Name = "btnPauseResume";
            this.btnPauseResume.Size = new System.Drawing.Size(94, 32);
            this.btnPauseResume.TabIndex = 15;
            this.btnPauseResume.Text = "Pause";
            this.btnPauseResume.UseVisualStyleBackColor = true;
            this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(791, 412);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(120, 95);
            this.lstFiles.TabIndex = 16;
            this.lstFiles.Visible = false;
            // 
            // frmEE4WebCam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 532);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.btnPauseResume);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.lblRecordingTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnBroadcast);
            this.Controls.Add(this.checkBoxShowConfigDialog);
            this.Controls.Add(this.btnGrabImage);
            this.Controls.Add(this.lblAudioDeviceSelectedForPreview);
            this.Controls.Add(this.lblVideoDeviceSelectedForPreview);
            this.Controls.Add(this.btnStartStopRecording);
            this.Controls.Add(this.lstAudioDevices);
            this.Controls.Add(this.lstVideoDevices);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.panelVideoPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEE4WebCam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Record video";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEE4WebCam_FormClosing);
            this.Load += new System.EventHandler(this.frmEE4WebCam_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelVideoPreview;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListBox lstVideoDevices;
        private System.Windows.Forms.ListBox lstAudioDevices;
        private System.Windows.Forms.Button btnStartStopRecording;
        private System.Windows.Forms.Label lblVideoDeviceSelectedForPreview;
        private System.Windows.Forms.Label lblAudioDeviceSelectedForPreview;
        private System.Windows.Forms.Button btnGrabImage;
        private System.Windows.Forms.CheckBox checkBoxShowConfigDialog;
        private System.Windows.Forms.Button btnBroadcast;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRecordingTime;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnPauseResume;
        private System.Windows.Forms.ListBox lstFiles;
    }
}

