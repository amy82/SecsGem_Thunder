using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Data
{
    public class _ModelData
    {
        public int ModelNo { get; set; }
        public string CurrentModelName { get; set; }       //현재 사용중인 모델 명
        public List<string> Modellist { get; set; }
    }

    public class RootModelData
    {
        public _ModelData ModelData { get; set; }
    }

    public class ModelListData
    {
        public _ModelData ModelData { get; set; }

        
        public bool ModelLoad()
        {
            //string filePath = CPath.yamlFilePathModel;
            string filePath = Path.Combine(CPath.BASE_MODEL_PATH, CPath.yamlFilePathModel);
            try
            {
                if (!File.Exists(filePath))
                    return false;

                //Globalo.yamlManager.ModelData = Data.YamlManager.LoadYaml<RootModelData>(filePath);
                ModelData = Data.YamlManager.LoadYaml<_ModelData>(filePath);
                //if (Globalo.yamlManager.ModelData == null)
                if (ModelData == null)
                {
                    ModelData = new _ModelData();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading modelLIstData: {ex.Message}");
                return false;
            }
        }

        public bool ModelSave()
        {
            //string filePath = CPath.yamlFilePathModel;
            string filePath = Path.Combine(CPath.BASE_MODEL_PATH, CPath.yamlFilePathModel);

            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath)) // 폴더가 존재하지 않으면
                {
                    Directory.CreateDirectory(directoryPath); // 폴더 생성
                }
                if (!File.Exists(filePath))
                    return false;

                //Data.YamlManager.SaveYaml(filePath, Globalo.yamlManager.ModelData);
                Data.YamlManager.SaveYaml(filePath, ModelData);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save ModelSave: {ex.Message}");
                return false;
            }
        }

        //public bool ModelDataSet(string modelName)
        //{
        //    //Sensor ini 파일 설정
        //    //CurrentModelName
        //    ModelData.CurrentModelName = modelName;
        //    string modelFolerPath = Path.Combine(CPath.BASE_MODEL_PATH, ModelData.CurrentModelName);
        //    if (!File.Exists(modelFolerPath))
        //    {
        //        //새로 추가된 모델 -> [DEFAULT_MODEL] 폴더 복사해서 추가해라
        //        //BASE_MODEL_DEFAULT_PATH  -> copy ->ModelData.CurrentModelName
        //        CopyDirectory(CPath.BASE_MODEL_DEFAULT_PATH, modelFolerPath);

        //    }
        //    return true;
        //}
        //public void CopyDirectory(string sourceDir, string destinationDir)
        //{
        //    // 대상 폴더가 없으면 생성
        //    if (!Directory.Exists(destinationDir))
        //    {
        //        Directory.CreateDirectory(destinationDir);
        //    }

        //    // 현재 폴더의 파일 복사
        //    foreach (string file in Directory.GetFiles(sourceDir))
        //    {
        //        string fileName = Path.GetFileName(file);
        //        string destFile = Path.Combine(destinationDir, fileName);
        //        File.Copy(file, destFile, true); // 덮어쓰기 허용
        //    }

        //    // 하위 폴더 복사 (재귀적으로 수행)
        //    foreach (string subDir in Directory.GetDirectories(sourceDir))
        //    {
        //        string dirName = Path.GetFileName(subDir);
        //        string destSubDir = Path.Combine(destinationDir, dirName);
        //        CopyDirectory(subDir, destSubDir); // 재귀 호출
        //    }
        //}
    }

    
}
