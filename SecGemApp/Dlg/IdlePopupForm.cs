using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecGemApp.Dlg
{
    public partial class IdlePopupForm : Form
    {
        //private List<string> idleList;
        private Dictionary<string, string> idleList;
        public IdlePopupForm(Dictionary<string, string> _IdleList)//List<string> _IdleList)
        {
            InitializeComponent();

            this.TopMost = true;
            this.CenterToScreen();
            this.Shown += (s, e) =>
            {
                this.Activate();
                this.BringToFront();
            };
            idleList = _IdleList;
            if (idleList.Count < 1)
            {
                return;
            }
            //comboBox_IdleList
            //textBox_Note
                string comboStr = "";
            foreach (var item in idleList)
            {
                comboStr = $"[{item.Key}] {item.Value}";

                comboBox_IdleList.Items.Add(comboStr); // 개별 추가
            }
            comboBox_IdleList.SelectedIndex = 0; // 첫 번째 항목 선택

            // DrawItem 이벤트 핸들러 연결 (항목을 직접 그리는 부분)
            comboBox_IdleList.DrawItem += DrawItem;
        }
        private void DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground(); // 기본 배경 색상 설정
            using (Brush brush = new SolidBrush(e.ForeColor))
            using (StringFormat sf = new StringFormat())
            {
                sf.LineAlignment = StringAlignment.Center; // 세로 가운데 정렬
                sf.Alignment = StringAlignment.Center; // 가로 가운데 정렬

                Font customFont = new Font("나눔고딕", 12, FontStyle.Bold); // 원하는 폰트 지정

                // 텍스트 그리기 (Bounds: 항목의 전체 영역)
                e.Graphics.DrawString(comboBox_IdleList.Items[e.Index].ToString(),
                                      customFont,
                                      brush,
                                      e.Bounds,
                                      sf);
            }
            e.DrawFocusRectangle(); // 포커스 표시
        }

        private void BTN_IDLE_SEND_Click(object sender, EventArgs e)
        {
            string selectedText = comboBox_IdleList.SelectedItem?.ToString(); // 선택된 값 가져오기
            int selectedIndex = comboBox_IdleList.SelectedIndex; // 선택된 인덱스

            string noteText = textBox_Note.Text;

            
            try
            {
                if (selectedText == null)
                {
                    this.Close();
                    return;
                }
                if (selectedText.Length < 0)
                {
                    this.Close();
                    return;
                }
                TcpSocket.EquipmentData IdleEqipData = new TcpSocket.EquipmentData();

                IdleEqipData.CommandParameter = new List<TcpSocket.EquipmentParameterInfo>();

                //string pattern = @"\[(.*?)\] (.*)";
                string pattern = @"\[(.*?)\]"; // 대괄호 안의 값만 추출하는 패턴
                Match match = Regex.Match(selectedText, pattern);
                string insideBrackets = "";
                if (match.Success)
                {
                    insideBrackets = match.Groups[1].Value;  // 대괄호 안의 값
                }

                //순서
                ////IDLECODE , IDLETEXT , IDLESTARTTIME , IDLEENDTIME , IDLENOTE
                IdleEqipData.CommandParameter.Add(new TcpSocket.EquipmentParameterInfo
                {
                    Name = "IDLECODE",
                    Value = insideBrackets
                });
                IdleEqipData.CommandParameter.Add(new TcpSocket.EquipmentParameterInfo
                {
                    Name = "IDLETEXT",
                    Value = idleList[insideBrackets]
                });
                IdleEqipData.CommandParameter.Add(new TcpSocket.EquipmentParameterInfo
                {
                    Name = "IDLENOTE",
                    Value = noteText
                });

                Globalo.tcpManager.IdleReport(IdleEqipData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BTN_IDLE_SEND_Click 처리 중 예외 발생: {ex.Message}");
            }
            

            this.Close();
        }

        private void IdlePopupForm_VisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
