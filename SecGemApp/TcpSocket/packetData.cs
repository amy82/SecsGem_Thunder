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
    public class MessageWrapper
    {
        public string Type { get; set; }        //공정명: EEPROM_WRITE, EEPROM_VERIFY, AOI, FW
        public object Data { get; set; }        //Data 안에 EquipmentData 또는 SocketTestState 추가해서 전달
    }

    //
}
