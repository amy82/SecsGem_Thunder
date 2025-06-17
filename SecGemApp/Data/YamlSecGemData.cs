using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Data
{
    public class _SecGemData
    {
        public string OperatorId { get; set; }
        public string MaterialId { get; set; }
        public string MaterialType { get; set; }
        public int RecipeNo { get; set; }
        public string CurrentRecipeName { get; set; }
        public int ModelNo { get; set; }
        public string CurrentModelName { get; set; }

        public List<string> Modellist { get; set; }

        
    }
    public class RootModel
    {
        public _SecGemData SecGemData { get; set; }
    }

    public class UgcSetFile
    {
        public string ugcFilePath { get; set; }

    }

    public class MesManager
    {
        public RootModel MesData { get; private set; }

        public MesManager()
        {

        }
        public bool MesLoad()
        {
            //string filePath = CPath.yamlFilePathModel;
            string filePath = Path.Combine(CPath.BASE_SECSGEM_PATH, CPath.yamlFilePathModel);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                MesData = Data.YamlManager.LoadYaml<RootModel>(filePath);
                if (MesData == null)
                {
                    return false;
                }

                Globalo.dataManage.mesData.m_sMesOperatorID = MesData.SecGemData.OperatorId;
                Globalo.dataManage.mesData.m_sRecipeId = MesData.SecGemData.CurrentRecipeName;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading MesLoad: {ex.Message}");
                return false;
            }
        }

        public bool MesSave()
        {
            //string filePath = CPath.yamlFilePathModel;
            string filePath = Path.Combine(CPath.BASE_SECSGEM_PATH, CPath.yamlFilePathModel);
            
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath)) // 폴더가 존재하지 않으면
                {
                    Directory.CreateDirectory(directoryPath); // 폴더 생성
                }
                if (!File.Exists(filePath))
                    return false;

                Data.YamlManager.SaveYaml(filePath, MesData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save YAML: {ex.Message}");
                return false;
            }
        }
    }


}
