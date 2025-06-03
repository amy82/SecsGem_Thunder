using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Data
{
    //public class YamlTerminalMsgDatacs
    public class TMsg
    {
        public string Time { get; set; }
        public string Message { get; set; }
    }

    public class TerminalMsgData
    {
        public List<TMsg> TMessages { get; set; } = new List<TMsg>();

        public bool tmLoad()
        {
            //Alarm_2025_02_04.yaml

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(CPath.yamlFilePathTerminalMsg); // "Alarm"
            string fileExtension = Path.GetExtension(CPath.yamlFilePathTerminalMsg); // ".yaml"
                                                                               // 현재 날짜를 "yyyy_MM_dd" 형식으로 가져오기
            string currentDate = DateTime.Now.ToString("yyyy_MM_dd");

            string alarmFilePath = $"{fileNameWithoutExtension}_{currentDate}{fileExtension}";

            currentDate = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("D2");
            string filePath = Path.Combine(CPath.BASE_LOG_TERMINAL_PATH, currentDate, alarmFilePath);

            try
            {
                if (!File.Exists(filePath))
                {
                    Globalo.yamlManager.terminalMsgData = new TerminalMsgData();
                    return false;
                }


                Globalo.yamlManager.terminalMsgData = Data.YamlManager.LoadYaml<TerminalMsgData>(filePath);
                if (Globalo.yamlManager.terminalMsgData == null)
                {
                    Globalo.yamlManager.terminalMsgData = new TerminalMsgData();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Alarm: {ex.Message}");
                return false;
            }
        }
        public bool tmSave()
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(CPath.yamlFilePathTerminalMsg);
            string fileExtension = Path.GetExtension(CPath.yamlFilePathTerminalMsg); // ".yaml"
                                                                               // 현재 날짜를 "yyyy_MM_dd" 형식으로 가져오기
            string currentDate = DateTime.Now.ToString("yyyy_MM_dd");

            string alarmFilePath = $"{fileNameWithoutExtension}_{currentDate}{fileExtension}";

            currentDate = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString("D2");
            string filePath = Path.Combine(CPath.BASE_LOG_TERMINAL_PATH, currentDate, alarmFilePath);

            
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath)) // 폴더가 존재하지 않으면
                {
                    Directory.CreateDirectory(directoryPath); // 폴더 생성
                }
                //if (!File.Exists(filePath))       //없으면 생성된다.
                //    return false;

                Data.YamlManager.SaveYaml(filePath, Globalo.yamlManager.terminalMsgData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save Alarm: {ex.Message}");
                return false;
            }
        }
    }



}
