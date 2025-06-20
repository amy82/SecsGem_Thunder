using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Process
{
    public class MultiLotProcess
    {
        
        public MultiLotProcess()
        {

        }
        public void ObjectReport_LotProcess(string productId, List<string> vLotList, string socketNum, List<TcpSocket.EquipmentParameterInfo> parameterInfos)
        {
            if (Globalo.activeTasks.ContainsKey(productId))
            {
                return; // 이미 처리 중이면 무시
            }
            var taskWork = new ParallelTaskWork
            {
                vNChipID = vLotList.Select(item => string.Copy(item)).ToList(),
                CurrentStep = 100,
                m_nStartStep = 100,
                EndStep = 1000,
                selfSocketIp = -1,
                //
                //착공
                //
                bNRecv_Lgit_Pp_select = -1,
                bNRecv_S2F49_LG_Lot_Start = -1,
                bRecv_S6F12_Process_State_Change = -1,
                bRecv_S6F12_PP_Selected = -1,
                bRecv_S7F25_Formatted_Process_Program = -1,
                bRecv_S2F49_PP_UpLoad_Confirm = -1,
                bRecv_S6F12_PP_UpLoad_Completed = -1,
                bRecv_S6F12_Lot_Processing_Started = -1,
                SpecialDataParameter = new List<TcpSocket.EquipmentParameterInfo>(),
                //
                //완공
                //
                bNRecv_S6F12_Lot_Apd = -1,
                bNRecv_S6F12_Lot_Processing_Completed = -1,
                bNRecv_S6F12_Lot_Processing_Completed_Ack = -1,
                vMesMultiApdData = new List<Data.ApdData>(),
                m_nMesMultiFinalResult = 0
            };

            Globalo.activeTasks[productId] = taskWork;
            Globalo.activeTasks[productId].selfSocketIp = int.Parse(socketNum);

            //foreach (var item in parameterInfos)
            //{
            //    Data.ApdData apddata = new Data.ApdData();
            //    apddata.DATANAME = item.Name;
            //    apddata.DATAVALUE = item.Value;

            //    Globalo.activeTasks[productId].vMesMultiApdData.Add(apddata);
            //}

            _ = Task.Run(async () =>
            {
                int nRunTimeOutSec = 60000;
                string szLog = string.Empty;
                int m_dTickCount = 0;
                int nRetStep = taskWork.CurrentStep;

                Console.WriteLine($"Object Task Start - {productId}");

                while (taskWork.CurrentStep < taskWork.EndStep)
                {
                    switch (taskWork.CurrentStep)
                    {
                        case 100:
                            nRunTimeOutSec = Globalo.dataManage.mesData.ConversationTimeoutCount * 1000;

                            if (nRunTimeOutSec < 1)
                            {
                                nRunTimeOutSec = 60 * 1000;
                            }

                            Globalo.dataManage.mesData.m_dProcessState[0] = (int)Ubisam.ePROCESS_STATE_INFO.eINIT;
                            Globalo.dataManage.mesData.m_dProcessState[1] = (int)Ubisam.ePROCESS_STATE_INFO.eIDLE;  //0,1 값이 같으면 진행이 안돼서 추가한듯

                            Globalo.tcpManager.IdleStateChange(false);       //ObjectIdProcessAsync timer stop
                            nRetStep = 150;
                            break;
                        case 150:
                            //Object id report
                            Globalo.activeTasks[productId].bNRecv_Lgit_Pp_select = -1;
                            Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start = -1;
                            //Globalo.activeTasks[productId].bRecv_S2F49_LG_EEprom_Data = -1;        //jump해서 미리 초기화해야된다.
                            //Globalo.activeTasks[productId].bRecv_S2F49_LG_EEprom_Fail = -1;


                            szLog = $"[LOT]{productId} Object Id Report Send [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);

                            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.OBJECT_ID_REPORT_10701);

                            m_dTickCount = Environment.TickCount;
                            nRetStep = 200;
                            break;
                        case 200:
                            if (Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start == 0)
                            {
                                szLog = $"[LOT]{productId} Lot Id Start Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                nRetStep = 700;     //jump Step
                            }
                            else if (Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start == 1)
                            {
                                Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start = -1;
                                //Recv LGIT_LOT_ID_FAIL

                                szLog = $"[LOT]{productId} Lot Id Fail Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);
                                nRetStep = -1;      //X X X X X
                            }
                            else if (Globalo.activeTasks[productId].bNRecv_Lgit_Pp_select == 0)
                            {
                                szLog = $"[LOT]{productId} Lgit PP Select Recv OK [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.activeTasks[productId].bNRecv_Lgit_Pp_select = -1;

                                nRetStep = 250;
                            }
                            else if (Globalo.activeTasks[productId].bNRecv_Lgit_Pp_select == 1)
                            {
                                //LGIT_PP_SELECT 가 왔는데, 사용중인 레시피 명과 다를 경우
                                //eeprom 쪽에서 팝업 띄운다 250325 확인 완료
                                szLog = $"[LOT]{productId} Lgit PP Select Recipe Compare Fail [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);
                                //
                                nRetStep = -1;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "89";
                                ProcessComData.ErrText = "[LOT]{productId} LGIT PP SELECT CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);


                                szLog = $"[LOT]{productId} LGIT PP SELECT CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10701");
                                nRetStep = -1;
                            }
                            break;
                        case 250:
                            Globalo.dataManage.mesData.m_dProcessState[0] = Globalo.dataManage.mesData.m_dProcessState[1];
                            Globalo.dataManage.mesData.m_dProcessState[1] = (int)Ubisam.ePROCESS_STATE_INFO.eSETUP;

                            Globalo.activeTasks[productId].bRecv_S6F12_Process_State_Change = -1;

                            szLog = $"[LOT] (Setup) Process State Change Report [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);

                            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_STATE_CHANGED_REPORT_10401);    //SEND S6F11

                            m_dTickCount = Environment.TickCount;

                            Globalo.tcpManager.SendProcessState("SETUP");
                            nRetStep = 300;
                            break;
                        case 300:
                            if (Globalo.activeTasks[productId].bRecv_S6F12_Process_State_Change == 0)    //SETUP
                            {
                                szLog = $"[LOT] (Setup)Process State Change Send Acknowledge [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.activeTasks[productId].bRecv_S6F12_Process_State_Change = -1;
                                //Recipe Setup Completion
                                //SV:Recipe ID Set

                                Globalo.dataManage.mesData.m_dProcessState[0] = Globalo.dataManage.mesData.m_dProcessState[1];
                                Globalo.dataManage.mesData.m_dProcessState[1] = (int)Ubisam.ePROCESS_STATE_INFO.eREADY;


                                szLog = $"[LOT] (Ready)Process State Change Report [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_STATE_CHANGED_REPORT_10401);
                                m_dTickCount = Environment.TickCount;
                                Globalo.tcpManager.SendProcessState("READY");
                                nRetStep = 350;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "90";
                                ProcessComData.ErrText = "[LOT] (Setup)Process State Change CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);

                                szLog = $"[LOT] (Setup)Process State Change CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10401");

                                nRetStep = -1;
                            }
                            break;
                        case 350:
                            if (Globalo.activeTasks[productId].bRecv_S6F12_Process_State_Change == 0)    //READY
                            {
                                szLog = $"[LOT] (Ready)Process State Change Send Acknowledge [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.activeTasks[productId].bRecv_S6F12_Process_State_Change = -1;
                                Globalo.activeTasks[productId].bRecv_S6F12_PP_Selected = -1;
                                Globalo.activeTasks[productId].bRecv_S7F25_Formatted_Process_Program = -1;       //<--미리 초기화
                                Globalo.activeTasks[productId].bRecv_S2F49_PP_UpLoad_Confirm = -1;
                                Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start = -1;        //미리 초기화


                                szLog = $"[LOT] PP-Selected Report  [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PP_SELECTED_REPORT_10702);//SEND S6F11

                                m_dTickCount = Environment.TickCount;
                                nRetStep = 400;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "91";
                                ProcessComData.ErrText = "[LOT] (Ready)Process State Change CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);


                                szLog = $"[LOT] (Ready)Process State Change CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10401");

                                nRetStep = -1;
                            }
                            break;
                        case 400:
                            if (Globalo.activeTasks[productId].bRecv_S6F12_PP_Selected == 0)     //S6F12 대기
                            {
                                Globalo.activeTasks[productId].bRecv_S6F12_PP_Selected = -1;

                                szLog = $"[LOT] PP-Selected Send Acknowledge [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                m_dTickCount = Environment.TickCount;
                                nRetStep = 450;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "92";
                                ProcessComData.ErrText = "[LOT] PP-Select Report CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);


                                szLog = $"[LOT] PP-Select Report CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10702");

                                nRetStep = -1;
                            }
                            break;
                        case 450:
                            //
                            ////Recipe Parameter Validation EQP 안하면 바로 Lot Start 온다.
                            //
                            m_dTickCount = Environment.TickCount;
                            nRetStep = 500;
                            break;
                        case 500:
                            if (Globalo.activeTasks[productId].bRecv_S7F25_Formatted_Process_Program == 0)       //Ubisam 에서 보내고 0으로 변경
                            {
                                Globalo.activeTasks[productId].bRecv_S7F25_Formatted_Process_Program = -1;
                                szLog = $"[LOT] Formatted Process Program Request [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);


                                m_dTickCount = Environment.TickCount;

                                nRetStep = 550;
                            }
                            else if (Globalo.activeTasks[productId].bRecv_S2F49_PP_UpLoad_Confirm == 1)  //LGIT_PP_UPLOAD_FAIL 확인필요 250112
                            {
                                szLog = $"[LOT] PP Upload Fail Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);
                                nRetStep = -1;
                            }
                            else if (Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start == 0)     //500
                            {
                                Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start = -1;

                                szLog = $"[LOT] Lot Id Start Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);
                                m_dTickCount = Environment.TickCount;
                                nRetStep = 700; //Jump Step
                            }
                            else if (Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start == 1)
                            {
                                Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start = -1;

                                szLog = $"[LOT] Lot Id Fail Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                nRetStep = -1;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "93";
                                ProcessComData.ErrText = "[LOT] Formatted Process Program CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);

                                szLog = $"[LOT] Formatted Process Program CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S07F25", "10702");

                                nRetStep = -1;
                            }
                            break;
                        case 550:
                            if (Globalo.activeTasks[productId].bRecv_S2F49_PP_UpLoad_Confirm == 0)   //LGIT_PP_UPLOAD_CONFIRM
                            {
                                szLog = $"[LOT] PP Upload Confirm Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                //LGIT_PP_UPLOAD_CONFIRM 오고 S2F50보낸뒤 , 정상 진행
                                Globalo.activeTasks[productId].bRecv_S2F49_PP_UpLoad_Confirm = -1;
                                Globalo.activeTasks[productId].bRecv_S6F12_PP_UpLoad_Completed = -1;
                                Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start = -1;      //미리 초기화

                                szLog = $"[LOT] PP Upload Completed Report [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PP_UPLOAD_COMPLETED_REPORT_10703);//SEND S6F11
                                m_dTickCount = Environment.TickCount;

                                nRetStep = 600;
                            }
                            else if (Globalo.activeTasks[productId].bRecv_S2F49_PP_UpLoad_Confirm == 1)  //LGIT_PP_UPLOAD_FAIL 확인필요 250112
                            {
                                szLog = $"[LOT] PP Upload Fail Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);
                                Globalo.activeTasks[productId].bRecv_S2F49_PP_UpLoad_Confirm = -1;
                                nRetStep = -1;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "94";
                                ProcessComData.ErrText = "[LOT] PP Upload Confirm CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);

                                szLog = $"[LOT] PP Upload Confirm CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S02F49", "10703");

                                nRetStep = -1;
                            }
                            break;
                        case 600:
                            if (Globalo.activeTasks[productId].bRecv_S6F12_PP_UpLoad_Completed == 0)
                            {
                                Globalo.activeTasks[productId].bRecv_S6F12_PP_UpLoad_Completed = -1;
                                //Recv S6F12
                                szLog = $"[LOT] PP Upload Completed Send acknowledge [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                m_dTickCount = Environment.TickCount;

                                nRetStep = 650;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "95";
                                ProcessComData.ErrText = "[LOT] PP Upload Completed CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);


                                szLog = $"[LOT] PP Upload Completed CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10703");

                                nRetStep = -1;  //OBJECT ID REPORT Step
                                break;
                            }
                            break;
                        case 650:
                            if (Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start == 0)  //650
                            {
                                szLog = $"[LOT] Lgit Lot Start Send acknowledge [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start = -1;

                                //Recv LGIT_LOT_START
                                m_dTickCount = Environment.TickCount;

                                nRetStep = 700;
                                break;
                            }
                            else if (Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start == 1)
                            {
                                szLog = $"[LOT] Lot Id Fail Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                nRetStep = -1;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "96";
                                ProcessComData.ErrText = "[LOT] Lot Start CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);

                                szLog = $"[LOT] Lot Start CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S02F49", "10704");
                                nRetStep = -1;
                            }
                            break;
                        case 700:
                            Globalo.dataManage.mesData.m_dProcessState[0] = Globalo.dataManage.mesData.m_dProcessState[1];
                            Globalo.dataManage.mesData.m_dProcessState[1] = (int)Ubisam.ePROCESS_STATE_INFO.eEXECUTING;

                            Globalo.dataManage.mesData.m_dLotProcessingState = (int)Ubisam.eLOT_PROCESSING_STATE.eWait;

                            szLog = $"[LOT] (Executing)Process State Change Report [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);

                            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_STATE_CHANGED_REPORT_10401);//SEND S6F11
                            m_dTickCount = Environment.TickCount;

                            Globalo.tcpManager.SendProcessState("EXECUTING");
                            nRetStep = 800;
                            break;
                        case 800:
                            m_dTickCount = Environment.TickCount;
                            nRetStep = 850;
                            break;
                        case 850:
                            if (Globalo.activeTasks[productId].bRecv_S6F12_Process_State_Change == 0)
                            {
                                szLog = $"[LOT] (Executing) Process State Change Acknowledge [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.activeTasks[productId].bRecv_S6F12_Process_State_Change = -1;
                                Globalo.activeTasks[productId].bRecv_S6F12_Lot_Processing_Started = -1;
                                Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start = -1;

                                szLog = $"[LOT] Lot Processing Started Report [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.LOT_PROCESSING_STARTED_REPORT_10704);//SEND S6F11 Lot Processing Started Report
                                m_dTickCount = Environment.TickCount;

                                nRetStep = 900;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "98";
                                ProcessComData.ErrText = "[LOT] (Executing)Process State Change CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);


                                szLog = $"[LOT] (Executing) Process State Change CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10401");

                                nRetStep = -1;  //OBJECT ID REPORT Step
                                break;
                            }
                            break;
                        case 900:
                            // Lot Processing Start 받고나서 Lot ID Start , Fail 체크해야되나?
                            //nLotProcessingComplete_ACK  값 확인해서 배출 할때 판단?

                            if (Globalo.activeTasks[productId].bRecv_S6F12_Lot_Processing_Started == 0)  //ack  0일대만 양품 배출해야된다.
                            {
                                szLog = $"[LOT] Lot Processing Started Acknowledge,Ack: {Globalo.activeTasks[productId].bRecv_S6F12_Lot_Processing_Started} [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                //ack 0 확인하고 진행하면되는지?
                                //아니면 LGIT_LOT_ID_FAIL 또 올 수 있는지 확인 필요
                                nRetStep = 950;
                            }
                            else if (Globalo.activeTasks[productId].bRecv_S6F12_Lot_Processing_Started == 1)   //nack 1일 아닐수도있다.
                            {
                                //ack를 체크해야되나?

                                szLog = $"[LOT] Lot Processing Started Fail Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                nRetStep = -1;
                            }
                            else if (Globalo.activeTasks[productId].bNRecv_S2F49_LG_Lot_Start == 1) //LGIT_LOT_ID_FAIL
                            {
                                szLog = $"[LOT] Lot Id Fail Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);
                                nRetStep = -1;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "99";
                                ProcessComData.ErrText = "[LOT] Lot Processing Start CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);

                                szLog = $"[LOT] Lot Processing Start CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10704");

                                nRetStep = -1;  //OBJECT ID REPORT Step
                                break;
                            }
                            break;
                        case 950:
                            //착공 성공
                            //if (false)    기존 착공 코드
                            //{
                            //    TcpSocket.EquipmentData LotstartData = new TcpSocket.EquipmentData();
                            //    LotstartData.Command = "APS_LOT_START_CMD";
                            //    LotstartData.Judge = 0;
                            //    LotstartData.CommandParameter = Globalo.dataManage.TaskWork.SpecialDataParameter.Select(item => item.DeepCopy()).ToList();
                            //    Globalo.tcpManager.SendMessageToHost(LotstartData);
                            //}

                            TcpSocket.MessageWrapper EqipData = new TcpSocket.MessageWrapper();
                            EqipData.Type = "EquipmentData";
                            TcpSocket.EquipmentData tData = new TcpSocket.EquipmentData();
                            tData.Command = "APS_LOT_START_CMD";
                            tData.CommandParameter = Globalo.activeTasks[productId].SpecialDataParameter.Select(item => item.DeepCopy()).ToList();

                            EqipData.Data = tData;

                            Globalo.tcpManager.SendMessageToTester(EqipData, Globalo.activeTasks[productId].selfSocketIp);

                            szLog = $"[LOT] Lot Processing Start Complete [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);
                            nRetStep = 1000;
                            break;
                        default:
                            //FAIL
                            szLog = $"[LOT] Apd Process Step Error [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);
                            nRetStep = -1;
                            break;
                    }

                    taskWork.CurrentStep = nRetStep;
                    if (taskWork.CurrentStep < 0)
                    {
                        break;
                    }
                    await Task.Delay(50);
                }
                Console.WriteLine($"Object Task Remove - {productId}");
                // 완료되면 제거 (선택 사항)
                Globalo.activeTasks.Remove(productId);
            });
        }
        public void ApdReport_LotProcess(string productId, int nFinal, string socketNum, List<TcpSocket.EquipmentParameterInfo> parameterInfos)
        {
            if (Globalo.activeTasks.ContainsKey(productId))
            {
                return; // 이미 처리 중이면 무시
            }

            var taskWork = new ParallelTaskWork
            {
                //m_szChipID = productId,
                vNChipID = new List<string>(),
                CurrentStep = 1000,
                m_nStartStep = 1000,
                EndStep = 2000,
                selfSocketIp = -1,
                //
                //착공
                //
                bNRecv_Lgit_Pp_select = -1,
                bNRecv_S2F49_LG_Lot_Start = -1,
                bRecv_S6F12_Process_State_Change = -1,
                bRecv_S6F12_PP_Selected = -1,
                bRecv_S7F25_Formatted_Process_Program = -1,
                bRecv_S2F49_PP_UpLoad_Confirm = -1,
                bRecv_S6F12_PP_UpLoad_Completed = -1,
                bRecv_S6F12_Lot_Processing_Started = -1,
                SpecialDataParameter = new List<TcpSocket.EquipmentParameterInfo>(),
                //
                //완공
                //
                bNRecv_S6F12_Lot_Apd = -1,
                bNRecv_S6F12_Lot_Processing_Completed = -1,
                bNRecv_S6F12_Lot_Processing_Completed_Ack = -1,
                vMesMultiApdData = new List<Data.ApdData>(),
                m_nMesMultiFinalResult = 0
            };
            Globalo.activeTasks[productId].vNChipID.Add(productId);
            Globalo.activeTasks[productId] = taskWork;
            Globalo.activeTasks[productId].selfSocketIp = int.Parse(socketNum);
            Globalo.activeTasks[productId].m_nMesMultiFinalResult = nFinal;
            foreach (var item in parameterInfos)
            {
                Data.ApdData apddata = new Data.ApdData();
                apddata.DATANAME = item.Name;
                apddata.DATAVALUE = item.Value;

                Globalo.activeTasks[productId].vMesMultiApdData.Add(apddata);
            }

            _ = Task.Run(async () =>
            {
                int nRunTimeOutSec = 60000;
                string szLog = string.Empty;
                int m_dTickCount = 0;
                int nRetStep = taskWork.CurrentStep;

                Console.WriteLine($"Apd Task Start - {productId}");

                while (taskWork.CurrentStep < taskWork.EndStep)
                {
                    switch (taskWork.CurrentStep)
                    {
                        case 1000:
                            nRunTimeOutSec = Globalo.dataManage.mesData.ConversationTimeoutCount * 1000;

                            if (nRunTimeOutSec < 1)
                            {
                                nRunTimeOutSec = 60 * 1000;
                            }
                            //if (Globalo.dataManage.mesData.vMesApdData.Count < 1)
                            if (Globalo.activeTasks[productId].vMesMultiApdData.Count < 1)
                            {
                                //fail
                                szLog = $"[APD]{productId} Lot APD Data Empty [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);
                                nRetStep = -1;
                                break;
                            }

                            szLog = $"[APD]{productId} Lot APD Data Count: {Globalo.activeTasks[productId].vMesMultiApdData.Count} [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);
                            nRetStep = 1100;
                            break;
                        case 1100:
                            Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Apd = -1;

                            szLog = $"[APD]{productId} Lot APD Report Send [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);

                            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.LOT_APD_REPORT_10711);

                            m_dTickCount = Environment.TickCount;
                            nRetStep = 1200;
                            break;
                        case 1200:
                            if (Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Apd == 0)           //bRecv_S6F12_Lot_Apd
                            {
                                szLog = $"[APD]{productId} Lot APD Send Acknowledge [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Processing_Completed = -1;
                                Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Processing_Completed_Ack = -1;

                                szLog = $"[APD]{productId} Lot Processing Completed Report Send [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.LOT_PROCESSING_COMPLETED_REPORT_10710);

                                m_dTickCount = Environment.TickCount;
                                nRetStep = 1300;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {
                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "100";
                                ProcessComData.ErrText = "[LOT] Lot APD CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);

                                szLog = $"[LOT]{productId} Lot APD CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10711");

                                nRetStep = -1;
                                break;
                            }
                            break;
                        case 1300:
                            if (Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Processing_Completed == 0)
                            {
                                int nack = Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Processing_Completed_Ack;
                                if (nack == 0)
                                {
                                    szLog = $"[LOT]{productId} Lot Processing Completed Acknowledge - {nack} [STEP : {nRetStep}]";
                                    Globalo.LogPrint("LotProcess", szLog);
                                }
                                else
                                {
                                    //UbisamForm.cs 에서 추가  "LOT_PROCESSING_COMPLETE_FAIL"
                                    szLog = $"[LOT]{productId} Lot Processing Completed Fail - {nack}[STEP : {nRetStep}]";
                                    Globalo.LogPrint("LotProcess", szLog);
                                }

                                nRetStep = 1400;
                                break;
                            }
                            else if ((Environment.TickCount - m_dTickCount) > nRunTimeOutSec)
                            {

                                TcpSocket.EquipmentData ProcessComData = new TcpSocket.EquipmentData();
                                ProcessComData.Command = "CT_TIMEOUT";
                                ProcessComData.Judge = 0;
                                ProcessComData.ErrCode = "101";
                                ProcessComData.ErrText = "[LOT] Lot Processing Completed CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);

                                


                                szLog = $"[LOT]{productId} Lot Processing Completed CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10710");

                                nRetStep = -1;
                                break;
                            }
                            break;
                        case 1900:
                            //완공 성공
                            if (false)      // old
                            {
                                //TcpSocket.EquipmentData LotCompleteData = new TcpSocket.EquipmentData();
                                //LotCompleteData.Command = "APS_LOT_COMPLETE_CMD";
                                //LotCompleteData.Judge = Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Processing_Completed_Ack;
                                //Globalo.tcpManager.SendMessageToHost(LotCompleteData);
                            }


                            //
                            //
                            //
                            TcpSocket.MessageWrapper EqipData = new TcpSocket.MessageWrapper();
                            EqipData.Type = "EquipmentData";
                            TcpSocket.EquipmentData tData = new TcpSocket.EquipmentData();
                            tData.Command = "APS_LOT_COMPLETE_CMD";
                            tData.Judge = Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Processing_Completed_Ack;

                            EqipData.Data = tData;

                            Globalo.tcpManager.SendMessageToTester(EqipData, Globalo.activeTasks[productId].selfSocketIp);







                            nRetStep = taskWork.EndStep+99;
                            break;
                        default:
                            break;
                    }

                    taskWork.CurrentStep = nRetStep;
                    if (taskWork.CurrentStep < 0)
                    {
                        break;
                    }
                    await Task.Delay(50);
                }
                Console.WriteLine($"Apd Task Remove - {productId}");
                // 완료되면 제거 (선택 사항)
                Globalo.activeTasks.Remove(productId);
            });
        }
    }
}
