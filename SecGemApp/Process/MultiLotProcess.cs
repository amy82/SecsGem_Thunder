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
        public void ObjectReport_LotProcess(string productId, int nFinal, string socketNum, List<TcpSocket.EquipmentParameterInfo> parameterInfos)
        {
            if (Globalo.activeTasks.ContainsKey(productId))
            {
                return; // 이미 처리 중이면 무시
            }
            var taskWork = new ParallelTaskWork
            {
                m_szChipID = productId,
                CurrentStep = 100,
                m_nStartStep = 100,
                EndStep = 1000,
                selfSocketIp = -1,
                vMesMultiApdData = new List<Data.ApdData>(),
                m_nMesMultiFinalResult = 0,
                //
                //
                bRecv_Lgit_Pp_select = -1,
                bRecv_S2F49_LG_Lot_Start = -1,
                bRecv_S6F12_Process_State_Change = -1,
                bRecv_S6F12_PP_Selected = -1,
                bRecv_S7F25_Formatted_Process_Program = -1,
                bRecv_S2F49_PP_UpLoad_Confirm = -1,
                bRecv_S6F12_PP_UpLoad_Completed = -1,
                bRecv_S6F12_Lot_Processing_Started = -1,
                //
                //
                bNRecv_S6F12_Lot_Apd = -1,
                bNRecv_S6F12_Lot_Processing_Completed = -1,
                bNRecv_S6F12_Lot_Processing_Completed_Ack = -1
            };

            Globalo.activeTasks[productId] = taskWork;
            Globalo.activeTasks[productId].selfSocketIp = int.Parse(socketNum);
            Globalo.activeTasks[productId].m_nMesMultiFinalResult = nFinal;

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
                            break;
                        case 150:
                            //Object id report
                            Globalo.activeTasks[productId].bRecv_Lgit_Pp_select = -1;
                            Globalo.activeTasks[productId].bRecv_S2F49_LG_Lot_Start = -1;
                            //Globalo.activeTasks[productId].bRecv_S2F49_LG_EEprom_Data = -1;        //jump해서 미리 초기화해야된다.
                            //Globalo.activeTasks[productId].bRecv_S2F49_LG_EEprom_Fail = -1;


                            szLog = $"[LOT]{productId} Object Id Report Send [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);

                            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.OBJECT_ID_REPORT_10701);

                            m_dTickCount = Environment.TickCount;
                            nRetStep = 200;
                            break;
                        case 200:
                            if (Globalo.activeTasks[productId].bRecv_S2F49_LG_Lot_Start == 0)
                            {
                                szLog = $"[LOT] Lot Id Start Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                nRetStep = 700;     //jump Step
                            }
                            else if (Globalo.activeTasks[productId].bRecv_S2F49_LG_Lot_Start == 1)
                            {
                                Globalo.activeTasks[productId].bRecv_S2F49_LG_Lot_Start = -1;
                                //Recv LGIT_LOT_ID_FAIL

                                szLog = $"[LOT] Lot Id Fail Recv [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);
                                nRetStep = -1;      //X X X X X
                            }
                            else if (Globalo.activeTasks[productId].bRecv_Lgit_Pp_select == 0)
                            {
                                szLog = $"[LOT] Lgit PP Select Recv OK [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.activeTasks[productId].bRecv_Lgit_Pp_select = -1;

                                nRetStep = 250;
                            }
                            else if (Globalo.activeTasks[productId].bRecv_Lgit_Pp_select == 1)
                            {
                                //LGIT_PP_SELECT 가 왔는데, 사용중인 레시피 명과 다를 경우
                                //eeprom 쪽에서 팝업 띄운다 250325 확인 완료
                                szLog = $"[LOT] Lgit PP Select Recipe Compare Fail [STEP : {nRetStep}]";
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
                                ProcessComData.ErrText = "[LOT] LGIT PP SELECT CT TimeOut";
                                Globalo.tcpManager.SendMessageToHost(ProcessComData);


                                szLog = $"[LOT] LGIT PP SELECT CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10701");
                                nRetStep = -1;
                            }
                            break;
                        case 250:

                            break;
                        case 300:

                            break;
                        case 350:

                            break;
                        case 400:

                            break;
                        case 450:

                            break;
                        case 500:

                            break;
                        case 550:

                            break;
                        case 600:

                            break;
                        case 650:

                            break;
                        case 700:

                            break;
                        case 800:

                            break;
                        case 850:

                            break;
                        case 900:

                            break;
                        case 950:
                            //착공 성공

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
                m_szChipID = productId,
                CurrentStep = 1000,
                m_nStartStep = 1000,
                EndStep = 2000,
                selfSocketIp = -1,
                vMesMultiApdData = new List<Data.ApdData>(),
                m_nMesMultiFinalResult = 0,
                //
                //착공
                //
                bRecv_Lgit_Pp_select = -1,
                bRecv_S2F49_LG_Lot_Start = -1,
                bRecv_S6F12_Process_State_Change = -1,
                bRecv_S6F12_PP_Selected = -1,
                bRecv_S7F25_Formatted_Process_Program = -1,
                bRecv_S2F49_PP_UpLoad_Confirm = -1,
                bRecv_S6F12_PP_UpLoad_Completed = -1,
                bRecv_S6F12_Lot_Processing_Started = -1,
                //
                //완공
                //
                bNRecv_S6F12_Lot_Apd = -1,
                bNRecv_S6F12_Lot_Processing_Completed = -1,
                bNRecv_S6F12_Lot_Processing_Completed_Ack = -1
            };

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
