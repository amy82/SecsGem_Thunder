﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Data
{
    public class DataManageClass
    {
        public WorkData workData = new WorkData();
        public TaskWork TaskWork = new TaskWork();

        public CMesData mesData = new CMesData();


        //public CEEpromData eepromData = new CEEpromData();
        //public RootModel MesData { get; private set; }
    }
    public class CPath
    {
        //BASE
        public const string BASE_PATH = "D:\\EVMS\\EEPROM_VERIFY";
        
        //CONFIG
        public const string BASE_DATA_PATH = "D:\\EVMS\\EEPROM_VERIFY\\Data";
        //LAON
        public const string MIU_DIR = "D:\\EVMS\\EEPROM_VERIFY\\Initialize";
        //Mes
        public const string BASE_SECSGEM_PATH = "D:\\EVMS\\EEPROM_VERIFY\\SecsGem";
        public const string BASE_UBISAM_PATH = "D:\\EVMS\\EEPROM_VERIFY\\SecsGem\\ugc";
        public const string BASE_RECIPE_PATH = "D:\\EVMS\\EEPROM_VERIFY\\SecsGem\\Recipe";
        public const string BASE_MODEL_PATH = "D:\\EVMS\\EEPROM_VERIFY\\Model";
        public const string BASE_MODEL_DEFAULT_PATH = "D:\\EVMS\\EEPROM_VERIFY\\Model\\DEFAULT_MODEL";


        //LOG
        public const string BASE_LOG_PATH = "D:\\EVMS\\LOG";
        public const string BASE_LOG_CLIENT_PATH = "D:\\EVMS\\LOG\\CLIENT";
        public const string BASE_LOG_ALARM_PATH = "D:\\EVMS\\LOG\\ALARM";
        public const string BASE_LOG_TERMINAL_PATH = "D:\\EVMS\\LOG\\TERMINAL_MSG";


        public const string yamlFilePathModel = "ClientSecGemData.yaml";
        public const string yamlFilePathConfig = "Client_Config.yaml";
        public const string yamlFilePathImage = "imageData.yaml";
        public const string yamlFilePathUgc = "ugcFilePath.yaml";
        public const string yamlFilePathRecipe = "Recipe.yaml";
        public const string yamlFilePathProduct = "products.yaml";
        public const string yamlFilePathUser = "users.yaml";
        public const string yamlFilePathAlarm = "Alarm.yaml";   //ex) Alarm_20250204  하루씩
        public const string yamlFilePathTerminalMsg = "TerminalMsg.yaml";   //ex) TerminalMsg20250204  하루씩


        //
    }
}
