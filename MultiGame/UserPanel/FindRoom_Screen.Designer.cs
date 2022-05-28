
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.findToMain_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.roomList_GridView = new System.Windows.Forms.DataGridView();
            this.roomNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enterRoom_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.roomList_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // findToMain_btn
            // 
            this.findToMain_btn.Location = new System.Drawing.Point(653, 29);
            this.findToMain_btn.Name = "findToMain_btn";
            this.findToMain_btn.Size = new System.Drawing.Size(114, 31);
            this.findToMain_btn.TabIndex = 0;
            this.findToMain_btn.Text = "뒤로가기";
            this.findToMain_btn.UseVisualStyleBackColor = true;
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.roomList_GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.roomList_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.roomList_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roomNum,
            this.roomName,
            this.personNum});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.roomList_GridView.DefaultCellStyle = dataGridViewCellStyle11;
            this.roomList_GridView.EnableHeadersVisualStyles = false;
            this.roomList_GridView.Location = new System.Drawing.Point(159, 238);
            this.roomList_GridView.MultiSelect = false;
            this.roomList_GridView.Name = "roomList_GridView";
            this.roomList_GridView.ReadOnly = true;
            this.roomList_GridView.RowHeadersVisible = false;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.roomList_GridView.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.roomList_GridView.RowTemplate.Height = 25;
            this.roomList_GridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.roomList_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.roomList_GridView.Size = new System.Drawing.Size(498, 150);
            this.roomList_GridView.TabIndex = 2;
            // 
            // roomNum
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.roomNum.DefaultCellStyle = dataGridViewCellStyle8;
            this.roomNum.HeaderText = "방 번호";
            this.roomNum.Name = "roomNum";
            this.roomNum.ReadOnly = true;
            this.roomNum.Width = 80;
            // 
            // roomName
            // 
            this.roomName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.roomName.DefaultCellStyle = dataGridViewCellStyle9;
            this.roomName.HeaderText = "방 이름";
            this.roomName.Name = "roomName";
            this.roomName.ReadOnly = true;
            // 
            // personNum
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.personNum.DefaultCellStyle = dataGridViewCellStyle10;
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
            this.Controls.Add(this.findToMain_btn);
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
        public System.Windows.Forms.Button findToMain_btn;
        public System.Windows.Forms.DataGridView roomList_GridView;
        public System.Windows.Forms.Button enterRoom_btn;
    }
}
