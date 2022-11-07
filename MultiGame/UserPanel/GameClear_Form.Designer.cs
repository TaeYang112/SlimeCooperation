
namespace MultiGame.UserPanel
{
    partial class GameClear_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.score_GridView = new System.Windows.Forms.DataGridView();
            this.rank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_record = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.score_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(83, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(498, 99);
            this.label1.TabIndex = 0;
            this.label1.Text = "GAME CLEAR!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(52, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(560, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(52, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(560, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(90, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(485, 64);
            this.label4.TabIndex = 0;
            this.label4.Text = "SCORE";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // score_GridView
            // 
            this.score_GridView.AllowUserToAddRows = false;
            this.score_GridView.AllowUserToDeleteRows = false;
            this.score_GridView.AllowUserToResizeColumns = false;
            this.score_GridView.AllowUserToResizeRows = false;
            this.score_GridView.BackgroundColor = System.Drawing.Color.White;
            this.score_GridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.score_GridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("휴먼엑스포", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.score_GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.score_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.score_GridView.ColumnHeadersVisible = false;
            this.score_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rank,
            this.title,
            this.time});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("휴먼엑스포", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.score_GridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.score_GridView.EnableHeadersVisualStyles = false;
            this.score_GridView.GridColor = System.Drawing.Color.White;
            this.score_GridView.Location = new System.Drawing.Point(52, 350);
            this.score_GridView.MultiSelect = false;
            this.score_GridView.Name = "score_GridView";
            this.score_GridView.ReadOnly = true;
            this.score_GridView.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.score_GridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.score_GridView.RowTemplate.Height = 35;
            this.score_GridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.score_GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.score_GridView.Size = new System.Drawing.Size(560, 359);
            this.score_GridView.TabIndex = 3;
            this.score_GridView.SelectionChanged += new System.EventHandler(this.score_GridView_SelectionChanged);
            // 
            // rank
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rank.DefaultCellStyle = dataGridViewCellStyle2;
            this.rank.HeaderText = "랭크";
            this.rank.Name = "rank";
            this.rank.ReadOnly = true;
            this.rank.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.rank.Width = 50;
            // 
            // title
            // 
            this.title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.title.DefaultCellStyle = dataGridViewCellStyle3;
            this.title.HeaderText = "방 이름";
            this.title.Name = "title";
            this.title.ReadOnly = true;
            this.title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // time
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.time.DefaultCellStyle = dataGridViewCellStyle4;
            this.time.HeaderText = "시간";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            this.time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // timer1
            // 
            this.timer1.Interval = 150;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_record
            // 
            this.btn_record.BackColor = System.Drawing.Color.White;
            this.btn_record.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_record.FlatAppearance.BorderSize = 3;
            this.btn_record.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_record.Location = new System.Drawing.Point(263, 231);
            this.btn_record.Name = "btn_record";
            this.btn_record.Size = new System.Drawing.Size(138, 49);
            this.btn_record.TabIndex = 4;
            this.btn_record.Text = "RECORD";
            this.btn_record.UseVisualStyleBackColor = false;
            this.btn_record.Click += new System.EventHandler(this.btn_record_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackgroundImage = global::MultiGame.Properties.Resources.Exit;
            this.btn_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_close.FlatAppearance.BorderSize = 0;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Location = new System.Drawing.Point(625, 12);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(26, 26);
            this.btn_close.TabIndex = 5;
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // GameClear_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(664, 744);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_record);
            this.Controls.Add(this.score_GridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameClear_Form";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GameClear_Form";
            this.Load += new System.EventHandler(this.GameClear_Form_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameClear_Form_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.score_GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DataGridView score_GridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn rank;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_record;
        private System.Windows.Forms.Button btn_close;
    }
}