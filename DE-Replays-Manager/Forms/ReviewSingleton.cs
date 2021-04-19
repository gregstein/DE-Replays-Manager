using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeReplaysManager
{
    
    public partial class ReviewSingleton : OfficeForm
    {
        public ReviewSingleton()
        {
            InitializeComponent();
            this.Text = "Review By " + this.SetAUTH;
        }
        public string SetURL { set; get; }
        public string SetAUTH { get; set; }
        public string myVIDEO;

        private void labelX1_Click(object sender, EventArgs e)
        {

        }
        async Task<int> ParseREV()
        {
            pgsbar.Visible = true;
            Core rp = new Core();
            ReplayParser dr = new ReplayParser();
            dr = ReplayParser.FromJsonText(await rp.DownloadSTRING(this.SetURL));
            NickPLAYER.Text = this.SetAUTH.Replace("By ","");
            myDESC.Text = dr.Description;

            if(dr.isVerified == true)
                NickPLAYER.Symbol = "\uf00c";
            else
            {
                yPRO.Text = "No";
                yPRO.Symbol = "\uf00d";
                NickPLAYER.Symbol = "\uf007";
            }
                

            //NickPLAYER.AutoSize = true;
            if (dr.Video != "")
            {
                myVIDEO = dr.Video;
                videoREV.Visible = true;
                isVID.Visible = true;
                //watchVID.Enabled = true;
                //watchVID.Visible = true;
                clipVID.Visible = true;
            }
            pgsbar.Visible = false;
            return 0;
        }
        private async void ReviewSingleton_Load(object sender, EventArgs e)
        {
            this.Text = "Review By " + this.SetAUTH;
            await ParseREV();
        }

        private void watchVID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void watchVID_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void clipVID_Click(object sender, EventArgs e)
        {
            try { Process.Start(myVIDEO); } catch { }
        }
    }
}
