using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


namespace SecGemApp.Data
{
    public class YamlManager
    {
        //private readonly string _filePath;
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        // 데이터를 보관할 속성
        public ModelListData modelLIstData { get; set; }
        public RecipeData recipeData { get; set; }
        public MesManager mesManager { get; set; }
        public UgcSetFile ugcSetFile { get; private set; }
        public ConfigManager configManager { get; private set; }    //client에서는 사용 안함
        public TerminalMsgData terminalMsgData { get; set; }

        public YamlManager()
        {
            // YAML Serializer & Deserializer 설정
            _serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance) // CamelCase 사용
                .Build();

            _deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            terminalMsgData = new TerminalMsgData();
            modelLIstData = new ModelListData();
            recipeData = new RecipeData();
            mesManager = new MesManager();
            configManager = new ConfigManager();
        }
        
        
        
        
        /// <summary>
        /// YAML 데이터를 불러옵니다.
        /// </summary>
        
       
        public bool UgcLoad()
        {
            string filePath = Path.Combine(CPath.BASE_UBISAM_PATH, "UIConfig", CPath.yamlFilePathUgc);
            try
            {
                if (!File.Exists(filePath))
                    return false;


                //Path.Combine(CPath.BASE_UBISAM_PATH, "UIConfig", CPath.yamlFilePathUgc)
                ugcSetFile = LoadYaml<UgcSetFile>(filePath);
                if (ugcSetFile == null)
                {
                    Globalo.LogPrint("Data", $"[Ugc] File Load Fail");
                    return false;
                }
                //ugcSetFile.ugcFilePath = Path.Combine(CPath.BASE_UBISAM_PATH, ugcSetFile.ugcFilePath);
                string logData = $"[Ugc] {ugcSetFile.ugcFilePath} Load";
                Globalo.LogPrint("Data", logData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading YAML: {ex.Message}");
                return false;
            }
        }
        public bool UgcSave()
        {
            string filePath = Path.Combine(CPath.BASE_UBISAM_PATH, "UIConfig", CPath.yamlFilePathUgc);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                SaveYaml(filePath, ugcSetFile);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save YAML: {ex.Message}");
                return false;
            }

        }


        public static T LoadYaml<T>(string filePath)
        {
            var deserializer = new DeserializerBuilder().Build();
            using (var reader = new StreamReader(filePath))
            {
                return deserializer.Deserialize<T>(reader);
            }
        }
        // 객체를 YAML 형식으로 저장하는 메서드
        public static void SaveYaml(string filePath, object data)
        {
            var serializer = new SerializerBuilder().Build();
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }

            Console.WriteLine($"YAML 파일이 {filePath}에 저장되었습니다.");
        }
    }
}
