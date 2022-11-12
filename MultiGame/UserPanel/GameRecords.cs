using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame.UserPanel
{
    public partial class GameRecords : UserControl
    {
        public GameRecords()
        {
            InitializeComponent();
            label4.Font = new Font(ResourceLibrary.Families[0], 25, FontStyle.Regular);
            score_GridView.DefaultCellStyle.Font = new Font(ResourceLibrary.Families[1], 16, FontStyle.Regular);
        }

        private void GameRecords_Load(object sender, EventArgs e)
        {
            
        }

        public void UpdateScoreBoard(List<string> titles, List<int> times)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < titles.Count)
                {
                    score_GridView.Rows.Add($"{i + 1}", titles[i], TimeToString(times[i]));
                }
                else
                {
                    score_GridView.Rows.Add($"{i + 1}", "-", "-");
                }
            }
        }

        private string TimeToString(int time)
        {
            int sec = time / 1000;
            int min = sec / 60;
            sec %= 60;

            if (min > 0)
                return $"{min}분 {sec}초";
            else
                return $"{sec}초";
        }

        private void score_GridView_SelectionChanged(object sender, EventArgs e)
        {
            score_GridView.ClearSelection();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
            
        }

        private void GameRecords_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid);
        }
    }
}
