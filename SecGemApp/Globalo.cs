using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace SecGemApp
{
    public class Globalo
    {
        public enum eMessageName : int
        {
            M_INFO = 0, M_ASK, M_WARNING, M_ERROR, M_VIEW, M_OP_CALL, M_MATERIAL_ID_FAIL, M_TERMINAL_MSG, M_NULL
        };

        public static MainForm MainForm;
        public static ModelControl modelControl;
        public static RecipeControl recipeControl;
        public static SecsGemStatusControl secsGemStatusControl;
        public static ReportControl reportControl;
        public static ConfigControl configControl;
        public static LogControl logControl;
        public static ThreadControl threadControl;



        public static TcpSocket.TcpManager tcpManager;// = new TcpSocket.TcpManager();
        public static Data.DataManageClass dataManage = new Data.DataManageClass();
        public static Data.YamlManager yamlManager = new Data.YamlManager();
        public static Data.TaskWork taskWork = new Data.TaskWork();
        public static Ubisam.UbisamForm ubisamForm;
        public static EquipCode.EEpromVerifyEquip eepromVerifyEquip = new EquipCode.EEpromVerifyEquip();
        public static Dlg.TerminalMsgForm terminalMsgForm = new Dlg.TerminalMsgForm();       //terminal message 로드 
        public static Http.HttpService httpService = new Http.HttpService();



        public static bool ENGINEER_MODE = false;      //엔지니어 모드 상태
        public static bool HOST_CONNECTED = false;      //MES 와 연결상태
        public static bool EQUIP_CONNECTED = false;     //검사 설비와 연결 상태

        public static Color DefaultColor = Color.FromArgb(34, 34, 34);
        public static Color DisconnectColor = Color.DarkRed; 
        public static Color ConnectColor = Color.Green;
        public static Color BtnOnColor = Color.FromArgb(34, 34, 34);
        public static Color BtnOffColor = Color.Transparent;

        public const string MODE_PASSWORD = "1234";

        public const int MAX_PATH = 256;
        public const int CHART_ROI_COUNT = 9;
        public const int MTF_ROI_COUNT = 20;

        public const int BASE_THREAD_INTERVAL = 10;
        public static int TerminalMessageDialog = 0;

        //Color.WhiteSmoke
        public static void MessageShowPopup(string LogDesc, Globalo.eMessageName bPopUpView = Globalo.eMessageName.M_NULL)
        {
            if (Globalo.MainForm.InvokeRequired)
            {
                Globalo.MainForm.Invoke(new Action(() => MessageShowPopup(LogDesc, bPopUpView)));
            }
            else
            {
                MessagePopUpForm messagePopUp = new MessagePopUpForm();
                messagePopUp.MessageSet(bPopUpView, LogDesc);
                messagePopUp.Show();
            }
        }
        public static DialogResult MessageAskPopup(string LogDesc)
        {
            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");
            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, LogDesc);

            //Globalo.serialPortManager.Barcode.BarcodeScanned += messagePopUp3.OnDialogCloseEvent;


            DialogResult result = messagePopUp3.ShowDialog();

            
            return result;
        }
        public static void LogPrint(object oSender, string LogDesc, Globalo.eMessageName bPopUpView = Globalo.eMessageName.M_NULL)
        {
            DateTime dTime = DateTime.Now;
            string LogInfo = $"[{dTime:hh:mm:ss.f}] {LogDesc}";
            //Globalo.threadControl.logThread.logQueue.Enqueue(LogInfo);
            Globalo.threadControl.LogAdd(LogInfo);
            if (bPopUpView != Globalo.eMessageName.M_NULL)
            {

                MessageShowPopup(LogDesc, bPopUpView);
                //MessagePopUpForm messagePopUp = new MessagePopUpForm();
                //messagePopUp.MessageSet(bPopUpView, LogDesc);
                //messagePopUp.Show();

                if (bPopUpView == Globalo.eMessageName.M_WARNING || bPopUpView == Globalo.eMessageName.M_ERROR)
                {
                    //Globalo.yamlManager.alarmData.Alarms.Add(new Data.Alarm
                    //{
                    //    No = 1,
                    //    Time = dTime.ToString("yyyy-MM-dd HH:mm:ss"),     //"2025-02-04 15:00:00"
                    //    Details = LogDesc
                    //});
                    //Globalo.yamlManager.AlarmSave();
                    //Globalo.mAlarmPanel.RefreshAlarm();
                }
            }
            ///Globalo.yamlManager.alarmData.Alarms.Count; 알람은 여기에 추가하면된다.
        }
        public static bool ShowAskMessageDialog(string LogDesc)
        {
            MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");
            messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, LogDesc);

            Globalo.MainForm.Enabled = false;
            DialogResult result = messagePopUp3.ShowDialog();

            if (result == DialogResult.Yes)
            {
                Globalo.MainForm.Enabled = true;
                return true;
            }
            Globalo.MainForm.Enabled = true;
            return false;
        }
        public static void ShowOpCallMessageDialog(string LogDesc)
        {
            MessagePopUpForm messageForm = new MessagePopUpForm();
            messageForm.MessageSet(Globalo.eMessageName.M_OP_CALL,
                LogDesc, "LGIT_OP_CALL", $"OPCALL CODE: {Globalo.dataManage.mesData.rCtrlOp_Call.OpCall_Code}",
                Globalo.dataManage.mesData.rCtrlOp_Call.CallType);
            //Globalo.MainForm.Enabled = false;
            //messageForm.Show(); // 새로운 창 계속 생성

            messageForm.TopMost = true;
            
            messageForm.Shown += (s, e) =>
            {
                messageForm.Activate();
                messageForm.BringToFront();
            };
            messageForm.ShowDialog();

            Globalo.MainForm.Enabled = true;
        }
        public static void ShowTerminalMessageDialog(string LogDesc)
        {
            MessagePopUpForm messageForm = new MessagePopUpForm();
            TerminalMessageDialog++;
            if (Globalo.MainForm.InvokeRequired)
            {
                Globalo.MainForm.Invoke(new Action(() => MessageShowPopup(LogDesc, Globalo.eMessageName.M_TERMINAL_MSG)));
            }
            else
            {
                MessagePopUpForm messagePopUp = new MessagePopUpForm("", "", "Confirm");
                messagePopUp.MessageSet(Globalo.eMessageName.M_TERMINAL_MSG, LogDesc);
                messagePopUp.TopMost = true;
                messagePopUp.Shown += (s, e) =>
                {
                    messagePopUp.Activate();
                    messagePopUp.BringToFront();
                };
                messagePopUp.Show();
            }


        }
        public static void ShowMaterialMessageDialog(string LogDesc)
        {
            //MessagePopUpForm messageForm = new MessagePopUpForm();
            //LogDesc ="CODE : " 
            //        +Globalo.dataManage.mesData.rMaterial_Id_Fail.Code 
            //        + "\n"
            //        + "TEXT : "
            //        + Globalo.dataManage.mesData.rMaterial_Id_Fail.Text;
            //messageForm.MessageSet(Globalo.eMessageName.M_MATERIAL_ID_FAIL,
            //    LogDesc, "LGIT_MATERIAL_ID_FAIL", $"MATERIAL ID: {Globalo.dataManage.mesData.rMaterial_Id_Fail.MaterialId}");

            //Globalo.MainForm.Enabled = false;     //250305 오류나서 주석처리
            //messageForm.ShowDialog();
            //Globalo.MainForm.Enabled = true;
        }
    }

    public static class ButtonColor
    {
        public static readonly string BTN_ON = "#FFB230";
        public static readonly string BTN_OFF = "#C3A279";
    }
    public static class SecsGemData
    {
        public static readonly string[] LOT_APD_INFO =
        {
            "BARCODE",
            "SENSOR_SERIAL_ID",
            "CURRENT"
        };
        public static readonly string LGIT_CTRLSTATE_CHG_REQ = "LGIT_CTRLSTATE_CHG_REQ";
        public static readonly string LGIT_OP_CALL = "LGIT_OP_CALL";
        public static readonly string LGIT_SETCODE_OFFLINE_REASON = "LGIT_SETCODE_OFFLINE_REASON";
        public static readonly string LGIT_SETCODE_IDLE_REASON = "LGIT_SETCODE_IDLE_REASON";
        public static readonly string LGIT_SETCODE_MATERIAL_EXCHANGE = "LGIT_SETCODE_MATERIAL_EXCHANGE";
        public static readonly string LGIT_SETCODE_MODEL_LIST = "LGIT_SETCODE_MODEL_LIST";
        public static readonly string LGIT_PP_SELECT = "LGIT_PP_SELECT";
        public static readonly string LGIT_LOT_ID_FAIL = "LGIT_LOT_ID_FAIL";
        public static readonly string LGIT_PP_UPLOAD_CONFIRM = "LGIT_PP_UPLOAD_CONFIRM";
        public static readonly string LGIT_PP_UPLOAD_FAIL = "LGIT_PP_UPLOAD_FAIL";
        public static readonly string LGIT_LOT_START = "LGIT_LOT_START";
        public static readonly string LGIT_MATERIAL_ID_CONFIRM = "LGIT_MATERIAL_ID_CONFIRM";
        public static readonly string LGIT_MATERIAL_ID_FAIL = "LGIT_MATERIAL_ID_FAIL";
        public static readonly string LGIT_PAUSE = "LGIT_PAUSE";

        public static readonly string LGIT_EEPROM_DATA = "LGIT_EEPROM_DATA";
        public static readonly string LGIT_EEPROM_FAIL = "LGIT_EEPROM_FAIL";



    }
}
