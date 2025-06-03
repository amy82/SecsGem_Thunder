
namespace SecGemApp
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.label_CommunicationStateVal = new System.Windows.Forms.Label();
            this.bigLabel10 = new ReaLTaiizor.Controls.BigLabel();
            this.button_UgcOpenFile = new System.Windows.Forms.Button();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.crownSeparator1 = new ReaLTaiizor.Controls.CrownSeparator();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.모드변경ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enginnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigLabel_TopMode = new ReaLTaiizor.Controls.BigLabel();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.BottomPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.BackColor = System.Drawing.Color.Transparent;
            this.BottomPanel.Controls.Add(this.label_CommunicationStateVal);
            this.BottomPanel.Controls.Add(this.bigLabel10);
            this.BottomPanel.Controls.Add(this.button_UgcOpenFile);
            this.BottomPanel.Location = new System.Drawing.Point(0, 554);
            this.BottomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(380, 191);
            this.BottomPanel.TabIndex = 2;
            // 
            // label_CommunicationStateVal
            // 
            this.label_CommunicationStateVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_CommunicationStateVal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_CommunicationStateVal.Font = new System.Drawing.Font("돋움", 9F);
            this.label_CommunicationStateVal.ForeColor = System.Drawing.Color.White;
            this.label_CommunicationStateVal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_CommunicationStateVal.Location = new System.Drawing.Point(86, 452);
            this.label_CommunicationStateVal.Name = "label_CommunicationStateVal";
            this.label_CommunicationStateVal.Size = new System.Drawing.Size(275, 25);
            this.label_CommunicationStateVal.TabIndex = 36;
            this.label_CommunicationStateVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bigLabel10
            // 
            this.bigLabel10.AutoSize = true;
            this.bigLabel10.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel10.Font = new System.Drawing.Font("돋움", 9.75F);
            this.bigLabel10.ForeColor = System.Drawing.Color.White;
            this.bigLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel10.Location = new System.Drawing.Point(19, 457);
            this.bigLabel10.Name = "bigLabel10";
            this.bigLabel10.Size = new System.Drawing.Size(61, 13);
            this.bigLabel10.TabIndex = 35;
            this.bigLabel10.Text = "UGC File:";
            // 
            // button_UgcOpenFile
            // 
            this.button_UgcOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_UgcOpenFile.FlatAppearance.BorderSize = 0;
            this.button_UgcOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_UgcOpenFile.ForeColor = System.Drawing.Color.LightGray;
            this.button_UgcOpenFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_UgcOpenFile.Location = new System.Drawing.Point(367, 452);
            this.button_UgcOpenFile.Name = "button_UgcOpenFile";
            this.button_UgcOpenFile.Size = new System.Drawing.Size(32, 25);
            this.button_UgcOpenFile.TabIndex = 34;
            this.button_UgcOpenFile.Text = "•••";
            this.button_UgcOpenFile.UseVisualStyleBackColor = false;
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.Transparent;
            this.LeftPanel.Location = new System.Drawing.Point(0, 45);
            this.LeftPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(380, 509);
            this.LeftPanel.TabIndex = 1;
            // 
            // crownSeparator1
            // 
            this.crownSeparator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.crownSeparator1.Location = new System.Drawing.Point(0, 31);
            this.crownSeparator1.Name = "crownSeparator1";
            this.crownSeparator1.Size = new System.Drawing.Size(784, 2);
            this.crownSeparator1.TabIndex = 8;
            this.crownSeparator1.Text = "crownSeparator1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.모드변경ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 8, 0, 2);
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip1.Size = new System.Drawing.Size(784, 31);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 모드변경ToolStripMenuItem
            // 
            this.모드변경ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enginnerToolStripMenuItem,
            this.operatorToolStripMenuItem});
            this.모드변경ToolStripMenuItem.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.모드변경ToolStripMenuItem.ForeColor = System.Drawing.Color.SeaShell;
            this.모드변경ToolStripMenuItem.Name = "모드변경ToolStripMenuItem";
            this.모드변경ToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.모드변경ToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.모드변경ToolStripMenuItem.Text = "＊모드변경";
            // 
            // enginnerToolStripMenuItem
            // 
            this.enginnerToolStripMenuItem.BackColor = System.Drawing.Color.DimGray;
            this.enginnerToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.enginnerToolStripMenuItem.Name = "enginnerToolStripMenuItem";
            this.enginnerToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.enginnerToolStripMenuItem.Text = "Enginner";
            this.enginnerToolStripMenuItem.Click += new System.EventHandler(this.enginnerToolStripMenuItem_Click);
            // 
            // operatorToolStripMenuItem
            // 
            this.operatorToolStripMenuItem.BackColor = System.Drawing.Color.DimGray;
            this.operatorToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.operatorToolStripMenuItem.Name = "operatorToolStripMenuItem";
            this.operatorToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.operatorToolStripMenuItem.Text = "Operator";
            this.operatorToolStripMenuItem.Click += new System.EventHandler(this.operatorToolStripMenuItem_Click);
            // 
            // bigLabel_TopMode
            // 
            this.bigLabel_TopMode.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel_TopMode.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bigLabel_TopMode.ForeColor = System.Drawing.Color.Lavender;
            this.bigLabel_TopMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel_TopMode.Location = new System.Drawing.Point(299, 6);
            this.bigLabel_TopMode.Name = "bigLabel_TopMode";
            this.bigLabel_TopMode.Size = new System.Drawing.Size(83, 22);
            this.bigLabel_TopMode.TabIndex = 39;
            this.bigLabel_TopMode.Text = "| Operator";
            this.bigLabel_TopMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RightPanel
            // 
            this.RightPanel.BackColor = System.Drawing.Color.Transparent;
            this.RightPanel.Location = new System.Drawing.Point(380, 45);
            this.RightPanel.Margin = new System.Windows.Forms.Padding(0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(400, 700);
            this.RightPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(784, 751);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.bigLabel_TopMode);
            this.Controls.Add(this.crownSeparator1);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "formTheme1";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BottomPanel;
        public System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Label label_CommunicationStateVal;
        private ReaLTaiizor.Controls.BigLabel bigLabel10;
        private System.Windows.Forms.Button button_UgcOpenFile;
        private ReaLTaiizor.Controls.CrownSeparator crownSeparator1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 모드변경ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enginnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operatorToolStripMenuItem;
        private ReaLTaiizor.Controls.BigLabel bigLabel_TopMode;
        public System.Windows.Forms.Panel RightPanel;
    }
}

