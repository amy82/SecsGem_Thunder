using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReaLTaiizor.Forms;  // ReaLTaiizor에서 제공하는 폼을 사용하기 위해 추가
using ReaLTaiizor.Controls;  // ReaLTaiizor에서 제공하는 컨트롤을 사용하기 위해 추가


//SecGemApp 이 Client 이고 Eol프로그램이 Server 이다.
namespace SecGemApp
{
    public partial class MainForm : Form
    {
        private int DlgGap = 4;
        private int DefaultWidth = 400;
        private int EngineerWidth = 800;

        private int DefaultHeight = 790;
        public MainForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            /// MetroForm 스타일로 변경
            /// 
            Globalo.MainForm = this;
            this.Width = DefaultWidth;
            
            string pgVer = "CLIENT  " + Program._Ver + " Build: " + Program._Build;
            this.Text = pgVer;
            
            Globalo.modelControl = new ModelControl();
            Globalo.recipeControl = new RecipeControl();
            Globalo.secsGemStatusControl = new SecsGemStatusControl();
            Globalo.reportControl = new ReportControl();
            Globalo.configControl = new ConfigControl();
            
            Globalo.logControl = new LogControl();
            Globalo.threadControl = new ThreadControl();

            Globalo.LogPrint("Main", "PG Start");
            Globalo.LogPrint("Main", pgVer);

            Globalo.yamlManager.UgcLoad();      //ugcFilePath.yaml 에 정의된 ugc 파일명 로드
            Globalo.yamlManager.mesManager.MesLoad();
            Globalo.yamlManager.recipeData.RecipeYamlListLoad();           //init
            Globalo.yamlManager.configManager.configDataLoad();

            Globalo.yamlManager.terminalMsgData.tmLoad();


            Globalo.modelControl.SetModelListView();
            Globalo.recipeControl.SetRecipeListView();


            Globalo.ubisamForm = new Ubisam.UbisamForm();
            
            LeftPanel.Controls.Add(Globalo.secsGemStatusControl);
            LeftPanel.Controls.Add(Globalo.recipeControl);
            LeftPanel.Controls.Add(Globalo.modelControl);
            LeftPanel.Controls.Add(Globalo.reportControl);

            Globalo.recipeControl.Location = new Point(0, Globalo.secsGemStatusControl.Location.Y + Globalo.secsGemStatusControl.Height + DlgGap);
            Globalo.modelControl.Location = new Point(0, Globalo.recipeControl.Location.Y + Globalo.recipeControl.Height + DlgGap);
            Globalo.reportControl.Location = new Point(0, Globalo.modelControl.Location.Y + Globalo.modelControl.Height + DlgGap);

            BottomPanel.Controls.Add(Globalo.logControl);
            RightPanel.Controls.Add(Globalo.configControl);
            

            Globalo.threadControl.AllThreadStart();

            //Globalo.tcpManager.AddClient("127.0.0.1", 2001);

            Globalo.tcpManager = new TcpSocket.TcpManager();
            Globalo.tcpManager.SetServer("", Globalo.yamlManager.configManager.configData.DrivingSettings.TesterPort);// 5000);
            Globalo.tcpManager.SetClient(Globalo.yamlManager.configManager.configData.DrivingSettings.HandlerIp, Globalo.yamlManager.configManager.configData.DrivingSettings.HandlerPort);// "127.0.0.1", 2001);
            Globalo.ubisamForm.UbisamUgcLoad();




            //Http.HttpService.Start();
            //Http.HttpService.RecipySend(0);       //test

            

            Globalo.configControl.setUgcPath();
            Globalo.configControl.ShowConfigData();


            // MEMO: ubisam Start는 설비와 연결된후 하는게 좋을듯 사용중인 레시피 ppid 받아와야돼서

            Globalo.yamlManager.recipeData.vPPRecipeSpecEquip = Globalo.yamlManager.recipeData.RecipeLoad(Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName);

            //Globalo.ubisamForm.Show();
            Globalo.secsGemStatusControl.Set_OperatorId(Globalo.dataManage.mesData.m_sMesOperatorID);
            ModeChanged(0);


            //Http.HttpService.ModelAllSend();
            if (Program.TEST_PG_SELECT == TESTER_PG.AOI)
            {
                //Http.HttpService.RecipeSend(1);
                //Http.HttpService.RecipeSend(2);
                Globalo.tcpManager.SendRecipeToTester(1);
                Globalo.tcpManager.SendRecipeToTester(2);
            }
        }
        
        private void sendBtn()
        {
            // 리스트 생성
            List<string> stringList = new List<string>();

            // 두 개의 string 값
            string value1 = "Hello";
            string value2 = "World";

            // value1과 value2를 반복하여 리스트에 추가
            for (int i = 0; i < 100; i++)
            {
                stringList.Add(value1 + value2); // 값 결합
            }


            TcpSocket.EquipmentData data = new TcpSocket.EquipmentData
            {
                //EQPID = "EQP001",
                //EQPNAME = "EquipmentA",
                //RECIPEID = "RCP1001",
                //LotInfoList = new TcpSocket.LotInfoList
                //{
                //    LotInfos = new List<TcpSocket.LotInfo>
                //    {
                //        new TcpSocket.LotInfo { LOTID = "LOT01", CARRIERID = "CARR01", MODULEID = "MOD01", PROCID = "PROC01", PRODID = "PROD01" }
                //    }
                //}
            };
            // 서버로 리스트 전송
            //Globalo.tcpManager.SendMessageToAll(jsonString);
            //Globalo.tcpManager.SendMessageToAll(jsonData);

            Globalo.tcpManager.SendMessageToHost(data);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            sendBtn();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Event.EventManager.RaisePgExit();
            Globalo.ubisamForm.UbisamClose();
            Globalo.tcpManager.DisconnectClient();

            Globalo.threadControl.AllClose();   //log 종료는 마지막으로 실행
        }

        private void button_UgcOpenFile_Click(object sender, EventArgs e)
        {
            
        }
        private void ModeChanged(int mode)
        {
            if(mode == 1)
            {
                Globalo.ENGINEER_MODE = true;
                Globalo.configControl.Enabled = true;
                //menuStrip1.BackColor = Color.Red;
                bigLabel_TopMode.ForeColor = Color.Blue;
                bigLabel_TopMode.Text = "| Engineer";
                this.Width = EngineerWidth;
                bigLabel_TopMode.Location = new Point(this.Width - bigLabel_TopMode.Width - 20, bigLabel_TopMode.Location.Y);
            }
            else
            {
                Globalo.ENGINEER_MODE = false;
                Globalo.configControl.Enabled = false;
                //menuStrip1.BackColor = Color.Transparent;
                bigLabel_TopMode.ForeColor = Color.Lavender;
                bigLabel_TopMode.Text = "| Operator";
                this.Width = DefaultWidth;
                bigLabel_TopMode.Location = new Point(this.Width - bigLabel_TopMode.Width - 20, bigLabel_TopMode.Location.Y);
            }

            this.Height = DefaultHeight;
        }
        private void button_en_Click(object sender, EventArgs e)
        {
            // 비밀번호 확인 (예: "1234" 입력 시만 선택 가능)
            
        }

        private void button_op_Click(object sender, EventArgs e)
        {
            ModeChanged(0);
        }

        private void enginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputForm passwordInputForm = new InputForm("PASSWORD INPUT", "비밀번호를 입력해주세요:", "", 1);
            DialogResult result = passwordInputForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                ModeChanged(1);
            }
            //string password = Microsoft.VisualBasic.Interaction.InputBox("비밀번호를 입력하세요:", "인증 필요", "");
        }

        private void operatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModeChanged(0);

            LeeTest leeTest = new LeeTest();
            leeTest.Show();
        }
    }
}
