using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Forms;

namespace SecGemApp.TcpSocket
{
    public class TcpManager
    {
        private System.Timers.Timer _IdleTimer;

        public TcpServer SecsGemServer;
        private CancellationTokenSource _cts;

        private readonly List<TcpClientHandler> _clients = new List<TcpClientHandler>();
        public TcpClientHandler _client;


        public TcpManager()
        {
            if (Program.NORIN_MODE == true)
            {
                _IdleTimer = new System.Timers.Timer(13000);
                //_IdleTimer.Start();
            }
            else
            {
                _IdleTimer = new System.Timers.Timer(60000 * 5); // 초기값 5분
            }
            

            _IdleTimer.Elapsed += Timer_IdleReason;
            _IdleTimer.AutoReset = false;   //한 번만 실행
            Event.EventManager.PgExitCall += OnPgExitCall;
            _cts = new CancellationTokenSource();
            
        }
        private void OnPgExitCall(object sender, EventArgs e)
        {
            // 이벤트 처리
            Console.WriteLine("TcpManager - OnPgExitCall");
            if (_IdleTimer != null)
            {
                _IdleTimer.Stop();
                _IdleTimer.Dispose();
                _IdleTimer = null;
            }
        }
        private async void serverStart()
        {
            await StartServerAsync();
        }
        // 서버 시작
        public async Task StartServerAsync()
        {
            await SecsGemServer.StartAsync(_cts.Token);
        }
        private async Task TesterClientMessageAsync(string receivedData, int clientIndex)
        {
            //Console.WriteLine($"JSON 데이터 길이: {receivedData.Length}");

            using (StreamReader sr = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(receivedData))))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                var wrapper = serializer.Deserialize<MessageWrapper>(reader);

                switch (wrapper.Type)
                {
                    case "EquipmentData":
                        EquipmentData edata = JsonConvert.DeserializeObject<EquipmentData>(wrapper.Data.ToString());
                        //hostMessageParse(edata);
                        TesterMessageParse(edata, clientIndex);  //Verify 검사에서 들어온다.
                        break;

                    case "TesterData":      
                        TesterData socketState = JsonConvert.DeserializeObject<TesterData>(wrapper.Data.ToString());
                        socketMessageParse(socketState, clientIndex);
                        break;
                }


                try
                {

                    await Task.Delay(10); // 가짜 비동기 작업 (예: DB 저장)
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"hostMessageParse 처리 중 예외 발생: {ex.Message}");
                }
            }
        }
        private void socketMessageParse(TesterData data, int index)        //index = ip뒷자리
        {
            int i = 0;
            int result = -1;

            string dataName = data.Name;
            string cmd = data.Cmd;
            int nStep = data.Step;


        }
        public void SetServer(string ip, int port)
        {
            SecsGemServer = new TcpServer("", port);
            SecsGemServer.OnServerMessageReceivedAsync += TesterClientMessageAsync;
            serverStart();
        }
        public void SetClient(string ip, int port)
        {
            _client = new TcpClientHandler(ip, port, this);
            _client.OnClientMessageReceivedAsync += HandleClientMessageAsync;
            _client.Connect();
        }
        // ✅ 클라이언트가 메시지를 보낼 때 실행될 비동기 함수
        private async Task HandleClientMessageAsync(string receivedData)
        {
            //Console.WriteLine($"JSON 데이터 길이: {receivedData.Length}");

            using (StreamReader sr = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(receivedData))))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                EquipmentData data = serializer.Deserialize<EquipmentData>(reader);

                try
                {
                    clientMessageParse(data);
                    await Task.Delay(10); // 가짜 비동기 작업 (예: DB 저장)
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"hostMessageParse 처리 중 예외 발생: {ex.Message}");
                }

            }
        }

        //Response = RES (응답)
        //Acknowledge = ACK (확인)
        //Answer = ANS (응답)
        //Reply = RPLY (응답)
        public void CmdPopupMessage(TcpSocket.EquipmentData popupData)//string Title, string message1)
        {
            //팝업을 띄워줘야 될 때 사용

            //TcpSocket.EquipmentData popupData = new TcpSocket.EquipmentData();
            //popupData.Command = "APS_POPUP_CMD";      //원래 자기 커맨드로 하자 ex) op call
            //popupData.DataID = nType.ToString();
            //popupData.bBuzzer = bUseBuzzer;
            //
            //popupData.CommandParameter = new List<TcpSocket.EquipmentParameterInfo>();
            //TcpSocket.EquipmentParameterInfo pInfo = new TcpSocket.EquipmentParameterInfo();
            ////
            //pInfo.Name = Title;
            //pInfo.Value = message1;
            //popupData.CommandParameter.Add(pInfo);

            SendMessageToHost(popupData);
        }
        public void CmdUbiGem_State(int index)       //유비젬 연결 상태 검사 pc로 보내기
        {
            TcpSocket.EquipmentData eepData = new TcpSocket.EquipmentData();
            eepData.Command = "APS_DRIVER_CMD";
            eepData.Judge = index;
            SendMessageToHost(eepData);
        }
        public void CmdPPid()       //사용중인 레시피 보내기
        {
            TcpSocket.EquipmentData eepData = new TcpSocket.EquipmentData();
            eepData.DataID = Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName;
            eepData.Command = "APS_RECIPE_CMD";//"APS_PPID_REQ";
            SendMessageToHost(eepData);
        }
        public void SendTerminalMsg(string tid , string Message)
        {
            string szLog = $"Code: [{tid}] \n{Message}";

            Globalo.terminalMsgForm.AddMessage(szLog);
            Globalo.ShowTerminalMessageDialog(szLog);
            //SendMessageToHost(eepData);
        }
        public void SendProcessState(string state)
        {
            TcpSocket.EquipmentData eepData = new TcpSocket.EquipmentData();
            eepData.Command = "APS_PROCESS_STATE_INFO";
            eepData.DataID = state;

            SendMessageToHost(eepData);
        }
        public void SendRecipeName(string ppid)
        {
            TcpSocket.EquipmentData eepData = new TcpSocket.EquipmentData();
            eepData.Command = "APS_RECIPE_CMD";
            eepData.DataID = ppid;

            SendMessageToHost(eepData);
        }
        public void SendModelName(string model)
        {
            TcpSocket.EquipmentData eepData = new TcpSocket.EquipmentData();
            eepData.Command = "APS_MODEL_CMD";
            eepData.DataID = model;

            SendMessageToHost(eepData);
        }

        
        public void TerminalMsgReport(string receivedData)
        {
            //Terminal Confirm 버튼 눌렀을 때 넘어온다.
            //tmData.ErrText  <--- 메시지 넘어온다.
            Globalo.LogPrint("TcpManager", "[Rerpot] Op Recognized Terminal Report");
            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.OP_RECOGNIZED_TERMINAL_REPORT_10901, receivedData);
        }
        public void IdleReport(TcpSocket.EquipmentData receivedData)        //Mes로 idle 보고
        {
            //작업자가 선택한 IDLE REASON
            //IDLECODE , IDLETEXT , IDLENOTE 3가지만 받는다.
            // { "IDLECODE", "IDLETEXT", "IDLESTARTTIME", "IDLEENDTIME", "IDLENOTE" };

            string _code = "";
            string _text = "";
            string _note = "";
            foreach (var item in receivedData.CommandParameter)
            {
                if (item.Name == "IDLECODE")
                {
                    _code = item.Value;
                }
                if (item.Name == "IDLETEXT")
                {
                    _text = item.Value;
                }
                if (item.Name == "IDLENOTE")
                {
                    _note = item.Value;
                }
            }
            Globalo.taskWork.m_szIdleEndTime = DateTime.Now.ToString("yyMMddhhmmss");
            string reportData = _code + "," + _text + "," + Globalo.taskWork.m_szIdleStartTime + "," + Globalo.taskWork.m_szIdleEndTime + "," + _note;

            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.IDLE_REASON_REPORT_10402, reportData);  //Idle Reason Report

            Globalo.taskWork.m_szIdleStartTime = DateTime.Now.ToString("yyMMddhhmmss");

            _IdleTimer.Stop();
            _IdleTimer.Start();
        }

        public void IdleStateChange(bool bRun)   //idle로 상태 변환
        {
            if (bRun)
            {
                if (Globalo.dataManage.mesData.m_dEqupControlState[1] == (int)Ubisam.eCURRENT_CONTROL_STATE.eOnlineRemote &&
                Globalo.dataManage.mesData.m_dProcessState[0] != Globalo.dataManage.mesData.m_dProcessState[1])
                {
                    Globalo.dataManage.mesData.m_dProcessState[0] = Globalo.dataManage.mesData.m_dProcessState[1];
                    Globalo.dataManage.mesData.m_dProcessState[1] = (int)Ubisam.ePROCESS_STATE_INFO.eIDLE;

                    Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_STATE_CHANGED_REPORT_10401);

                    Globalo.LogPrint("MainControl", "[Rerpot] Process State State Changed Report - IDLE");
                    Globalo.taskWork.m_szIdleStartTime = DateTime.Now.ToString("yyMMddhhmmss");

                    _IdleTimer.Stop();

                    if (Globalo.dataManage.mesData.IdleSetTimeInterval < 1)
                    {
                        Globalo.dataManage.mesData.IdleSetTimeInterval = 5;  //min  1min = 60000
                    }
                    _IdleTimer.Interval = 60000 * Globalo.dataManage.mesData.IdleSetTimeInterval; // 초기값 5분
                    if (Program.NORIN_MODE == false)
                    {
                        _IdleTimer.Interval = 10000;

                    }
                        
                    _IdleTimer.Start();         //IDLE_REPORT 받는 곳


                    Globalo.tcpManager.SendProcessState("IDLE");        //eeprom에서 idle보낸거라서 안보내도 될듯
                }
            }
            else
            {
                _IdleTimer.Stop();
            }
            
        }
        private void Timer_IdleReason(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Globalo.yamlManager.configManager.configData.DrivingSettings.IdleReportPass == true)
            {
                return;
            }
            Console.WriteLine("Timer_IdleReason Call!");
            _IdleTimer.Stop();

            //if (g_clModelData[0].m_nIdleReasonPass == 1)  사용중일 때만 보낸다.
            Dictionary<string, string> idleDicList = new Dictionary<string, string>();

            foreach (var item in Globalo.dataManage.mesData.vIdleReason)
            {
                idleDicList.Add(item.CpName, item.CepVal);
            }

            //if (Globalo.MainForm.WindowState == FormWindowState.Minimized)
            //{
            //    Globalo.MainForm.WindowState = FormWindowState.Normal; // 폼을 복원
            //    Globalo.MainForm.BringToFront();
            //}

            Dlg.IdlePopupForm idlePopup = new Dlg.IdlePopupForm(idleDicList);
            idlePopup.ShowDialog();
        }
        public void SendRecipeToTester(int index)
        {
            TcpSocket.MessageWrapper EqipData = new TcpSocket.MessageWrapper();
            EqipData.Type = "EquipmentData";
            TcpSocket.EquipmentData tData = new TcpSocket.EquipmentData();
            tData.Command = "RECV_SECS_RECIPE";
            tData.RecipeID = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.Ppid;
            foreach (var item in Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap)
            {
                TcpSocket.EquipmentParameterInfo pInfo = new TcpSocket.EquipmentParameterInfo();

                pInfo.Name = item.Key;
                pInfo.Value = item.Value.value;
                tData.CommandParameter.Add(pInfo);
            }

            EqipData.Data = tData;

            Globalo.tcpManager.SendMessageToTester(EqipData, index);
        }
        public void SendModelToTester(int index)
        {
            TcpSocket.MessageWrapper EqipData = new TcpSocket.MessageWrapper();
            EqipData.Type = "EquipmentData";

            TcpSocket.EquipmentData tData = new TcpSocket.EquipmentData();
            tData.Command = "RECV_SECS_MODEL";
            tData.DataID = Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentModelName;
            EqipData.Data = tData;

            Globalo.tcpManager.SendMessageToTester(EqipData, index);
        }
        private void TesterMessageParse(EquipmentData data, int testerIp = -1)
        {
            int i = 0;
            int j = 0;
            int result = -1;
            int cnt = data.CommandParameter.Count;
            string logData = $"[Recv] Host Command: {data.Command} [{cnt}]";



            Globalo.LogPrint("TcpManager", logData);

            if (data.Command == "REQ_RECIPE")
            {
                SendRecipeToTester(testerIp);

                
            }
            
            if (data.Command == "REQ_MODEL")
            {
                SendModelToTester(testerIp);
                //
                
            }
        }
        private void clientMessageParse(EquipmentData data)
        {
            int i = 0;
            int j = 0;
            int result = -1;
            int cnt = data.CommandParameter.Count;
            string logData = $"[Recv] Host Command: {data.Command} [{cnt}]";



            Globalo.LogPrint("TcpManager", logData);

            
            if (data.Command == "APS_RECIPE_ACK")    //"APS_PPID_RES")
            {
                //Console.WriteLine($"Recv PPID: {data.DataID}");

                //label_PPId
                //Globalo.recipeControl.setRecipeId(data.DataID);

                //PPID 요청 받으면 모델 명 보내기
                Globalo.tcpManager.SendModelName(Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentModelName);
            }

            if (data.Command == "APS_MODEL_ACK")
            {
                if (Globalo.HOST_CONNECTED == true)
                {
                    //유비젬과 CLIENT는 연결돼있는 상태에서 , 검사 PC가 켜지는 상황
                    Globalo.tcpManager.CmdUbiGem_State(1);      //유비젬 접속된 상태에서 설비 프로그램과 연결 될 수 있어서..
                }
                SendProcessState("INIT");
                //APS_PROCESS_STATE_INFO - 초기 init보내기
            }

            if (data.Command == "APS_RECIPE_DOWN")      //RequestFmtPPRequest
            {
                Globalo.ubisamForm.FormattedProcessProgramRequest(data.DataID);
            }
            if (data.Command == "APS_RECIPE_SAVE")
            {
                if (data.CommandParameter.Count == 2)     //2개 들어와야된다. 
                {
                    Globalo.dataManage.mesData.m_dPPChangeArr[0] = int.Parse(data.CommandParameter[0].Value);
                    Globalo.dataManage.mesData.m_dPPChangeArr[1] = int.Parse(data.CommandParameter[1].Value);

                    Globalo.LogPrint("MainControl", "[Rerpot] Process Program State Changed Report - Created");

                    Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601, data.DataID);//ppRs.RECIPE.Ppid);
                }
                
            }
            if (data.Command == "APS_ALARM_CMD")
            {
                //sendEqipData.ErrCode = "L";H
                //sendEqipData.ErrText = nAlarmID;
                bool alarmLevel = false;
                if (data.ErrCode == "H")
                {
                    alarmLevel = true;
                }
                int alarmNum = int.Parse(data.ErrText);
                Globalo.ubisamForm.AlarmSendFn(alarmNum, alarmLevel);
            }


            if (data.Command == "OBJECT_ID_REPORT")       //착공
            {
                //OBJECT_ID_REPORT 로 FAIL 해도 무조건 OBJECT ID REPORT로 온다.
                //
                Console.WriteLine($"OBJECT_ID_REPORT Recv");
                Globalo.dataManage.TaskWork.m_szChipID = data.LotID;            //OBJECT_ID_REPORT

                if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
                {
                    Console.WriteLine($"OBJECT_ID_REPORT autoRunthread Run");
                    Globalo.threadControl.autoRunthread.Stop();
                    Thread.Sleep(300);
                }

                //g_pCarAABonderDlg->KillTimer(WM_IDLE_REASON_TIMER);		//OBJECT ID REPORT start

                Globalo.taskWork.m_nStartStep = 100;
                Globalo.taskWork.m_nCurrentStep = 100;
                Globalo.taskWork.m_nEndStep = 1000;
                bool rtn = Globalo.threadControl.autoRunthread.Start();


                TcpSocket.EquipmentData ObjectRtn = new TcpSocket.EquipmentData();
                ObjectRtn.Command = "OBJECT_ID_REPORT_ACK";
                if (rtn)
                {
                    ObjectRtn.Judge = 0;//ACK
                }
                else
                {
                    ObjectRtn.Judge = 1;//NAK
                }

                Globalo.tcpManager.SendMessageToHost(ObjectRtn);
            }
            else if (data.Command == "LOT_APD_REPORT")       //From Tester pg, apd 보고
            {
                Console.WriteLine($"LOT_APD_REPORT Recv [{data.CommandParameter.Count}]");
                Globalo.dataManage.mesData.vMesApdData.Clear();
                Globalo.dataManage.TaskWork.m_szChipID = data.LotID;            //LOT_APD_REPORT

                Globalo.dataManage.mesData.m_nMesFinalResult = data.Judge;          //apd 양불 판정때만 1 = 양품 , 0 = 불량

                foreach (var item in data.CommandParameter)
                {
                    Data.ApdData apddata = new Data.ApdData();
                    apddata.DATANAME = item.Name;
                    apddata.DATAVALUE = item.Value;

                    Globalo.dataManage.mesData.vMesApdData.Add(apddata);
                }
                //TODO: APS 1~ N개의 값이 넘어 오면 다 담아야 된다.
                //
                //Globalo.dataManage.mesData.vMesApdData   
                if (Globalo.threadControl.autoRunthread.GetThreadRun() == true)
                {
                    Console.WriteLine($"LOT_APD_REPORT autoRunthread Run");
                    Globalo.threadControl.autoRunthread.Stop();
                }

                Globalo.taskWork.m_nStartStep = 1000;
                Globalo.taskWork.m_nCurrentStep = 1000;
                Globalo.taskWork.m_nEndStep = 2000;
                Globalo.threadControl.autoRunthread.Start();
            }
            
        }
        public async void SendMessageToHost(EquipmentData data)//string message)
        {
            if (_client.bHostConnectedState() == false)
            {
                return;
            }
            string jsonData = JsonConvert.SerializeObject(data);
            await _client.SendDataAsync(jsonData);
        }

        public async void SendMessageToTester(TcpSocket.MessageWrapper equipData, int clintNum = -1)
        {
            if (SecsGemServer.bClientConnectedState(clintNum) == false || clintNum == -1)
            {
                Console.WriteLine($"bClientConnectedState - {clintNum}");
                return;
            }
            string jsonData = JsonConvert.SerializeObject(equipData);
            await SecsGemServer.BroadcastMessageAsync(jsonData, clintNum-1);
        }


        public async void SendMessageToHostNew(MessageWrapper data)//string message)
        {
            if (_client.bHostConnectedState() == false)
            {
                return;
            }
            string jsonData = JsonConvert.SerializeObject(data);
            await _client.SendDataAsync(jsonData);
        }
        public void DisconnectClient()
        {
            _client.Disconnect(false);
        }
        //
        //
        //
        //public void AddClient(string ip, int port)
        //{
        //    var clientHandler = new TcpClientHandler(ip, port, this);

        //    clientHandler.OnClientMessageReceivedAsync += async message =>
        //    {
        //        Console.WriteLine($"Message from {ip}:{port} - {message}");
        //        await Task.CompletedTask; // 비동기 컨텍스트 유지
        //    };
        //    clientHandler.OnDisconnected += () => Console.WriteLine($"Disconnected from {ip}:{port}");

        //    if (clientHandler.Connect())
        //    {
        //        _clients.Add(clientHandler);        //접속이 실패하면 이쪽으로 안들어온다. 
        //    }
        //}
        public async void SendMessageToAll(string message)
        {
            foreach (var client in _clients)
            {
                //client.SendMessage(message);
                await client.SendDataAsync(message);
            }

            //_clients[0].SendMessage
            //await SendDataAsync(ip, port, jsonString);
        }
        public void DisconnectAll()
        {
            foreach (var client in _clients)
            {
                client.Disconnect();
            }
            _clients.Clear();
        }
    }
}
