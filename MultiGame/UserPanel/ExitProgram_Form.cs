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
    public partial class ExitProgram_Form : Form
    {
        public ExitProgram_Form()
        {
            InitializeComponent();
            label1.Font = new Font(ResourceLibrary.Families[1], 25, FontStyle.Regular);
            ExitNo_btn.Font = new Font(ResourceLibrary.Families[1], 15, FontStyle.Regular);
            ExitYes_btn.Font = new Font(ResourceLibrary.Families[1], 15, FontStyle.Regular);
        }

        private void ExitNo_btn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void ExitYes_btn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;

        }

        private void ExitProgram_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                DialogResult = DialogResult.No;
        }
    }
}
