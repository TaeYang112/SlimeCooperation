
namespace MultiGame.UserPanel
{
    partial class LobbyRoom_Screen
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.roomTitle_lbl = new System.Windows.Forms.Label();
            this.leftPicBox = new System.Windows.Forms.PictureBox();
            this.centerPicBox = new System.Windows.Forms.PictureBox();
            this.rightPicBox = new System.Windows.Forms.PictureBox();
            this.ready_btn = new System.Windows.Forms.Button();
            this.lobbyToFind_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lb_LReadyCheck = new System.Windows.Forms.Label();
            this.lb_CReadyCheck = new System.Windows.Forms.Label();
            this.lb_RReadyCheck = new System.Windows.Forms.Label();
            this.roomNum_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // roomTitle_lbl
            // 
            this.roomTitle_lbl.BackColor = System.Drawing.Color.Transparent;
            this.roomTitle_lbl.Font = new System.Drawing.Font("맑은 고딕", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.roomTitle_lbl.Location = new System.Drawing.Point(288, 127);
            this.roomTitle_lbl.Name = "roomTitle_lbl";
            this.roomTitle_lbl.Size = new System.Drawing.Size(864, 36);
            this.roomTitle_lbl.TabIndex = 0;
            this.roomTitle_lbl.Text = "방 이름";
            this.roomTitle_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.roomTitle_lbl.UseCompatibleTextRendering = true;
            // 
            // leftPicBox
            // 
            this.leftPicBox.BackColor = System.Drawing.Color.White;
            this.leftPicBox.Location = new System.Drawing.Point(496, 351);
            this.leftPicBox.Name = "leftPicBox";
            this.leftPicBox.Size = new System.Drawing.Size(60, 50);
            this.leftPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.leftPicBox.TabIndex = 1;
            this.leftPicBox.TabStop = false;
            // 
            // centerPicBox
            // 
            this.centerPicBox.BackColor = System.Drawing.Color.White;
            this.centerPicBox.Location = new System.Drawing.Point(686, 351);
            this.centerPicBox.Name = "centerPicBox";
            this.centerPicBox.Size = new System.Drawing.Size(60, 50);
            this.centerPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.centerPicBox.TabIndex = 2;
            this.centerPicBox.TabStop = false;
            // 
            // rightPicBox
            // 
            this.rightPicBox.BackColor = System.Drawing.Color.White;
            this.rightPicBox.Location = new System.Drawing.Point(888, 351);
            this.rightPicBox.Name = "rightPicBox";
            this.rightPicBox.Size = new System.Drawing.Size(60, 50);
            this.rightPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rightPicBox.TabIndex = 3;
            this.rightPicBox.TabStop = false;
            // 
            // ready_btn
            // 
            this.ready_btn.BackColor = System.Drawing.Color.White;
            this.ready_btn.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.ready_btn.FlatAppearance.BorderSize = 3;
            this.ready_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ready_btn.Location = new System.Drawing.Point(638, 715);
            this.ready_btn.Name = "ready_btn";
            this.ready_btn.Size = new System.Drawing.Size(158, 63);
            this.ready_btn.TabIndex = 4;
            this.ready_btn.Text = "READY?";
            this.ready_btn.UseCompatibleTextRendering = true;
            this.ready_btn.UseVisualStyleBackColor = false;
            this.ready_btn.Click += new System.EventHandler(this.ready_btn_Click);
            // 
            // lobbyToFind_btn
            // 
            this.lobbyToFind_btn.BackColor = System.Drawing.Color.White;
            this.lobbyToFind_btn.Location = new System.Drawing.Point(1260, 773);
            this.lobbyToFind_btn.Name = "lobbyToFind_btn";
            this.lobbyToFind_btn.Size = new System.Drawing.Size(108, 44);
            this.lobbyToFind_btn.TabIndex = 5;
            this.lobbyToFind_btn.Text = "BACK";
            this.lobbyToFind_btn.UseVisualStyleBackColor = false;
            this.lobbyToFind_btn.Click += new System.EventHandler(this.lobbyToFind_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(858, 289);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 151);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Location = new System.Drawing.Point(656, 289);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(121, 151);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.Location = new System.Drawing.Point(466, 289);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(121, 151);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox3_Paint);
            // 
            // lb_LReadyCheck
            // 
            this.lb_LReadyCheck.BackColor = System.Drawing.Color.White;
            this.lb_LReadyCheck.Location = new System.Drawing.Point(472, 310);
            this.lb_LReadyCheck.Name = "lb_LReadyCheck";
            this.lb_LReadyCheck.Size = new System.Drawing.Size(109, 25);
            this.lb_LReadyCheck.TabIndex = 9;
            this.lb_LReadyCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_CReadyCheck
            // 
            this.lb_CReadyCheck.BackColor = System.Drawing.Color.White;
            this.lb_CReadyCheck.Location = new System.Drawing.Point(662, 310);
            this.lb_CReadyCheck.Name = "lb_CReadyCheck";
            this.lb_CReadyCheck.Size = new System.Drawing.Size(109, 25);
            this.lb_CReadyCheck.TabIndex = 9;
            this.lb_CReadyCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_RReadyCheck
            // 
            this.lb_RReadyCheck.BackColor = System.Drawing.Color.White;
            this.lb_RReadyCheck.Location = new System.Drawing.Point(864, 310);
            this.lb_RReadyCheck.Name = "lb_RReadyCheck";
            this.lb_RReadyCheck.Size = new System.Drawing.Size(109, 25);
            this.lb_RReadyCheck.TabIndex = 9;
            this.lb_RReadyCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // roomNum_lbl
            // 
            this.roomNum_lbl.BackColor = System.Drawing.Color.Transparent;
            this.roomNum_lbl.Font = new System.Drawing.Font("맑은 고딕", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.roomNum_lbl.Location = new System.Drawing.Point(288, 59);
            this.roomNum_lbl.Name = "roomNum_lbl";
            this.roomNum_lbl.Size = new System.Drawing.Size(864, 57);
            this.roomNum_lbl.TabIndex = 10;
            this.roomNum_lbl.Text = "방 번호";
            this.roomNum_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.roomNum_lbl.UseCompatibleTextRendering = true;
            // 
            // LobbyRoom_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MultiGame.Properties.Resources.BackGround1;
            this.Controls.Add(this.roomNum_lbl);
            this.Controls.Add(this.lb_RReadyCheck);
            this.Controls.Add(this.lb_CReadyCheck);
            this.Controls.Add(this.lb_LReadyCheck);
            this.Controls.Add(this.lobbyToFind_btn);
            this.Controls.Add(this.ready_btn);
            this.Controls.Add(this.rightPicBox);
            this.Controls.Add(this.centerPicBox);
            this.Controls.Add(this.leftPicBox);
            this.Controls.Add(this.roomTitle_lbl);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Name = "LobbyRoom_Screen";
            this.Size = new System.Drawing.Size(1440, 862);
            this.Load += new System.EventHandler(this.LobbyRoom_Screen_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LobbyRoom_Screen_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.leftPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.PictureBox leftPicBox;
        public System.Windows.Forms.PictureBox centerPicBox;
        public System.Windows.Forms.PictureBox rightPicBox;
        public System.Windows.Forms.Button ready_btn;
        public System.Windows.Forms.Button lobbyToFind_btn;
        public System.Windows.Forms.Label roomTitle_lbl;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lb_LReadyCheck;
        private System.Windows.Forms.Label lb_CReadyCheck;
        private System.Windows.Forms.Label lb_RReadyCheck;
        public System.Windows.Forms.Label roomNum_lbl;
    }
}
