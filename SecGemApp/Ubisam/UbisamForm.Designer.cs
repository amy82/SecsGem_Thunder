
namespace SecGemApp.Ubisam
{
    partial class UbisamForm
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
            this.txtLogs = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_OnlineRemote = new System.Windows.Forms.Button();
            this.button_Offline = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_OpenUgc = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Initlalize = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnListECIDValue = new System.Windows.Forms.Button();
            this.btnSetECIDValue = new System.Windows.Forms.Button();
            this.txtECIDValue = new System.Windows.Forms.TextBox();
            this.cbbECID = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSetVariableList = new System.Windows.Forms.Button();
            this.btnSetVIDValue = new System.Windows.Forms.Button();
            this.txtVIDValue = new System.Windows.Forms.TextBox();
            this.cbbVID = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnReportTest = new System.Windows.Forms.Button();
            this.btnReport2 = new System.Windows.Forms.Button();
            this.btnReport1 = new System.Windows.Forms.Button();
            this.cbbCE = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnProcessingStateChange = new System.Windows.Forms.Button();
            this.txtEQPProcessingState = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnClearAlarm = new System.Windows.Forms.Button();
            this.btnSetAlarm = new System.Windows.Forms.Button();
            this.txtAlarm = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtTerminalMessage = new System.Windows.Forms.TextBox();
            this.btnReportTerminalMessage = new System.Windows.Forms.Button();
            this.txtTerminalTID = new System.Windows.Forms.TextBox();
            this.button_Ubisam_Close = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.txtPPID = new System.Windows.Forms.TextBox();
            this.button_PPChanged = new System.Windows.Forms.Button();
            this.button_PPLoadInquire = new System.Windows.Forms.Button();
            this.button_PPSend = new System.Windows.Forms.Button();
            this.button_PPRequest = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnRequestFmtPPVerificationSend = new System.Windows.Forms.Button();
            this.txtFMTPPID = new System.Windows.Forms.TextBox();
            this.btnRequestFmtPPSend = new System.Windows.Forms.Button();
            this.btnRequestFmtPPSendWithoutValue = new System.Windows.Forms.Button();
            this.btnRequestFmtPPChanged = new System.Windows.Forms.Button();
            this.btnRequestFmtPPRequest = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLogs
            // 
            this.txtLogs.Location = new System.Drawing.Point(12, 12);
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.Size = new System.Drawing.Size(623, 486);
            this.txtLogs.TabIndex = 0;
            this.txtLogs.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_OnlineRemote);
            this.groupBox1.Controls.Add(this.button_Offline);
            this.groupBox1.Location = new System.Drawing.Point(649, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 61);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control State";
            // 
            // button_OnlineRemote
            // 
            this.button_OnlineRemote.Location = new System.Drawing.Point(97, 24);
            this.button_OnlineRemote.Name = "button_OnlineRemote";
            this.button_OnlineRemote.Size = new System.Drawing.Size(107, 23);
            this.button_OnlineRemote.TabIndex = 1;
            this.button_OnlineRemote.Text = "Online Remote";
            this.button_OnlineRemote.UseVisualStyleBackColor = true;
            this.button_OnlineRemote.Click += new System.EventHandler(this.button_OnlineRemote_Click);
            // 
            // button_Offline
            // 
            this.button_Offline.Location = new System.Drawing.Point(16, 24);
            this.button_Offline.Name = "button_Offline";
            this.button_Offline.Size = new System.Drawing.Size(75, 23);
            this.button_Offline.TabIndex = 0;
            this.button_Offline.Text = "Offline";
            this.button_Offline.UseVisualStyleBackColor = true;
            this.button_Offline.Click += new System.EventHandler(this.button_Offline_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_OpenUgc);
            this.groupBox2.Controls.Add(this.button_Stop);
            this.groupBox2.Controls.Add(this.button_Start);
            this.groupBox2.Controls.Add(this.button_Initlalize);
            this.groupBox2.Location = new System.Drawing.Point(649, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 82);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Driver";
            // 
            // button_OpenUgc
            // 
            this.button_OpenUgc.Location = new System.Drawing.Point(16, 53);
            this.button_OpenUgc.Name = "button_OpenUgc";
            this.button_OpenUgc.Size = new System.Drawing.Size(75, 23);
            this.button_OpenUgc.TabIndex = 3;
            this.button_OpenUgc.Text = "Open";
            this.button_OpenUgc.UseVisualStyleBackColor = true;
            this.button_OpenUgc.Click += new System.EventHandler(this.button_OpenUgc_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(178, 24);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(75, 23);
            this.button_Stop.TabIndex = 2;
            this.button_Stop.Text = "Stop";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(97, 24);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(75, 23);
            this.button_Start.TabIndex = 1;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Initlalize
            // 
            this.button_Initlalize.Location = new System.Drawing.Point(16, 24);
            this.button_Initlalize.Name = "button_Initlalize";
            this.button_Initlalize.Size = new System.Drawing.Size(75, 23);
            this.button_Initlalize.TabIndex = 0;
            this.button_Initlalize.Text = "Initialize";
            this.button_Initlalize.UseVisualStyleBackColor = true;
            this.button_Initlalize.Click += new System.EventHandler(this.button_Initlalize_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnListECIDValue);
            this.groupBox3.Controls.Add(this.btnSetECIDValue);
            this.groupBox3.Controls.Add(this.txtECIDValue);
            this.groupBox3.Controls.Add(this.cbbECID);
            this.groupBox3.Location = new System.Drawing.Point(649, 185);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(304, 72);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ECV";
            // 
            // btnListECIDValue
            // 
            this.btnListECIDValue.Location = new System.Drawing.Point(178, 43);
            this.btnListECIDValue.Name = "btnListECIDValue";
            this.btnListECIDValue.Size = new System.Drawing.Size(75, 23);
            this.btnListECIDValue.TabIndex = 7;
            this.btnListECIDValue.Text = "EC List Set";
            this.btnListECIDValue.UseVisualStyleBackColor = true;
            this.btnListECIDValue.Click += new System.EventHandler(this.btnListECIDValue_Click);
            // 
            // btnSetECIDValue
            // 
            this.btnSetECIDValue.Location = new System.Drawing.Point(97, 43);
            this.btnSetECIDValue.Name = "btnSetECIDValue";
            this.btnSetECIDValue.Size = new System.Drawing.Size(75, 23);
            this.btnSetECIDValue.TabIndex = 4;
            this.btnSetECIDValue.Text = "EC Set";
            this.btnSetECIDValue.UseVisualStyleBackColor = true;
            this.btnSetECIDValue.Click += new System.EventHandler(this.btnSetECIDValue_Click);
            // 
            // txtECIDValue
            // 
            this.txtECIDValue.Location = new System.Drawing.Point(16, 45);
            this.txtECIDValue.Name = "txtECIDValue";
            this.txtECIDValue.Size = new System.Drawing.Size(75, 21);
            this.txtECIDValue.TabIndex = 6;
            // 
            // cbbECID
            // 
            this.cbbECID.FormattingEnabled = true;
            this.cbbECID.Location = new System.Drawing.Point(16, 20);
            this.cbbECID.Name = "cbbECID";
            this.cbbECID.Size = new System.Drawing.Size(282, 20);
            this.cbbECID.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSetVariableList);
            this.groupBox4.Controls.Add(this.btnSetVIDValue);
            this.groupBox4.Controls.Add(this.txtVIDValue);
            this.groupBox4.Controls.Add(this.cbbVID);
            this.groupBox4.Location = new System.Drawing.Point(649, 284);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(304, 72);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SV/DVval";
            // 
            // btnSetVariableList
            // 
            this.btnSetVariableList.Location = new System.Drawing.Point(178, 43);
            this.btnSetVariableList.Name = "btnSetVariableList";
            this.btnSetVariableList.Size = new System.Drawing.Size(75, 23);
            this.btnSetVariableList.TabIndex = 9;
            this.btnSetVariableList.Text = "V List Set";
            this.btnSetVariableList.UseVisualStyleBackColor = true;
            this.btnSetVariableList.Click += new System.EventHandler(this.btnSetVariableList_Click);
            // 
            // btnSetVIDValue
            // 
            this.btnSetVIDValue.Location = new System.Drawing.Point(97, 43);
            this.btnSetVIDValue.Name = "btnSetVIDValue";
            this.btnSetVIDValue.Size = new System.Drawing.Size(75, 23);
            this.btnSetVIDValue.TabIndex = 8;
            this.btnSetVIDValue.Text = "V Set";
            this.btnSetVIDValue.UseVisualStyleBackColor = true;
            this.btnSetVIDValue.Click += new System.EventHandler(this.btnSetVIDValue_Click);
            // 
            // txtVIDValue
            // 
            this.txtVIDValue.Location = new System.Drawing.Point(16, 45);
            this.txtVIDValue.Name = "txtVIDValue";
            this.txtVIDValue.Size = new System.Drawing.Size(75, 21);
            this.txtVIDValue.TabIndex = 7;
            // 
            // cbbVID
            // 
            this.cbbVID.FormattingEnabled = true;
            this.cbbVID.Location = new System.Drawing.Point(16, 20);
            this.cbbVID.Name = "cbbVID";
            this.cbbVID.Size = new System.Drawing.Size(282, 20);
            this.cbbVID.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnReportTest);
            this.groupBox5.Controls.Add(this.btnReport2);
            this.groupBox5.Controls.Add(this.btnReport1);
            this.groupBox5.Controls.Add(this.cbbCE);
            this.groupBox5.Location = new System.Drawing.Point(649, 384);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(304, 72);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "CE";
            // 
            // btnReportTest
            // 
            this.btnReportTest.Location = new System.Drawing.Point(178, 43);
            this.btnReportTest.Name = "btnReportTest";
            this.btnReportTest.Size = new System.Drawing.Size(92, 23);
            this.btnReportTest.TabIndex = 10;
            this.btnReportTest.Text = "Report Test";
            this.btnReportTest.UseVisualStyleBackColor = true;
            this.btnReportTest.Click += new System.EventHandler(this.btnReportTest_Click);
            // 
            // btnReport2
            // 
            this.btnReport2.Location = new System.Drawing.Point(97, 43);
            this.btnReport2.Name = "btnReport2";
            this.btnReport2.Size = new System.Drawing.Size(75, 23);
            this.btnReport2.TabIndex = 9;
            this.btnReport2.Text = "Report 2";
            this.btnReport2.UseVisualStyleBackColor = true;
            this.btnReport2.Click += new System.EventHandler(this.btnReport2_Click);
            // 
            // btnReport1
            // 
            this.btnReport1.Location = new System.Drawing.Point(16, 43);
            this.btnReport1.Name = "btnReport1";
            this.btnReport1.Size = new System.Drawing.Size(75, 23);
            this.btnReport1.TabIndex = 8;
            this.btnReport1.Text = "Report 1";
            this.btnReport1.UseVisualStyleBackColor = true;
            this.btnReport1.Click += new System.EventHandler(this.btnReport1_Click);
            // 
            // cbbCE
            // 
            this.cbbCE.FormattingEnabled = true;
            this.cbbCE.Location = new System.Drawing.Point(16, 20);
            this.cbbCE.Name = "cbbCE";
            this.cbbCE.Size = new System.Drawing.Size(282, 20);
            this.cbbCE.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnProcessingStateChange);
            this.groupBox6.Controls.Add(this.txtEQPProcessingState);
            this.groupBox6.Location = new System.Drawing.Point(649, 476);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(304, 53);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Process State";
            // 
            // btnProcessingStateChange
            // 
            this.btnProcessingStateChange.Location = new System.Drawing.Point(112, 20);
            this.btnProcessingStateChange.Name = "btnProcessingStateChange";
            this.btnProcessingStateChange.Size = new System.Drawing.Size(84, 23);
            this.btnProcessingStateChange.TabIndex = 8;
            this.btnProcessingStateChange.Text = "Change";
            this.btnProcessingStateChange.UseVisualStyleBackColor = true;
            this.btnProcessingStateChange.Click += new System.EventHandler(this.btnProcessingStateChange_Click);
            // 
            // txtEQPProcessingState
            // 
            this.txtEQPProcessingState.Location = new System.Drawing.Point(14, 20);
            this.txtEQPProcessingState.Name = "txtEQPProcessingState";
            this.txtEQPProcessingState.Size = new System.Drawing.Size(92, 21);
            this.txtEQPProcessingState.TabIndex = 7;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnClearAlarm);
            this.groupBox7.Controls.Add(this.btnSetAlarm);
            this.groupBox7.Controls.Add(this.txtAlarm);
            this.groupBox7.Location = new System.Drawing.Point(331, 551);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(304, 53);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Alarm";
            // 
            // btnClearAlarm
            // 
            this.btnClearAlarm.Location = new System.Drawing.Point(200, 20);
            this.btnClearAlarm.Name = "btnClearAlarm";
            this.btnClearAlarm.Size = new System.Drawing.Size(84, 23);
            this.btnClearAlarm.TabIndex = 9;
            this.btnClearAlarm.Text = "Alarm Clear";
            this.btnClearAlarm.UseVisualStyleBackColor = true;
            this.btnClearAlarm.Click += new System.EventHandler(this.btnClearAlarm_Click);
            // 
            // btnSetAlarm
            // 
            this.btnSetAlarm.Location = new System.Drawing.Point(112, 20);
            this.btnSetAlarm.Name = "btnSetAlarm";
            this.btnSetAlarm.Size = new System.Drawing.Size(84, 23);
            this.btnSetAlarm.TabIndex = 8;
            this.btnSetAlarm.Text = "Alarm Set";
            this.btnSetAlarm.UseVisualStyleBackColor = true;
            this.btnSetAlarm.Click += new System.EventHandler(this.btnSetAlarm_Click);
            // 
            // txtAlarm
            // 
            this.txtAlarm.Location = new System.Drawing.Point(14, 20);
            this.txtAlarm.Name = "txtAlarm";
            this.txtAlarm.Size = new System.Drawing.Size(92, 21);
            this.txtAlarm.TabIndex = 7;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtTerminalMessage);
            this.groupBox8.Controls.Add(this.btnReportTerminalMessage);
            this.groupBox8.Controls.Add(this.txtTerminalTID);
            this.groupBox8.Location = new System.Drawing.Point(328, 643);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(304, 123);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Terminal Message";
            // 
            // txtTerminalMessage
            // 
            this.txtTerminalMessage.Location = new System.Drawing.Point(14, 59);
            this.txtTerminalMessage.Multiline = true;
            this.txtTerminalMessage.Name = "txtTerminalMessage";
            this.txtTerminalMessage.Size = new System.Drawing.Size(270, 51);
            this.txtTerminalMessage.TabIndex = 9;
            // 
            // btnReportTerminalMessage
            // 
            this.btnReportTerminalMessage.Location = new System.Drawing.Point(112, 24);
            this.btnReportTerminalMessage.Name = "btnReportTerminalMessage";
            this.btnReportTerminalMessage.Size = new System.Drawing.Size(84, 23);
            this.btnReportTerminalMessage.TabIndex = 8;
            this.btnReportTerminalMessage.Text = "Send";
            this.btnReportTerminalMessage.UseVisualStyleBackColor = true;
            this.btnReportTerminalMessage.Click += new System.EventHandler(this.btnReportTerminalMessage_Click);
            // 
            // txtTerminalTID
            // 
            this.txtTerminalTID.Location = new System.Drawing.Point(14, 25);
            this.txtTerminalTID.Name = "txtTerminalTID";
            this.txtTerminalTID.Size = new System.Drawing.Size(92, 21);
            this.txtTerminalTID.TabIndex = 7;
            // 
            // button_Ubisam_Close
            // 
            this.button_Ubisam_Close.Location = new System.Drawing.Point(855, 733);
            this.button_Ubisam_Close.Name = "button_Ubisam_Close";
            this.button_Ubisam_Close.Size = new System.Drawing.Size(107, 36);
            this.button_Ubisam_Close.TabIndex = 4;
            this.button_Ubisam_Close.Text = "Close";
            this.button_Ubisam_Close.UseVisualStyleBackColor = true;
            this.button_Ubisam_Close.Click += new System.EventHandler(this.button_Ubisam_Close_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.txtPPID);
            this.groupBox9.Controls.Add(this.button_PPChanged);
            this.groupBox9.Controls.Add(this.button_PPLoadInquire);
            this.groupBox9.Controls.Add(this.button_PPSend);
            this.groupBox9.Controls.Add(this.button_PPRequest);
            this.groupBox9.Location = new System.Drawing.Point(18, 512);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(294, 118);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Process Program";
            // 
            // txtPPID
            // 
            this.txtPPID.Location = new System.Drawing.Point(28, 22);
            this.txtPPID.Name = "txtPPID";
            this.txtPPID.Size = new System.Drawing.Size(110, 21);
            this.txtPPID.TabIndex = 10;
            // 
            // button_PPChanged
            // 
            this.button_PPChanged.Location = new System.Drawing.Point(157, 78);
            this.button_PPChanged.Name = "button_PPChanged";
            this.button_PPChanged.Size = new System.Drawing.Size(110, 23);
            this.button_PPChanged.TabIndex = 3;
            this.button_PPChanged.Text = "PP Changed";
            this.button_PPChanged.UseVisualStyleBackColor = true;
            this.button_PPChanged.Click += new System.EventHandler(this.button_PPChanged_Click);
            // 
            // button_PPLoadInquire
            // 
            this.button_PPLoadInquire.Location = new System.Drawing.Point(28, 78);
            this.button_PPLoadInquire.Name = "button_PPLoadInquire";
            this.button_PPLoadInquire.Size = new System.Drawing.Size(110, 23);
            this.button_PPLoadInquire.TabIndex = 2;
            this.button_PPLoadInquire.Text = "PP Load Inquire";
            this.button_PPLoadInquire.UseVisualStyleBackColor = true;
            this.button_PPLoadInquire.Click += new System.EventHandler(this.button_PPLoadInquire_Click);
            // 
            // button_PPSend
            // 
            this.button_PPSend.Location = new System.Drawing.Point(157, 49);
            this.button_PPSend.Name = "button_PPSend";
            this.button_PPSend.Size = new System.Drawing.Size(110, 23);
            this.button_PPSend.TabIndex = 1;
            this.button_PPSend.Text = "PP Send";
            this.button_PPSend.UseVisualStyleBackColor = true;
            this.button_PPSend.Click += new System.EventHandler(this.button_PPSend_Click);
            // 
            // button_PPRequest
            // 
            this.button_PPRequest.Location = new System.Drawing.Point(28, 49);
            this.button_PPRequest.Name = "button_PPRequest";
            this.button_PPRequest.Size = new System.Drawing.Size(110, 23);
            this.button_PPRequest.TabIndex = 0;
            this.button_PPRequest.Text = "PP Request";
            this.button_PPRequest.UseVisualStyleBackColor = true;
            this.button_PPRequest.Click += new System.EventHandler(this.button_PPRequest_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnRequestFmtPPVerificationSend);
            this.groupBox10.Controls.Add(this.txtFMTPPID);
            this.groupBox10.Controls.Add(this.btnRequestFmtPPSend);
            this.groupBox10.Controls.Add(this.btnRequestFmtPPSendWithoutValue);
            this.groupBox10.Controls.Add(this.btnRequestFmtPPChanged);
            this.groupBox10.Controls.Add(this.btnRequestFmtPPRequest);
            this.groupBox10.Location = new System.Drawing.Point(18, 636);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(294, 146);
            this.groupBox10.TabIndex = 11;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Formatted Process Program";
            // 
            // btnRequestFmtPPVerificationSend
            // 
            this.btnRequestFmtPPVerificationSend.Location = new System.Drawing.Point(28, 107);
            this.btnRequestFmtPPVerificationSend.Name = "btnRequestFmtPPVerificationSend";
            this.btnRequestFmtPPVerificationSend.Size = new System.Drawing.Size(110, 23);
            this.btnRequestFmtPPVerificationSend.TabIndex = 11;
            this.btnRequestFmtPPVerificationSend.Text = "Verification Send";
            this.btnRequestFmtPPVerificationSend.UseVisualStyleBackColor = true;
            this.btnRequestFmtPPVerificationSend.Click += new System.EventHandler(this.btnRequestFmtPPVerificationSend_Click);
            // 
            // txtFMTPPID
            // 
            this.txtFMTPPID.Location = new System.Drawing.Point(28, 22);
            this.txtFMTPPID.Name = "txtFMTPPID";
            this.txtFMTPPID.Size = new System.Drawing.Size(110, 21);
            this.txtFMTPPID.TabIndex = 10;
            // 
            // btnRequestFmtPPSend
            // 
            this.btnRequestFmtPPSend.Location = new System.Drawing.Point(157, 78);
            this.btnRequestFmtPPSend.Name = "btnRequestFmtPPSend";
            this.btnRequestFmtPPSend.Size = new System.Drawing.Size(110, 23);
            this.btnRequestFmtPPSend.TabIndex = 3;
            this.btnRequestFmtPPSend.Text = "PP Send";
            this.btnRequestFmtPPSend.UseVisualStyleBackColor = true;
            this.btnRequestFmtPPSend.Click += new System.EventHandler(this.btnRequestFmtPPSend_Click);
            // 
            // btnRequestFmtPPSendWithoutValue
            // 
            this.btnRequestFmtPPSendWithoutValue.Location = new System.Drawing.Point(28, 78);
            this.btnRequestFmtPPSendWithoutValue.Name = "btnRequestFmtPPSendWithoutValue";
            this.btnRequestFmtPPSendWithoutValue.Size = new System.Drawing.Size(110, 23);
            this.btnRequestFmtPPSendWithoutValue.TabIndex = 2;
            this.btnRequestFmtPPSendWithoutValue.Text = "PP Send(Value)";
            this.btnRequestFmtPPSendWithoutValue.UseVisualStyleBackColor = true;
            this.btnRequestFmtPPSendWithoutValue.Click += new System.EventHandler(this.btnRequestFmtPPSendWithoutValue_Click);
            // 
            // btnRequestFmtPPChanged
            // 
            this.btnRequestFmtPPChanged.Location = new System.Drawing.Point(157, 49);
            this.btnRequestFmtPPChanged.Name = "btnRequestFmtPPChanged";
            this.btnRequestFmtPPChanged.Size = new System.Drawing.Size(110, 23);
            this.btnRequestFmtPPChanged.TabIndex = 1;
            this.btnRequestFmtPPChanged.Text = "PP Changed";
            this.btnRequestFmtPPChanged.UseVisualStyleBackColor = true;
            this.btnRequestFmtPPChanged.Click += new System.EventHandler(this.btnRequestFmtPPChanged_Click);
            // 
            // btnRequestFmtPPRequest
            // 
            this.btnRequestFmtPPRequest.Location = new System.Drawing.Point(28, 49);
            this.btnRequestFmtPPRequest.Name = "btnRequestFmtPPRequest";
            this.btnRequestFmtPPRequest.Size = new System.Drawing.Size(110, 23);
            this.btnRequestFmtPPRequest.TabIndex = 0;
            this.btnRequestFmtPPRequest.Text = "PP Request";
            this.btnRequestFmtPPRequest.UseVisualStyleBackColor = true;
            this.btnRequestFmtPPRequest.Click += new System.EventHandler(this.btnRequestFmtPPRequest_Click);
            // 
            // UbisamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 794);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.button_Ubisam_Close);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtLogs);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UbisamForm";
            this.ShowInTaskbar = false;
            this.Text = "UbisamForm";
            this.VisibleChanged += new System.EventHandler(this.UbisamForm_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLogs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_OnlineRemote;
        private System.Windows.Forms.Button button_Offline;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Initlalize;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbbECID;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbbVID;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbbCE;
        private System.Windows.Forms.Button button_OpenUgc;
        private System.Windows.Forms.Button btnListECIDValue;
        private System.Windows.Forms.Button btnSetECIDValue;
        private System.Windows.Forms.TextBox txtECIDValue;
        private System.Windows.Forms.TextBox txtVIDValue;
        private System.Windows.Forms.Button btnSetVIDValue;
        private System.Windows.Forms.Button btnReport2;
        private System.Windows.Forms.Button btnReport1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnProcessingStateChange;
        private System.Windows.Forms.TextBox txtEQPProcessingState;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnClearAlarm;
        private System.Windows.Forms.Button btnSetAlarm;
        private System.Windows.Forms.TextBox txtAlarm;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtTerminalMessage;
        private System.Windows.Forms.Button btnReportTerminalMessage;
        private System.Windows.Forms.TextBox txtTerminalTID;
        private System.Windows.Forms.Button btnSetVariableList;
        private System.Windows.Forms.Button button_Ubisam_Close;
        private System.Windows.Forms.Button btnReportTest;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button_PPChanged;
        private System.Windows.Forms.Button button_PPLoadInquire;
        private System.Windows.Forms.Button button_PPSend;
        private System.Windows.Forms.Button button_PPRequest;
        private System.Windows.Forms.TextBox txtPPID;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox txtFMTPPID;
        private System.Windows.Forms.Button btnRequestFmtPPSend;
        private System.Windows.Forms.Button btnRequestFmtPPSendWithoutValue;
        private System.Windows.Forms.Button btnRequestFmtPPChanged;
        private System.Windows.Forms.Button btnRequestFmtPPRequest;
        private System.Windows.Forms.Button btnRequestFmtPPVerificationSend;
    }
}