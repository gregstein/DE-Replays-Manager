namespace DeReplaysManager.Forms
{
    partial class DERM_Reader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DERM_Reader));
            this.cmdSCREEN = new System.Windows.Forms.RichTextBox();
            this.progCMD = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.steamLBL = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ddate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dcount = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.styleManagerAmbient1 = new DevComponents.DotNetBar.StyleManagerAmbient(this.components);
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSCREEN
            // 
            this.cmdSCREEN.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmdSCREEN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSCREEN.Location = new System.Drawing.Point(0, 139);
            this.cmdSCREEN.Name = "cmdSCREEN";
            this.cmdSCREEN.Size = new System.Drawing.Size(521, 103);
            this.cmdSCREEN.TabIndex = 17;
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
            this.progCMD.Location = new System.Drawing.Point(0, 242);
            this.progCMD.Name = "progCMD";
            this.progCMD.Size = new System.Drawing.Size(521, 20);
            this.progCMD.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.progCMD.TabIndex = 18;
            this.progCMD.Text = "[0/0 Depots] (0%)";
            this.progCMD.TextVisible = true;
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonHeader1.Size = new System.Drawing.Size(521, 33);
            this.kryptonHeader1.TabIndex = 19;
            this.kryptonHeader1.Values.Description = "Rollback To Previous Patches";
            this.kryptonHeader1.Values.Heading = "DE Replays Manager";
            this.kryptonHeader1.Values.Image = null;
            // 
            // steamLBL
            // 
            this.steamLBL.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.steamLBL, true);
            this.steamLBL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamLBL.ForeColor = System.Drawing.Color.Crimson;
            this.steamLBL.Location = new System.Drawing.Point(43, 15);
            this.steamLBL.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.steamLBL.Name = "steamLBL";
            this.steamLBL.Size = new System.Drawing.Size(73, 16);
            this.steamLBL.TabIndex = 21;
            this.steamLBL.Text = "Steam OFF";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 106);
            this.panel1.TabIndex = 23;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.steamLBL);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.ddate);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.dcount);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(521, 106);
            this.flowLayoutPanel1.TabIndex = 25;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DeReplaysManager.Properties.Resources.steam_icon;
            this.pictureBox1.Location = new System.Drawing.Point(3, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 18);
            this.label2.TabIndex = 24;
            this.label2.Text = "Downgrading To:";
            // 
            // ddate
            // 
            this.ddate.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.ddate, true);
            this.ddate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddate.ForeColor = System.Drawing.Color.RoyalBlue;
            this.ddate.Location = new System.Drawing.Point(143, 58);
            this.ddate.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.ddate.Name = "ddate";
            this.ddate.Size = new System.Drawing.Size(19, 16);
            this.ddate.TabIndex = 23;
            this.ddate.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 18);
            this.label3.TabIndex = 25;
            this.label3.Text = "Depots:";
            // 
            // dcount
            // 
            this.dcount.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.dcount, true);
            this.dcount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dcount.ForeColor = System.Drawing.Color.RoyalBlue;
            this.dcount.Location = new System.Drawing.Point(74, 81);
            this.dcount.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.dcount.Name = "dcount";
            this.dcount.Size = new System.Drawing.Size(15, 16);
            this.dcount.TabIndex = 26;
            this.dcount.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007VistaGlass;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // DERM_Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 262);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.kryptonHeader1);
            this.Controls.Add(this.cmdSCREEN);
            this.Controls.Add(this.progCMD);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DERM_Reader";
            this.Text = "DERM Reader (Downgrader Tool)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DERM_Reader_FormClosing);
            this.Load += new System.EventHandler(this.DERM_Reader_Load);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox cmdSCREEN;
        private DevComponents.DotNetBar.Controls.ProgressBarX progCMD;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private System.Windows.Forms.Label steamLBL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ddate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label dcount;
        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.StyleManagerAmbient styleManagerAmbient1;
    }
}