
namespace SecGemApp.Dlg
{
    partial class RecipePopup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_Recipe = new System.Windows.Forms.Label();
            this.BTN_RECIPE_CLOSE = new System.Windows.Forms.Button();
            this.BTN_RECIPE_SAVE = new System.Windows.Forms.Button();
            this.BTN_RECIPE_DOWN_REQ = new System.Windows.Forms.Button();
            this.dataGridView_Recipe = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Recipe)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.label_Recipe);
            this.groupBox2.Controls.Add(this.BTN_RECIPE_CLOSE);
            this.groupBox2.Controls.Add(this.BTN_RECIPE_SAVE);
            this.groupBox2.Controls.Add(this.BTN_RECIPE_DOWN_REQ);
            this.groupBox2.Controls.Add(this.dataGridView_Recipe);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(497, 524);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            // 
            // label_Recipe
            // 
            this.label_Recipe.BackColor = System.Drawing.Color.Transparent;
            this.label_Recipe.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Recipe.ForeColor = System.Drawing.Color.Black;
            this.label_Recipe.Location = new System.Drawing.Point(100, 21);
            this.label_Recipe.Name = "label_Recipe";
            this.label_Recipe.Size = new System.Drawing.Size(380, 23);
            this.label_Recipe.TabIndex = 50;
            this.label_Recipe.Text = "RECIPE_TEMP";
            this.label_Recipe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BTN_RECIPE_CLOSE
            // 
            this.BTN_RECIPE_CLOSE.BackColor = System.Drawing.Color.Tan;
            this.BTN_RECIPE_CLOSE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_RECIPE_CLOSE.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_RECIPE_CLOSE.ForeColor = System.Drawing.Color.White;
            this.BTN_RECIPE_CLOSE.Location = new System.Drawing.Point(376, 449);
            this.BTN_RECIPE_CLOSE.Name = "BTN_RECIPE_CLOSE";
            this.BTN_RECIPE_CLOSE.Size = new System.Drawing.Size(104, 40);
            this.BTN_RECIPE_CLOSE.TabIndex = 49;
            this.BTN_RECIPE_CLOSE.Text = "CLOSE";
            this.BTN_RECIPE_CLOSE.UseVisualStyleBackColor = false;
            this.BTN_RECIPE_CLOSE.Click += new System.EventHandler(this.BTN_RECIPE_CLOSE_Click);
            // 
            // BTN_RECIPE_SAVE
            // 
            this.BTN_RECIPE_SAVE.BackColor = System.Drawing.Color.Tan;
            this.BTN_RECIPE_SAVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_RECIPE_SAVE.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_RECIPE_SAVE.ForeColor = System.Drawing.Color.White;
            this.BTN_RECIPE_SAVE.Location = new System.Drawing.Point(376, 122);
            this.BTN_RECIPE_SAVE.Name = "BTN_RECIPE_SAVE";
            this.BTN_RECIPE_SAVE.Size = new System.Drawing.Size(104, 53);
            this.BTN_RECIPE_SAVE.TabIndex = 48;
            this.BTN_RECIPE_SAVE.Text = "SAVE";
            this.BTN_RECIPE_SAVE.UseVisualStyleBackColor = false;
            this.BTN_RECIPE_SAVE.Click += new System.EventHandler(this.BTN_RECIPE_SAVE_Click);
            // 
            // BTN_RECIPE_DOWN_REQ
            // 
            this.BTN_RECIPE_DOWN_REQ.BackColor = System.Drawing.Color.Tan;
            this.BTN_RECIPE_DOWN_REQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_RECIPE_DOWN_REQ.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BTN_RECIPE_DOWN_REQ.ForeColor = System.Drawing.Color.White;
            this.BTN_RECIPE_DOWN_REQ.Location = new System.Drawing.Point(376, 63);
            this.BTN_RECIPE_DOWN_REQ.Name = "BTN_RECIPE_DOWN_REQ";
            this.BTN_RECIPE_DOWN_REQ.Size = new System.Drawing.Size(104, 53);
            this.BTN_RECIPE_DOWN_REQ.TabIndex = 47;
            this.BTN_RECIPE_DOWN_REQ.Text = "DOWNLOAD REQEST";
            this.BTN_RECIPE_DOWN_REQ.UseVisualStyleBackColor = false;
            this.BTN_RECIPE_DOWN_REQ.Click += new System.EventHandler(this.BTN_RECIPE_DOWN_REQ_Click);
            // 
            // dataGridView_Recipe
            // 
            this.dataGridView_Recipe.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.GhostWhite;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Recipe.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Recipe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Recipe.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_Recipe.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView_Recipe.Location = new System.Drawing.Point(26, 63);
            this.dataGridView_Recipe.Name = "dataGridView_Recipe";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.RosyBrown;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Recipe.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_Recipe.RowTemplate.Height = 23;
            this.dataGridView_Recipe.Size = new System.Drawing.Size(309, 426);
            this.dataGridView_Recipe.TabIndex = 46;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(23, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 23);
            this.label3.TabIndex = 26;
            this.label3.Text = "RECIPE :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RecipePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 524);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RecipePopup";
            this.Text = "RecipePopup";
            this.VisibleChanged += new System.EventHandler(this.RecipePopup_VisibleChanged);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Recipe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView_Recipe;
        private System.Windows.Forms.Button BTN_RECIPE_SAVE;
        private System.Windows.Forms.Button BTN_RECIPE_DOWN_REQ;
        private System.Windows.Forms.Button BTN_RECIPE_CLOSE;
        private System.Windows.Forms.Label label_Recipe;
    }
}