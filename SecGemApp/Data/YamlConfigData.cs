using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Data
{
    public class _DrivingSettings
    {
        public bool IdleReportPass { get; set; }
        public string HandlerIp { get; set; }
        public int HandlerPort { get; set; }
        public int TesterPort { get; set; }
    }
    public class ConfigData
    {
        public _DrivingSettings DrivingSettings { get; set; }
    }


    public class ConfigManager
    {
        public ConfigData configData { get; set; }
        public ConfigManager()
        {

        }

        //public void GetConfigData()
        //{
        //    configData.DrivingSettings.IdleReportPass = Globalo.configControl.checkBox_IdleReportPass.Checked;

        //    string Handlerip = Globalo.configControl.label_Handler_Ip1.Text + "."
        //        + Globalo.configControl.label_Handler_Ip2.Text + "."
        //        + Globalo.configControl.label_Handler_Ip3.Text + "."
        //        + Globalo.configControl.label_Handler_Ip4.Text;


        //    configData.DrivingSettings.HandlerIp = Handlerip;
        //    configData.DrivingSettings.HandlerPort = int.Parse(Globalo.configControl.label_Handler_Port.Text);
        //    configData.DrivingSettings.TesterPort = int.Parse(Globalo.configControl.label_Tester_Port.Text);

        //}

        //public void ShowConfigData()
        //{
        //    Globalo.configControl.checkBox_IdleReportPass.Checked = configData.DrivingSettings.IdleReportPass;

        //    string Handlerip = configData.DrivingSettings.HandlerIp;
        //    string[] parts = Handlerip.Split('.');

        //    Globalo.configControl.label_Handler_Ip1.Text = parts[0];
        //    Globalo.configControl.label_Handler_Ip2.Text = parts[1];
        //    Globalo.configControl.label_Handler_Ip3.Text = parts[2];
        //    Globalo.configControl.label_Handler_Ip4.Text = parts[3];

        //    Globalo.configControl.label_Handler_Port.Text = configData.DrivingSettings.HandlerPort.ToString();
        //    Globalo.configControl.label_Tester_Port.Text = configData.DrivingSettings.TesterPort.ToString();


        //}

        public bool configDataSave()
        {

            string filePath = Path.Combine(CPath.BASE_ENV_PATH, CPath.yamlFilePathConfig);
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath)) // 폴더가 존재하지 않으면
                {
                    Directory.CreateDirectory(directoryPath); // 폴더 생성
                }
                if (!File.Exists(filePath))
                    return false;

                Data.YamlManager.SaveYaml(filePath, configData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save YAML: {ex.Message}");
                return false;
            }
        }
        public bool configDataLoad()
        {
            string filePath = Path.Combine(CPath.BASE_ENV_PATH, CPath.yamlFilePathConfig);
            try
            {
                if (!File.Exists(filePath))
                    return false;


                configData = Data.YamlManager.LoadYaml<ConfigData>(filePath);
                if (configData == null)
                {
                    configData = new ConfigData();
                    return false;
                }

                ///ShowConfigData();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading YAML: {ex.Message}");
                return false;
            }
        }
    }
}
