using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Process
{
    public class ParallelTaskWork
    {
        //public string m_szChipID { get; set; }
        public List<string> vNChipID { get; set; }
        public bool bRunning { get; set; }
        public int CurrentStep { get; set; }
        public int m_nStartStep { get; set; }
        public int EndStep { get; set; }
        public int selfSocketIp { get; set; }
        //----------------------------------------------------------
        //
        //착공 변수
        //
        //
        public int bNRecv_Lgit_Pp_select { get; set; }
        public int bNRecv_S2F49_LG_Lot_Start { get; set; }
        public int bNRecv_S6F12_Process_State_Change { get; set; }
        public int bNRecv_S6F12_PP_Selected { get; set; }
        public int bNRecv_S7F25_Formatted_Process_Program { get; set; }
        public int bNRecv_S2F49_PP_UpLoad_Confirm { get; set; }
        public int bNRecv_S6F12_PP_UpLoad_Completed { get; set; }
        public int bNRecv_S6F12_Lot_Processing_Started { get; set; }

        public List<TcpSocket.EquipmentParameterInfo> SpecialDataParameter { get; set; }
        
    }

    public class ApdParallelTaskWork
    {
        //----------------------------------------------------------
        //
        //완공 변수
        //
        //
        public string m_szNChipID { get; set; }
        public bool bRunning { get; set; }
        public int CurrentStep { get; set; }
        public int m_nStartStep { get; set; }
        public int EndStep { get; set; }
        public int selfSocketIp { get; set; }
        public int bNRecv_S6F12_Lot_Apd { get; set; }
        public int bNRecv_S6F12_Lot_Processing_Completed { get; set; }
        public int bNRecv_S6F12_Lot_Processing_Completed_Ack { get; set; }

        public int m_nMesMultiFinalResult { get; set; }
        public List<Data.ApdData> vMesMultiApdData { get; set; }
    }
}
