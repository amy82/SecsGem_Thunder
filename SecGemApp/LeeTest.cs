using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecGemApp
{
    public partial class LeeTest : Form
    {
        public LeeTest()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var recipe = new
            {
                NAME = "recipename",
                O_RING = "1",
                CONE = "1",
                KEYTYPE = "C",
                HEIGHT_LH_MIN = 7.6,
                HEIGHT_LH_MAX = 8.0,
                HEIGHT_MH_MIN = 29.62,
                HEIGHT_MH_MAX = 30.42,
                HEIGHT_RH_MIN = 7.6,
                HEIGHT_RH_MAX = 8.0,
                CONCENTRICITY_IN_MIN = 0.3,
                CONCENTRICITY_IN_MAX = 0.3,
                CONCENTRICITY_OUT_MIN = 0.3, 
                CONCENTRICITY_OUT_MAX = 0.3,
                GASKET_MIN = 150,
                GASKET_MAX = 150,
                DENT_MIN = 220,
                DENT_MAX = 220
            };
            string json = JsonConvert.SerializeObject(recipe);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
                var response = await client.PostAsync("http://127.0.0.1:4001/set-recipe", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("레시피 전송 성공!");
                }
                else
                {
                    Console.WriteLine($"전송 실패: 상태 코드 {(int)response.StatusCode} {response.ReasonPhrase}");
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("서버 응답 내용: " + errorContent);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HTTP 요청 중 오류 발생: " + ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("요청 시간이 초과되었습니다: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("알 수 없는 예외: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TcpSocket.MessageWrapper objectData = new TcpSocket.MessageWrapper();
            objectData.Type = "EquipmentData";
            //



            TcpSocket.EquipmentData LotstartData = new TcpSocket.EquipmentData();
            LotstartData.Command = "APS_LOT_START_CMD";
            LotstartData.Judge = 0;
            LotstartData.CommandParameter = Globalo.dataManage.TaskWork.SpecialDataParameter.Select(item => item.DeepCopy()).ToList();


            objectData.Data = LotstartData;
            //LotstartData.CommandParameter = Globalo.dataManage.TaskWork.SpecialDataParameter;
            //TODO: 여기서 Special Data 여기서 보내야된다.
            //
            Globalo.tcpManager.SendMessageToHostNew(objectData);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //apd Test
            string[] apdName = { "LH", "RH", "MH", "Gasket", "KeyType", 
                "CircleDented", "Concentrycity_A", "Concentrycity_D", 
                "Cone", "ORing", "Result", "Barcode", "Socket_Num" };

            Globalo.dataManage.mesData.vMesApdData.Clear();

            for (int i = 0; i < apdName.Length; i++)
            {
                Data.ApdData apddata = new Data.ApdData();
                apddata.DATANAME = apdName[i];
                apddata.DATAVALUE = (0.1 + i).ToString();
                Globalo.dataManage.mesData.vMesApdData.Add(apddata);
            }

            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.LOT_APD_REPORT_10711);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //LOT_PROCESSING_COMPLETED_REPORT
            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.LOT_PROCESSING_COMPLETED_REPORT_10710);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Object Report
            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.OBJECT_ID_REPORT_10701);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int alarmNum = 1000;
            bool alarmLevel = false;

            Globalo.ubisamForm.AlarmSendFn(alarmNum, alarmLevel);
        }
    }
}
