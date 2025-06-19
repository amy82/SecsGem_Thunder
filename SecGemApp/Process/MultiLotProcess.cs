using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Process
{
    public class MultiLotProcess
    {
        public Dictionary<string, ParallelTaskWork> activeTasks = new Dictionary<string, ParallelTaskWork>();
        public MultiLotProcess()
        {

        }
        public void ApdReport_LotProcess(string productId)
        {
            if (activeTasks.ContainsKey(productId))
            {
                return; // 이미 처리 중이면 무시
            }

            var taskWork = new ParallelTaskWork
            {
                m_szChipID = productId,
                CurrentStep = 1000,
                m_nStartStep = 1000,
                EndStep = 2000,
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
                bRecv_S6F12_Lot_Apd = -1,
                bRecv_S6F12_Lot_Processing_Completed = -1,
                bRecv_S6F12_Lot_Processing_Completed_Ack = -1
            };

            activeTasks[productId] = taskWork;

            _ = Task.Run(async () =>
            {
                int nRunTimeOutSec = 60000;
                string szLog = string.Empty;
                int m_dTickCount = 0;
                int nRetStep = taskWork.CurrentStep;
                Console.WriteLine($"Task Start - {productId}");

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
                            if (Globalo.dataManage.mesData.vMesApdData.Count < 1)
                            {
                                //fail
                                szLog = $"[APD] Lot APD Data Empty [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);
                                nRetStep = -1;
                                break;
                            }

                            szLog = $"[APD] Lot APD Data Count: {Globalo.dataManage.mesData.vMesApdData.Count} [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);
                            nRetStep = 1100;
                            break;
                        case 1100:
                            activeTasks[productId].bRecv_S6F12_Lot_Apd = -1;

                            szLog = $"[APD] Lot APD Report Send [STEP : {nRetStep}]";
                            Globalo.LogPrint("LotProcess", szLog);

                            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.LOT_APD_REPORT_10711);

                            m_dTickCount = Environment.TickCount;
                            nRetStep = 1200;
                            break;
                        case 1200:
                            if (activeTasks[productId].bRecv_S6F12_Lot_Apd == 0)
                            {
                                szLog = $"[APD] Lot APD Send Acknowledge [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                activeTasks[productId].bRecv_S6F12_Lot_Processing_Completed = -1;
                                activeTasks[productId].bRecv_S6F12_Lot_Processing_Completed_Ack = -1;

                                szLog = $"[APD] Lot Processing Completed Report Send [STEP : {nRetStep}]";
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

                                szLog = $"[LOT] Lot APD CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10711");

                                nRetStep = -1;
                                break;
                            }
                            break;
                        case 1300:
                            if (activeTasks[productId].bRecv_S6F12_Lot_Processing_Completed == 0)
                            {
                                if (activeTasks[productId].bRecv_S6F12_Lot_Processing_Completed_Ack == 0)
                                {
                                    szLog = $"[LOT] Lot Processing Completed Acknowledge -{activeTasks[productId].bRecv_S6F12_Lot_Processing_Completed_Ack} [STEP : {nRetStep}]";
                                    Globalo.LogPrint("LotProcess", szLog);
                                }
                                else
                                {
                                    //UbisamForm.cs 에서 추가  "LOT_PROCESSING_COMPLETE_FAIL"
                                    szLog = $"[LOT] Lot Processing Completed Fail -{activeTasks[productId].bRecv_S6F12_Lot_Processing_Completed_Ack}[STEP : {nRetStep}]";
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


                                szLog = $"[LOT] Lot Processing Completed CT TimeOut [STEP : {nRetStep}]";
                                Globalo.LogPrint("LotProcess", szLog);

                                Globalo.ubisamForm.cTTimeOutSendFn("S06F12", "10710");

                                nRetStep = -1;
                                break;
                            }
                            break;
                        case 1900:
                            nRetStep = taskWork.EndStep+99;
                            break;
                        default:
                            break;
                    }
                    taskWork.CurrentStep = nRetStep;

                    await Task.Delay(50);
                }
                Console.WriteLine($"Task Remove - {productId}");
                // 완료되면 제거 (선택 사항)
                activeTasks.Remove(productId);
            });
        }
    }
}
