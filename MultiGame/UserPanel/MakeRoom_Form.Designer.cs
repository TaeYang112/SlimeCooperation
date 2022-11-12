
namespace MultiGame.UserPanel
{
    partial class MakeRoom_Control
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
            this.label1 = new System.Windows.Forms.Label();
            this.roomTitle_TB = new System.Windows.Forms.TextBox();
            this.make_btn = new System.Windows.Forms.Button();
            this.cancelMakeRoom_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(86, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "CREATE ROOM";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // roomTitle_TB
            // 
            this.roomTitle_TB.Location = new System.Drawing.Point(86, 83);
            this.roomTitle_TB.MaxLength = 30;
            this.roomTitle_TB.Name = "roomTitle_TB";
            this.roomTitle_TB.Size = new System.Drawing.Size(460, 23);
            this.roomTitle_TB.TabIndex = 1;
            this.roomTitle_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.roomTitle_TB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.roomTitle_TB_KeyDown);
            // 
            // make_btn
            // 
            this.make_btn.BackColor = System.Drawing.Color.White;
            this.make_btn.FlatAppearance.BorderSize = 2;
            this.make_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.make_btn.Location = new System.Drawing.Point(136, 120);
            this.make_btn.Name = "make_btn";
            this.make_btn.Size = new System.Drawing.Size(130, 43);
            this.make_btn.TabIndex = 2;
            this.make_btn.Text = "MAKING";
            this.make_btn.UseVisualStyleBackColor = false;
            this.make_btn.Click += new System.EventHandler(this.make_btn_Click);
            // 
            // cancelMakeRoom_btn
            // 
            this.cancelMakeRoom_btn.BackColor = System.Drawing.Color.White;
            this.cancelMakeRoom_btn.FlatAppearance.BorderSize = 2;
            this.cancelMakeRoom_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelMakeRoom_btn.Location = new System.Drawing.Point(366, 120);
            this.cancelMakeRoom_btn.Name = "cancelMakeRoom_btn";
            this.cancelMakeRoom_btn.Size = new System.Drawing.Size(130, 43);
            this.cancelMakeRoom_btn.TabIndex = 3;
            this.cancelMakeRoom_btn.Text = "CANCEL";
            this.cancelMakeRoom_btn.UseVisualStyleBackColor = false;
            this.cancelMakeRoom_btn.Click += new System.EventHandler(this.cancelMakeRoom_btn_Click);
            // 
            // MakeRoom_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(632, 181);
            this.Controls.Add(this.cancelMakeRoom_btn);
            this.Controls.Add(this.make_btn);
            this.Controls.Add(this.roomTitle_TB);
            this.Controls.Add(this.label1);
            this.Name = "MakeRoom_Form";
            this.Text = "MakeRoom_Form";
            this.Load += new System.EventHandler(this.MakeRoom_Form_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MakeRoom_Form_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox roomTitle_TB;
        public System.Windows.Forms.Button make_btn;
        public System.Windows.Forms.Button cancelMakeRoom_btn;
    }
}