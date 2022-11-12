
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
            this.findRoom_btn = new System.Windows.Forms.Button();
            this.makeRoom_btn = new System.Windows.Forms.Button();
            this.exitGame_btn = new System.Windows.Forms.Button();
            this.records_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // findRoom_btn
            // 
            this.findRoom_btn.BackColor = System.Drawing.Color.Transparent;
            this.findRoom_btn.FlatAppearance.BorderSize = 0;
            this.findRoom_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.findRoom_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.findRoom_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findRoom_btn.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.findRoom_btn.ForeColor = System.Drawing.Color.White;
            this.findRoom_btn.Location = new System.Drawing.Point(556, 476);
            this.findRoom_btn.Name = "findRoom_btn";
            this.findRoom_btn.Size = new System.Drawing.Size(310, 66);
            this.findRoom_btn.TabIndex = 6;
            this.findRoom_btn.TabStop = false;
            this.findRoom_btn.Text = "J O I N";
            this.findRoom_btn.UseCompatibleTextRendering = true;
            this.findRoom_btn.UseVisualStyleBackColor = false;
            this.findRoom_btn.Click += new System.EventHandler(this.findRoom_btn_Click);
            this.findRoom_btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.findRoom_btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // makeRoom_btn
            // 
            this.makeRoom_btn.BackColor = System.Drawing.Color.Transparent;
            this.makeRoom_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.makeRoom_btn.FlatAppearance.BorderSize = 0;
            this.makeRoom_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.makeRoom_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.makeRoom_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.makeRoom_btn.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.makeRoom_btn.ForeColor = System.Drawing.Color.White;
            this.makeRoom_btn.Location = new System.Drawing.Point(556, 374);
            this.makeRoom_btn.Name = "makeRoom_btn";
            this.makeRoom_btn.Size = new System.Drawing.Size(310, 66);
            this.makeRoom_btn.TabIndex = 5;
            this.makeRoom_btn.TabStop = false;
            this.makeRoom_btn.Text = "M A K E";
            this.makeRoom_btn.UseCompatibleTextRendering = true;
            this.makeRoom_btn.UseVisualStyleBackColor = true;
            this.makeRoom_btn.Click += new System.EventHandler(this.makeRoom_btn_Click);
            this.makeRoom_btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.makeRoom_btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // exitGame_btn
            // 
            this.exitGame_btn.BackColor = System.Drawing.Color.Transparent;
            this.exitGame_btn.FlatAppearance.BorderSize = 0;
            this.exitGame_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.exitGame_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.exitGame_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitGame_btn.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exitGame_btn.ForeColor = System.Drawing.Color.White;
            this.exitGame_btn.Location = new System.Drawing.Point(556, 680);
            this.exitGame_btn.Name = "exitGame_btn";
            this.exitGame_btn.Size = new System.Drawing.Size(310, 66);
            this.exitGame_btn.TabIndex = 7;
            this.exitGame_btn.TabStop = false;
            this.exitGame_btn.Text = "E X I T";
            this.exitGame_btn.UseCompatibleTextRendering = true;
            this.exitGame_btn.UseVisualStyleBackColor = false;
            this.exitGame_btn.Click += new System.EventHandler(this.exitGame_btn_Click);
            this.exitGame_btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.exitGame_btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // records_btn
            // 
            this.records_btn.BackColor = System.Drawing.Color.Transparent;
            this.records_btn.FlatAppearance.BorderSize = 0;
            this.records_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.records_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.records_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.records_btn.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.records_btn.ForeColor = System.Drawing.Color.White;
            this.records_btn.Location = new System.Drawing.Point(481, 578);
            this.records_btn.Name = "records_btn";
            this.records_btn.Size = new System.Drawing.Size(460, 66);
            this.records_btn.TabIndex = 8;
            this.records_btn.TabStop = false;
            this.records_btn.Text = "R E C O R D";
            this.records_btn.UseCompatibleTextRendering = true;
            this.records_btn.UseVisualStyleBackColor = false;
            this.records_btn.Click += new System.EventHandler(this.records_btn_Click);
            this.records_btn.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.records_btn.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // MainMenu_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MultiGame.Properties.Resources.MainMenu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.records_btn);
            this.Controls.Add(this.exitGame_btn);
            this.Controls.Add(this.makeRoom_btn);
            this.Controls.Add(this.findRoom_btn);
            this.DoubleBuffered = true;
            this.Name = "MainMenu_Screen";
            this.Size = new System.Drawing.Size(1440, 862);
            this.Load += new System.EventHandler(this.MainMenu_Screen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button findRoom_btn;
        private System.Windows.Forms.Button makeRoom_btn;
        private System.Windows.Forms.Button exitGame_btn;
        private System.Windows.Forms.Button records_btn;
    }
}
