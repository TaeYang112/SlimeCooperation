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
    public partial class GameOver_Form : Form
    {
        public GameOver_Form()
        {
            InitializeComponent();
            label1.Font = new Font(ResourceLibrary.Families[0], 30, FontStyle.Regular);
            label2.Font = new Font(ResourceLibrary.Families[1], 15, FontStyle.Regular);
        }

        private void GameOver_Form_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid,
                                 Color.Black, 3, ButtonBorderStyle.Solid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
