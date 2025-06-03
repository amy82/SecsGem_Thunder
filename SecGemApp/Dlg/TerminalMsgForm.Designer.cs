
namespace SecGemApp.Dlg
{
    partial class TerminalMsgForm
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
            this.button_Terminal_Close = new System.Windows.Forms.Button();
            this.dataGridView_TerminalMsg = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_TerminalMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Terminal_Close
            // 
            this.button_Terminal_Close.Location = new System.Drawing.Point(518, 230);
            this.button_Terminal_Close.Name = "button_Terminal_Close";
            this.button_Terminal_Close.Size = new System.Drawing.Size(105, 42);
            this.button_Terminal_Close.TabIndex = 1;
            this.button_Terminal_Close.Text = "CLOSE";
            this.button_Terminal_Close.UseVisualStyleBackColor = true;
            this.button_Terminal_Close.Click += new System.EventHandler(this.button_Terminal_Close_Click);
            // 
            // dataGridView_TerminalMsg
            // 
            this.dataGridView_TerminalMsg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_TerminalMsg.Location = new System.Drawing.Point(13, 13);
            this.dataGridView_TerminalMsg.MultiSelect = false;
            this.dataGridView_TerminalMsg.Name = "dataGridView_TerminalMsg";
            this.dataGridView_TerminalMsg.ReadOnly = true;
            this.dataGridView_TerminalMsg.RowTemplate.Height = 23;
            this.dataGridView_TerminalMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_TerminalMsg.Size = new System.Drawing.Size(610, 206);
            this.dataGridView_TerminalMsg.TabIndex = 2;
            // 
            // TerminalMsgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 284);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView_TerminalMsg);
            this.Controls.Add(this.button_Terminal_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TerminalMsgForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TerminalMsgForm";
            this.Load += new System.EventHandler(this.TerminalMsgForm_Load);
            this.VisibleChanged += new System.EventHandler(this.TerminalMsgForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_TerminalMsg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_Terminal_Close;
        private System.Windows.Forms.DataGridView dataGridView_TerminalMsg;
    }
}