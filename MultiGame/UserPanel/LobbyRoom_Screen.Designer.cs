
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
            this.leftCheckPicBox = new System.Windows.Forms.PictureBox();
            this.centerCheckPicBox = new System.Windows.Forms.PictureBox();
            this.rightCheckPicBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftCheckPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerCheckPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightCheckPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // roomTitle_lbl
            // 
            this.roomTitle_lbl.Font = new System.Drawing.Font("맑은 고딕", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.roomTitle_lbl.Location = new System.Drawing.Point(126, 41);
            this.roomTitle_lbl.Name = "roomTitle_lbl";
            this.roomTitle_lbl.Size = new System.Drawing.Size(513, 36);
            this.roomTitle_lbl.TabIndex = 0;
            this.roomTitle_lbl.Text = "방 이름";
            this.roomTitle_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // leftPicBox
            // 
            this.leftPicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftPicBox.Location = new System.Drawing.Point(126, 130);
            this.leftPicBox.Name = "leftPicBox";
            this.leftPicBox.Size = new System.Drawing.Size(121, 151);
            this.leftPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPicBox.TabIndex = 1;
            this.leftPicBox.TabStop = false;
            // 
            // centerPicBox
            // 
            this.centerPicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.centerPicBox.Location = new System.Drawing.Point(316, 130);
            this.centerPicBox.Name = "centerPicBox";
            this.centerPicBox.Size = new System.Drawing.Size(121, 151);
            this.centerPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.centerPicBox.TabIndex = 2;
            this.centerPicBox.TabStop = false;
            // 
            // rightPicBox
            // 
            this.rightPicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightPicBox.Location = new System.Drawing.Point(518, 130);
            this.rightPicBox.Name = "rightPicBox";
            this.rightPicBox.Size = new System.Drawing.Size(121, 151);
            this.rightPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.rightPicBox.TabIndex = 3;
            this.rightPicBox.TabStop = false;
            // 
            // ready_btn
            // 
            this.ready_btn.Location = new System.Drawing.Point(316, 352);
            this.ready_btn.Name = "ready_btn";
            this.ready_btn.Size = new System.Drawing.Size(121, 55);
            this.ready_btn.TabIndex = 4;
            this.ready_btn.Text = "준비";
            this.ready_btn.UseVisualStyleBackColor = true;
            // 
            // lobbyToFind_btn
            // 
            this.lobbyToFind_btn.Location = new System.Drawing.Point(702, 405);
            this.lobbyToFind_btn.Name = "lobbyToFind_btn";
            this.lobbyToFind_btn.Size = new System.Drawing.Size(75, 23);
            this.lobbyToFind_btn.TabIndex = 5;
            this.lobbyToFind_btn.Text = "나가기";
            this.lobbyToFind_btn.UseVisualStyleBackColor = true;
            // 
            // leftCheckPicBox
            // 
            this.leftCheckPicBox.Location = new System.Drawing.Point(174, 140);
            this.leftCheckPicBox.Name = "leftCheckPicBox";
            this.leftCheckPicBox.Size = new System.Drawing.Size(20, 20);
            this.leftCheckPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.leftCheckPicBox.TabIndex = 6;
            this.leftCheckPicBox.TabStop = false;
            // 
            // centerCheckPicBox
            // 
            this.centerCheckPicBox.Location = new System.Drawing.Point(366, 140);
            this.centerCheckPicBox.Name = "centerCheckPicBox";
            this.centerCheckPicBox.Size = new System.Drawing.Size(20, 20);
            this.centerCheckPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.centerCheckPicBox.TabIndex = 6;
            this.centerCheckPicBox.TabStop = false;
            // 
            // rightCheckPicBox
            // 
            this.rightCheckPicBox.Location = new System.Drawing.Point(571, 140);
            this.rightCheckPicBox.Name = "rightCheckPicBox";
            this.rightCheckPicBox.Size = new System.Drawing.Size(20, 20);
            this.rightCheckPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rightCheckPicBox.TabIndex = 6;
            this.rightCheckPicBox.TabStop = false;
            // 
            // LobbyRoom_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rightCheckPicBox);
            this.Controls.Add(this.centerCheckPicBox);
            this.Controls.Add(this.leftCheckPicBox);
            this.Controls.Add(this.lobbyToFind_btn);
            this.Controls.Add(this.ready_btn);
            this.Controls.Add(this.rightPicBox);
            this.Controls.Add(this.centerPicBox);
            this.Controls.Add(this.leftPicBox);
            this.Controls.Add(this.roomTitle_lbl);
            this.Name = "LobbyRoom_Screen";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.leftPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftCheckPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerCheckPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightCheckPicBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.PictureBox leftPicBox;
        public System.Windows.Forms.PictureBox centerPicBox;
        public System.Windows.Forms.PictureBox rightPicBox;
        public System.Windows.Forms.Button ready_btn;
        public System.Windows.Forms.Button lobbyToFind_btn;
        public System.Windows.Forms.Label roomTitle_lbl;
        public System.Windows.Forms.PictureBox leftCheckPicBox;
        public System.Windows.Forms.PictureBox centerCheckPicBox;
        public System.Windows.Forms.PictureBox rightCheckPicBox;
    }
}
