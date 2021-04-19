namespace DeReplaysManager
{
    partial class ImportREC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportREC));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.progBAR = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.importrecord = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label1 = new System.Windows.Forms.Label();
            this.listPROFILE = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panel3 = new System.Windows.Forms.Panel();
            this.repLIST = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(236)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 318);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.27044F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.72956F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(436, 318);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.progBAR);
            this.panel2.Controls.Add(this.importrecord);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.listPROFILE);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(430, 67);
            this.panel2.TabIndex = 0;
            // 
            // progBAR
            // 
            // 
            // 
            // 
            this.progBAR.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progBAR.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progBAR.Location = new System.Drawing.Point(0, 47);
            this.progBAR.Name = "progBAR";
            this.progBAR.Size = new System.Drawing.Size(430, 20);
            this.progBAR.TabIndex = 21;
            // 
            // importrecord
            // 
            this.importrecord.AutoSize = true;
            this.importrecord.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Custom1;
            this.importrecord.Location = new System.Drawing.Point(328, 10);
            this.importrecord.Name = "importrecord";
            this.importrecord.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.importrecord.Size = new System.Drawing.Size(77, 28);
            this.importrecord.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.SteelBlue;
            this.importrecord.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.75F, System.Drawing.FontStyle.Bold);
            this.importrecord.TabIndex = 20;
            this.importrecord.Values.Image = global::DeReplaysManager.Properties.Resources.icons8_save_48;
            this.importrecord.Values.Text = "Import";
            this.importrecord.Click += new System.EventHandler(this.importrecord_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SlateGray;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose Your Profile";
            // 
            // listPROFILE
            // 
            this.listPROFILE.DisplayMember = "Text";
            this.listPROFILE.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listPROFILE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listPROFILE.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPROFILE.FormattingEnabled = true;
            this.listPROFILE.ItemHeight = 18;
            this.listPROFILE.Location = new System.Drawing.Point(158, 13);
            this.listPROFILE.Name = "listPROFILE";
            this.listPROFILE.Size = new System.Drawing.Size(164, 24);
            this.listPROFILE.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.listPROFILE.TabIndex = 1;
            this.listPROFILE.SelectedIndexChanged += new System.EventHandler(this.listPROFILE_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.repLIST);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 76);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(430, 239);
            this.panel3.TabIndex = 1;
            // 
            // repLIST
            // 
            this.repLIST.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.repLIST.Location = new System.Drawing.Point(0, 29);
            this.repLIST.Name = "repLIST";
            this.repLIST.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Black;
            this.repLIST.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.repLIST.Size = new System.Drawing.Size(430, 210);
            this.repLIST.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(236)))), ((int)(((byte)(240)))));
            this.repLIST.TabIndex = 5;
            this.repLIST.SelectedIndexChanged += new System.EventHandler(this.repLIST_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(140, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "SaveGame Preview";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // ImportREC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 318);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportREC";
            this.Text = "Replay Importer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImportREC_FormClosed);
            this.Load += new System.EventHandler(this.ImportREC_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx listPROFILE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton importrecord;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox repLIST;
        private DevComponents.DotNetBar.Controls.ProgressBarX progBAR;
    }
}