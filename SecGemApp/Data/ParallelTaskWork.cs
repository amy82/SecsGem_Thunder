using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Data
{
    public class ParallelTaskWork
    {
        public string m_szChipID { get; set; }

        public int m_nCurrentStep { get; set; }
        public int m_nStartStep { get; set; }
        public int m_nEndStep { get; set; }


        public int bRecv_Lgit_Pp_select { get; set; }

        public int bRecv_S6F12_Process_State_Change { get; set; }
        public int bRecv_S6F12_PP_Selected { get; set; }
        public int bRecv_S6F12_PP_UpLoad_Completed { get; set; }
        public int bRecv_S6F12_Lot_Processing_Started { get; set; }
        public int bRecv_S6F12_Lot_Apd { get; set; }
        public int bRecv_S6F12_Lot_Processing_Completed { get; set; }
        public int bRecv_S6F12_Lot_Processing_Completed_Ack { get; set; }

        public int bRecv_S7F25_Formatted_Process_Program { get; set; }

        public int bRecv_S2F49_PP_UpLoad_Confirm { get; set; }      //Confirm or Fail
        public int bRecv_S2F49_LG_Lot_Start { get; set; }	//Start or Id Fail


        public int bRecv_S2F49_LG_EEprom_Data { get; set; }
        public int bRecv_S2F49_LG_EEprom_Fail { get; set; }

        public string m_szIdleStartTime { get; set; }
        public string m_szIdleEndTime { get; set; }

        public ParallelTaskWork()
        {
            m_szChipID = "";

            bRecv_Lgit_Pp_select = -1;
            bRecv_S6F12_Process_State_Change = -1;
            bRecv_S6F12_PP_Selected = -1;
            bRecv_S7F25_Formatted_Process_Program = -1;
            bRecv_S2F49_PP_UpLoad_Confirm = -1;
            bRecv_S6F12_PP_UpLoad_Completed = -1;
            bRecv_S2F49_LG_Lot_Start = -1;
            bRecv_S6F12_Lot_Processing_Started = -1;
            bRecv_S6F12_Lot_Apd = -1;
            bRecv_S6F12_Lot_Processing_Completed = -1;
            bRecv_S6F12_Lot_Processing_Completed_Ack = -1;

            bRecv_S2F49_LG_EEprom_Data = -1;
            bRecv_S2F49_LG_EEprom_Fail = -1;

            m_nCurrentStep = 0;
            m_nStartStep = 0;
            m_nEndStep = 0;

            m_szIdleStartTime = DateTime.Now.ToString("yyMMddhhmmss");
        }

        /*
         * 
         * parallelTaskWorks
         * 
         // 제품 착공 시작 시
        string chipId = "ABC123";
        parallelTaskWorks[chipId] = new ParallelTaskWork
        {
            ChipID = chipId,
            StartStep = 0,
            EndStep = 100,
            CurrentStep = 0,
        };

        // 이벤트 수신 시
        parallelTaskWorks[chipId].IsPpSelectReceived = true;
        parallelTaskWorks[chipId].CurrentStep = 10;
         
         */
    }
}
