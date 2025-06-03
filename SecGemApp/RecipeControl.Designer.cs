
namespace SecGemApp
{
    partial class RecipeControl
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
            this.parrotGroupBox2 = new ReaLTaiizor.Controls.ParrotGroupBox();
            this.dataGridView_Recipe = new System.Windows.Forms.DataGridView();
            this.crownButton_Recipe_Change = new ReaLTaiizor.Controls.CrownButton();
            this.crownButton_Recipe_View = new ReaLTaiizor.Controls.CrownButton();
            this.parrotGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Recipe)).BeginInit();
            this.SuspendLayout();
            // 
            // parrotGroupBox2
            // 
            this.parrotGroupBox2.BorderColor = System.Drawing.Color.White;
            this.parrotGroupBox2.BorderWidth = 1;
            this.parrotGroupBox2.Controls.Add(this.crownButton_Recipe_View);
            this.parrotGroupBox2.Controls.Add(this.dataGridView_Recipe);
            this.parrotGroupBox2.Controls.Add(this.crownButton_Recipe_Change);
            this.parrotGroupBox2.Location = new System.Drawing.Point(3, 3);
            this.parrotGroupBox2.Name = "parrotGroupBox2";
            this.parrotGroupBox2.ShowText = true;
            this.parrotGroupBox2.Size = new System.Drawing.Size(374, 104);
            this.parrotGroupBox2.TabIndex = 37;
            this.parrotGroupBox2.TabStop = false;
            this.parrotGroupBox2.Text = "RECIPE";
            this.parrotGroupBox2.TextColor = System.Drawing.Color.White;
            // 
            // dataGridView_Recipe
            // 
            this.dataGridView_Recipe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Recipe.Location = new System.Drawing.Point(10, 19);
            this.dataGridView_Recipe.Name = "dataGridView_Recipe";
            this.dataGridView_Recipe.RowTemplate.Height = 23;
            this.dataGridView_Recipe.Size = new System.Drawing.Size(248, 72);
            this.dataGridView_Recipe.TabIndex = 56;
            // 
            // crownButton_Recipe_Change
            // 
            this.crownButton_Recipe_Change.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.crownButton_Recipe_Change.Location = new System.Drawing.Point(276, 19);
            this.crownButton_Recipe_Change.Name = "crownButton_Recipe_Change";
            this.crownButton_Recipe_Change.Padding = new System.Windows.Forms.Padding(5);
            this.crownButton_Recipe_Change.Size = new System.Drawing.Size(89, 25);
            this.crownButton_Recipe_Change.TabIndex = 55;
            this.crownButton_Recipe_Change.Text = "Change";
            this.crownButton_Recipe_Change.Click += new System.EventHandler(this.crownButton_Recipe_Change_Click);
            // 
            // crownButton_Recipe_View
            // 
            this.crownButton_Recipe_View.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.crownButton_Recipe_View.Location = new System.Drawing.Point(276, 53);
            this.crownButton_Recipe_View.Name = "crownButton_Recipe_View";
            this.crownButton_Recipe_View.Padding = new System.Windows.Forms.Padding(5);
            this.crownButton_Recipe_View.Size = new System.Drawing.Size(89, 25);
            this.crownButton_Recipe_View.TabIndex = 57;
            this.crownButton_Recipe_View.Text = "View";
            this.crownButton_Recipe_View.Click += new System.EventHandler(this.crownButton_Recipe_View_Click);
            // 
            // RecipeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.parrotGroupBox2);
            this.Name = "RecipeControl";
            this.Size = new System.Drawing.Size(380, 117);
            this.parrotGroupBox2.ResumeLayout(false);
            this.parrotGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Recipe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.ParrotGroupBox parrotGroupBox2;
        private System.Windows.Forms.DataGridView dataGridView_Recipe;
        private ReaLTaiizor.Controls.CrownButton crownButton_Recipe_Change;
        private ReaLTaiizor.Controls.CrownButton crownButton_Recipe_View;
    }
}
