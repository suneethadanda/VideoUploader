namespace WpfVideoUploader
{
    partial class PlayVideo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayVideo));
            this.menuFileOpenClip = new System.Windows.Forms.MenuItem();
            this.menuFileCloseClip = new System.Windows.Forms.MenuItem();
            this.menuFileSep1 = new System.Windows.Forms.MenuItem();
            this.menuFileExit = new System.Windows.Forms.MenuItem();
            this.menuTopFile = new System.Windows.Forms.MenuItem();
            this.menuControlPause = new System.Windows.Forms.MenuItem();
            this.menuControlStop = new System.Windows.Forms.MenuItem();
            this.menuControlMute = new System.Windows.Forms.MenuItem();
            this.menuControlSep1 = new System.Windows.Forms.MenuItem();
            this.menuControlStep = new System.Windows.Forms.MenuItem();
            this.menuControlSep2 = new System.Windows.Forms.MenuItem();
            this.menuControlHalf = new System.Windows.Forms.MenuItem();
            this.menuControlThreeQ = new System.Windows.Forms.MenuItem();
            this.menuControlNormal = new System.Windows.Forms.MenuItem();
            this.menuControlDouble = new System.Windows.Forms.MenuItem();
            this.menuControlSep3 = new System.Windows.Forms.MenuItem();
            this.menuControlFullScr = new System.Windows.Forms.MenuItem();
            this.menuTopControl = new System.Windows.Forms.MenuItem();
            this.menuRateIncr = new System.Windows.Forms.MenuItem();
            this.menuRateDecr = new System.Windows.Forms.MenuItem();
            this.menuRateSep1 = new System.Windows.Forms.MenuItem();
            this.menuRateNormal = new System.Windows.Forms.MenuItem();
            this.menuRateHalf = new System.Windows.Forms.MenuItem();
            this.menuRateDouble = new System.Windows.Forms.MenuItem();
            this.menuTopRate = new System.Windows.Forms.MenuItem();
            this.menuHelpAbout = new System.Windows.Forms.MenuItem();
            this.menuTopHelp = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.SuspendLayout();
            // 
            // menuFileOpenClip
            // 
            this.menuFileOpenClip.Index = 0;
            this.menuFileOpenClip.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuFileOpenClip.Text = "&Open Clip...";
            this.menuFileOpenClip.Click += new System.EventHandler(this.menuFileOpenClip_Click);
            // 
            // menuFileCloseClip
            // 
            this.menuFileCloseClip.Enabled = false;
            this.menuFileCloseClip.Index = 1;
            this.menuFileCloseClip.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.menuFileCloseClip.Text = "&Close Clip";
            this.menuFileCloseClip.Click += new System.EventHandler(this.menuFileCloseClip_Click);
            // 
            // menuFileSep1
            // 
            this.menuFileSep1.Index = 2;
            this.menuFileSep1.Text = "-";
            // 
            // menuFileExit
            // 
            this.menuFileExit.Index = 3;
            this.menuFileExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.menuFileExit.Text = "E&xit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuTopFile
            // 
            this.menuTopFile.Index = 0;
            this.menuTopFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFileOpenClip,
            this.menuFileCloseClip,
            this.menuFileSep1,
            this.menuFileExit});
            this.menuTopFile.Text = "&File";
            // 
            // menuControlPause
            // 
            this.menuControlPause.Enabled = false;
            this.menuControlPause.Index = 0;
            this.menuControlPause.Text = "&Pause/Play\tP";
            this.menuControlPause.Click += new System.EventHandler(this.menuControlPause_Click);
            // 
            // menuControlStop
            // 
            this.menuControlStop.Enabled = false;
            this.menuControlStop.Index = 1;
            this.menuControlStop.Text = "&Stop\tS";
            this.menuControlStop.Click += new System.EventHandler(this.menuControlStop_Click);
            // 
            // menuControlMute
            // 
            this.menuControlMute.Enabled = false;
            this.menuControlMute.Index = 2;
            this.menuControlMute.Text = "&Mute/Unmute\tM";
            this.menuControlMute.Click += new System.EventHandler(this.menuControlMute_Click);
            // 
            // menuControlSep1
            // 
            this.menuControlSep1.Index = 3;
            this.menuControlSep1.Text = "-";
            // 
            // menuControlStep
            // 
            this.menuControlStep.Enabled = false;
            this.menuControlStep.Index = 4;
            this.menuControlStep.Text = "Single F&rame Step\t<Space>";
            this.menuControlStep.Click += new System.EventHandler(this.menuControlStep_Click);
            // 
            // menuControlSep2
            // 
            this.menuControlSep2.Index = 5;
            this.menuControlSep2.Text = "-";
            // 
            // menuControlHalf
            // 
            this.menuControlHalf.Enabled = false;
            this.menuControlHalf.Index = 6;
            this.menuControlHalf.RadioCheck = true;
            this.menuControlHalf.Text = "&Half size (50%)\tH";
            this.menuControlHalf.Click += new System.EventHandler(this.menuControlHalf_Click);
            // 
            // menuControlThreeQ
            // 
            this.menuControlThreeQ.Enabled = false;
            this.menuControlThreeQ.Index = 7;
            this.menuControlThreeQ.RadioCheck = true;
            this.menuControlThreeQ.Text = "&Three-quarter size (75%)\tT";
            this.menuControlThreeQ.Click += new System.EventHandler(this.menuControlThreeQ_Click);
            // 
            // menuControlNormal
            // 
            this.menuControlNormal.Enabled = false;
            this.menuControlNormal.Index = 8;
            this.menuControlNormal.RadioCheck = true;
            this.menuControlNormal.Text = "&Normal size (100%)\tN";
            this.menuControlNormal.Click += new System.EventHandler(this.menuControlNormal_Click);
            // 
            // menuControlDouble
            // 
            this.menuControlDouble.Enabled = false;
            this.menuControlDouble.Index = 9;
            this.menuControlDouble.RadioCheck = true;
            this.menuControlDouble.Text = "&Double size (200%)\tD";
            this.menuControlDouble.Click += new System.EventHandler(this.menuControlDouble_Click);
            // 
            // menuControlSep3
            // 
            this.menuControlSep3.Index = 10;
            this.menuControlSep3.Text = "-";
            // 
            // menuControlFullScr
            // 
            this.menuControlFullScr.Enabled = false;
            this.menuControlFullScr.Index = 11;
            this.menuControlFullScr.Text = "&Full screen\t<enter> or F";
            this.menuControlFullScr.Click += new System.EventHandler(this.menuControlFullScr_Click);
            // 
            // menuTopControl
            // 
            this.menuTopControl.Index = 1;
            this.menuTopControl.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuControlPause,
            this.menuControlStop,
            this.menuControlMute,
            this.menuControlSep1,
            this.menuControlStep,
            this.menuControlSep2,
            this.menuControlHalf,
            this.menuControlThreeQ,
            this.menuControlNormal,
            this.menuControlDouble,
            this.menuControlSep3,
            this.menuControlFullScr});
            this.menuTopControl.Text = "&Control";
            // 
            // menuRateIncr
            // 
            this.menuRateIncr.Enabled = false;
            this.menuRateIncr.Index = 0;
            this.menuRateIncr.Text = "&Increase playback rate\t<Right arrow>";
            this.menuRateIncr.Click += new System.EventHandler(this.menuRateIncr_Click);
            // 
            // menuRateDecr
            // 
            this.menuRateDecr.Enabled = false;
            this.menuRateDecr.Index = 1;
            this.menuRateDecr.Text = "&Decrease playback rate\t<Left arrow>";
            this.menuRateDecr.Click += new System.EventHandler(this.menuRateDecr_Click);
            // 
            // menuRateSep1
            // 
            this.menuRateSep1.Index = 2;
            this.menuRateSep1.Text = "-";
            // 
            // menuRateNormal
            // 
            this.menuRateNormal.Enabled = false;
            this.menuRateNormal.Index = 3;
            this.menuRateNormal.RadioCheck = true;
            this.menuRateNormal.Text = "&Normal playback rate\t<Back>";
            this.menuRateNormal.Click += new System.EventHandler(this.menuRateNormal_Click);
            // 
            // menuRateHalf
            // 
            this.menuRateHalf.Enabled = false;
            this.menuRateHalf.Index = 4;
            this.menuRateHalf.RadioCheck = true;
            this.menuRateHalf.Text = "&Half playback rate\t<Down arrow>";
            this.menuRateHalf.Click += new System.EventHandler(this.menuRateHalf_Click);
            // 
            // menuRateDouble
            // 
            this.menuRateDouble.Enabled = false;
            this.menuRateDouble.Index = 5;
            this.menuRateDouble.RadioCheck = true;
            this.menuRateDouble.Text = "Dou&ble playback rate\t<Up arrow>";
            this.menuRateDouble.Click += new System.EventHandler(this.menuRateDouble_Click);
            // 
            // menuTopRate
            // 
            this.menuTopRate.Index = 2;
            this.menuTopRate.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuRateIncr,
            this.menuRateDecr,
            this.menuRateSep1,
            this.menuRateNormal,
            this.menuRateHalf,
            this.menuRateDouble});
            this.menuTopRate.Text = "&Rate";
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Index = 0;
            this.menuHelpAbout.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuHelpAbout.Text = "&About...";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // menuTopHelp
            // 
            this.menuTopHelp.Index = 3;
            this.menuTopHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuHelpAbout});
            this.menuTopHelp.Text = "&Help";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuTopFile,
            this.menuTopControl,
            this.menuTopRate,
            this.menuTopHelp});
            this.mainMenu1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // PlayVideo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(408, 274);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "PlayVideo";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Play";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuFileOpenClip;
        private System.Windows.Forms.MenuItem menuFileCloseClip;
        private System.Windows.Forms.MenuItem menuFileSep1;
        private System.Windows.Forms.MenuItem menuFileExit;
        private System.Windows.Forms.MenuItem menuTopFile;
        private System.Windows.Forms.MenuItem menuControlPause;
        private System.Windows.Forms.MenuItem menuControlStop;
        private System.Windows.Forms.MenuItem menuControlMute;
        private System.Windows.Forms.MenuItem menuControlSep1;
        private System.Windows.Forms.MenuItem menuControlStep;
        private System.Windows.Forms.MenuItem menuControlSep2;
        private System.Windows.Forms.MenuItem menuControlHalf;
        private System.Windows.Forms.MenuItem menuControlThreeQ;
        private System.Windows.Forms.MenuItem menuControlNormal;
        private System.Windows.Forms.MenuItem menuControlDouble;
        private System.Windows.Forms.MenuItem menuControlSep3;
        private System.Windows.Forms.MenuItem menuControlFullScr;
        private System.Windows.Forms.MenuItem menuTopControl;
        private System.Windows.Forms.MenuItem menuRateIncr;
        private System.Windows.Forms.MenuItem menuRateDecr;
        private System.Windows.Forms.MenuItem menuRateSep1;
        private System.Windows.Forms.MenuItem menuRateNormal;
        private System.Windows.Forms.MenuItem menuRateHalf;
        private System.Windows.Forms.MenuItem menuRateDouble;
        private System.Windows.Forms.MenuItem menuTopRate;
        private System.Windows.Forms.MenuItem menuHelpAbout;
        private System.Windows.Forms.MenuItem menuTopHelp;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}