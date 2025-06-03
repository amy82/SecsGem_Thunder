
namespace SecGemApp
{
    partial class ReportControl
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
            this.parrotGroupBox6 = new ReaLTaiizor.Controls.ParrotGroupBox();
            this.crownButton_Terminal_View = new ReaLTaiizor.Controls.CrownButton();
            this.bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            this.crownButton3 = new ReaLTaiizor.Controls.CrownButton();
            this.metroTextBox4 = new ReaLTaiizor.Controls.MetroTextBox();
            this.bigLabel10 = new ReaLTaiizor.Controls.BigLabel();
            this.parrotGroupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // parrotGroupBox6
            // 
            this.parrotGroupBox6.BorderColor = System.Drawing.Color.White;
            this.parrotGroupBox6.BorderWidth = 1;
            this.parrotGroupBox6.Controls.Add(this.crownButton_Terminal_View);
            this.parrotGroupBox6.Controls.Add(this.bigLabel1);
            this.parrotGroupBox6.Controls.Add(this.crownButton3);
            this.parrotGroupBox6.Controls.Add(this.metroTextBox4);
            this.parrotGroupBox6.Controls.Add(this.bigLabel10);
            this.parrotGroupBox6.Location = new System.Drawing.Point(3, 3);
            this.parrotGroupBox6.Name = "parrotGroupBox6";
            this.parrotGroupBox6.ShowText = true;
            this.parrotGroupBox6.Size = new System.Drawing.Size(374, 81);
            this.parrotGroupBox6.TabIndex = 34;
            this.parrotGroupBox6.TabStop = false;
            this.parrotGroupBox6.Text = "REPORT";
            this.parrotGroupBox6.TextColor = System.Drawing.Color.White;
            // 
            // crownButton_Terminal_View
            // 
            this.crownButton_Terminal_View.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.crownButton_Terminal_View.Location = new System.Drawing.Point(307, 43);
            this.crownButton_Terminal_View.Name = "crownButton_Terminal_View";
            this.crownButton_Terminal_View.Padding = new System.Windows.Forms.Padding(5);
            this.crownButton_Terminal_View.Size = new System.Drawing.Size(60, 25);
            this.crownButton_Terminal_View.TabIndex = 33;
            this.crownButton_Terminal_View.Text = "View";
            this.crownButton_Terminal_View.Click += new System.EventHandler(this.crownButton_Terminal_View_Click);
            // 
            // bigLabel1
            // 
            this.bigLabel1.AutoSize = true;
            this.bigLabel1.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel1.Font = new System.Drawing.Font("돋움", 9.75F);
            this.bigLabel1.ForeColor = System.Drawing.Color.White;
            this.bigLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel1.Location = new System.Drawing.Point(257, 25);
            this.bigLabel1.Name = "bigLabel1";
            this.bigLabel1.Size = new System.Drawing.Size(111, 13);
            this.bigLabel1.TabIndex = 32;
            this.bigLabel1.Text = "Terminal Message";
            // 
            // crownButton3
            // 
            this.crownButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.crownButton3.Location = new System.Drawing.Point(183, 43);
            this.crownButton3.Name = "crownButton3";
            this.crownButton3.Padding = new System.Windows.Forms.Padding(5);
            this.crownButton3.Size = new System.Drawing.Size(60, 25);
            this.crownButton3.TabIndex = 31;
            this.crownButton3.Text = "send";
            this.crownButton3.Click += new System.EventHandler(this.crownButton3_Click);
            // 
            // metroTextBox4
            // 
            this.metroTextBox4.AutoCompleteCustomSource = null;
            this.metroTextBox4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.metroTextBox4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.metroTextBox4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.metroTextBox4.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.metroTextBox4.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox4.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox4.Font = new System.Drawing.Font("돋움", 9.75F);
            this.metroTextBox4.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroTextBox4.Image = null;
            this.metroTextBox4.IsDerivedStyle = true;
            this.metroTextBox4.Lines = null;
            this.metroTextBox4.Location = new System.Drawing.Point(21, 43);
            this.metroTextBox4.Margin = new System.Windows.Forms.Padding(0);
            this.metroTextBox4.MaxLength = 32767;
            this.metroTextBox4.Multiline = false;
            this.metroTextBox4.Name = "metroTextBox4";
            this.metroTextBox4.ReadOnly = false;
            this.metroTextBox4.Size = new System.Drawing.Size(159, 25);
            this.metroTextBox4.Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            this.metroTextBox4.StyleManager = null;
            this.metroTextBox4.TabIndex = 30;
            this.metroTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.metroTextBox4.ThemeAuthor = "Taiizor";
            this.metroTextBox4.ThemeName = "MetroDark";
            this.metroTextBox4.UseSystemPasswordChar = false;
            this.metroTextBox4.WatermarkText = "";
            // 
            // bigLabel10
            // 
            this.bigLabel10.AutoSize = true;
            this.bigLabel10.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel10.Font = new System.Drawing.Font("돋움", 9.75F);
            this.bigLabel10.ForeColor = System.Drawing.Color.White;
            this.bigLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel10.Location = new System.Drawing.Point(20, 25);
            this.bigLabel10.Name = "bigLabel10";
            this.bigLabel10.Size = new System.Drawing.Size(81, 13);
            this.bigLabel10.TabIndex = 28;
            this.bigLabel10.Text = "Material Data";
            // 
            // ReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.parrotGroupBox6);
            this.Name = "ReportControl";
            this.Size = new System.Drawing.Size(380, 91);
            this.parrotGroupBox6.ResumeLayout(false);
            this.parrotGroupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.ParrotGroupBox parrotGroupBox6;
        private ReaLTaiizor.Controls.CrownButton crownButton3;
        private ReaLTaiizor.Controls.MetroTextBox metroTextBox4;
        private ReaLTaiizor.Controls.BigLabel bigLabel10;
        private ReaLTaiizor.Controls.CrownButton crownButton_Terminal_View;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
    }
}
