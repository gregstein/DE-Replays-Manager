namespace DeReplaysManager
{
    partial class KeyInfo
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.aoebuildsbtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DeReplaysManager.Properties.Resources.buykeys;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(449, 232);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // aoebuildsbtn
            // 
            this.aoebuildsbtn.Image = global::DeReplaysManager.Properties.Resources.aoebuildslogo;
            this.aoebuildsbtn.Location = new System.Drawing.Point(173, 241);
            this.aoebuildsbtn.Name = "aoebuildsbtn";
            this.aoebuildsbtn.Size = new System.Drawing.Size(103, 30);
            this.aoebuildsbtn.TabIndex = 6;
            this.aoebuildsbtn.UseVisualStyleBackColor = true;
            this.aoebuildsbtn.Click += new System.EventHandler(this.aoebuildsbtn_Click);
            // 
            // KeyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(478, 283);
            this.Controls.Add(this.aoebuildsbtn);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(494, 322);
            this.MinimumSize = new System.Drawing.Size(494, 322);
            this.Name = "KeyInfo";
            this.ShowIcon = false;
            this.Text = "What\'s Ticket Key?";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button aoebuildsbtn;
    }
}