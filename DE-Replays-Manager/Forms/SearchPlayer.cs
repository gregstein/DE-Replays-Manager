using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DEBoard;

namespace De_Roll
{
    public partial class SearchPlayer : KryptonForm
    {
        

        public SearchPlayer()
        {
            InitializeComponent();
        }
        public string NameP { get; set; }


        private void kryptonDataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)//if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    Process.Start(kryptonDataGridView3.SelectedCells[6].Value.ToString());

                }
                if (e.ColumnIndex == 5)//if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    Process.Start(kryptonDataGridView3.SelectedCells[7].Value.ToString());

                }
            }
            catch (SystemException)
            {
            }
        }

        private void SearchPlayer_Load(object sender, EventArgs e)
        {
            kryptonTextBox2.Text = this.NameP;
        }
        public void RemoveDuplicate(DataGridView grv)
        {
            for (int currentRow = 0; currentRow < grv.Rows.Count - 1; currentRow++)
            {
                DataGridViewRow rowToCompare = grv.Rows[currentRow];

                for (int otherRow = currentRow + 1; otherRow < grv.Rows.Count; otherRow++)
                {
                    DataGridViewRow row = grv.Rows[otherRow];

                    bool duplicateRow = true;

                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        if (!rowToCompare.Cells[cellIndex].Value.Equals(row.Cells[cellIndex].Value))
                        {
                            duplicateRow = false;
                            break;
                        }
                    }

                    if (duplicateRow)
                    {
                        grv.Rows.Remove(row);
                        otherRow--;
                    }
                }
            }
        }
        public async Task<string> DownloadStringAsync(Uri uri, int timeOut = 60000)
        {
            string output = null;
            bool cancelledOrError = false;
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Proxy = null;
                client.DownloadStringCompleted += (sender, e) =>
                {
                    if (e.Error != null || e.Cancelled)
                    {
                        cancelledOrError = true;
                    }
                    else
                    {
                        output = e.Result;
                    }
                };
                client.DownloadStringAsync(uri);

                var n = DateTime.Now;
                while (output == null && !cancelledOrError && DateTime.Now.Subtract(n).TotalMilliseconds < timeOut)
                {

                    await Task.Delay(100); // wait for respsonse
                }

            }

            return await Task.FromResult(output);
        }
        private string filterText = String.Empty;
        private async void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
            kryptonDataGridView3.Rows.Clear();
            await Task.Delay(500);

            if (filterText != kryptonTextBox2.Text)
            {

                Uri apiLOB = new Uri(@"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=3&start=1&search=" + kryptonTextBox2.Text);
                string jsonLOBBIES = await DownloadStringAsync(apiLOB);
                //MessageBox.Show(jsonLOBBIES.Substring(0, 8));
                var ldb = QueryPlayer.FromJson(jsonLOBBIES);

                int i = 1;
                foreach (var l in ldb.Leaderboard)
                {
                    i++;
                    LinkLabel ln = new LinkLabel();
                    Button btn = new Button();
                    ln.Text = "View";
                    ln.Name = "view" + i.ToString();
                    btn.Name = "btn" + i.ToString();
                    btn.Text = "Join";

                    kryptonDataGridView3.Rows.Add(l.Rank, l.Name, l.Rating, l.Games, "View", "View", @"https://www.ageofempires.com/stats/?profileId=" + l.ProfileId + @"&game=age2", @"http://steamcommunity.com/profiles/" + l.SteamId);
                }
            }
            RemoveDuplicate(kryptonDataGridView3);

            if (kryptonTextBox2.Text == null)
            {
                kryptonDataGridView3.Rows.Clear();
            }
        }
    }
}
