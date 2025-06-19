using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Process
{
    public class ParallelTaskWork
    {
        public string m_szChipID { get; set; }

        public int CurrentStep { get; set; }
        public int m_nStartStep { get; set; }
        public int EndStep { get; set; }

        //----------------------------------------------------------
        //
        //착공 변수
        //
        //
        public int bRecv_Lgit_Pp_select { get; set; }
        public int bRecv_S2F49_LG_Lot_Start { get; set; }
        public int bRecv_S6F12_Process_State_Change { get; set; }
        public int bRecv_S6F12_PP_Selected { get; set; }
        public int bRecv_S7F25_Formatted_Process_Program { get; set; }
        public int bRecv_S2F49_PP_UpLoad_Confirm { get; set; }
        public int bRecv_S6F12_PP_UpLoad_Completed { get; set; }
        public int bRecv_S6F12_Lot_Processing_Started { get; set; }

        //----------------------------------------------------------
        //
        //완공 변수
        //
        //
        public int bRecv_S6F12_Lot_Apd { get; set; }
        public int bRecv_S6F12_Lot_Processing_Completed { get; set; }
        public int bRecv_S6F12_Lot_Processing_Completed_Ack { get; set; }

        public ParallelTaskWork()
        {
            Reset(); // 생성 시 초기화
        }
        public void Reset()
        {
            m_szChipID = string.Empty;
            CurrentStep = 0;
            m_nStartStep = 0;
            EndStep = 0;

            bRecv_Lgit_Pp_select = 0;
            bRecv_S2F49_LG_Lot_Start = 0;
            bRecv_S6F12_Process_State_Change = 0;
            bRecv_S6F12_PP_Selected = 0;
            bRecv_S7F25_Formatted_Process_Program = 0;
            bRecv_S2F49_PP_UpLoad_Confirm = 0;
            bRecv_S6F12_PP_UpLoad_Completed = 0;
            bRecv_S6F12_Lot_Processing_Started = 0;

            bRecv_S6F12_Lot_Apd = 0;
            bRecv_S6F12_Lot_Processing_Completed = 0;
            bRecv_S6F12_Lot_Processing_Completed_Ack = 0;
        }
    }
}
