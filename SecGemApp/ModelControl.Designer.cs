
namespace SecGemApp
{
    partial class ModelControl
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
            this.parrotGroupBox1 = new ReaLTaiizor.Controls.ParrotGroupBox();
            this.dataGridView_Model = new System.Windows.Forms.DataGridView();
            this.crownButton_Model_Change = new ReaLTaiizor.Controls.CrownButton();
            this.parrotGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Model)).BeginInit();
            this.SuspendLayout();
            // 
            // parrotGroupBox1
            // 
            this.parrotGroupBox1.BorderColor = System.Drawing.Color.White;
            this.parrotGroupBox1.BorderWidth = 1;
            this.parrotGroupBox1.Controls.Add(this.dataGridView_Model);
            this.parrotGroupBox1.Controls.Add(this.crownButton_Model_Change);
            this.parrotGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.parrotGroupBox1.Name = "parrotGroupBox1";
            this.parrotGroupBox1.ShowText = true;
            this.parrotGroupBox1.Size = new System.Drawing.Size(374, 104);
            this.parrotGroupBox1.TabIndex = 36;
            this.parrotGroupBox1.TabStop = false;
            this.parrotGroupBox1.Text = "MODEL LIST";
            this.parrotGroupBox1.TextColor = System.Drawing.Color.White;
            // 
            // dataGridView_Model
            // 
            this.dataGridView_Model.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Model.Location = new System.Drawing.Point(10, 19);
            this.dataGridView_Model.Name = "dataGridView_Model";
            this.dataGridView_Model.RowTemplate.Height = 23;
            this.dataGridView_Model.Size = new System.Drawing.Size(248, 72);
            this.dataGridView_Model.TabIndex = 54;
            // 
            // crownButton_Model_Change
            // 
            this.crownButton_Model_Change.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.crownButton_Model_Change.Location = new System.Drawing.Point(272, 23);
            this.crownButton_Model_Change.Name = "crownButton_Model_Change";
            this.crownButton_Model_Change.Padding = new System.Windows.Forms.Padding(5);
            this.crownButton_Model_Change.Size = new System.Drawing.Size(89, 25);
            this.crownButton_Model_Change.TabIndex = 53;
            this.crownButton_Model_Change.Text = "Change";
            this.crownButton_Model_Change.Click += new System.EventHandler(this.crownButton_Model_Change_Click);
            // 
            // ModelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.parrotGroupBox1);
            this.Name = "ModelControl";
            this.Size = new System.Drawing.Size(380, 117);
            this.parrotGroupBox1.ResumeLayout(false);
            this.parrotGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Model)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.ParrotGroupBox parrotGroupBox1;
        private ReaLTaiizor.Controls.CrownButton crownButton_Model_Change;
        private System.Windows.Forms.DataGridView dataGridView_Model;
    }
}
