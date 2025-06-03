using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecGemApp
{
    public partial class MessagePopUpForm : Form
    {
        public string Text1 = "";
        public string Text2 = "";
        public string Text3 = "";

        private int nBuzzerOnType = 0;

        Globalo.eMessageName nMessageYype;

        public int Result { get; private set; }
        public MessagePopUpForm(string BtnLeftName = "", string BtnMidName = "", string BtnRightName = "확인")
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            this.TopMost = true;
            this.CenterToScreen();
 
            this.Shown += (s, e) =>
            {
                this.Activate();
                this.BringToFront();
            };
            BTN_MESSAGE1.Text = BtnRightName;
            BTN_MESSAGE2.Text = BtnMidName;
            BTN_MESSAGE3.Text = BtnLeftName;

            BTN_MESSAGE1.Visible = false;
            BTN_MESSAGE3.Visible = false;
            BTN_MESSAGE2.Visible = false;

            MessageBody.Text = "";
            nMessageYype = Globalo.eMessageName.M_INFO;



        }
        public void MessageSet(Globalo.eMessageName ntype, string sBodyMsg, string sTitleTxt = "", string sTopTxt = "", int option1 = 0)
        {
            nMessageYype = ntype;

            MessageBody.Text = sBodyMsg;
            nBuzzerOnType = option1;
            //
            if (nMessageYype == Globalo.eMessageName.M_INFO)
            {
                this.Text = "INFO";
                //warnningImage.Image = Properties.Resources.info;
                labelTop.Text = "INFO";
                this.BackColor = Color.DodgerBlue;
                //
                BTN_MESSAGE1.Visible = true;    //#1 Button
            }
            else if(nMessageYype == Globalo.eMessageName.M_ASK)
            {
                this.Text = "ASK";
                //warnningImage.Image = Properties.Resources.question;
                labelTop.Text = "ASK";
                this.BackColor = Color.MediumAquamarine;

                BTN_MESSAGE1.Visible = true;    //#1 Button
                BTN_MESSAGE2.Visible = true;    //#2 Button

                BTN_MESSAGE2.Click += YesMessage;
                BTN_MESSAGE1.Click += CloseMessage;
            }
            else if (nMessageYype == Globalo.eMessageName.M_WARNING)
            {
                this.Text = "WARNING";
                //warnningImage.Image = Properties.Resources.warning;
                labelTop.Text = "WARNING";
                this.BackColor = Color.YellowGreen;
                //
                BTN_MESSAGE1.Visible = true;    //#1 Button
            }
            else if (nMessageYype == Globalo.eMessageName.M_TERMINAL_MSG)
            {
                this.Text = "Popup";
                warnningImage.Image = Properties.Resources.info;
                labelTop.Text = "TERMINAL MESSAGE";
                this.BackColor = Color.YellowGreen;
                //
                BTN_MESSAGE1.Visible = true;    //#1 Button

                int dOffsetX = 40;
                int dOffsetY = 40;
                int newX = this.Location.X + (dOffsetX * (Globalo.TerminalMessageDialog - 2));      //-2는 좀더 위에서 시작하기위해
                int newY = this.Location.Y + (dOffsetY * (Globalo.TerminalMessageDialog - 2));

                this.Location = new Point(newX, newY);
                //터미널 메시지일때 계단식으로 보여줘야돼서
                //좌표 계산해야된다.
                //Globalo.TerminalMessageDialog
            }
            else if(nMessageYype == Globalo.eMessageName.M_VIEW)
            {
                this.Text = "VIEW";
                //warnningImage.Image = Properties.Resources.info;
                labelTop.Text = "VIEW";
                this.BackColor = Color.YellowGreen;
                //
                BTN_MESSAGE1.Visible = true;    //#1 Button
            }
            else if (nMessageYype == Globalo.eMessageName.M_ERROR)
            {
                this.Text = "ERROR";
                //warnningImage.Image = Properties.Resources.error;
                labelTop.Text = "ERROR";
                this.BackColor = Color.Red;
                //
                BTN_MESSAGE1.Visible = true;    //#1 Button
            }
            else
            {
                //M_CUSTOM
                this.Text = sTitleTxt;
                //warnningImage.Image = Properties.Resources.error;

                labelTop.Text = sTopTxt;       //본문
                this.BackColor = Color.Red;
                //
                BTN_MESSAGE1.Visible = true;    //#1 Button
            }
        }

        private void BTN_MESSAGE1_Click(object sender, EventArgs e) //가장 우측 버튼
        {
            if (nMessageYype == Globalo.eMessageName.M_OP_CALL)
            {

            }
            if (nMessageYype == Globalo.eMessageName.M_TERMINAL_MSG)
            {
                //보내야된다.

                Globalo.tcpManager.TerminalMsgReport(this.MessageBody.Text);
                Globalo.TerminalMessageDialog--;

            }
            ///Globalo.serialPortManager.Barcode.BarcodeScanned -= OnDialogCloseEvent;
            this.Close();
        }

        private void BTN_MESSAGE3_Click(object sender, EventArgs e) //가운데 버튼
        {
            //Globalo.serialPortManager.Barcode.BarcodeScanned -= OnDialogCloseEvent;
        }

        private void BTN_MESSAGE2_Click(object sender, EventArgs e) //가장 좌측 버튼
        {
            //Globalo.serialPortManager.Barcode.BarcodeScanned -= OnDialogCloseEvent;
        }
        private void CloseMessage(object sender, EventArgs e)
        {
            Result = -1;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void ConfirmMessage(object sender, EventArgs e)
        {
            Result = 1;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void YesMessage(object sender, EventArgs e)
        {
            Result = 0;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        public void OnDialogCloseEvent(string barcodeData)
        {
            // this.Invoke((MethodInvoker)(() => this.Close())); // 안전한 UI 스레드 호출
            this.Invoke((MethodInvoker)(() =>
            {
                //Globalo.serialPortManager.Barcode.BarcodeScanned -= OnDialogCloseEvent;
                //MessageBox.Show($"Barcode Scanned: {barcodeData}");
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
        private void labelTop_VisibleChanged(object sender, EventArgs e)
        {
            if (nMessageYype == Globalo.eMessageName.M_OP_CALL)
            {
                if (nBuzzerOnType == 1)
                {
                    //Globalo.dIoControl.Setbuzzer(true, 1);        //eeprom verify 설비에 부저 없음
                    //Buzzer On
                }
            }
                
        }

        private void MessagePopUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           // Globalo.serialPortManager.Barcode.BarcodeScanned -= OnDialogCloseEvent;
        }
    }
}
