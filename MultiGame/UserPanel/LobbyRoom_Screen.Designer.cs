
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
            this.goMain_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // roomTitle_lbl
            // 
            this.roomTitle_lbl.AutoSize = true;
            this.roomTitle_lbl.Font = new System.Drawing.Font("맑은 고딕", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.roomTitle_lbl.Location = new System.Drawing.Point(316, 41);
            this.roomTitle_lbl.Name = "roomTitle_lbl";
            this.roomTitle_lbl.Size = new System.Drawing.Size(102, 36);
            this.roomTitle_lbl.TabIndex = 0;
            this.roomTitle_lbl.Text = "방 이름";
            // 
            // leftPicBox
            // 
            this.leftPicBox.Location = new System.Drawing.Point(126, 130);
            this.leftPicBox.Name = "leftPicBox";
            this.leftPicBox.Size = new System.Drawing.Size(121, 151);
            this.leftPicBox.TabIndex = 1;
            this.leftPicBox.TabStop = false;
            // 
            // centerPicBox
            // 
            this.centerPicBox.Location = new System.Drawing.Point(316, 130);
            this.centerPicBox.Name = "centerPicBox";
            this.centerPicBox.Size = new System.Drawing.Size(121, 151);
            this.centerPicBox.TabIndex = 2;
            this.centerPicBox.TabStop = false;
            // 
            // rightPicBox
            // 
            this.rightPicBox.Location = new System.Drawing.Point(518, 130);
            this.rightPicBox.Name = "rightPicBox";
            this.rightPicBox.Size = new System.Drawing.Size(121, 151);
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
            // goMain_btn
            // 
            this.goMain_btn.Location = new System.Drawing.Point(702, 405);
            this.goMain_btn.Name = "goMain_btn";
            this.goMain_btn.Size = new System.Drawing.Size(75, 23);
            this.goMain_btn.TabIndex = 5;
            this.goMain_btn.Text = "나가기";
            this.goMain_btn.UseVisualStyleBackColor = true;
            // 
            // LobbyRoom_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.goMain_btn);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.PictureBox leftPicBox;
        public System.Windows.Forms.PictureBox centerPicBox;
        public System.Windows.Forms.PictureBox rightPicBox;
        public System.Windows.Forms.Button ready_btn;
        public System.Windows.Forms.Button goMain_btn;
        public System.Windows.Forms.Label roomTitle_lbl;
    }
}
