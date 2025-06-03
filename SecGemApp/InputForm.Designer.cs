
namespace SecGemApp
{
    partial class InputForm
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
            this.Label1 = new ReaLTaiizor.Controls.BigLabel();
            this.textBox_Input = new System.Windows.Forms.TextBox();
            this.crownButton_ok = new ReaLTaiizor.Controls.CrownButton();
            this.crownButton_cancel = new ReaLTaiizor.Controls.CrownButton();
            this.SecretLabel = new ReaLTaiizor.Controls.BigLabel();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("돋움", 9.75F);
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label1.Location = new System.Drawing.Point(12, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(158, 13);
            this.Label1.TabIndex = 27;
            this.Label1.Text = "비밀번호를 입력해주세요:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_Input
            // 
            this.textBox_Input.Font = new System.Drawing.Font("돋움", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Input.Location = new System.Drawing.Point(15, 51);
            this.textBox_Input.Name = "textBox_Input";
            this.textBox_Input.Size = new System.Drawing.Size(322, 25);
            this.textBox_Input.TabIndex = 28;
            this.textBox_Input.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // crownButton_ok
            // 
            this.crownButton_ok.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.crownButton_ok.Location = new System.Drawing.Point(211, 93);
            this.crownButton_ok.Name = "crownButton_ok";
            this.crownButton_ok.Padding = new System.Windows.Forms.Padding(5);
            this.crownButton_ok.Size = new System.Drawing.Size(60, 25);
            this.crownButton_ok.TabIndex = 32;
            this.crownButton_ok.Text = "확인";
            this.crownButton_ok.Click += new System.EventHandler(this.crownButton_ok_Click);
            // 
            // crownButton_cancel
            // 
            this.crownButton_cancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.crownButton_cancel.Location = new System.Drawing.Point(277, 93);
            this.crownButton_cancel.Name = "crownButton_cancel";
            this.crownButton_cancel.Padding = new System.Windows.Forms.Padding(5);
            this.crownButton_cancel.Size = new System.Drawing.Size(60, 25);
            this.crownButton_cancel.TabIndex = 33;
            this.crownButton_cancel.Text = "취소";
            this.crownButton_cancel.Click += new System.EventHandler(this.crownButton_cancel_Click);
            // 
            // SecretLabel
            // 
            this.SecretLabel.AutoSize = true;
            this.SecretLabel.BackColor = System.Drawing.Color.Transparent;
            this.SecretLabel.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SecretLabel.ForeColor = System.Drawing.Color.Red;
            this.SecretLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SecretLabel.Location = new System.Drawing.Point(178, 23);
            this.SecretLabel.Name = "SecretLabel";
            this.SecretLabel.Size = new System.Drawing.Size(157, 13);
            this.SecretLabel.TabIndex = 34;
            this.SecretLabel.Text = "잘못된 비밀번호입니다.";
            this.SecretLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SecretLabel.Visible = false;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(353, 140);
            this.Controls.Add(this.SecretLabel);
            this.Controls.Add(this.crownButton_cancel);
            this.Controls.Add(this.crownButton_ok);
            this.Controls.Add(this.textBox_Input);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(1920, 1040);
            this.MinimumSize = new System.Drawing.Size(261, 61);
            this.Name = "InputForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PASSWORD";
            this.TransparencyKey = System.Drawing.Color.Purple;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.BigLabel Label1;
        private System.Windows.Forms.TextBox textBox_Input;
        private ReaLTaiizor.Controls.CrownButton crownButton_ok;
        private ReaLTaiizor.Controls.CrownButton crownButton_cancel;
        private ReaLTaiizor.Controls.BigLabel SecretLabel;
    }
}