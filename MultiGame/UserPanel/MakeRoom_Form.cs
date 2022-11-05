using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiGame.UserPanel
{
    public partial class MakeRoom_Form : Form
    {
        public MakeRoom_Form()
        {
            InitializeComponent();

            roomTitle_TB.Font = new Font(ResourceLibrary.Families[1], 15, FontStyle.Regular);
            label1.Font = new Font(ResourceLibrary.Families[0], 30, FontStyle.Regular);

            make_btn.Font = new Font(ResourceLibrary.Families[0], 13, FontStyle.Regular);
            cancelMakeRoom_btn.Font = new Font(ResourceLibrary.Families[0], 13, FontStyle.Regular);

        }

        private void roomTitle_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                make_btn.PerformClick();
        }

        private void make_btn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelMakeRoom_btn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void MakeRoom_Form_Load(object sender, EventArgs e)
        {

        }

        private void MakeRoom_Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Brushes.Black, 10);
            g.DrawRectangle(pen, new Rectangle(new Point(1,1), new Size(630,179)));
        }
    }
}
