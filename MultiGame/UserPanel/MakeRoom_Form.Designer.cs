
namespace MultiGame.UserPanel
{
    partial class MakeRoom_Form
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(245, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "방제목";
            // 
            // roomTitle_TB
            // 
            this.roomTitle_TB.Location = new System.Drawing.Point(122, 70);
            this.roomTitle_TB.Name = "roomTitle_TB";
            this.roomTitle_TB.Size = new System.Drawing.Size(365, 23);
            this.roomTitle_TB.TabIndex = 1;
            // 
            // make_btn
            // 
            this.make_btn.Location = new System.Drawing.Point(199, 99);
            this.make_btn.Name = "make_btn";
            this.make_btn.Size = new System.Drawing.Size(75, 23);
            this.make_btn.TabIndex = 2;
            this.make_btn.Text = "만들기";
            this.make_btn.UseVisualStyleBackColor = true;
            // 
            // cancelMakeRoom_btn
            // 
            this.cancelMakeRoom_btn.Location = new System.Drawing.Point(324, 100);
            this.cancelMakeRoom_btn.Name = "cancelMakeRoom_btn";
            this.cancelMakeRoom_btn.Size = new System.Drawing.Size(75, 23);
            this.cancelMakeRoom_btn.TabIndex = 3;
            this.cancelMakeRoom_btn.Text = "취소";
            this.cancelMakeRoom_btn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 145);
            this.panel1.TabIndex = 4;
            // 
            // MakeRoom_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 145);
            this.Controls.Add(this.cancelMakeRoom_btn);
            this.Controls.Add(this.make_btn);
            this.Controls.Add(this.roomTitle_TB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MakeRoom_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MakeRoom_Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox roomTitle_TB;
        public System.Windows.Forms.Button make_btn;
        public System.Windows.Forms.Button cancelMakeRoom_btn;
        private System.Windows.Forms.Panel panel1;
    }
}