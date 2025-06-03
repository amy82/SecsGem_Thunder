using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.EquipCode
{
    public class MesEEpromCsvData      //15개
    {
        public string SHOPID { get; set; }  //0
        public string PRODID { get; set; }  //1
        public string PROCID { get; set; }  //2
        public string EEP_ITEM { get; set; }  //3
        public int ADDRESS { get; set; }  //4
        public int DATA_SIZE { get; set; }  //5
        public string DATA_FORMAT { get; set; }  //6
        public string BYTE_ORDER { get; set; }  //7
        public string FIX_YN { get; set; }  //8
        public string ITEM_CODE { get; set; }  //9
        public string ITEM_VALUE { get; set; }  //10
        public string CRC_START { get; set; }  //11
        public string CRC_END { get; set; }  //12
        public string PAD_VALUE { get; set; }  //13
        public string PAD_POSITION { get; set; }  //14

        public MesEEpromCsvData()
        {
            SHOPID = "LGIT AM Paju";
            PRODID = "Ford CE1";
            PROCID = "Verify";
            EEP_ITEM = "ITEM";
            ADDRESS = 0;
            DATA_SIZE = 0;
            DATA_FORMAT = "";
            BYTE_ORDER = "";
            FIX_YN = "";
            ITEM_CODE = "";
            ITEM_VALUE = "";
            CRC_START = "";
            CRC_END = "";
            PAD_VALUE = "";
            PAD_POSITION = "";
        }
    }
}
