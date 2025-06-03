
namespace SecGemApp.Dlg
{
    partial class IdlePopupForm
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
            this.comboBox_IdleList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Note = new System.Windows.Forms.TextBox();
            this.BTN_IDLE_SEND = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox_IdleList
            // 
            this.comboBox_IdleList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox_IdleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_IdleList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_IdleList.FormattingEnabled = true;
            this.comboBox_IdleList.IntegralHeight = false;
            this.comboBox_IdleList.ItemHeight = 25;
            this.comboBox_IdleList.Location = new System.Drawing.Point(132, 12);
            this.comboBox_IdleList.Name = "comboBox_IdleList";
            this.comboBox_IdleList.Size = new System.Drawing.Size(359, 31);
            this.comboBox_IdleList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Idle reason";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(14, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 115);
            this.label2.TabIndex = 2;
            this.label2.Text = "Note";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_Note
            // 
            this.textBox_Note.Font = new System.Drawing.Font("나눔고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Note.Location = new System.Drawing.Point(132, 51);
            this.textBox_Note.Multiline = true;
            this.textBox_Note.Name = "textBox_Note";
            this.textBox_Note.Size = new System.Drawing.Size(359, 115);
            this.textBox_Note.TabIndex = 3;
            // 
            // BTN_IDLE_SEND
            // 
            this.BTN_IDLE_SEND.BackColor = System.Drawing.Color.Tan;
            this.BTN_IDLE_SEND.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_IDLE_SEND.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_IDLE_SEND.ForeColor = System.Drawing.Color.White;
            this.BTN_IDLE_SEND.Location = new System.Drawing.Point(387, 173);
            this.BTN_IDLE_SEND.Name = "BTN_IDLE_SEND";
            this.BTN_IDLE_SEND.Size = new System.Drawing.Size(104, 53);
            this.BTN_IDLE_SEND.TabIndex = 49;
            this.BTN_IDLE_SEND.Text = "OK";
            this.BTN_IDLE_SEND.UseVisualStyleBackColor = false;
            this.BTN_IDLE_SEND.Click += new System.EventHandler(this.BTN_IDLE_SEND_Click);
            // 
            // IdlePopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(503, 241);
            this.ControlBox = false;
            this.Controls.Add(this.BTN_IDLE_SEND);
            this.Controls.Add(this.textBox_Note);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_IdleList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "IdlePopupForm";
            this.Text = "IdlePopup";
            this.VisibleChanged += new System.EventHandler(this.IdlePopupForm_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_IdleList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Note;
        private System.Windows.Forms.Button BTN_IDLE_SEND;
    }
}