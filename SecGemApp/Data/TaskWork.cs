using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace SecGemApp.Data
{
    public class TaskWork
    {
        public int m_nCurrentStep;
        public int m_nStartStep;
        public int m_nEndStep;

        public string m_szChipID;

        public int bRecv_Lgit_Pp_select;

        public int bRecv_S6F12_Process_State_Change;
        public int bRecv_S6F12_PP_Selected;
        public int bRecv_S6F12_PP_UpLoad_Completed;
        public int bRecv_S6F12_Lot_Processing_Started;
        public int bRecv_S6F12_Lot_Apd;
        public int bRecv_S6F12_Lot_Processing_Completed;
        public int bRecv_S6F12_Lot_Processing_Completed_Ack;

        public int bRecv_S7F25_Formatted_Process_Program;

        public int bRecv_S2F49_PP_UpLoad_Confirm;      //Confirm or Fail
        public int bRecv_S2F49_LG_Lot_Start;	//Start or Id Fail


        public int bRecv_S2F49_LG_EEprom_Data;
        public int bRecv_S2F49_LG_EEprom_Fail;

        public string m_szIdleStartTime;
        public string m_szIdleEndTime;

        public TaskWork()
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
    }
}
