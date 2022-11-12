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
    public partial class GameOver_Control : UserControl
    {
        private Form1 form;

        public GameOver_Control(Form1 form)
        {
            InitializeComponent();
            this.form = form;

            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

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
            MainMenu_Screen mainMenu_Screen = new MainMenu_Screen(form);
            mainMenu_Screen.Name = "mainMenu_Screen";

            // 화면 전환
            form.ChangeScreen(mainMenu_Screen);

            GameManager.GetInstance().clientManager.ClientDic.Clear();
            GameManager.GetInstance().objectManager.ClearObjects();
        }
    }
}
