
namespace MultiGame.UserPanel
{
    partial class ExitProgram_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExitProgram_Form));
            this.label1 = new System.Windows.Forms.Label();
            this.ExitYes_btn = new System.Windows.Forms.Button();
            this.ExitNo_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(446, 101);
            this.label1.TabIndex = 0;
            this.label1.Text = "게임을 종료하시겠습니까?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExitYes_btn
            // 
            this.ExitYes_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ExitYes_btn.FlatAppearance.BorderSize = 3;
            this.ExitYes_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitYes_btn.Location = new System.Drawing.Point(91, 160);
            this.ExitYes_btn.Name = "ExitYes_btn";
            this.ExitYes_btn.Size = new System.Drawing.Size(89, 37);
            this.ExitYes_btn.TabIndex = 1;
            this.ExitYes_btn.Text = "예";
            this.ExitYes_btn.UseVisualStyleBackColor = true;
            this.ExitYes_btn.Click += new System.EventHandler(this.ExitYes_btn_Click);
            // 
            // ExitNo_btn
            // 
            this.ExitNo_btn.FlatAppearance.BorderSize = 3;
            this.ExitNo_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitNo_btn.Location = new System.Drawing.Point(301, 160);
            this.ExitNo_btn.Name = "ExitNo_btn";
            this.ExitNo_btn.Size = new System.Drawing.Size(89, 37);
            this.ExitNo_btn.TabIndex = 1;
            this.ExitNo_btn.Text = "아니요";
            this.ExitNo_btn.UseVisualStyleBackColor = true;
            this.ExitNo_btn.Click += new System.EventHandler(this.ExitNo_btn_Click);
            // 
            // ExitProgram_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(470, 222);
            this.Controls.Add(this.ExitNo_btn);
            this.Controls.Add(this.ExitYes_btn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExitProgram_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "종료";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExitProgram_Form_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ExitYes_btn;
        private System.Windows.Forms.Button ExitNo_btn;
    }
}