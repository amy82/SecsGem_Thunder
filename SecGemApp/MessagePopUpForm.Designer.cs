
namespace SecGemApp
{
    partial class MessagePopUpForm
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
            this.labelTop = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BTN_MESSAGE1 = new System.Windows.Forms.Button();
            this.BTN_MESSAGE2 = new System.Windows.Forms.Button();
            this.BTN_MESSAGE3 = new System.Windows.Forms.Button();
            this.MessageBody = new System.Windows.Forms.Label();
            this.warnningImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.warnningImage)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTop
            // 
            this.labelTop.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTop.ForeColor = System.Drawing.Color.White;
            this.labelTop.Location = new System.Drawing.Point(44, 3);
            this.labelTop.Margin = new System.Windows.Forms.Padding(0);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(243, 42);
            this.labelTop.TabIndex = 1;
            this.labelTop.Text = "Info";
            this.labelTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTop.VisibleChanged += new System.EventHandler(this.labelTop_VisibleChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Chartreuse;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(0, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(743, 1);
            this.label2.TabIndex = 4;
            // 
            // BTN_MESSAGE1
            // 
            this.BTN_MESSAGE1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.BTN_MESSAGE1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.BTN_MESSAGE1.FlatAppearance.BorderSize = 0;
            this.BTN_MESSAGE1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MESSAGE1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_MESSAGE1.ForeColor = System.Drawing.Color.White;
            this.BTN_MESSAGE1.Location = new System.Drawing.Point(638, 167);
            this.BTN_MESSAGE1.Name = "BTN_MESSAGE1";
            this.BTN_MESSAGE1.Size = new System.Drawing.Size(87, 40);
            this.BTN_MESSAGE1.TabIndex = 2;
            this.BTN_MESSAGE1.Text = "취소";
            this.BTN_MESSAGE1.UseVisualStyleBackColor = false;
            this.BTN_MESSAGE1.Click += new System.EventHandler(this.BTN_MESSAGE1_Click);
            // 
            // BTN_MESSAGE2
            // 
            this.BTN_MESSAGE2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.BTN_MESSAGE2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.BTN_MESSAGE2.FlatAppearance.BorderSize = 0;
            this.BTN_MESSAGE2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MESSAGE2.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_MESSAGE2.ForeColor = System.Drawing.Color.White;
            this.BTN_MESSAGE2.Location = new System.Drawing.Point(545, 167);
            this.BTN_MESSAGE2.Name = "BTN_MESSAGE2";
            this.BTN_MESSAGE2.Size = new System.Drawing.Size(87, 40);
            this.BTN_MESSAGE2.TabIndex = 5;
            this.BTN_MESSAGE2.Text = "확인";
            this.BTN_MESSAGE2.UseVisualStyleBackColor = false;
            this.BTN_MESSAGE2.Click += new System.EventHandler(this.BTN_MESSAGE2_Click);
            // 
            // BTN_MESSAGE3
            // 
            this.BTN_MESSAGE3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.BTN_MESSAGE3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.BTN_MESSAGE3.FlatAppearance.BorderSize = 0;
            this.BTN_MESSAGE3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_MESSAGE3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_MESSAGE3.ForeColor = System.Drawing.Color.White;
            this.BTN_MESSAGE3.Location = new System.Drawing.Point(452, 167);
            this.BTN_MESSAGE3.Name = "BTN_MESSAGE3";
            this.BTN_MESSAGE3.Size = new System.Drawing.Size(87, 40);
            this.BTN_MESSAGE3.TabIndex = 6;
            this.BTN_MESSAGE3.Text = "중단";
            this.BTN_MESSAGE3.UseVisualStyleBackColor = false;
            this.BTN_MESSAGE3.Click += new System.EventHandler(this.BTN_MESSAGE3_Click);
            // 
            // MessageBody
            // 
            this.MessageBody.Font = new System.Drawing.Font("맑은 고딕", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MessageBody.ForeColor = System.Drawing.Color.White;
            this.MessageBody.Location = new System.Drawing.Point(9, 50);
            this.MessageBody.Margin = new System.Windows.Forms.Padding(0);
            this.MessageBody.Name = "MessageBody";
            this.MessageBody.Size = new System.Drawing.Size(734, 114);
            this.MessageBody.TabIndex = 7;
            this.MessageBody.Text = "자동 운전 중 설정할 수 없습니다.";
            // 
            // warnningImage
            // 
            this.warnningImage.Image = global::SecGemApp.Properties.Resources.info;
            this.warnningImage.Location = new System.Drawing.Point(9, 9);
            this.warnningImage.Margin = new System.Windows.Forms.Padding(0);
            this.warnningImage.Name = "warnningImage";
            this.warnningImage.Size = new System.Drawing.Size(30, 30);
            this.warnningImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.warnningImage.TabIndex = 0;
            this.warnningImage.TabStop = false;
            // 
            // MessagePopUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(744, 224);
            this.Controls.Add(this.MessageBody);
            this.Controls.Add(this.BTN_MESSAGE3);
            this.Controls.Add(this.BTN_MESSAGE2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BTN_MESSAGE1);
            this.Controls.Add(this.labelTop);
            this.Controls.Add(this.warnningImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MessagePopUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessagePopUpForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessagePopUpForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.warnningImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox warnningImage;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BTN_MESSAGE1;
        private System.Windows.Forms.Button BTN_MESSAGE2;
        private System.Windows.Forms.Button BTN_MESSAGE3;
        private System.Windows.Forms.Label MessageBody;
    }
}