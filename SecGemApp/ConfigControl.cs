using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecGemApp
{
    public partial class ConfigControl : UserControl
    {
        public ConfigControl()
        {
            InitializeComponent();

            DriverButtonSet(1);
        }

        public void setUgcPath()
        {
            //string fileName = Path.GetFileName(openFileDialog.FileName);
            label_UgcPath.Text = Globalo.yamlManager.ugcSetFile.ugcFilePath;
        }
        public void DriverButtonSet(int index)
        {
            if(index == 1)
            {
                //ubiGem Start
                button_Driver_Start.BackColor = Color.Green;
                button_Driver_Stop.BackColor = Color.FromArgb(69, 73, 74);
            }
            else
            {
                //ubiGem Stop
                //crownButton_Driver_Stop
                button_Driver_Start.BackColor = Color.FromArgb(69, 73, 74);
                button_Driver_Stop.BackColor = Color.Green;
            }
        }
        public void ControlStateButtonSet(int index)
        {
            if (index == 1)
            {
                //ubiGem Online
                button_Control_Online.BackColor = Color.Green;
                button_Control_Offline.BackColor = Color.FromArgb(69, 73, 74);
            }
            else if (index == 0)
            {
                //ubiGem Offline
                //crownButton_Driver_Stop
                button_Control_Online.BackColor = Color.FromArgb(69, 73, 74);
                button_Control_Offline.BackColor = Color.Green;
            }
            else
            {
                //ubiGem Offline
                //crownButton_Driver_Stop
                button_Control_Online.BackColor = Color.FromArgb(69, 73, 74);
                button_Control_Offline.BackColor = Color.FromArgb(69, 73, 74);
            }
        }
        private void crownButton_Driver_Init_Click(object sender, EventArgs e)
        {
            Globalo.ubisamForm.OnMnuInitilaize();
            Globalo.LogPrint("[config]", $"[Config] UbiGem itilaize ");
        }


        private void crownButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Ubisam.UbisamForm.UGC_FILE_FILTER
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //_ugcFileName = openFileDialog.FileName;
                string fileName = Path.GetFileName(openFileDialog.FileName);
                Globalo.yamlManager.ugcSetFile.ugcFilePath = fileName;
                Globalo.yamlManager.UgcSave();

                label_UgcPath.Text = Globalo.yamlManager.ugcSetFile.ugcFilePath;

                Globalo.LogPrint("[Config]", $"[Config] {fileName} Save ");
            }
        }

        private void crownButton_Control_Offline_Click(object sender, EventArgs e)
        {
            
        }

        private void crownButton_Control_Online_Click(object sender, EventArgs e)
        {
            
        }

        private void Button_RecipeCreate_Click(object sender, EventArgs e)
        {
            //레시피 생성
            InputForm inputForm = new InputForm("RECIPE CREATE","생성할 레시피명 입력해주세요:","", 0);
            DialogResult result = inputForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string createRecipe = inputForm.InputText;
                if(createRecipe.Length < 1)
                {
                    return;
                }

                //기존 리스트에 동일한 이름 있는지 확인하기
                //
                int recipeCount = Globalo.yamlManager.recipeData.recipeInventory.recipeYamlFiles.Count();
                for (int i = 0; i < recipeCount; i++)
                {
                    if(createRecipe == Globalo.yamlManager.recipeData.recipeInventory.recipeYamlFiles[i])
                    {
                        //이미 존재하는 레시피 입니다.
                        return;
                    }
                }
                //
                //레시피 파일 복사
                //
                Globalo.yamlManager.recipeData.RecipeYamlFileCopy(Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName, createRecipe);


                //생성한 레시피 파라미터 로드
                //
                Data.PP_RECIPE_SPEC ppRs = Globalo.yamlManager.recipeData.RecipeLoad(createRecipe);     //Recipe Load

                if (ppRs == null)
                {
                    Globalo.LogPrint("MainControl", "[INFO] Recipe Create Fail", Globalo.eMessageName.M_ERROR);
                    return;
                }
                ppRs.RECIPE.Ppid = createRecipe;
                ppRs.RECIPE.Version = "1";
                Globalo.yamlManager.recipeData.RecipeSave(ppRs);       //Recipe Save

                Thread.Sleep(100);
                //레시피 파일 리스트 갱신
                //
                Globalo.yamlManager.recipeData.RecipeYamlListLoad();       //Recipe Create

                //ShowRecipeList();
                Globalo.recipeControl.SetRecipeListView();

                Globalo.dataManage.mesData.m_dPPChangeArr[0] = (int)Ubisam.ePP_CHANGE_STATE.eCreated;            //1 = Created
                Globalo.dataManage.mesData.m_dPPChangeArr[1] = (int)Ubisam.ePP_CHANGE_ORDER_TYPE.eOperator;      //1 = Host, 2 = Operator

                Globalo.LogPrint("MainControl", "[Rerpot] Process Program State Changed Report - Created");

                Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601, ppRs.RECIPE.Ppid);
            }
        }

        private void Button_RecipeDel_Click(object sender, EventArgs e)
        {
            //레시피 삭제

            //사용중인 레시피 명인지 확인 후 리턴
            string selectedRecipe = Globalo.recipeControl.SelectedRecipedName();
            if (selectedRecipe == Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName)
            {
                Globalo.LogPrint("[config]", $"[Config] 사용중인 레시피 삭제 불가 ", Globalo.eMessageName.M_WARNING);
                return;
            }

            //삭제 하시겠습니까? ask 팝업 추가
            string logStr = $"[{selectedRecipe}] 레시피 삭제 하시겠습니까 ?";
            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");
            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, logStr);
            DialogResult result = messagePopUp.ShowDialog();

            if (result != DialogResult.Yes)
            {
                return;
            }
            //

            Data.PP_RECIPE_SPEC ppRs = Globalo.yamlManager.recipeData.RecipeLoad(selectedRecipe);     //Recipe Load
            if (ppRs == null)
            {
                Globalo.LogPrint("MainControl", "[INFO] Recipe Delete Fail", Globalo.eMessageName.M_ERROR);
                return;
            }
            Globalo.dataManage.mesData.m_sMesRecipeRevision = ppRs.RECIPE.Version;

            Globalo.dataManage.mesData.m_dPPChangeArr[0] = (int)Ubisam.ePP_CHANGE_STATE.eDeleted;            //1 = Created
            Globalo.dataManage.mesData.m_dPPChangeArr[1] = (int)Ubisam.ePP_CHANGE_ORDER_TYPE.eOperator;      //1 = Host, 2 = Operator

            //레시피 파일 삭제
            Globalo.yamlManager.recipeData.RecipeYamlFileDel(selectedRecipe);
            
            //레시피 리스드 다시 로드
            Thread.Sleep(100);
            //레시피 파일 리스트 갱신
            
            Globalo.yamlManager.recipeData.RecipeYamlListLoad();       //Recipe Del

            Globalo.recipeControl.SetRecipeListView();
            Globalo.LogPrint("MainControl", "[Rerpot] Process Program State Changed Report - Deleted");
            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601, ppRs.RECIPE.Ppid);
        }

        private void crownButton_Model_Add_Click(object sender, EventArgs e)
        {
            InputForm inputForm = new InputForm("MODEL CREATE", "생성할 모델명 입력해주세요:","", 0);
            DialogResult result = inputForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string createModel = inputForm.InputText;
                if (createModel.Length < 1)
                {
                    return;
                }

                int addCount = Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist.Count();
                Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist.Add(createModel);
                Globalo.yamlManager.mesManager.MesSave();

                Globalo.modelControl.SetModelListView();
            }
        }

        private void crownButton_Model_Del_Click(object sender, EventArgs e)
        {
            //사용중인 레시피 명인지 확인 후 리턴
            string selectedModel = Globalo.modelControl.SelectedModelName();

            if (selectedModel == Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentModelName)
            {
                Globalo.LogPrint("[config]", $"[Config] 사용중인 모델 삭제 불가 ", Globalo.eMessageName.M_WARNING);
                return;
            }
            string logStr = $"[{selectedModel}] 모델 삭제 하시겠습니까 ?";
            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");
            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, logStr);
            DialogResult result = messagePopUp.ShowDialog();

            if (result != DialogResult.Yes)
            {
                return;
            }
            //삭제 하시겠습니까? ask 팝업 추가

            //Globalo.yamlManager.MesData.SecGemData.Modellist.RemoveAt(SelectedCellRow);
            // "11"과 같은 요소 제거
            //////Globalo.yamlManager.MesData.SecGemData.Modellist.RemoveAll(item => item == selectedModel);
            int cnt = Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist.Count;
            for (int i = 0; i < cnt; i++)
            {
                if (Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist[i] == selectedModel)
                {
                    //removedIndexes.Add(i); // 삭제된 인덱스 저장

                    Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist.RemoveAt(i); // 해당 인덱스의 요소 삭제

                    if (Globalo.yamlManager.mesManager.MesData.SecGemData.ModelNo >= i)
                    {
                        Globalo.yamlManager.mesManager.MesData.SecGemData.ModelNo--;
                    }

                    Globalo.yamlManager.mesManager.MesSave();
                    Globalo.modelControl.SetModelListView();
                    break;
                }
            }

        }

        private void button_Driver_Start_Click(object sender, EventArgs e)
        {
            Globalo.ubisamForm.OnMnuStart();
            Globalo.LogPrint("[config]", $"[Config] UbiGem Start ");

            DriverButtonSet(1);
        }

        private void button_Driver_Stop_Click(object sender, EventArgs e)
        {
            Globalo.ubisamForm.OnMnuStop();
            Globalo.LogPrint("[config]", $"[Config] UbiGem Stop ");

            DriverButtonSet(0);
        }

        private void button_Control_Offline_Click(object sender, EventArgs e)
        {
            string logStr = $"설비 오프라인 전환 하시겠습니까 ?";
            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");
            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, logStr);
            DialogResult result = messagePopUp.ShowDialog();

            if (result != DialogResult.Yes)
            {
                return;
            }
            Globalo.ubisamForm.OnMnuOffLIne();
            Globalo.LogPrint("[config]", $"[Config] UbiGem Offline ");
        }

        private void button_Control_Online_Click(object sender, EventArgs e)
        {
            string logStr = $"설비 온라인 전환 하시겠습니까 ?";
            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");
            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, logStr);
            DialogResult result = messagePopUp.ShowDialog();

            if (result != DialogResult.Yes)
            {
                return;
            }
            Globalo.ubisamForm.OnMnuOnLIne();
            Globalo.LogPrint("[config]", $"[Config] UbiGem Online ");
        }

        private void crownButton_Config_Save_Click(object sender, EventArgs e)
        {
            string logStr = $"설정 저장 하시겠습니까 ?";

            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");
            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, logStr);
            DialogResult result = messagePopUp.ShowDialog();

            if (result == DialogResult.Yes)
            {
                Globalo.yamlManager.configManager.configDataSave();
                Globalo.LogPrint("[config]", $"[Config] Data Save Complete");
            }


            
        }

        private void crownButton_Config_Refresh_Click(object sender, EventArgs e)
        {
            Globalo.yamlManager.configManager.ShowConfigData();
        }
    }
}
