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
    public partial class KeyInfo : Form
    {
        public KeyInfo()
        {
            InitializeComponent();
        }

        private void patreondermbtn_Click(object sender, EventArgs e)
        {
            Access("https://www.patreon.com/DERM");
        }

        private void aoebuildsbtn_Click(object sender, EventArgs e)
        {
            Access("https://www.aoebuilds.com/");
        }
        static Task<int> Access(string url)
        {
            try { Process.Start(url); return Task.FromResult(0); } catch (SystemException) { return Task.FromResult(0); }

        }
    }
}
