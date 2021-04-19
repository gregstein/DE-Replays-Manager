namespace DeReplaysManager
{
    partial class ReviewSingleton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReviewSingleton));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pgsbar = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.NickPLAYER = new DevComponents.DotNetBar.LabelX();
            this.verPRO = new DevComponents.DotNetBar.LabelX();
            this.yPRO = new DevComponents.DotNetBar.LabelX();
            this.videoREV = new DevComponents.DotNetBar.LabelX();
            this.isVID = new DevComponents.DotNetBar.LabelX();
            this.clipVID = new System.Windows.Forms.PictureBox();
            this.myDESC = new System.Windows.Forms.RichTextBox();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.panelEx1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clipVID)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.Color.Transparent;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.flowLayoutPanel1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(483, 403);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackgroundImage = global::DeReplaysManager.Properties.Resources.techtree_panel;
            this.flowLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(483, 403);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.panel1, true);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 30);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pgsbar);
            this.panel2.Controls.Add(this.labelX1);
            this.panel2.Controls.Add(this.NickPLAYER);
            this.panel2.Controls.Add(this.verPRO);
            this.panel2.Controls.Add(this.yPRO);
            this.panel2.Controls.Add(this.videoREV);
            this.panel2.Controls.Add(this.isVID);
            this.panel2.Controls.Add(this.clipVID);
            this.panel2.Controls.Add(this.myDESC);
            this.panel2.Location = new System.Drawing.Point(3, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(479, 361);
            this.panel2.TabIndex = 1;
            // 
            // pgsbar
            // 
            // 
            // 
            // 
            this.pgsbar.BackgroundStyle.BorderGradientAngle = 10;
            this.pgsbar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pgsbar.Location = new System.Drawing.Point(171, 191);
            this.pgsbar.Name = "pgsbar";
            this.pgsbar.ProgressType = DevComponents.DotNetBar.eProgressItemType.Marquee;
            this.pgsbar.Size = new System.Drawing.Size(116, 14);
            this.pgsbar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.pgsbar.TabIndex = 20;
            this.pgsbar.Text = "Loading..";
            this.pgsbar.Visible = false;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Sienna;
            this.labelX1.Location = new System.Drawing.Point(19, -3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(155, 18);
            this.labelX1.TabIndex = 11;
            this.labelX1.Text = "Review Submitted By: ";
            // 
            // NickPLAYER
            // 
            // 
            // 
            // 
            this.NickPLAYER.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.NickPLAYER.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NickPLAYER.ForeColor = System.Drawing.Color.Black;
            this.NickPLAYER.Location = new System.Drawing.Point(180, -3);
            this.NickPLAYER.Name = "NickPLAYER";
            this.NickPLAYER.Size = new System.Drawing.Size(154, 18);
            this.NickPLAYER.SymbolSize = 10F;
            this.NickPLAYER.TabIndex = 12;
            // 
            // verPRO
            // 
            // 
            // 
            // 
            this.verPRO.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.verPRO.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verPRO.ForeColor = System.Drawing.Color.Sienna;
            this.verPRO.Location = new System.Drawing.Point(19, 21);
            this.verPRO.Name = "verPRO";
            this.verPRO.Size = new System.Drawing.Size(98, 26);
            this.verPRO.TabIndex = 13;
            this.verPRO.Text = "Verified Pro";
            // 
            // yPRO
            // 
            this.yPRO.AutoSize = true;
            // 
            // 
            // 
            this.yPRO.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.yPRO.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yPRO.ForeColor = System.Drawing.Color.Black;
            this.yPRO.Location = new System.Drawing.Point(123, 21);
            this.yPRO.Name = "yPRO";
            this.yPRO.Size = new System.Drawing.Size(55, 26);
            this.yPRO.Symbol = "";
            this.yPRO.TabIndex = 14;
            this.yPRO.Text = "YES";
            // 
            // videoREV
            // 
            // 
            // 
            // 
            this.videoREV.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.videoREV.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videoREV.ForeColor = System.Drawing.Color.Sienna;
            this.videoREV.Location = new System.Drawing.Point(19, 53);
            this.videoREV.Name = "videoREV";
            this.videoREV.Size = new System.Drawing.Size(98, 30);
            this.videoREV.TabIndex = 16;
            this.videoREV.Text = "Video Review";
            this.videoREV.Visible = false;
            // 
            // isVID
            // 
            this.isVID.AutoSize = true;
            // 
            // 
            // 
            this.isVID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.isVID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.isVID.FontBold = true;
            this.isVID.ForeColor = System.Drawing.Color.Black;
            this.isVID.Location = new System.Drawing.Point(123, 53);
            this.isVID.Name = "isVID";
            this.isVID.Size = new System.Drawing.Size(55, 26);
            this.isVID.Symbol = "";
            this.isVID.TabIndex = 17;
            this.isVID.Text = "YES";
            this.isVID.Visible = false;
            // 
            // clipVID
            // 
            this.clipVID.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clipVID.Image = global::DeReplaysManager.Properties.Resources.icons8_play_button_48;
            this.clipVID.Location = new System.Drawing.Point(184, 47);
            this.clipVID.Name = "clipVID";
            this.clipVID.Size = new System.Drawing.Size(44, 32);
            this.clipVID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.clipVID.TabIndex = 18;
            this.clipVID.TabStop = false;
            this.clipVID.Visible = false;
            this.clipVID.Click += new System.EventHandler(this.clipVID_Click);
            // 
            // myDESC
            // 
            this.myDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(174)))), ((int)(((byte)(139)))));
            this.myDESC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myDESC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myDESC.Location = new System.Drawing.Point(33, 89);
            this.myDESC.Name = "myDESC";
            this.myDESC.ReadOnly = true;
            this.myDESC.Size = new System.Drawing.Size(424, 263);
            this.myDESC.TabIndex = 19;
            this.myDESC.Text = "";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // ReviewSingleton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 403);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(499, 442);
            this.MinimumSize = new System.Drawing.Size(499, 442);
            this.Name = "ReviewSingleton";
            this.Text = "Review Display";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
            this.Load += new System.EventHandler(this.ReviewSingleton_Load);
            this.panelEx1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clipVID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX NickPLAYER;
        private DevComponents.DotNetBar.LabelX verPRO;
        private DevComponents.DotNetBar.LabelX yPRO;
        private DevComponents.DotNetBar.LabelX videoREV;
        private DevComponents.DotNetBar.LabelX isVID;
        private System.Windows.Forms.PictureBox clipVID;
        private System.Windows.Forms.RichTextBox myDESC;
        private DevComponents.DotNetBar.Controls.ProgressBarX pgsbar;
    }
}