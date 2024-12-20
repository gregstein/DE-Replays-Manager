﻿namespace De_Roll
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbPATCH = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.userBOX = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.passBOX = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.savesession = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.steamLBL = new System.Windows.Forms.Label();
            this.settingbtn = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.autoSTEAM = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.bWK = new System.ComponentModel.BackgroundWorker();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmdSCREEN = new System.Windows.Forms.RichTextBox();
            this.progCMD = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.steamSTATUS = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.validitypng = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.cbPATCH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.steamSTATUS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.validitypng)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Enabled = false;
            this.kryptonButton1.Location = new System.Drawing.Point(3, 166);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.kryptonButton1.Size = new System.Drawing.Size(120, 27);
            this.kryptonButton1.TabIndex = 3;
            this.kryptonButton1.Values.Text = "Downgrade Now!";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // cbPATCH
            // 
            this.cbPATCH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPATCH.DropDownWidth = 138;
            this.cbPATCH.Enabled = false;
            this.cbPATCH.Items.AddRange(new object[] {
            "Select Patch Version",
            "17 November 2020 (42848)",
            "21 October 2020 (41855)",
            "23 September 2020 (40874)",
            "25 August 2020 (40220)",
            "28 July 2020 (39515)",
            "21 July 2020 (39284)",
            "3 June 2020 (37906)",
            "28 May 2020 (37650)",
            "30 April 2020 (36906)",
            "27 February 2020 (35584)",
            "13 February 2020 (35209)",
            "21 January 2020 (34699)",
            "13 January 2020 (34397)",
            "17 December 2019 (34055)",
            "27 November 2019 (33315)",
            "22 November 2019 (33164)",
            "20 November 2019 (33059)",
            "16 November 2019 (32911)"});
            this.cbPATCH.Location = new System.Drawing.Point(243, 77);
            this.cbPATCH.Name = "cbPATCH";
            this.cbPATCH.Size = new System.Drawing.Size(175, 21);
            this.cbPATCH.TabIndex = 4;
            this.cbPATCH.Text = "Select Patch Version";
            // 
            // userBOX
            // 
            this.userBOX.Location = new System.Drawing.Point(45, 13);
            this.userBOX.Name = "userBOX";
            this.userBOX.Size = new System.Drawing.Size(100, 23);
            this.userBOX.TabIndex = 1;
            // 
            // passBOX
            // 
            this.passBOX.Location = new System.Drawing.Point(45, 42);
            this.passBOX.Name = "passBOX";
            this.passBOX.PasswordChar = '●';
            this.passBOX.Size = new System.Drawing.Size(100, 23);
            this.passBOX.TabIndex = 2;
            this.passBOX.UseSystemPasswordChar = true;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonGroupBox1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.SeparatorHighInternalProfile;
            this.kryptonGroupBox1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.GridHeaderRowList;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(12, 40);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.savesession);
            this.kryptonGroupBox1.Panel.Controls.Add(this.passBOX);
            this.kryptonGroupBox1.Panel.Controls.Add(this.userBOX);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(166, 120);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "Steam Login";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(4, 45);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonLabel2.Size = new System.Drawing.Size(34, 20);
            this.kryptonLabel2.TabIndex = 0;
            this.kryptonLabel2.Values.Text = "Pass";
            // 
            // savesession
            // 
            this.savesession.Location = new System.Drawing.Point(45, 73);
            this.savesession.Name = "savesession";
            this.savesession.Size = new System.Drawing.Size(95, 16);
            this.savesession.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savesession.TabIndex = 13;
            this.savesession.Values.Text = "Save Session";
            this.savesession.CheckedChanged += new System.EventHandler(this.autoSTEAM_CheckedChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 16);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonLabel1.Size = new System.Drawing.Size(35, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "User";
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(430, 31);
            this.kryptonHeader1.TabIndex = 5;
            this.kryptonHeader1.Values.Description = "Rollback To Previous Patches";
            this.kryptonHeader1.Values.Heading = "DE Replays Manager";
            this.kryptonHeader1.Values.Image = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Sienna;
            this.label1.Location = new System.Drawing.Point(288, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "...";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // steamLBL
            // 
            this.steamLBL.AutoSize = true;
            this.steamLBL.Font = new System.Drawing.Font("Tahoma", 10F);
            this.steamLBL.Location = new System.Drawing.Point(267, 50);
            this.steamLBL.Name = "steamLBL";
            this.steamLBL.Size = new System.Drawing.Size(75, 17);
            this.steamLBL.TabIndex = 9;
            this.steamLBL.Text = "Steam OFF";
            // 
            // settingbtn
            // 
            this.settingbtn.AutoSize = true;
            this.settingbtn.Location = new System.Drawing.Point(129, 166);
            this.settingbtn.Name = "settingbtn";
            this.settingbtn.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.settingbtn.Size = new System.Drawing.Size(79, 27);
            this.settingbtn.TabIndex = 11;
            this.settingbtn.Values.Text = "Settings";
            this.settingbtn.Click += new System.EventHandler(this.settingbtn_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // autoSTEAM
            // 
            this.autoSTEAM.Location = new System.Drawing.Point(214, 172);
            this.autoSTEAM.Name = "autoSTEAM";
            this.autoSTEAM.Size = new System.Drawing.Size(115, 16);
            this.autoSTEAM.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoSTEAM.TabIndex = 13;
            this.autoSTEAM.Values.Text = "Skip Steam Login";
            this.autoSTEAM.Visible = false;
            this.autoSTEAM.CheckedChanged += new System.EventHandler(this.autoSTEAM_CheckedChanged);
            // 
            // bWK
            // 
            this.bWK.WorkerReportsProgress = true;
            this.bWK.WorkerSupportsCancellation = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdSCREEN);
            this.flowLayoutPanel1.Controls.Add(this.progCMD);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 199);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(430, 133);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // cmdSCREEN
            // 
            this.cmdSCREEN.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmdSCREEN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSCREEN.Location = new System.Drawing.Point(3, 3);
            this.cmdSCREEN.Name = "cmdSCREEN";
            this.cmdSCREEN.Size = new System.Drawing.Size(427, 103);
            this.cmdSCREEN.TabIndex = 15;
            this.cmdSCREEN.Text = "";
            this.cmdSCREEN.TextChanged += new System.EventHandler(this.cmdSCREEN_TextChanged);
            // 
            // progCMD
            // 
            // 
            // 
            // 
            this.progCMD.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progCMD.ChunkGradientAngle = 100;
            this.progCMD.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progCMD.Location = new System.Drawing.Point(3, 112);
            this.progCMD.Name = "progCMD";
            this.progCMD.Size = new System.Drawing.Size(427, 20);
            this.progCMD.TabIndex = 16;
            this.progCMD.Text = "[0/0 Depots] (0%)";
            this.progCMD.TextVisible = true;
            // 
            // steamSTATUS
            // 
            this.steamSTATUS.Image = global::DeReplaysManager.Properties.Resources.dotred;
            this.steamSTATUS.Location = new System.Drawing.Point(223, 80);
            this.steamSTATUS.Name = "steamSTATUS";
            this.steamSTATUS.Size = new System.Drawing.Size(14, 14);
            this.steamSTATUS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.steamSTATUS.TabIndex = 10;
            this.steamSTATUS.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DeReplaysManager.Properties.Resources.steam_icon;
            this.pictureBox1.Location = new System.Drawing.Point(238, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // validitypng
            // 
            this.validitypng.Image = global::DeReplaysManager.Properties.Resources.aoe2de;
            this.validitypng.Location = new System.Drawing.Point(250, 107);
            this.validitypng.Name = "validitypng";
            this.validitypng.Size = new System.Drawing.Size(32, 32);
            this.validitypng.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.validitypng.TabIndex = 6;
            this.validitypng.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(430, 332);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.autoSTEAM);
            this.Controls.Add(this.settingbtn);
            this.Controls.Add(this.steamSTATUS);
            this.Controls.Add(this.steamLBL);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.validitypng);
            this.Controls.Add(this.kryptonHeader1);
            this.Controls.Add(this.kryptonGroupBox1);
            this.Controls.Add(this.cbPATCH);
            this.Controls.Add(this.kryptonButton1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Downgrader Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.cbPATCH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.steamSTATUS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.validitypng)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbPATCH;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox userBOX;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox passBOX;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private System.Windows.Forms.PictureBox validitypng;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label steamLBL;
        private System.Windows.Forms.PictureBox steamSTATUS;
        private ComponentFactory.Krypton.Toolkit.KryptonButton settingbtn;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox autoSTEAM;
        private System.ComponentModel.BackgroundWorker bWK;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RichTextBox cmdSCREEN;
        private DevComponents.DotNetBar.Controls.ProgressBarX progCMD;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox savesession;
    }
}

