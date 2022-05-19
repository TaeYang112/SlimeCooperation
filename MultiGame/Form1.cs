using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace MultiGame
{
    public partial class Form1 : Form
    {
        private MyClient myClient;                                                  // 서버와 통신하는 클래스
        private System.Windows.Forms.Timer MoveTimer;                               // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머

        // 키가 눌려있는지 확인하는 변수
        bool bLeftDown;
        bool bRIghtDown;

        public Form1()
        {
            InitializeComponent();

            // 멤버변수 초기화
            bLeftDown = false;
            bRIghtDown = false;

            // 클라이언트 객체 생성
            myClient = new MyClient(this);

            // 눌려있는 키를 확인하여 캐릭터를 움직이게 하는 타이머 ( 0.01초마다 확인 )
            MoveTimer = new System.Windows.Forms.Timer();
            MoveTimer.Interval = 10;
            MoveTimer.Tick += new EventHandler(MoveCharacter_timer);

        }

        // 폼이 완전히 로드되었을 때 호출
        private void Form1_Load(object sender, EventArgs e)
        {
            // 클라이언트 시작
            myClient.Start();
        }


        // 현재 KeyDown 되어있는 키를 확인하여 움직임
        private void MoveCharacter_timer(object sender, EventArgs e)
        {
            if (bLeftDown == true)
            {
                button1.Location = new Point(button1.Location.X - 2, button1.Location.Y);
            }
            if (bRIghtDown == true)
            {
                button1.Location = new Point(button1.Location.X + 2, button1.Location.Y);
            }
        }
        
        // 키가 눌렸을 때
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                if (bLeftDown == false)
                {
                    bLeftDown = true;
                    MoveTimer.Start();
                }
                return true;
            }
            if (keyData == Keys.Right)
            {
                if (bRIghtDown == false)
                {
                    bRIghtDown = true;
                    MoveTimer.Start();
                }
                    
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // 키가 뗴어졌을 때
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    bLeftDown = false;
                    break;
                case Keys.Right:
                    bRIghtDown = false;
                    break;
            }

            if (!(bLeftDown || bRIghtDown)) MoveTimer.Stop();
        }




        // 폼의 포커스가 풀리면 ( 알트 탭 등 ) Key UP 이벤트가 안생기기 때문에 강제로 키 다운 변수를 false로 바꿈
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            bLeftDown = false;
            bRIghtDown = false;
        }
    }
}