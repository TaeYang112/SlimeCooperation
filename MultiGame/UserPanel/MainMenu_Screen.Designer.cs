
namespace MultiGame.UserPanel
{
    partial class MainMenu_Screen
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
            this.label1 = new System.Windows.Forms.Label();
            this.makeRoom_btn = new System.Windows.Forms.Button();
            this.findRoom_btn = new System.Windows.Forms.Button();
            this.exitGame_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(302, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "게임 이름";
            // 
            // makeRoom_btn
            // 
            this.makeRoom_btn.Location = new System.Drawing.Point(253, 195);
            this.makeRoom_btn.Name = "makeRoom_btn";
            this.makeRoom_btn.Size = new System.Drawing.Size(253, 51);
            this.makeRoom_btn.TabIndex = 1;
            this.makeRoom_btn.Text = "방 만들기";
            this.makeRoom_btn.UseVisualStyleBackColor = true;
            // 
            // findRoom_btn
            // 
            this.findRoom_btn.Location = new System.Drawing.Point(253, 252);
            this.findRoom_btn.Name = "findRoom_btn";
            this.findRoom_btn.Size = new System.Drawing.Size(253, 51);
            this.findRoom_btn.TabIndex = 2;
            this.findRoom_btn.Text = "방 찾기";
            this.findRoom_btn.UseVisualStyleBackColor = true;
            // 
            // exitGame_btn
            // 
            this.exitGame_btn.Location = new System.Drawing.Point(253, 309);
            this.exitGame_btn.Name = "exitGame_btn";
            this.exitGame_btn.Size = new System.Drawing.Size(253, 51);
            this.exitGame_btn.TabIndex = 2;
            this.exitGame_btn.Text = "게임 종료";
            this.exitGame_btn.UseVisualStyleBackColor = true;
            // 
            // MainMenu_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.exitGame_btn);
            this.Controls.Add(this.findRoom_btn);
            this.Controls.Add(this.makeRoom_btn);
            this.Controls.Add(this.label1);
            this.Name = "MainMenu_Screen";
            this.Size = new System.Drawing.Size(800, 450);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button makeRoom_btn;
        public System.Windows.Forms.Button findRoom_btn;
        public System.Windows.Forms.Button exitGame_btn;
    }
}
