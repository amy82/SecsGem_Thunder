
namespace SecGemApp
{
    partial class SecsGemStatusControl
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
            this.parrotGroupBox5 = new ReaLTaiizor.Controls.ParrotGroupBox();
            this.label_OperatorId = new System.Windows.Forms.Label();
            this.label_ControlStateVal = new System.Windows.Forms.Label();
            this.label_CommunicationStateVal = new System.Windows.Forms.Label();
            this.label_EquipStatusVal = new System.Windows.Forms.Label();
            this.bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            this.bigLabel5 = new ReaLTaiizor.Controls.BigLabel();
            this.bigLabel6 = new ReaLTaiizor.Controls.BigLabel();
            this.bigLabel8 = new ReaLTaiizor.Controls.BigLabel();
            this.bigLabel2 = new ReaLTaiizor.Controls.BigLabel();
            this.label_Tester1 = new System.Windows.Forms.Label();
            this.label_Tester2 = new System.Windows.Forms.Label();
            this.label_Tester3 = new System.Windows.Forms.Label();
            this.label_Tester4 = new System.Windows.Forms.Label();
            this.parrotGroupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // parrotGroupBox5
            // 
            this.parrotGroupBox5.BorderColor = System.Drawing.Color.White;
            this.parrotGroupBox5.BorderWidth = 1;
            this.parrotGroupBox5.Controls.Add(this.label_Tester4);
            this.parrotGroupBox5.Controls.Add(this.label_Tester3);
            this.parrotGroupBox5.Controls.Add(this.label_Tester2);
            this.parrotGroupBox5.Controls.Add(this.label_Tester1);
            this.parrotGroupBox5.Controls.Add(this.bigLabel2);
            this.parrotGroupBox5.Controls.Add(this.label_OperatorId);
            this.parrotGroupBox5.Controls.Add(this.label_ControlStateVal);
            this.parrotGroupBox5.Controls.Add(this.label_CommunicationStateVal);
            this.parrotGroupBox5.Controls.Add(this.label_EquipStatusVal);
            this.parrotGroupBox5.Controls.Add(this.bigLabel1);
            this.parrotGroupBox5.Controls.Add(this.bigLabel5);
            this.parrotGroupBox5.Controls.Add(this.bigLabel6);
            this.parrotGroupBox5.Controls.Add(this.bigLabel8);
            this.parrotGroupBox5.Location = new System.Drawing.Point(3, 3);
            this.parrotGroupBox5.Name = "parrotGroupBox5";
            this.parrotGroupBox5.ShowText = true;
            this.parrotGroupBox5.Size = new System.Drawing.Size(374, 189);
            this.parrotGroupBox5.TabIndex = 33;
            this.parrotGroupBox5.TabStop = false;
            this.parrotGroupBox5.Text = "Secs/Gem Status";
            this.parrotGroupBox5.TextColor = System.Drawing.Color.White;
            // 
            // label_OperatorId
            // 
            this.label_OperatorId.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label_OperatorId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_OperatorId.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_OperatorId.ForeColor = System.Drawing.Color.White;
            this.label_OperatorId.Location = new System.Drawing.Point(159, 114);
            this.label_OperatorId.Name = "label_OperatorId";
            this.label_OperatorId.Size = new System.Drawing.Size(200, 25);
            this.label_OperatorId.TabIndex = 34;
            this.label_OperatorId.Text = "OP ID";
            this.label_OperatorId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_OperatorId.Click += new System.EventHandler(this.label_OperatorId_Click);
            // 
            // label_ControlStateVal
            // 
            this.label_ControlStateVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.label_ControlStateVal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_ControlStateVal.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_ControlStateVal.ForeColor = System.Drawing.Color.White;
            this.label_ControlStateVal.Location = new System.Drawing.Point(159, 52);
            this.label_ControlStateVal.Name = "label_ControlStateVal";
            this.label_ControlStateVal.Size = new System.Drawing.Size(200, 25);
            this.label_ControlStateVal.TabIndex = 33;
            this.label_ControlStateVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_CommunicationStateVal
            // 
            this.label_CommunicationStateVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.label_CommunicationStateVal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_CommunicationStateVal.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_CommunicationStateVal.ForeColor = System.Drawing.Color.White;
            this.label_CommunicationStateVal.Location = new System.Drawing.Point(159, 20);
            this.label_CommunicationStateVal.Name = "label_CommunicationStateVal";
            this.label_CommunicationStateVal.Size = new System.Drawing.Size(200, 25);
            this.label_CommunicationStateVal.TabIndex = 32;
            this.label_CommunicationStateVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_EquipStatusVal
            // 
            this.label_EquipStatusVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.label_EquipStatusVal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_EquipStatusVal.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_EquipStatusVal.ForeColor = System.Drawing.Color.White;
            this.label_EquipStatusVal.Location = new System.Drawing.Point(159, 83);
            this.label_EquipStatusVal.Name = "label_EquipStatusVal";
            this.label_EquipStatusVal.Size = new System.Drawing.Size(200, 25);
            this.label_EquipStatusVal.TabIndex = 31;
            this.label_EquipStatusVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bigLabel1
            // 
            this.bigLabel1.AutoSize = true;
            this.bigLabel1.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel1.Font = new System.Drawing.Font("돋움", 9.75F);
            this.bigLabel1.ForeColor = System.Drawing.Color.White;
            this.bigLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel1.Location = new System.Drawing.Point(18, 89);
            this.bigLabel1.Name = "bigLabel1";
            this.bigLabel1.Size = new System.Drawing.Size(78, 13);
            this.bigLabel1.TabIndex = 29;
            this.bigLabel1.Text = "Equip Status";
            // 
            // bigLabel5
            // 
            this.bigLabel5.AutoSize = true;
            this.bigLabel5.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel5.Font = new System.Drawing.Font("돋움", 9.75F);
            this.bigLabel5.ForeColor = System.Drawing.Color.White;
            this.bigLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel5.Location = new System.Drawing.Point(18, 119);
            this.bigLabel5.Name = "bigLabel5";
            this.bigLabel5.Size = new System.Drawing.Size(70, 13);
            this.bigLabel5.TabIndex = 26;
            this.bigLabel5.Text = "Operator ID";
            // 
            // bigLabel6
            // 
            this.bigLabel6.AutoSize = true;
            this.bigLabel6.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel6.Font = new System.Drawing.Font("돋움", 9.75F);
            this.bigLabel6.ForeColor = System.Drawing.Color.White;
            this.bigLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel6.Location = new System.Drawing.Point(18, 23);
            this.bigLabel6.Name = "bigLabel6";
            this.bigLabel6.Size = new System.Drawing.Size(124, 13);
            this.bigLabel6.TabIndex = 26;
            this.bigLabel6.Text = "Communication State";
            this.bigLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bigLabel8
            // 
            this.bigLabel8.AutoSize = true;
            this.bigLabel8.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel8.Font = new System.Drawing.Font("돋움", 9.75F);
            this.bigLabel8.ForeColor = System.Drawing.Color.White;
            this.bigLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel8.Location = new System.Drawing.Point(18, 56);
            this.bigLabel8.Name = "bigLabel8";
            this.bigLabel8.Size = new System.Drawing.Size(79, 13);
            this.bigLabel8.TabIndex = 24;
            this.bigLabel8.Text = "Control State";
            this.bigLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bigLabel2
            // 
            this.bigLabel2.AutoSize = true;
            this.bigLabel2.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel2.Font = new System.Drawing.Font("돋움", 9.75F);
            this.bigLabel2.ForeColor = System.Drawing.Color.White;
            this.bigLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel2.Location = new System.Drawing.Point(18, 153);
            this.bigLabel2.Name = "bigLabel2";
            this.bigLabel2.Size = new System.Drawing.Size(42, 13);
            this.bigLabel2.TabIndex = 35;
            this.bigLabel2.Text = "Tester";
            // 
            // label_Tester1
            // 
            this.label_Tester1.BackColor = System.Drawing.Color.Transparent;
            this.label_Tester1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Tester1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Tester1.ForeColor = System.Drawing.Color.White;
            this.label_Tester1.Location = new System.Drawing.Point(158, 153);
            this.label_Tester1.Name = "label_Tester1";
            this.label_Tester1.Size = new System.Drawing.Size(50, 22);
            this.label_Tester1.TabIndex = 39;
            this.label_Tester1.Text = "# T01";
            this.label_Tester1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Tester2
            // 
            this.label_Tester2.BackColor = System.Drawing.Color.Transparent;
            this.label_Tester2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Tester2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Tester2.ForeColor = System.Drawing.Color.White;
            this.label_Tester2.Location = new System.Drawing.Point(209, 153);
            this.label_Tester2.Name = "label_Tester2";
            this.label_Tester2.Size = new System.Drawing.Size(50, 22);
            this.label_Tester2.TabIndex = 40;
            this.label_Tester2.Text = "# T02";
            this.label_Tester2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Tester3
            // 
            this.label_Tester3.BackColor = System.Drawing.Color.Transparent;
            this.label_Tester3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Tester3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Tester3.ForeColor = System.Drawing.Color.White;
            this.label_Tester3.Location = new System.Drawing.Point(260, 153);
            this.label_Tester3.Name = "label_Tester3";
            this.label_Tester3.Size = new System.Drawing.Size(50, 22);
            this.label_Tester3.TabIndex = 41;
            this.label_Tester3.Text = "# T03";
            this.label_Tester3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Tester4
            // 
            this.label_Tester4.BackColor = System.Drawing.Color.Transparent;
            this.label_Tester4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Tester4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Tester4.ForeColor = System.Drawing.Color.White;
            this.label_Tester4.Location = new System.Drawing.Point(311, 153);
            this.label_Tester4.Name = "label_Tester4";
            this.label_Tester4.Size = new System.Drawing.Size(50, 22);
            this.label_Tester4.TabIndex = 42;
            this.label_Tester4.Text = "# T04";
            this.label_Tester4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SecsGemStatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.parrotGroupBox5);
            this.Name = "SecsGemStatusControl";
            this.Size = new System.Drawing.Size(380, 195);
            this.parrotGroupBox5.ResumeLayout(false);
            this.parrotGroupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.ParrotGroupBox parrotGroupBox5;
        private ReaLTaiizor.Controls.BigLabel bigLabel5;
        private ReaLTaiizor.Controls.BigLabel bigLabel6;
        private ReaLTaiizor.Controls.BigLabel bigLabel8;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private System.Windows.Forms.Label label_EquipStatusVal;
        private System.Windows.Forms.Label label_OperatorId;
        private System.Windows.Forms.Label label_ControlStateVal;
        private System.Windows.Forms.Label label_CommunicationStateVal;
        private ReaLTaiizor.Controls.BigLabel bigLabel2;
        private System.Windows.Forms.Label label_Tester4;
        private System.Windows.Forms.Label label_Tester3;
        private System.Windows.Forms.Label label_Tester2;
        private System.Windows.Forms.Label label_Tester1;
    }
}
