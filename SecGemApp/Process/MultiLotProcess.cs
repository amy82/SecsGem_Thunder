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
        public void ApdReport_LotProcess(string productId, int nFinal, List<TcpSocket.EquipmentParameterInfo> parameterInfos)
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
                vMesMultiApdData = new List<Data.ApdData>(),
                m_nMesMultiFinalResult = 0,
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
                bNRecv_S6F12_Lot_Apd = -1,
                bNRecv_S6F12_Lot_Processing_Completed = -1,
                bNRecv_S6F12_Lot_Processing_Completed_Ack = -1
            };

            Globalo.activeTasks[productId] = taskWork;
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
                            if (Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Apd == 0)
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
                            TcpSocket.EquipmentData LotCompleteData = new TcpSocket.EquipmentData();
                            LotCompleteData.Command = "APS_LOT_COMPLETE_CMD";

                            LotCompleteData.Judge = Globalo.activeTasks[productId].bNRecv_S6F12_Lot_Processing_Completed_Ack;

                            Globalo.tcpManager.SendMessageToHost(LotCompleteData);

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
                Console.WriteLine($"Task Remove - {productId}");
                // 완료되면 제거 (선택 사항)
                Globalo.activeTasks.Remove(productId);
            });
        }
    }
}
