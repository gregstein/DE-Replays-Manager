namespace DeReplaysManager
{
    partial class Submission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Submission));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.subreview = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.titlerec = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.reclabel = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.subody = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.REVbasic = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.REVpro = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblticket = new DevComponents.DotNetBar.LabelX();
            this.ticketcode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tickethelp = new System.Windows.Forms.Button();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 502);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.progressBar2);
            this.panel2.Controls.Add(this.subreview);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 433);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(605, 69);
            this.panel2.TabIndex = 1;
            // 
            // progressBar2
            // 
            this.progressBar2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar2.Location = new System.Drawing.Point(0, 55);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(605, 14);
            this.progressBar2.TabIndex = 21;
            // 
            // subreview
            // 
            this.subreview.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Custom2;
            this.subreview.Location = new System.Drawing.Point(156, 0);
            this.subreview.Name = "subreview";
            this.subreview.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.subreview.Size = new System.Drawing.Size(202, 43);
            this.subreview.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.DodgerBlue;
            this.subreview.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subreview.TabIndex = 20;
            this.subreview.Values.Image = global::DeReplaysManager.Properties.Resources.sendrev;
            this.subreview.Values.Text = "Submit For Review";
            this.subreview.Click += new System.EventHandler(this.subreview_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.groupPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(605, 433);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.flowLayoutPanel1);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Location = new System.Drawing.Point(3, 3);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(595, 349);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 4;
            this.groupPanel2.Text = "Your Replay Details";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.labelX1);
            this.flowLayoutPanel1.Controls.Add(this.textBoxX1);
            this.flowLayoutPanel1.Controls.Add(this.labelX3);
            this.flowLayoutPanel1.Controls.Add(this.titlerec);
            this.flowLayoutPanel1.Controls.Add(this.labelX4);
            this.flowLayoutPanel1.Controls.Add(this.reclabel);
            this.flowLayoutPanel1.Controls.Add(this.labelX2);
            this.flowLayoutPanel1.Controls.Add(this.line1);
            this.flowLayoutPanel1.Controls.Add(this.subody);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(589, 328);
            this.flowLayoutPanel1.TabIndex = 0;
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
            this.labelX1.Size = new System.Drawing.Size(98, 30);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "Nickname";
            // 
            // textBoxX1
            // 
            this.textBoxX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.DisabledBackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.SetFlowBreak(this.textBoxX1, true);
            this.textBoxX1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxX1.Location = new System.Drawing.Point(107, 3);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.Size = new System.Drawing.Size(233, 26);
            this.textBoxX1.TabIndex = 0;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.Location = new System.Drawing.Point(3, 39);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(98, 30);
            this.labelX3.TabIndex = 11;
            this.labelX3.Text = "Title";
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
            this.flowLayoutPanel1.SetFlowBreak(this.titlerec, true);
            this.titlerec.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titlerec.Location = new System.Drawing.Point(107, 39);
            this.titlerec.Name = "titlerec";
            this.titlerec.PreventEnterBeep = true;
            this.titlerec.Size = new System.Drawing.Size(443, 26);
            this.titlerec.TabIndex = 10;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.Location = new System.Drawing.Point(3, 75);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(90, 21);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "Replay Name";
            // 
            // reclabel
            // 
            this.reclabel.AutoSize = true;
            // 
            // 
            // 
            this.reclabel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.flowLayoutPanel1.SetFlowBreak(this.reclabel, true);
            this.reclabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reclabel.ForeColor = System.Drawing.Color.Maroon;
            this.reclabel.Location = new System.Drawing.Point(99, 75);
            this.reclabel.Name = "reclabel";
            this.reclabel.Size = new System.Drawing.Size(80, 21);
            this.reclabel.TabIndex = 9;
            this.reclabel.Text = "replayname";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.flowLayoutPanel1.SetFlowBreak(this.labelX2, true);
            this.labelX2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.DimGray;
            this.labelX2.Location = new System.Drawing.Point(3, 102);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(327, 21);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "Description (The reviewer will see this message.)";
            // 
            // line1
            // 
            this.line1.EndLineCapSize = new System.Drawing.Size(6, 0);
            this.line1.Location = new System.Drawing.Point(3, 129);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(505, 1);
            this.line1.TabIndex = 8;
            this.line1.Text = "line1";
            // 
            // subody
            // 
            this.subody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.subody.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.subody.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subody.ForeColor = System.Drawing.Color.MidnightBlue;
            this.subody.Location = new System.Drawing.Point(3, 136);
            this.subody.Name = "subody";
            this.subody.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.subody.Size = new System.Drawing.Size(583, 216);
            this.subody.TabIndex = 4;
            this.subody.Text = "Type here..";
            this.subody.Click += new System.EventHandler(this.subody_Click);
            this.subody.TextChanged += new System.EventHandler(this.subody_TextChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.REVbasic);
            this.flowLayoutPanel2.Controls.Add(this.REVpro);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 358);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(599, 29);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // REVbasic
            // 
            this.REVbasic.AutoSize = true;
            // 
            // 
            // 
            this.REVbasic.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.REVbasic.CheckBoxImageChecked = global::DeReplaysManager.Properties.Resources.checkedico;
            this.REVbasic.CheckBoxImageIndeterminate = global::DeReplaysManager.Properties.Resources.uncheckedico;
            this.REVbasic.CheckBoxImageUnChecked = global::DeReplaysManager.Properties.Resources.uncheckedico;
            this.REVbasic.Checked = true;
            this.REVbasic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.REVbasic.CheckValue = "Y";
            this.REVbasic.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.REVbasic.Location = new System.Drawing.Point(3, 3);
            this.REVbasic.Name = "REVbasic";
            this.REVbasic.Size = new System.Drawing.Size(104, 22);
            this.REVbasic.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.REVbasic.TabIndex = 2;
            this.REVbasic.Text = "Basic Review";
            // 
            // REVpro
            // 
            this.REVpro.AutoSize = true;
            // 
            // 
            // 
            this.REVpro.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.REVpro.CheckBoxImageChecked = global::DeReplaysManager.Properties.Resources.checkedico;
            this.REVpro.CheckBoxImageIndeterminate = global::DeReplaysManager.Properties.Resources.uncheckedico;
            this.REVpro.CheckBoxImageUnChecked = global::DeReplaysManager.Properties.Resources.uncheckedico;
            this.REVpro.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.REVpro.Location = new System.Drawing.Point(113, 3);
            this.REVpro.Name = "REVpro";
            this.REVpro.Size = new System.Drawing.Size(182, 22);
            this.REVpro.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.REVpro.TabIndex = 3;
            this.REVpro.Text = "Request Pro Review Ticket";
            this.REVpro.CheckedChanged += new System.EventHandler(this.REVpro_CheckedChanged);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.lblticket);
            this.flowLayoutPanel3.Controls.Add(this.ticketcode);
            this.flowLayoutPanel3.Controls.Add(this.tickethelp);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 393);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(599, 43);
            this.flowLayoutPanel3.TabIndex = 6;
            // 
            // lblticket
            // 
            this.lblticket.AutoSize = true;
            // 
            // 
            // 
            this.lblticket.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblticket.Enabled = false;
            this.lblticket.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblticket.ForeColor = System.Drawing.Color.Maroon;
            this.lblticket.Location = new System.Drawing.Point(3, 3);
            this.lblticket.Name = "lblticket";
            this.lblticket.Size = new System.Drawing.Size(62, 18);
            this.lblticket.TabIndex = 2;
            this.lblticket.Text = "Ticket Key";
            this.lblticket.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // ticketcode
            // 
            this.ticketcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            // 
            // 
            // 
            this.ticketcode.Border.Class = "TextBoxBorder";
            this.ticketcode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ticketcode.DisabledBackColor = System.Drawing.Color.White;
            this.ticketcode.Enabled = false;
            this.ticketcode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ticketcode.Location = new System.Drawing.Point(71, 3);
            this.ticketcode.Name = "ticketcode";
            this.ticketcode.PreventEnterBeep = true;
            this.ticketcode.Size = new System.Drawing.Size(445, 23);
            this.ticketcode.TabIndex = 6;
            this.ticketcode.WatermarkEnabled = false;
            // 
            // tickethelp
            // 
            this.tickethelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.tickethelp.Image = global::DeReplaysManager.Properties.Resources.icons8_question_mark_64;
            this.tickethelp.Location = new System.Drawing.Point(522, 3);
            this.tickethelp.Name = "tickethelp";
            this.tickethelp.Size = new System.Drawing.Size(21, 23);
            this.tickethelp.TabIndex = 7;
            this.tickethelp.UseVisualStyleBackColor = true;
            this.tickethelp.Click += new System.EventHandler(this.tickethelp_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // Submission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(605, 502);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Submission";
            this.Text = "Submit Your Replay For Review";
            this.Load += new System.EventHandler(this.Submission_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.RichTextBox subody;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.Controls.Line line1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private DevComponents.DotNetBar.Controls.CheckBoxX REVbasic;
        private DevComponents.DotNetBar.Controls.CheckBoxX REVpro;
        private System.Windows.Forms.Panel panel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton subreview;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private DevComponents.DotNetBar.LabelX lblticket;
        private DevComponents.DotNetBar.Controls.TextBoxX ticketcode;
        private System.Windows.Forms.Button tickethelp;
        private System.Windows.Forms.ProgressBar progressBar2;
        private DevComponents.DotNetBar.LabelX reclabel;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX titlerec;
    }
}