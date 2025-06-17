using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp.Data
{
    public class Param
    {
        public string value { get; set; }
        public bool use { get; set; }
    }
    public class PPRecipeSpec
    {
        public string Ppid { get; set; }
        public string Version { get; set; }

        public Dictionary<string, Param> ParamMap { get; set; }
    }

    public class PP_RECIPE_SPEC
    {
        public PPRecipeSpec RECIPE { get; set; }
    }

    public class RecipeInventory
    {
        public List<string> recipeYamlFiles = new List<string>();
    }
    public class RecipeData
    {
        public PP_RECIPE_SPEC vPPRecipeSpecEquip { get; set; }
        public PP_RECIPE_SPEC vPPRecipeSpec__Host { get; set; }
        public RecipeInventory recipeInventory { get; set; }

        public RecipeData()
        {
            recipeInventory = new RecipeInventory();
            vPPRecipeSpec__Host = new PP_RECIPE_SPEC();
            vPPRecipeSpec__Host.RECIPE = new PPRecipeSpec();
            vPPRecipeSpec__Host.RECIPE.ParamMap = new Dictionary<string, Param>();
        }
        public PP_RECIPE_SPEC RecipeLoad(string recipeFilePPid)
        {
            //string filePath = Path.Combine(CPath.BASE_RECIPE_PATH, CPath.yamlFilePathRecipe);
            string filePath = Path.Combine(CPath.BASE_RECIPE_PATH, recipeFilePPid + ".yaml");
            PP_RECIPE_SPEC tempRecipe = null;
            try
            {

                if (!File.Exists(filePath))
                {
                    return tempRecipe;
                }

                tempRecipe = Data.YamlManager.LoadYaml<PP_RECIPE_SPEC>(filePath);
                return tempRecipe;

                //if (tempRecipe == null)
                //{
                //    return tempRecipe;
                //}
                // PP_RECIPE_SPEC 정보 출력
                //Console.WriteLine($"PPId: {vPPRecipeSpecEquip.RECIPE.Ppid}");
                //Console.WriteLine($"VERSION: {vPPRecipeSpecEquip.RECIPE.Version}");

                //Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap["Task1"] = new Data.Param { value = 999, use = true };
                //Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap["Task 1"] = new Data.Param { value = 888, use = true };

                //Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap["Task2"] = new Data.Param { value = 1, use = Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap["Task1"].use };

                // ParamMap 출력 (Value와 Flag 값 출력)
                //foreach (var kvp in vPPRecipeSpecEquip.RECIPE.ParamMap)
                //{
                //    Console.WriteLine($"Task: {kvp.Key}, Value: {kvp.Value.value}, Flag: {kvp.Value.use}");
                //}
                //return tempRecipe;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading RecipeLoad: {ex.Message}");
                return tempRecipe;
            }
        }
        public bool RecipeSave(PP_RECIPE_SPEC ppRecipe)
        {
            string filePath = Path.Combine(CPath.BASE_RECIPE_PATH, ppRecipe.RECIPE.Ppid + ".yaml");//   CPath.yamlFilePathRecipe);
            
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath)) // 폴더가 존재하지 않으면
                {
                    Directory.CreateDirectory(directoryPath); // 폴더 생성
                }
                //if (!File.Exists(filePath))       //존재하지 않아도 저장해야돼서 주석처리 250324
                //return false;

                Data.YamlManager.SaveYaml(filePath, ppRecipe);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Save YAML: {ex.Message}");
                return false;
            }
        }
        public bool RecipeYamlFileCopy(string copyPPid, string createPPid)
        {
            //
            string folderPath = CPath.BASE_RECIPE_PATH;                 // 복사할 폴더 경로
            string sourcePath = Path.Combine(folderPath, copyPPid + ".yaml");
            string destinationPath = Path.Combine(folderPath, createPPid + ".yaml");
            //
            //
            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, destinationPath, true); // true = 같은 파일이 있으면 덮어쓰기
                    Console.WriteLine($"복사 완료: {destinationPath}");

                    return true;
                }
                else
                {
                    Console.WriteLine("원본 파일이 존재하지 않습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생: {ex.Message}");
            }

            return false;
        }
        public bool RecipeYamlFileDel(string ppid)
        {
            string folderPath = CPath.BASE_RECIPE_PATH; // 검색할 폴더 경로

            string filePath = Path.Combine(folderPath, ppid + ".yaml");
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine($"삭제됨: {filePath}");
                    return true;
                }
                else
                {
                    Console.WriteLine("파일이 존재하지 않습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생: {ex.Message}");
            }
            return false;
        }
        public bool RecipeYamlListLoad()
        {
            string folderPath = CPath.BASE_RECIPE_PATH; // 검색할 폴더 경로
            recipeInventory.recipeYamlFiles.Clear();

            string[] files = Directory.GetFiles(folderPath, "*.yaml"); // 모든 .yaml 파일 가져오기

            // 확장자가 .yaml인 파일만 가져오기
            //recipeYamlFiles.AddRange(Directory.GetFiles(folderPath, "*.yaml"));

            foreach (string file in files)
            {
                //string fileName = Path.GetFileName(file); // 파일명만 추출 확장자 포함
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file); //확장자 제외
                recipeInventory.recipeYamlFiles.Add(fileNameWithoutExt);
            }

            Console.WriteLine("Recipe File Load");
            // 결과 출력
            foreach (var file in recipeInventory.recipeYamlFiles)
            {
                Console.WriteLine(file);
            }


            return true;
        }

    }
}
