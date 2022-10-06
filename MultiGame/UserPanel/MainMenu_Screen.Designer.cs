
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
            this.makeRoom_btn = new System.Windows.Forms.Label();
            this.findRoom_btn = new System.Windows.Forms.Label();
            this.exitGame_btn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // makeRoom_btn
            // 
            this.makeRoom_btn.BackColor = System.Drawing.Color.Transparent;
            this.makeRoom_btn.Location = new System.Drawing.Point(583, 419);
            this.makeRoom_btn.Name = "makeRoom_btn";
            this.makeRoom_btn.Size = new System.Drawing.Size(253, 51);
            this.makeRoom_btn.TabIndex = 3;
            this.makeRoom_btn.Click += new System.EventHandler(this.makeRoom_btn_Click);
            // 
            // findRoom_btn
            // 
            this.findRoom_btn.BackColor = System.Drawing.Color.Transparent;
            this.findRoom_btn.Location = new System.Drawing.Point(583, 531);
            this.findRoom_btn.Name = "findRoom_btn";
            this.findRoom_btn.Size = new System.Drawing.Size(253, 51);
            this.findRoom_btn.TabIndex = 4;
            this.findRoom_btn.Click += new System.EventHandler(this.findRoom_btn_Click);
            // 
            // exitGame_btn
            // 
            this.exitGame_btn.BackColor = System.Drawing.Color.Transparent;
            this.exitGame_btn.Location = new System.Drawing.Point(592, 640);
            this.exitGame_btn.Name = "exitGame_btn";
            this.exitGame_btn.Size = new System.Drawing.Size(253, 51);
            this.exitGame_btn.TabIndex = 5;
            this.exitGame_btn.Click += new System.EventHandler(this.exitGame_btn_Click);
            // 
            // MainMenu_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MultiGame.Properties.Resources.MainMenu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.exitGame_btn);
            this.Controls.Add(this.findRoom_btn);
            this.Controls.Add(this.makeRoom_btn);
            this.DoubleBuffered = true;
            this.Name = "MainMenu_Screen";
            this.Size = new System.Drawing.Size(1440, 862);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label makeRoom_btn;
        private System.Windows.Forms.Label findRoom_btn;
        private System.Windows.Forms.Label exitGame_btn;
    }
}
