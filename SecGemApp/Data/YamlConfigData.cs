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

        public void GetConfigData()
        {
            configData.DrivingSettings.IdleReportPass = Globalo.configControl.checkBox_IdleReportPass.Checked;

            configData.DrivingSettings.HandlerIp = "";      //label_Handler_Ip1
            configData.DrivingSettings.HandlerPort = 1234;      //label_Handler_Ip1

        }

        public void ShowConfigData()
        {
            Globalo.configControl.checkBox_IdleReportPass.Checked = configData.DrivingSettings.IdleReportPass;
        }

        public bool configDataSave()
        {
            GetConfigData();

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

                ShowConfigData();
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
