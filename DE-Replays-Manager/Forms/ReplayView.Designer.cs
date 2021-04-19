namespace DeReplaysManager
{
    partial class ReplayView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplayView));
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.expandablePanel2 = new DevComponents.DotNetBar.ExpandablePanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.nickFIELD = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.subody = new System.Windows.Forms.RichTextBox();
            this.vidLBL = new DevComponents.DotNetBar.LabelX();
            this.vidFIELD = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.titlerec = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.picVER = new System.Windows.Forms.PictureBox();
            this.subreview = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.progressBarX1 = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.expaNEL = new DevComponents.DotNetBar.ExpandablePanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pgsbar = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.importrec = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label2 = new System.Windows.Forms.Label();
            this.bodytext = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.lbltitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.expandablePanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVER)).BeginInit();
            this.expaNEL.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.expandablePanel2);
            this.panel1.Controls.Add(this.expaNEL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 525);
            this.panel1.TabIndex = 0;
            // 
            // expandablePanel2
            // 
            this.expandablePanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel2.Controls.Add(this.flowLayoutPanel1);
            this.expandablePanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.expandablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expandablePanel2.HideControlsWhenCollapsed = true;
            this.expandablePanel2.Location = new System.Drawing.Point(0, 236);
            this.expandablePanel2.Name = "expandablePanel2";
            this.expandablePanel2.Size = new System.Drawing.Size(799, 289);
            this.expandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel2.Style.GradientAngle = 90;
            this.expandablePanel2.TabIndex = 11;
            this.expandablePanel2.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.SplitterBackground;
            this.expandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel2.TitleStyle.GradientAngle = 90;
            this.expandablePanel2.TitleText = "Submit a review";
            this.expandablePanel2.EnabledChanged += new System.EventHandler(this.expandablePanel2_EnabledChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.labelX1);
            this.flowLayoutPanel1.Controls.Add(this.nickFIELD);
            this.flowLayoutPanel1.Controls.Add(this.line1);
            this.flowLayoutPanel1.Controls.Add(this.subody);
            this.flowLayoutPanel1.Controls.Add(this.vidLBL);
            this.flowLayoutPanel1.Controls.Add(this.vidFIELD);
            this.flowLayoutPanel1.Controls.Add(this.labelX3);
            this.flowLayoutPanel1.Controls.Add(this.titlerec);
            this.flowLayoutPanel1.Controls.Add(this.picVER);
            this.flowLayoutPanel1.Controls.Add(this.subreview);
            this.flowLayoutPanel1.Controls.Add(this.progressBarX1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 26);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(799, 263);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(3, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(118, 30);
            this.labelX1.TabIndex = 13;
            this.labelX1.Text = "Reviewer Name";
            // 
            // nickFIELD
            // 
            this.nickFIELD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            // 
            // 
            // 
            this.nickFIELD.Border.Class = "TextBoxBorder";
            this.nickFIELD.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.nickFIELD.DisabledBackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.SetFlowBreak(this.nickFIELD, true);
            this.nickFIELD.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nickFIELD.Location = new System.Drawing.Point(127, 3);
            this.nickFIELD.Name = "nickFIELD";
            this.nickFIELD.PreventEnterBeep = true;
            this.nickFIELD.Size = new System.Drawing.Size(233, 26);
            this.nickFIELD.TabIndex = 12;
            // 
            // line1
            // 
            this.line1.EndLineCapSize = new System.Drawing.Size(6, 0);
            this.line1.Location = new System.Drawing.Point(3, 39);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(671, 10);
            this.line1.TabIndex = 17;
            this.line1.Text = "line1";
            // 
            // subody
            // 
            this.subody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.subody.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.flowLayoutPanel1.SetFlowBreak(this.subody, true);
            this.subody.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subody.ForeColor = System.Drawing.Color.MidnightBlue;
            this.subody.Location = new System.Drawing.Point(3, 55);
            this.subody.Name = "subody";
            this.subody.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.subody.Size = new System.Drawing.Size(671, 133);
            this.subody.TabIndex = 16;
            this.subody.Text = "Type here..";
            this.subody.Click += new System.EventHandler(this.subody_Click);
            this.subody.TextChanged += new System.EventHandler(this.subody_TextChanged);
            // 
            // vidLBL
            // 
            // 
            // 
            // 
            this.vidLBL.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.vidLBL.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vidLBL.Location = new System.Drawing.Point(3, 194);
            this.vidLBL.Name = "vidLBL";
            this.vidLBL.Size = new System.Drawing.Size(73, 26);
            this.vidLBL.TabIndex = 19;
            this.vidLBL.Text = "Video Link";
            this.vidLBL.Visible = false;
            // 
            // vidFIELD
            // 
            this.vidFIELD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            // 
            // 
            // 
            this.vidFIELD.Border.Class = "TextBoxBorder";
            this.vidFIELD.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.vidFIELD.DisabledBackColor = System.Drawing.Color.White;
            this.vidFIELD.Enabled = false;
            this.vidFIELD.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vidFIELD.Location = new System.Drawing.Point(82, 194);
            this.vidFIELD.Name = "vidFIELD";
            this.vidFIELD.PreventEnterBeep = true;
            this.vidFIELD.Size = new System.Drawing.Size(233, 26);
            this.vidFIELD.TabIndex = 18;
            this.vidFIELD.Visible = false;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.Location = new System.Drawing.Point(321, 194);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(39, 26);
            this.labelX3.TabIndex = 15;
            this.labelX3.Text = "Key?";
            // 
            // titlerec
            // 
            this.titlerec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            // 
            // 
            // 
            this.titlerec.Border.Class = "TextBoxBorder";
            this.titlerec.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.titlerec.DisabledBackColor = System.Drawing.Color.White;
            this.titlerec.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titlerec.Location = new System.Drawing.Point(366, 194);
            this.titlerec.Name = "titlerec";
            this.titlerec.PreventEnterBeep = true;
            this.titlerec.Size = new System.Drawing.Size(229, 26);
            this.titlerec.TabIndex = 14;
            this.titlerec.TextChanged += new System.EventHandler(this.titlerec_TextChanged);
            // 
            // picVER
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.picVER, true);
            this.picVER.Image = global::DeReplaysManager.Properties.Resources.no_icon;
            this.picVER.Location = new System.Drawing.Point(601, 194);
            this.picVER.Name = "picVER";
            this.picVER.Size = new System.Drawing.Size(16, 16);
            this.picVER.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picVER.TabIndex = 22;
            this.picVER.TabStop = false;
            this.picVER.BackgroundImageChanged += new System.EventHandler(this.picVER_BackgroundImageChanged);
            this.picVER.BackgroundImageLayoutChanged += new System.EventHandler(this.picVER_BackgroundImageChanged);
            this.picVER.Click += new System.EventHandler(this.picVER_Click);
            // 
            // subreview
            // 
            this.subreview.AutoSize = true;
            this.subreview.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Custom2;
            this.subreview.Location = new System.Drawing.Point(3, 226);
            this.subreview.Name = "subreview";
            this.subreview.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.subreview.Size = new System.Drawing.Size(157, 28);
            this.subreview.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.DodgerBlue;
            this.subreview.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subreview.TabIndex = 21;
            this.subreview.Values.Image = global::DeReplaysManager.Properties.Resources.sendrev;
            this.subreview.Values.Text = "Submit Review";
            this.subreview.Click += new System.EventHandler(this.subreview_Click);
            // 
            // progressBarX1
            // 
            // 
            // 
            // 
            this.progressBarX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarX1.Location = new System.Drawing.Point(166, 226);
            this.progressBarX1.Name = "progressBarX1";
            this.progressBarX1.Size = new System.Drawing.Size(224, 28);
            this.progressBarX1.TabIndex = 23;
            this.progressBarX1.Text = "progressBarX1";
            // 
            // expaNEL
            // 
            this.expaNEL.CanvasColor = System.Drawing.SystemColors.Control;
            this.expaNEL.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expaNEL.Controls.Add(this.panel2);
            this.expaNEL.DisabledBackColor = System.Drawing.Color.Empty;
            this.expaNEL.Dock = System.Windows.Forms.DockStyle.Top;
            this.expaNEL.ExpandOnTitleClick = true;
            this.expaNEL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expaNEL.HideControlsWhenCollapsed = true;
            this.expaNEL.Location = new System.Drawing.Point(0, 0);
            this.expaNEL.Name = "expaNEL";
            this.expaNEL.Size = new System.Drawing.Size(799, 236);
            this.expaNEL.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expaNEL.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expaNEL.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expaNEL.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expaNEL.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expaNEL.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expaNEL.Style.GradientAngle = 90;
            this.expaNEL.TabIndex = 4;
            this.expaNEL.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expaNEL.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.SplitterBackground;
            this.expaNEL.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expaNEL.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expaNEL.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expaNEL.TitleStyle.ForeColor.Color = System.Drawing.Color.Gray;
            this.expaNEL.TitleStyle.GradientAngle = 90;
            this.expaNEL.TitleText = "Replay Details By ...";
            this.expaNEL.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.expaNEL_ExpandedChanged);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(236)))), ((int)(((byte)(240)))));
            this.panel2.Controls.Add(this.pgsbar);
            this.panel2.Controls.Add(this.importrec);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.bodytext);
            this.panel2.Controls.Add(this.lbltitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(799, 210);
            this.panel2.TabIndex = 4;
            // 
            // pgsbar
            // 
            // 
            // 
            // 
            this.pgsbar.BackgroundStyle.BorderGradientAngle = 10;
            this.pgsbar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pgsbar.Location = new System.Drawing.Point(304, 89);
            this.pgsbar.Name = "pgsbar";
            this.pgsbar.ProgressType = DevComponents.DotNetBar.eProgressItemType.Marquee;
            this.pgsbar.Size = new System.Drawing.Size(116, 14);
            this.pgsbar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.pgsbar.TabIndex = 20;
            this.pgsbar.Text = "Loading..";
            // 
            // importrec
            // 
            this.importrec.AutoSize = true;
            this.importrec.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.InputControl;
            this.importrec.Location = new System.Drawing.Point(284, 174);
            this.importrec.Name = "importrec";
            this.importrec.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.importrec.Size = new System.Drawing.Size(172, 31);
            this.importrec.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Maroon;
            this.importrec.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importrec.TabIndex = 19;
            this.importrec.Values.Image = global::DeReplaysManager.Properties.Resources.clip_rec;
            this.importrec.Values.Text = "...";
            this.importrec.Visible = false;
            this.importrec.Click += new System.EventHandler(this.importrec_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(78, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Import Recorded Game >";
            this.label2.Visible = false;
            // 
            // bodytext
            // 
            this.bodytext.BackColorRichTextBox = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(236)))), ((int)(((byte)(240)))));
            // 
            // 
            // 
            this.bodytext.BackgroundStyle.BorderGradientAngle = 0;
            this.bodytext.BackgroundStyle.BorderLightGradientAngle = 0;
            this.bodytext.BackgroundStyle.Class = "RibbonFileMenuColumnOneContainer";
            this.bodytext.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.bodytext.ForeColor = System.Drawing.Color.Black;
            this.bodytext.Location = new System.Drawing.Point(12, 26);
            this.bodytext.Name = "bodytext";
            this.bodytext.ReadOnly = true;
            this.bodytext.Rtf = "{\\rtf1\\ansi\\deff0\\nouicompat{\\fonttbl{\\f0\\fnil\\fcharset0 Tahoma;}}\r\n{\\*\\generator" +
    " Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\b\\f0\\fs20\\lang1033 ...\\par\r\n}\r\n";
            this.bodytext.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.bodytext.Size = new System.Drawing.Size(775, 142);
            this.bodytext.TabIndex = 4;
            this.bodytext.Text = "...";
            this.bodytext.Visible = false;
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbltitle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbltitle.Location = new System.Drawing.Point(0, 0);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(18, 18);
            this.lbltitle.TabIndex = 2;
            this.lbltitle.Text = "..";
            this.lbltitle.Visible = false;
            // 
            // ReplayView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 525);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReplayView";
            this.Text = "ReplayView";
            this.Load += new System.EventHandler(this.ReplayView_Load);
            this.panel1.ResumeLayout(false);
            this.expandablePanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVER)).EndInit();
            this.expaNEL.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ExpandablePanel expaNEL;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbltitle;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx bodytext;
        private ComponentFactory.Krypton.Toolkit.KryptonButton importrec;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.ProgressBarX pgsbar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX nickFIELD;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX titlerec;
        private System.Windows.Forms.RichTextBox subody;
        private DevComponents.DotNetBar.LabelX vidLBL;
        private DevComponents.DotNetBar.Controls.TextBoxX vidFIELD;
        private DevComponents.DotNetBar.Controls.Line line1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton subreview;
        private System.Windows.Forms.PictureBox picVER;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX1;
    }
}