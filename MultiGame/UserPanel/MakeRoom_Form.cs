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
    public partial class MakeRoom_Form : Form
    {
        public MakeRoom_Form()
        {
            InitializeComponent();
        }

        private void roomTitle_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                make_btn.PerformClick();
        }
    }
}
