
namespace MultiGame.UserPanel
{
    partial class FindRoom_Screen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.backToMain_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.roomList_GridView = new System.Windows.Forms.DataGridView();
            this.roomNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enterRoom_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.roomList_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // backToMain_btn
            // 
            this.backToMain_btn.Location = new System.Drawing.Point(653, 29);
            this.backToMain_btn.Name = "backToMain_btn";
            this.backToMain_btn.Size = new System.Drawing.Size(114, 31);
            this.backToMain_btn.TabIndex = 0;
            this.backToMain_btn.Text = "뒤로가기";
            this.backToMain_btn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(333, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "게임 이름";
            // 
            // roomList_GridView
            // 
            this.roomList_GridView.AllowUserToAddRows = false;
            this.roomList_GridView.AllowUserToDeleteRows = false;
            this.roomList_GridView.AllowUserToResizeColumns = false;
            this.roomList_GridView.AllowUserToResizeRows = false;
            this.roomList_GridView.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
            this.roomList_GridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.roomList_GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.roomList_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.roomList_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roomNum,
            this.roomName,
            this.personNum});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.roomList_GridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.roomList_GridView.EnableHeadersVisualStyles = false;
            this.roomList_GridView.Location = new System.Drawing.Point(159, 238);
            this.roomList_GridView.MultiSelect = false;
            this.roomList_GridView.Name = "roomList_GridView";
            this.roomList_GridView.ReadOnly = true;
            this.roomList_GridView.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.roomList_GridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.roomList_GridView.RowTemplate.Height = 25;
            this.roomList_GridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.roomList_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.roomList_GridView.Size = new System.Drawing.Size(498, 150);
            this.roomList_GridView.TabIndex = 2;
            // 
            // roomNum
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.roomNum.DefaultCellStyle = dataGridViewCellStyle2;
            this.roomNum.HeaderText = "방 번호";
            this.roomNum.Name = "roomNum";
            this.roomNum.ReadOnly = true;
            this.roomNum.Width = 80;
            // 
            // roomName
            // 
            this.roomName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.roomName.DefaultCellStyle = dataGridViewCellStyle3;
            this.roomName.HeaderText = "방 이름";
            this.roomName.Name = "roomName";
            this.roomName.ReadOnly = true;
            // 
            // personNum
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.personNum.DefaultCellStyle = dataGridViewCellStyle4;
            this.personNum.HeaderText = "인원수";
            this.personNum.Name = "personNum";
            this.personNum.ReadOnly = true;
            // 
            // enterRoom_btn
            // 
            this.enterRoom_btn.Location = new System.Drawing.Point(363, 406);
            this.enterRoom_btn.Name = "enterRoom_btn";
            this.enterRoom_btn.Size = new System.Drawing.Size(75, 23);
            this.enterRoom_btn.TabIndex = 3;
            this.enterRoom_btn.Text = "입장";
            this.enterRoom_btn.UseVisualStyleBackColor = true;
            // 
            // FindRoom_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.enterRoom_btn);
            this.Controls.Add(this.roomList_GridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backToMain_btn);
            this.Name = "FindRoom_Screen";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.roomList_GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn personNum;
        public System.Windows.Forms.Button backToMain_btn;
        public System.Windows.Forms.DataGridView roomList_GridView;
        public System.Windows.Forms.Button enterRoom_btn;
    }
}
