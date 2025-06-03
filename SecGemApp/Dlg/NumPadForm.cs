using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SecGemApp.Dlg
{
    public partial class NumPadForm : Form
    {
        // 이벤트 정의
        private bool m_bInit = false;
        private string m_sVal;
        private string m_sCalcVal;
        
        public string NumPadResult { get; private set; }
        //public double EnteredNumber { get; private set; }
        public NumPadForm(string initValue)//Dlg.TeachingControl teachingControl, 
        {
            InitializeComponent();
            this.CenterToScreen();

            // this.teachingControl = teachingControl;
            this.m_sVal = initValue;// this.teachingControl.LABEL_TEACH_MOVE_VALUE.Text;
            NumberTextBox.Text = this.m_sVal;
            BTN_NUMPAD_0.Click += Num_Button_Click;
            BTN_NUMPAD_1.Click += Num_Button_Click;
            BTN_NUMPAD_2.Click += Num_Button_Click;
            BTN_NUMPAD_3.Click += Num_Button_Click;
            BTN_NUMPAD_4.Click += Num_Button_Click;
            BTN_NUMPAD_5.Click += Num_Button_Click;
            BTN_NUMPAD_6.Click += Num_Button_Click;
            BTN_NUMPAD_7.Click += Num_Button_Click;
            BTN_NUMPAD_8.Click += Num_Button_Click;
            BTN_NUMPAD_9.Click += Num_Button_Click;

            materialButton1.Font = new Font("맑은 고딕", 20, FontStyle.Bold);


            m_sVal = "";
            m_sCalcVal = "";
    }
        private void Num_Button_Click(object sender, EventArgs e)
        {
            // sender를 Button 타입으로 캐스팅
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                if (m_bInit == true)
                {
                    m_sVal = "";
                    m_bInit = false;
                }
                if (m_sVal.Length > 0)
                {
                    int rtn = m_sVal.IndexOf("0", 0, 1);
                    if (rtn > -1)
                    {
                        rtn = m_sVal.IndexOf('.');
                        if (rtn < 0)
                        {
                            return;
                        }
                    }
                }
                // 버튼의 텍스트 가져오기
                string buttonText = clickedButton.Text;
                m_sVal += buttonText;

                NumberTextBox.Text = m_sVal;
            }
        }
        private void NumPadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void BTN_NUMPAD_POINT_Click(object sender, EventArgs e)
        {
            int rtn = m_sVal.IndexOf('.');
            if (rtn < 0)
            {
                m_sVal += ".";
            }
            NumberTextBox.Text = m_sVal;
        }

        private void BTN_NUMPAD_PLUS_Click(object sender, EventArgs e)
        {
            if(m_sVal.Length > 0)
            {
                m_sCalcVal = "+ ";
                m_sCalcVal += m_sVal;

                m_sVal = "";
                NumberTextBox.Text = m_sVal;
                NumberCalcTextBox.Text = m_sCalcVal;
            }
        }

        private void BTN_NUMPAD_EQUALS_Click(object sender, EventArgs e)
        {
            if (m_sCalcVal == null || m_sVal == null)
            {
                return;
            }
            try
            {
                double dCalcValue = 0.0;
                if (m_sCalcVal.Length > 0 && m_sVal.Length > 0)
                {
                    m_sCalcVal = m_sCalcVal.Substring(1);
                    if (m_sCalcVal.IndexOf('-', 0, 1) > 0)
                    {
                        //string result = m_sCalcVal.Replace(m_sCalcVal[0], ' ');
                        dCalcValue = double.Parse(m_sCalcVal) - double.Parse(m_sVal);
                    }
                    else
                    {
                        dCalcValue = double.Parse(m_sCalcVal) + double.Parse(m_sVal);
                    }
                    m_sCalcVal = "";
                    //m_sVal = String.Format("{0:0.000}", dCalcValue);
                    m_sVal = dCalcValue.ToString();
                    NumberTextBox.Text = m_sVal;
                    NumberCalcTextBox.Text = m_sCalcVal;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NumPadForm 처리 중 예외 발생: {ex.Message}");
            }
            
        }

        private void BTN_NUMPAD_MINUS_Click(object sender, EventArgs e)
        {
            if (m_sVal.Length > 0)
            {
                m_sCalcVal = "- ";
                m_sCalcVal += m_sVal;

                m_sVal = "";
                NumberTextBox.Text = m_sVal;
                NumberCalcTextBox.Text = m_sCalcVal;
            }
        }

        private void BTN_NUMPAD_ENTER_Click(object sender, EventArgs e)
        {
            // 입력된 숫자 값을 가져오기
            // double number = 0.0;
            //// this.teachingControl.LABEL_TEACH_MOVE_VALUE.Text = NumberTextBox.Text;
            //NumPadResult = double.Parse(NumberTextBox.Text);
            NumPadResult = NumberTextBox.Text;


            this.DialogResult = DialogResult.OK; // DialogResult 설정
            this.Close();
            //if (double.TryParse(NumberTextBox.Text, out number))
            //{
            //    //EnteredNumber = number;
            //    NumberEntered?.Invoke(this, number, true);
            //}

        }

        private void BTN_NUMPAD_CANCEL_Click(object sender, EventArgs e)
        {
            // 입력된 숫자 값을 가져오기
            this.DialogResult = DialogResult.Cancel; // DialogResult 설정
            this.Close();
        }

        private void BTN_NUMPAD_CLEAR_Click(object sender, EventArgs e)
        {
            m_sVal = "";
            m_sCalcVal = "";

            NumberTextBox.Text = m_sVal;
            NumberCalcTextBox.Text = m_sCalcVal;
        }

        private void BTN_NUMPAD_DEL_Click(object sender, EventArgs e)
        {
            if (m_sVal.Length > 0)
            {
                m_sVal = m_sVal.Substring(0,m_sVal.Length - 1);
                NumberTextBox.Text = m_sVal;
            }
        }
    }
}
