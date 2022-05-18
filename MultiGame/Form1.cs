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
        private MyClient myClient;

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
            System.Windows.Forms.Timer MoveTimer = new System.Windows.Forms.Timer();
            MoveTimer.Interval = 10;
            MoveTimer.Tick += new EventHandler(MoveCharacter_timer);
            MoveTimer.Start();


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
                button1.Location = new Point(button1.Location.X - 1, button1.Location.Y);
            }
            if (bRIghtDown == true)
            {
                button1.Location = new Point(button1.Location.X + 1, button1.Location.Y);
            }
        }

        // 키가 눌렸을 때
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        // 키가 뗴어졌을 때
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        }


        // 방향키, 엔터, 스페이스바 등은 KeyDown으로 감지할 수 없음
        // ProcessCmdKey를 오버라이딩해서 감지
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            Console.WriteLine(msg.Msg);
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Left:
                        bLeftDown = true;
                        return true;
                    case Keys.Right:
                        bRIghtDown = true;
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }

        // 방향키, 엔터, 스페이스바 등은 KeyUP 으로 감지할 수 없음
        // ProcessKeyPreview를 오버라이딩해서 감지
        protected override bool ProcessKeyPreview(ref Message msg)
        {
            const int WM_KEYUP = 0x101;
            const int WM_SYSKEYUP = 0x105;
            Console.WriteLine(msg.Msg);
            if ((msg.Msg == WM_KEYUP) || (msg.Msg == WM_SYSKEYUP))
            {
                switch ((Keys)msg.WParam)
                {
                    case Keys.Left:
                        bLeftDown = false;
                        return true;
                    case Keys.Right:
                        bRIghtDown = false;
                        return true;
                }
            }

            return base.ProcessKeyPreview(ref msg);
        }


        // 폼의 포커스가 풀리면 ( 알트 탭 등 ) Key UP 이벤트가 안생기기 때문에 강제로 키 다운 변수를 false로 바꿈
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            bLeftDown = false;
            bRIghtDown = false;
        }
    }
}