using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbiCom.Net.Structure;

namespace SecGemApp.TcpSocket
{
    // MEMO: 여긴 Mes 와 통신하는 부분  

    //완공시 APD DATA 받아야 된다.
    //ALARM LIST 받아야 된다.
    //
    public enum CMD_POPUP_MESSAGE : int { cpTEMINAL = 1, cpDefault }; //cpOPCALL = 1, cpFmt_PPReq,
    public class EquipmentParameterInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<EquipmentParameterInfo> ChildItem { get; set; } = new List<EquipmentParameterInfo>();

        // 깊은 복사 메서드
        public EquipmentParameterInfo DeepCopy()
        {
            return new EquipmentParameterInfo
            {
                Name = this.Name,
                Value = this.Value,
                ChildItem = this.ChildItem.Select(child => child.DeepCopy()).ToList()
            };
        }
    }

    public class EquipmentData
    {
        public string Command { get; set; } //LGIT_LOT_ID_FAIL
        public string DataID { get; set; }
        public int Judge { get; set; }
        public bool bBuzzer { get; set; }
        public string RecipeID { get; set; }
        public string MaterialID { get; set; }
        public string LotID { get; set; }
        public int CallType { get; set; }
        public string ErrCode { get; set; }
        public string ErrText { get; set; }
        public List<EquipmentParameterInfo> CommandParameter { get; set; } = new List<EquipmentParameterInfo>();
    }
    public class TesterData         //검사 시작, 검사 종료 , Apd 보고 , AOI(Z축 이동), 
    {
        public string Name { get; set; }
        public string Cmd { get; set; }
        public int Step { get; set; }
        public int result { get; set; }
        public string[] LotId { get; set; }//"LOT20240601"
        public int socketNum { get; set; }    //1,2,3,4로 보내야된다.
        public int[] States { get; set; }       //{ 1, 1, 1, 1}  EEPROM ,AOI는 0번 index만 사용
        //public List<EquipmentParameterInfo> CommandParameter { get; set; } = new List<EquipmentParameterInfo>();

        public List<TcpSocket.EquipmentParameterInfo>[] specialData { get; set; }
        public TesterData()
        {
            States = new int[4];
            LotId = new string[4];
            specialData = new List<TcpSocket.EquipmentParameterInfo>[4];

            for (int i = 0; i < 4; i++)
            {
                LotId[i] = string.Empty;
                States[i] = -1;
                specialData[i] = new List<EquipmentParameterInfo>();
            }

        }
        //TESTER  --> H /  REQ_APD_REPORT,
        //HANDLER --> T /  RESP_APD_REPORT,
        //TESTER  --> H /  CMD_Z_MOVE_STEP1, CMD_Z_MOVE_STEP2,
        //HANDLER --> T /  CMD_TEST_STEP1, CMD_TEST_STEP2,
    }
    public class MessageWrapper
    {
        public string Type { get; set; }        //공정명: EEPROM_WRITE, EEPROM_VERIFY, AOI, FW
        public object Data { get; set; }        //Data 안에 EquipmentData 또는 SocketTestState 추가해서 전달
    }

    //
}
