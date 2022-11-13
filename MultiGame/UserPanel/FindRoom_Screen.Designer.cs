
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
            this.findToMain_btn = new System.Windows.Forms.Button();
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
            this.findToMain_btn.BackColor = System.Drawing.Color.White;
            this.findToMain_btn.Location = new System.Drawing.Point(1260, 773);
            this.findToMain_btn.Name = "findToMain_btn";
            this.findToMain_btn.Size = new System.Drawing.Size(108, 44);
            this.findToMain_btn.TabIndex = 0;
            this.findToMain_btn.Text = "BACK";
            this.findToMain_btn.UseCompatibleTextRendering = true;
            this.findToMain_btn.UseVisualStyleBackColor = false;
            this.findToMain_btn.Click += new System.EventHandler(this.findToMain_btn_Click);
            // 
            // roomList_GridView
            // 
            this.roomList_GridView.AllowUserToAddRows = false;
            this.roomList_GridView.AllowUserToDeleteRows = false;
            this.roomList_GridView.AllowUserToResizeColumns = false;
            this.roomList_GridView.AllowUserToResizeRows = false;
            this.roomList_GridView.BackgroundColor = System.Drawing.Color.White;
            this.roomList_GridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.roomList_GridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("휴먼엑스포", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
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
            dataGridViewCellStyle5.Font = new System.Drawing.Font("휴먼엑스포", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.roomList_GridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.roomList_GridView.EnableHeadersVisualStyles = false;
            this.roomList_GridView.GridColor = System.Drawing.Color.White;
            this.roomList_GridView.Location = new System.Drawing.Point(363, 365);
            this.roomList_GridView.MultiSelect = false;
            this.roomList_GridView.Name = "roomList_GridView";
            this.roomList_GridView.ReadOnly = true;
            this.roomList_GridView.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.roomList_GridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.roomList_GridView.RowTemplate.Height = 35;
            this.roomList_GridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.roomList_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.roomList_GridView.Size = new System.Drawing.Size(726, 303);
            this.roomList_GridView.TabIndex = 2;
            this.roomList_GridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.roomList_GridView_CellDoubleClick);
            this.roomList_GridView.Paint += new System.Windows.Forms.PaintEventHandler(this.roomList_GridView_Paint);
            // 
            // roomNum
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.roomNum.DefaultCellStyle = dataGridViewCellStyle2;
            this.roomNum.HeaderText = "방 번호";
            this.roomNum.Name = "roomNum";
            this.roomNum.ReadOnly = true;
            this.roomNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // roomName
            // 
            this.roomName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.roomName.DefaultCellStyle = dataGridViewCellStyle3;
            this.roomName.HeaderText = "방 이름";
            this.roomName.Name = "roomName";
            this.roomName.ReadOnly = true;
            this.roomName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // personNum
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.personNum.DefaultCellStyle = dataGridViewCellStyle4;
            this.personNum.HeaderText = "인원수";
            this.personNum.Name = "personNum";
            this.personNum.ReadOnly = true;
            this.personNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // enterRoom_btn
            // 
            this.enterRoom_btn.BackColor = System.Drawing.Color.White;
            this.enterRoom_btn.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.enterRoom_btn.FlatAppearance.BorderSize = 3;
            this.enterRoom_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enterRoom_btn.Location = new System.Drawing.Point(638, 715);
            this.enterRoom_btn.Name = "enterRoom_btn";
            this.enterRoom_btn.Size = new System.Drawing.Size(158, 63);
            this.enterRoom_btn.TabIndex = 3;
            this.enterRoom_btn.Text = "JOIN";
            this.enterRoom_btn.UseCompatibleTextRendering = true;
            this.enterRoom_btn.UseVisualStyleBackColor = false;
            this.enterRoom_btn.Click += new System.EventHandler(this.enterRoom_btn_Click);
            // 
            // FindRoom_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MultiGame.Properties.Resources.MainMenu;
            this.Controls.Add(this.enterRoom_btn);
            this.Controls.Add(this.roomList_GridView);
            this.Controls.Add(this.findToMain_btn);
            this.Name = "FindRoom_Screen";
            this.Size = new System.Drawing.Size(1440, 862);
            this.Load += new System.EventHandler(this.FindRoom_Screen_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FindRoom_Screen_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.roomList_GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button findToMain_btn;
        public System.Windows.Forms.DataGridView roomList_GridView;
        public System.Windows.Forms.Button enterRoom_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn personNum;
    }
}
