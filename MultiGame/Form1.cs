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

        #region 멤버변수

        // 서버와 TCP통신을 담당하는 객체
        private MyClient myClient;

        // 다른 클라이언트들을 관리하는 객체
        private ClientManager clientManager;    
        
        // 사용자의 캐릭터
        private ClientCharacter userCharacter;

        // 게임 시작 여부
        public bool IsGameStart { get; set; }                                       

        

        // UI
        private UserPanel.MainMenu_Screen mainMenu_Screen;
        private UserPanel.LobbyRoom_Screen lobbyRoom_Screen;
        private UserPanel.FindRoom_Screen findRoom_Screen;
        private UserPanel.MakeRoom_Form makeRoom_Form;
        private UserPanel.InGame_Screen inGame_Screen;
        #endregion



        public Form1()
        {
            InitializeComponent();
            InitializeScreen();

            // 다른 클라이언트들을 관리할 객체
            clientManager = new ClientManager();

            // 사용자 캐릭터
            userCharacter = new ClientCharacter(-1,new Point(364,293), 0);


            // 클라이언트 객체 생성
            myClient = new MyClient();

            // 서버로부터 메세지를 받으면 onTakeMessage함수 호출
            myClient.TakeMessage += new TakeMessageEventHandler(TakeMessage);

            

            this.Controls.Add(mainMenu_Screen);
        }

       

        ~Form1()
        {
            
        }

        // 폼이 완전히 로드되었을 때 호출
        private void Form1_Load(object sender, EventArgs e)
        {
            // 클라이언트 시작
            myClient.Start();
        }

        

        // 서버로부터 받은 메세지를 처리
        private void TakeMessage(string message)
        {
            // 메세지는 '@'으로 끝을 구분함, 메세지 여러개가 겹쳐있을 수 있기때문에 Split으로 나눔 ex) Location#1#2@Location#2#3@Location#5#2@
            string[] Messages = message.Split('@');

            for (int i = 0; i < Messages.Length - 1; i++)
            {
                // 메세지 해석
                ParseMessage(Messages[i]);
            }
        }

        // 메세지를 해석 후 실행
        private void ParseMessage(string Message)
        {
            // 메세지는 '#'으로 각 매개인자를 구분함
            string[] SplitMessage = Message.Split('#');

            switch (SplitMessage[0])
            {
                // 클라이언트의 캐릭터 위치 수신
                case "Location":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 좌표
                        int x = int.Parse(SplitMessage[2]);
                        int y = int.Parse(SplitMessage[3]);

                        ClientCharacter client;

                        // key == -1 ( 유저 캐릭터 )
                        if (key == -1)
                            client = userCharacter;
                        else
                        {
                            // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                            bool result = clientManager.ClientDic.TryGetValue(key, out client);

                            // 해당 클라이언트가 존재하지 않을경우 리턴
                            if (result == false) return;
                        }
                        if (key == -1)
                            Console.WriteLine($"동기화 :: X : {client.Location.X}  Y : {client.Location.Y}  ->  X : {x}  Y : {y}");
                        else
                            Console.WriteLine($"{client.key}번 클라이언트 동기화 :: X : {client.Location.X}  Y : {client.Location.Y}  ->  X : {x}  Y : {y}");

                        client.Location = new Point(x, y);
                    }
                    break;
                // 클라이언트 정보 업데이트
                case "UpdateClient":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 스킨 번호
                        int skinNum = int.Parse(SplitMessage[2]);

                        ClientCharacter client;

                        // key == -1 ( 유저 캐릭터 )
                        if (key == -1)
                            client = userCharacter;
                        else
                        {
                            // 키를 이용하여 배열에서 해당 클라이언트를 찾아 client에 대입함 ( out ) 그 후, 결과를 result에 대입 ( 찾았으면 TRUE / 아니면 FALSE )
                            bool result = clientManager.ClientDic.TryGetValue(key, out client);

                            // 해당 클라이언트가 존재하지 않을경우 리턴
                            if (result == false) return;
                        }

                        client.SetSkin(skinNum);

                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            UpdateLobby();
                        }));
                    }
                    break;
                // 다른 클라이언트가 방을 나감
                case "LeaveRoom":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        ClientCharacter clientChar;

                        // 키에 해당하는 캐릭터를 찾아 client변수에 대입
                        bool result = clientManager.ClientDic.TryGetValue(key, out clientChar);

                        // 만약 존재하지 않으면 리턴
                        if (result == false)
                        {
                            return;
                        }

                        //클라이언트 배열에서 제거
                        clientManager.RemoveClient(clientChar);
                        inGame_Screen.Paint -= clientChar.OnPaint;


                        // 게임이 시작하지 않았다면 로비 업데이트 
                        if( IsGameStart == false)
                        {
                            UpdateLobby();
                        }
                        
                    }
                    break;
                // 다른 클라이언트의 키보드 입력
                case "KeyInput":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 입력된 키
                        char InpKey = char.Parse(SplitMessage[2]);

                        // 눌렸으면 true / 아니면 false
                        bool bKeyDown = bool.Parse(SplitMessage[3]);

                        ClientCharacter client;

                        // 키에 해당하는 캐릭터를 찾아 client변수에 대입
                        bool result = clientManager.ClientDic.TryGetValue(key, out client);

                        // 해당 클라이언트가 존재하지 않을경우 리턴
                        if (result == false) return;

                        switch (InpKey)
                        {
                            case 'L':
                                client.bLeftDown = bKeyDown;
                                break;
                            case 'R':
                                client.bRightDown = bKeyDown;
                                break;
                        }
                        if (bKeyDown == true) client.MoveStart();
                        else if (!(client.bLeftDown || client.bRightDown))
                        {
                            client.MoveStop();
                        }

                    }
                    break;
                // 방에 입장
                case "EnterRoom":
                    {
                        // 방 번호
                        string roomCode = SplitMessage[1];

                        // 방 제목
                        string roomTItle = SplitMessage[2];

                        Console.WriteLine($"{roomCode}번 '{roomTItle}' 방에 접속");


                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            lobbyRoom_Screen.roomTitle_lbl.Text = $"{roomCode}번방 {roomTItle}";
                            this.Controls.Clear();
                            this.Controls.Add(lobbyRoom_Screen);
                            RequestLobbyInfo(false);
                            UpdateLobby();
                        }));
                    }
                    break;
                // 방에 다른 클라이언트 입장
                case "EnterRoomOther":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 레디 여부
                        bool bReady = bool.Parse(SplitMessage[2]);

                        // 스킨 번호
                        int skinNum = int.Parse(SplitMessage[3]);

                        ClientCharacter clientCharacter;

                        // 새로운 클라이언트 생성
                        clientCharacter = clientManager.AddOrGetClient(key, new Point(0, 0), 1);
                        clientCharacter.isReady = false;
                        clientCharacter.SetSkin(skinNum);
                        

                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            UpdateLobby();
                        }));
                    }
                    break;
                // 다른 클라이언트가 레디함
                case "ReadyOther":
                    {
                        // 플레이어 번호
                        int key = int.Parse(SplitMessage[1]);

                        // 레디 여부
                        bool bReady = bool.Parse(SplitMessage[2]);

                        // 플레이어 번호를 가지고 플레이어를 찾음
                        ClientCharacter clientCharacter;

                        bool result = clientManager.ClientDic.TryGetValue(key, out clientCharacter);

                        // 존재하지 않은 클라이언트면 종료
                        if (result == false) return;

                        clientCharacter.isReady = bReady;

                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            UpdateLobby();
                        }));
                    }
                    break;
                // 게임시 시작함
                case "RoomStart":
                    {
                        IsGameStart = true;
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            this.Controls.Clear();
                            this.Controls.Add(inGame_Screen);
                            inGame_Screen.StartUpdateScreen(true);
                        }));

                        foreach (var item in clientManager.ClientDic)
                        {
                            inGame_Screen.Paint += item.Value.OnPaint;
                            item.Value.isVisible = true;
                            item.Value.Location = new Point(0, 0);
                        }

                        // 유저 캐릭터
                        inGame_Screen.Paint += userCharacter.OnPaint;
                        userCharacter.isReady = false;
                        userCharacter.Location = new Point(0, 0);
                        userCharacter.isVisible = true;
                    }
                    break;
                // 클라이언트가 접속중인지 확인하기 위해 서버가 보내는 메시지
                case "Ping":
                    {
                        // 클라이언트는 반응이 없어도 됨
                    }
                    break;
                // 방찾기 화면에서 방 목록관련 정보 수신
                case "RoomList":
                    {
                        switch (SplitMessage[1])
                        {
                            // 방 정보 추가 ( 방찾기 화면 입장 or 방이 새로 생김 )
                            case "Add":
                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    findRoom_Screen.roomList_GridView.Rows.Add(SplitMessage[2], SplitMessage[3], SplitMessage[4] + "/3");
                                }));
                                break;
                            // 방 정보 삭제 ( 방이 사라짐 or 해당 방 게임이 시작함 )
                            case "Del":
                                {
                                    foreach (DataGridViewRow item in findRoom_Screen.roomList_GridView.Rows)
                                    {
                                        // 방 배열을 돌면서 방번호와 같은 방을 찾음
                                        if (item.Cells[0].Value.ToString() == SplitMessage[2])
                                        {
                                            this.Invoke(new MethodInvoker(delegate ()
                                            {
                                                // 방 제거
                                                findRoom_Screen.roomList_GridView.Rows.Remove(item);
                                            }));
                                        }
                                    }

                                }
                                break;
                            // 방 정보 수정 ( 방의 인원수가 변경됨 )
                            case "Update":
                                {
                                    foreach (DataGridViewRow item in findRoom_Screen.roomList_GridView.Rows)
                                    {
                                        // 방 배열을 돌면서 방번호와 같은 방을 찾음
                                        if (item.Cells[0].Value.ToString() == SplitMessage[2])
                                        {
                                            this.Invoke(new MethodInvoker(delegate ()
                                            {
                                                // 인원수 업데이트
                                                item.Cells[2].Value = SplitMessage[3] + "/3";
                                            }));
                                        }
                                    }
                                }
                                break;
                            default:
                                break;

                            }
                    }
                    break;
                case "Error":
                    {
                        int errorCode = int.Parse(SplitMessage[1]);

                        switch(errorCode)
                        {
                            case 0:
                                {
                                    MessageBox.Show("해당 방은 꽉찼습니다.", $"에러코드 : {errorCode}", MessageBoxButtons.OK);
                                }
                                break;
                            case 1:
                                {
                                    MessageBox.Show("존재하지 않은 방입니다.",$"에러코드 : {errorCode}", MessageBoxButtons.OK);
                                }
                                break;
                            default:
                                Console.WriteLine("알수 없는 에러코드 {0}",errorCode);
                                break;
                        }
                    }
                    break;
                default:
                    Console.WriteLine("디폴트 : {0}", Message);
                    break;
            }
        }


        // 유저가 입력한 키를 서버로 보냄 ( 입력키, 누르면 true / 뗐으면 false )
        private void SendInputedKey(char inputKey, bool bPressed)
        {
            string message = $"KeyInput#{inputKey}#{bPressed}#@";
            myClient.SendMessage(message);
        }

        // 서버로 방만들기 요청
        public void RequestCreateRoom(string RoomTitle)
        {
            myClient.SendMessage($"CreateRoom#{RoomTitle}@");
        }

        // 로비 정보 요청
        public void RequestLobbyInfo(bool result)
        {
            myClient.SendMessage($"LobbyInfo#{result}@");
        }
        
        // 방 입장 요청
        public void RequestEnterRoom(int i)
        {
            myClient.SendMessage($"TryEnterRoom#{i}@");
        }

        // 준비
        public void RequestReady(bool bReady)
        {
            myClient.SendMessage($"Ready#{bReady}@");
        }

        // 로비 화면 업데이트 ( 누가 접속했고,  레디를 했는지 등.. )
        public void UpdateLobby()
        {
            // 현재 로비화면이 아닐경우 리턴
            if (this.Controls.Contains(lobbyRoom_Screen) == false) return;


            ClientCharacter[] clientChar = new ClientCharacter[2] { null, null };

            int count = 0;


            // 로비에 있는 다른 두 플레이어를 찾음
            foreach (var item in clientManager.ClientDic)
            {
                clientChar[count] = item.Value;
                count++;

                if (count > 2) break;
            }




            // 로비화면 캐릭터 이미지 업데이트
            lobbyRoom_Screen.centerPicBox.Image = userCharacter.image;

            // 레디했으면 레디버튼 삽입
            if (userCharacter.isReady == true)
            {
                lobbyRoom_Screen.centerCheckPicBox.Image = MultiGame.Properties.Resources.ReadyCheck;
            }
            else
            {
                lobbyRoom_Screen.centerCheckPicBox.Image = null;
            }

            // 다른 클라이언트를 못찾았으면 빈 이미지 삽입
            if (clientChar[0] == null)
            {
                lobbyRoom_Screen.leftPicBox.Image = null;
                lobbyRoom_Screen.leftCheckPicBox.Image = null;
            }
            else
            {
                lobbyRoom_Screen.leftPicBox.Image = clientChar[0].image;

                // 레디 했으면 레디버튼 삽입
                if (clientChar[0].isReady == true)
                {
                    lobbyRoom_Screen.leftCheckPicBox.Image = MultiGame.Properties.Resources.ReadyCheck;
                }
                else
                {
                    lobbyRoom_Screen.leftCheckPicBox.Image = null;
                }
                   
            }

            // 다른 클라이언트를 못찾았으면 빈 이미지 삽입
            if ( clientChar[1] == null)
            {
                lobbyRoom_Screen.rightPicBox.Image = null;
                lobbyRoom_Screen.rightCheckPicBox.Image = null;
            }
            else
            {
                lobbyRoom_Screen.rightPicBox.Image = clientChar[1].image;

                // 레디 했으면 레디버튼 삽입
                if (clientChar[1].isReady == true)
                {
                    lobbyRoom_Screen.rightCheckPicBox.Image = MultiGame.Properties.Resources.ReadyCheck;
                }
                else
                {
                    lobbyRoom_Screen.rightCheckPicBox.Image = null;
                }
            }

            // 레디 버튼 텍스트 업데이트
            if(userCharacter.isReady == true)
            {
                lobbyRoom_Screen.ready_btn.Text = "준비 취소";
            }
            else
            {
                lobbyRoom_Screen.ready_btn.Text = "준비";
            }
            
        }







        #region UI관련

        private void InitializeScreen()
        {
            // 메인 메뉴
            mainMenu_Screen = new UserPanel.MainMenu_Screen();
            mainMenu_Screen.findRoom_btn.Click += button_Click;
            mainMenu_Screen.makeRoom_btn.Click += button_Click;
            mainMenu_Screen.exitGame_btn.Click += button_Click;

            // 로비
            lobbyRoom_Screen = new UserPanel.LobbyRoom_Screen();
            lobbyRoom_Screen.ready_btn.Click += button_Click;
            lobbyRoom_Screen.lobbyToFind_btn.Click += button_Click;

            // 방찾기
            findRoom_Screen = new UserPanel.FindRoom_Screen();
            findRoom_Screen.findToMain_btn.Click += button_Click;
            findRoom_Screen.enterRoom_btn.Click += button_Click;
            findRoom_Screen.roomList_GridView.CellDoubleClick += DataGridDoubleClick;

            // 인게임
            inGame_Screen = new UserPanel.InGame_Screen();
          

            // 방만들기
            makeRoom_Form = new UserPanel.MakeRoom_Form();
            makeRoom_Form.make_btn.Click += button_Click;
            makeRoom_Form.cancelMakeRoom_btn.Click += button_Click;

        }

        // 키가 눌렸을 때
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (IsGameStart == false)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            // 왼쪽 방향키
            if (keyData == Keys.Left)
            {
                if (userCharacter.bLeftDown == false)
                {
                    // bLeftDown을 True로 바꿔 MoveCharacter_timer이 호출될 때 마다 이동하게 함
                    userCharacter.bLeftDown = true;

                    // 서버한테 이동을 시작했다고 알림
                    SendInputedKey('L', true);

                    // MoveCharacter_timer을 주기적으로 호출하는 타이머 시작
                    userCharacter.MoveStart();
                }
                return true;
            }

            // 오른쪽 방향키
            if (keyData == Keys.Right)
            {
                if (userCharacter.bRightDown == false)
                {
                    // bRightDown을 True로 바꿔 MoveCharacter_timer이 호출될 때 마다 이동하게 함
                    userCharacter.bRightDown = true;

                    // 서버한테 이동을 시작했다고 알림
                    SendInputedKey('R', true);

                    // MoveCharacter_timer을 주기적으로 호출하는 타이머 시작
                    userCharacter.MoveStart();                                            
                }
                    
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // 키가 뗴어졌을 때
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (IsGameStart == false)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    // bLeftDown을 False로 바꿔 MoveCharacter_timer이 호출되어도 이동하지 않음
                    userCharacter.bLeftDown = false;

                    // 왼쪽 방향키가 떼어졌다는걸 알림
                    SendInputedKey('L', false);
                    break;

                case Keys.Right:
                    // bRightDown을 False로 바꿔 MoveCharacter_timer이 호출되어도 이동하지 않음
                    userCharacter.bRightDown = false;

                    // 왼쪽 방향키가 떼어졌다는걸 알림
                    SendInputedKey('R', false);
                    break;
            }
            // 왼쪽과 오른쪽 모두 떼어져 있다면 캐릭터 이동 타이머를 멈춤
            if (!(userCharacter.bLeftDown || userCharacter.bRightDown)) userCharacter.MoveStop();     
        }




        // 폼의 포커스가 풀리면 ( 알트 탭, 다른 윈도우 선택시 ) 이벤트 발생
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            // 게임이 시작하지 않았다면 키 입력을 처리할 필요가 없음
            if (IsGameStart == false)
            {
                return;
            }

            // 왼쪽 키가 떼어졌다고 설정 후 서버에 알림
            userCharacter.bLeftDown = false;                                                            
            SendInputedKey('L', false);

            // 오른쪽 키가 떼어졌다고 설정 후 서버에 알림
            userCharacter.bRightDown = false;                                                           
            SendInputedKey('R', false);

            // 이동이 중단되었기 때문에 이동 타이머 멈춤
            userCharacter.MoveStop();

        }

        /*
        // 폼을 그릴때 호출되는 메소드 
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        */

        // 버튼을 클릭했을 때 호출됨
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch(btn.Name)
            {
                // 메인메뉴 화면 ( MainMenu_Screen )
                case "makeRoom_btn":    // 방만들기
                    {
                        makeRoom_Form.roomTitle_TB.Text = "";

                        // 방제목 입력하는 폼을 띄움
                        makeRoom_Form.ShowDialog();
                    }
                    break;
                case "findRoom_btn":    // 방찾기
                    {
                        // 방목록 초기화
                        findRoom_Screen.roomList_GridView.Rows.Clear();

                        // 앞으로 서버로 부터 방목록 정보를 받도록 설정
                        RequestLobbyInfo(true);

                        // 방찾기 창을 띄움
                        this.Controls.Clear();
                        this.Controls.Add(findRoom_Screen);
                    }
                    break;
                case "exitGame_btn":    // 게임종료
                    {
                        MessageBox.Show("종료", "게임을 종료합니다.", MessageBoxButtons.OK);
                        Application.Exit();
                    }
                    break;

                // 방 만들기 화면 ( MakeRoom_Screen )
                case "cancelMakeRoom_btn":// 취소
                    {
                        // 방만들기 폼을 종료하고 메인화면으로 이동
                        this.Controls.Clear();
                        this.Controls.Add(mainMenu_Screen);
                        makeRoom_Form.Close();
                    }
                    break;
                case "make_btn":        // 만들기
                    {
                        // 서버로 제목과 함께 방만들기 요청을 보냄
                        RequestCreateRoom(makeRoom_Form.roomTitle_TB.Text);

                        // 방만들기 폼 종료
                        makeRoom_Form.Close();
                    }
                    break;

                // 방 찾기 화면 ( FindRoom_Screen )
                case "enterRoom_btn": // 입장
                    {
                        // 선택한 방이 없으면 무시
                        if (findRoom_Screen.roomList_GridView.SelectedRows.Count == 0)
                        {
                            return;
                        }

                        // 선택한 방( 행 )의 키를 받음
                        int roomKey = int.Parse(findRoom_Screen.roomList_GridView.SelectedRows[0].Cells[0].Value.ToString());

                        // 서버로 입장 요청
                        RequestEnterRoom(roomKey);
                    }
                    break;
                case "findToMain_btn":  // 뒤로가기
                    {
                        // 서버로부터 받는 방목록 정보 수신을 종료함
                        RequestLobbyInfo(false);

                        // 메인화면으로 이동
                        this.Controls.Clear();
                        this.Controls.Add(mainMenu_Screen);
                    }
                    break;
                // 방 대기 화면 ( Room_Screen )  
                case "ready_btn":       // 준비 / 준비 취소
                    {
                        // 유저가 준비상태에서 버튼 누름
                        if ( userCharacter.isReady == true )
                        {
                            // 준비 취소 시킴
                            RequestReady(false);
                            userCharacter.isReady = false;
                        }
                        else
                        {
                            // 준비 상태로 만듬
                            RequestReady(true);
                            userCharacter.isReady = true;
                        }
                        UpdateLobby();
                    }
                    break;
                case "lobbyToFind_btn":     // 나가기
                    {
                        // 서버로 방을 나갔다고 알림
                        myClient.SendMessage($"ExitLobby#d@");

                        // 방찾기화면으로 이동, 방목록 정보 수신
                        findRoom_Screen.roomList_GridView.Rows.Clear();
                        RequestLobbyInfo(true);

                        userCharacter.isReady = false;
                        this.Controls.Clear();
                        this.Controls.Add(findRoom_Screen);
                    }
                    break;

                // 모든 화면 공용
                case "goMain_btn":     // 메인화면으로, 뒤로가기 등..
                    {
                        this.Controls.Clear();
                        this.Controls.Add(mainMenu_Screen);
                    }
                    break;
                default:
                    break;
            }

            
        }
        private void DataGridDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int roomKey = int.Parse(findRoom_Screen.roomList_GridView.SelectedRows[0].Cells[0].Value.ToString());
            RequestEnterRoom(roomKey);
        }

        #endregion
    }
}