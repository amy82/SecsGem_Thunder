using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecGemApp.Dlg
{
    public partial class RecipePopup : Form
    {
        private Data.PP_RECIPE_SPEC reciveSpec;
        private const int RecipeGridRowViewCount = 13;
        private int[] GridColWidth = { 30, 200, 150, 70, 270, 50, 50, 1 };
        private int RecipeGridWidth = 0;
        private int GridRowHeight = 30;
        private int GridHeaderHeight = 30;
        private readonly string[] keyTypes = { "A", "B", "C", "D", "E" };
        private string loadRecipeName = "";

        public RecipePopup(string recipyName)
        {
            InitializeComponent();

            loadRecipeName = recipyName;
            this.CenterToScreen();
            InitRecipeGrid();


            label_Recipe.Text = loadRecipeName;

            
            if (loadRecipeName != Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName)
            {
                BTN_RECIPE_DOWN_REQ.Enabled = false;
                BTN_RECIPE_SAVE.Enabled = false;
            }

            reciveSpec = Globalo.yamlManager.recipeData.RecipeLoad(loadRecipeName);
            label_Recipe_Version.Text = reciveSpec.RECIPE.Version;

        }
        private int GetRecipeList()
        {
            int nRtn = 0; //0 = 은 변경이 없는 경우
            int i = 0;
           // string selectedItem = comboBox_RecipeList.SelectedItem.ToString();

            string selectedItem = loadRecipeName;

            //Globalo.dataManage.mesData.m_sMesPPID = selectedItem;

            if (reciveSpec != null)
            {
                reciveSpec.RECIPE.Ppid = selectedItem;
            }

            Globalo.dataManage.mesData.m_sMesRecipeRevision = reciveSpec.RECIPE.Version;


            int nGridRow = dataGridView_Recipe.RowCount;
            int getCount = reciveSpec.RECIPE.ParamMap.Count();

            string sName = "";
            string sValue = "";
            bool bCheck = false;


            for (i = 0; i < getCount; i++)
            {
                //strData = CardGrid.Rows[i].Cells[j].Value.ToString();
                sName = dataGridView_Recipe.Rows[i].Cells[1].Value.ToString();
                sValue = dataGridView_Recipe.Rows[i].Cells[2].Value.ToString();

                bCheck = Convert.ToBoolean(dataGridView_Recipe.Rows[i].Cells[0].Value);
                if (reciveSpec.RECIPE.ParamMap.TryGetValue(sName, out var value))
                {
                    if (bCheck != reciveSpec.RECIPE.ParamMap[sName].use)
                    {
                        nRtn = (int)Ubisam.ePP_CHANGE_STATE.eUploadListChanged;
                    }

                    reciveSpec.RECIPE.ParamMap[sName].use = bCheck;
                    if (bCheck)
                    {
                        if (sValue != reciveSpec.RECIPE.ParamMap[sName].value)
                        {
                            nRtn = (int)Ubisam.ePP_CHANGE_STATE.eEdited;
                        }
                    }
                    reciveSpec.RECIPE.ParamMap[sName].value = sValue;
                }


            }
            return nRtn;
        }
        public void ShowRecipeGrid()
        {
            //int nCol = dataGridView_Recipe.ColumnCount;         //7 옆으로 행
            //int nRow = dataGridView_Recipe.RowCount;        //0 아래로 열 빈칸 -1

            int dataCount = reciveSpec.RECIPE.ParamMap.Count();
            if (dataCount > 0)
            {
                dataGridView_Recipe.Rows.Clear();
                int count = 0;

                foreach (var kvp in reciveSpec.RECIPE.ParamMap)
                {
                    //Console.WriteLine($"Task: {kvp.Key}, Value: {kvp.Value.value}, Flag: {kvp.Value.use}");
                    dataGridView_Recipe.Rows.Add(
                            kvp.Value.use,
                            kvp.Key,
                            kvp.Value.value);
                    //if (count <= RecipeGridRowViewCount)
                    //{
                    //    dataGridView_Recipe.Rows.Add(
                    //        kvp.Value.use,
                    //        kvp.Key,
                    //        kvp.Value.value);
                    //}
                    //else
                    //{
                    //    dataGridView_Recipe.Rows.Add(false, "", ""); // 행 추가
                    //}

                    dataGridView_Recipe.Rows[count].Cells[0].Style.BackColor = Color.White;
                    dataGridView_Recipe.Rows[count].Cells[0].Style.ForeColor = Color.Black;
                    dataGridView_Recipe.Rows[count].Cells[1].Style.BackColor = Color.White;
                    dataGridView_Recipe.Rows[count].Cells[1].Style.ForeColor = Color.Black;
                    dataGridView_Recipe.Rows[count].Cells[2].Style.BackColor = Color.White;
                    dataGridView_Recipe.Rows[count].Cells[2].Style.ForeColor = Color.Black;
                    dataGridView_Recipe.Rows[count].Cells[0].Style.Font = new Font("나눔고딕", 10F, FontStyle.Regular);
                    dataGridView_Recipe.Rows[count].Cells[1].Style.Font = new Font("나눔고딕", 10F, FontStyle.Regular);
                    dataGridView_Recipe.Rows[count].Cells[2].Style.Font = new Font("나눔고딕", 10F, FontStyle.Regular);

                    // dataGridView_Recipe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // dataGridView_Recipe.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    //dataGridView_Recipe.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    count++;
                }
                int gridViewCount = dataCount;
                if (gridViewCount > RecipeGridRowViewCount)
                {
                    dataGridView_Recipe.Width = RecipeGridWidth + 20; //스크롤 추가시 grid Width 조정
                }

                dataGridView_Recipe.ClearSelection();
            }
        }
        #region [INIT GRID]
        
        private void InitRecipeGrid()
        {
            //BankGrid
            int i = 0;
            int j = 0;
            // 열 추가
            // 행 헤더 숨기기
            dataGridView_Recipe.RowHeadersVisible = false;
            //사이즈 조절 막기
            dataGridView_Recipe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // 행 높이 자동 조정
            dataGridView_Recipe.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; // 모든 셀에 맞게 자동 조정

            // 열 자동 크기 조정
            dataGridView_Recipe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // 또는
            // 셀 내용 줄바꿈
            dataGridView_Recipe.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // 헤더 폰트 설정
            dataGridView_Recipe.ColumnHeadersDefaultCellStyle.Font = new Font("나눔고딕", 10F, FontStyle.Bold);

            // 헤더 배경색 설정
            dataGridView_Recipe.ColumnHeadersDefaultCellStyle.BackColor = Color.GhostWhite;// LightGray;// Color.FromArgb(94, 129, 244); //Color.LightBlue;
            // 헤더 폰트 색
            dataGridView_Recipe.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;// .Gray;

            dataGridView_Recipe.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView_Recipe.RowsDefaultCellStyle.ForeColor = Color.Black;

            // Set the selection background color for all the cells.
            //dataGridView_Recipe.DefaultCellStyle.SelectionBackColor = Color.White;
            // dataGridView_Recipe.DefaultCellStyle.SelectionForeColor = Color.Black;
            // dataGridView_Recipe.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Empty;
            // dataGridView_Recipe.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

            //dataGridView_Recipe.DefaultCellStyle.SelectionForeColor = Color.Empty;

            //dataGridView_Recipe.DefaultCellStyle.SelectionBackColor = Color.Empty;
            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            // dataGridView_Recipe.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            // dataGridView_Recipe.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Empty;

            dataGridView_Recipe.EnableHeadersVisualStyles = false;
            // 열 헤더 가운데 정렬
            dataGridView_Recipe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //운영 체제의 기본 시각적 스타일을 무시
            dataGridView_Recipe.AllowUserToResizeRows = false;

            dataGridView_Recipe.ReadOnly = true;
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            cellStyle.Font = new Font("나눔고딕", 10, FontStyle.Regular); // Change font and size
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center align text
            //cellStyle.SelectionBackColor = Color.LightBlue;
            //cellStyle.SelectionForeColor = Color.Empty;

            dataGridView_Recipe.DefaultCellStyle = cellStyle;

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn[] textColumns = new DataGridViewTextBoxColumn[2];

            for (i = 0; i < 2; i++)
            {
                textColumns[i] = new DataGridViewTextBoxColumn();
            }

            //DataGridView
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.Name = "checkBoxColumn";
            textColumns[0].HeaderText = "Name";
            textColumns[1].HeaderText = "Value";

            //textColumns[0].Name = "No";
            //textColumns[1].Name = "Model";




            dataGridView_Recipe.Columns.Add(checkBoxColumn);
            dataGridView_Recipe.Columns.Add(textColumns[0]);
            dataGridView_Recipe.Columns.Add(textColumns[1]);

            for (i = 0; i < dataGridView_Recipe.ColumnCount; i++)
            {
                dataGridView_Recipe.Columns[i].Resizable = DataGridViewTriState.False;
            }
            int gridWidth = dataGridView_Recipe.Width;
            dataGridView_Recipe.Columns[0].Width = GridColWidth[0];
            dataGridView_Recipe.Columns[1].Width = GridColWidth[1];
            dataGridView_Recipe.Columns[2].Width = gridWidth - GridColWidth[0] - GridColWidth[1];




            // 행 높이 조정
            dataGridView_Recipe.RowTemplate.Height = GridRowHeight; // 자동 추가되는 행 높이 설정
            dataGridView_Recipe.ColumnHeadersHeight = GridHeaderHeight;
            //dataGridView_Recipe.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;

            for (i = 0; i < RecipeGridRowViewCount; i++)
            {

                string text = $"예시 텍스트 {i}"; // 예시 텍스트 생성
                //bool isChecked = (i % 2 == 0); // 짝수인 경우 체크박스가 체크됨
                dataGridView_Recipe.Rows.Add(true, "", ""); // 행 추가
                dataGridView_Recipe.Rows[i].Height = GridRowHeight;

                for (j = 0; j < dataGridView_Recipe.ColumnCount; j++)
                {
                    //dataGridView.Columns[i].Resizable = DataGridViewTriState.False;
                    //dataGridView_Recipe.Columns[j].Width = GridColWidth[j];
                    dataGridView_Recipe.Columns[j].Resizable = DataGridViewTriState.False;

                }
            }

            dataGridView_Recipe.Height = RecipeGridRowViewCount * GridRowHeight + GridRowHeight + 2;
            if (dataGridView_Recipe.AllowUserToAddRows == true)
            {
                //dataGridView_Model.Rows[GridRowCount].Height = GridRowHeight;
            }


            dataGridView_Recipe.MultiSelect = false; // 여러 개 선택 불가능
            dataGridView_Recipe.AllowUserToAddRows = false; // 빈 행 추가 방지
            dataGridView_Recipe.ScrollBars = ScrollBars.Vertical;      //가로 스크롤 안보이게 설정

            //dataGridView_Recipe.CellContentClick += new DataGridViewCellEventHandler(RecipeGridView_CellContentClick);     //삭제 버튼 클릭시 사용
            // 버튼 클릭 이벤트 등록
            dataGridView_Recipe.CellClick += new DataGridViewCellEventHandler(RecipeGridView_CellClick); //textbox 한번 클릭으로 바로 수정되게 추가
            //dataGridView_Recipe.SelectionChanged += dataGridView1_SelectionChanged;
            // 이벤트 핸들러 추가
            //CardGrid.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(CardGrid_EditingControlShowing);
            //dataGridView_Recipe.CellFormatting += dataGridView_Model_CellFormatting;

            RecipeGridWidth = dataGridView_Recipe.Width;     //<---스크롤 생겼을때 사이즈 조절위해 초기 Grid 넓이 저장

            // 각 컬럼의 헤더 텍스트 정렬 설정
            foreach (DataGridViewColumn column in dataGridView_Recipe.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            CheckBox headerCheckBox = new CheckBox();
            headerCheckBox.Size = new Size(15, 15);
            headerCheckBox.BackColor = Color.Transparent;
            headerCheckBox.Checked = false;
            // 헤더 위치 조정
            Point headerCellLocation = dataGridView_Recipe.GetCellDisplayRectangle(0, -1, true).Location;
            headerCheckBox.Location = new Point(headerCellLocation.X + (dataGridView_Recipe.Columns[0].Width / 2) - (headerCheckBox.Width / 2),
                                                headerCellLocation.Y + (dataGridView_Recipe.ColumnHeadersHeight / 2) - (headerCheckBox.Height / 2));
            headerCheckBox.CheckedChanged += new EventHandler(HeaderCheckBox_CheckedChanged);
            dataGridView_Recipe.Controls.Add(headerCheckBox);

            //dataGridView_Model.Columns[0].ReadOnly = true; // 읽기 전용
            dataGridView_Recipe.Columns[0].DefaultCellStyle.BackColor = Color.LightGray; // 배경색 설정
            dataGridView_Recipe.Columns[0].DefaultCellStyle.ForeColor = Color.Yellow; // 배경색 설정
            dataGridView_Recipe.Columns[0].DefaultCellStyle.Font = new Font("나눔고딕", 10F, FontStyle.Bold); // 굵은 글씨
            dataGridView_Recipe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Recipe.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView_Recipe.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            dataGridView_Recipe.EnableHeadersVisualStyles = false;
            dataGridView_Recipe.DefaultCellStyle.SelectionBackColor = dataGridView_Recipe.DefaultCellStyle.BackColor;
            dataGridView_Recipe.DefaultCellStyle.SelectionForeColor = dataGridView_Recipe.DefaultCellStyle.ForeColor;


            dataGridView_Recipe.ClearSelection();
        }
        #endregion
        private void RecipeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 0)//dataGridView.Columns["Selected"].Index)
            {
                var cell = dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                if (cell != null)
                {
                    cell.Value = !(bool)cell.Value; // 체크 상태를 반전
                }
            }
            if (e.ColumnIndex == 2) //value
            {
                string sValue = dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                //250603 AOI 레시피 전용
                string titleChk = dataGridView_Recipe.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (titleChk == "O_RING" || titleChk == "CONE")
                {
                    //0하고 1만 반복
                    if (sValue == "0")
                    {
                        sValue = "1";
                    }
                    else
                    {
                        sValue = "0";
                    }
                    dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = sValue;
                }
                else if (titleChk == "KEYTYPE")
                {
                    //A ~ E 까지만 반복
                    int index = Array.IndexOf(keyTypes, sValue);
                    int nextIndex = (index >= 0) ? (index + 1) % keyTypes.Length : 0;


                    dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = keyTypes[nextIndex];
                }
                else
                {
                    NumPadForm popupForm = new NumPadForm(sValue);

                    DialogResult dialogResult = popupForm.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = popupForm.NumPadResult;

                        //if (popupForm.NumPadResult.Contains(".") == true)
                        //{
                        //    //Globalo.LogPrint("Recipe", "소수 점이 포함돼 있습니다.", Globalo.eMessageName.M_WARNING);
                        //    Globalo.LogPrint("Recipe", "입력이 값 확인바랍니다.", Globalo.eMessageName.M_WARNING);

                        //}
                        //else
                        //{
                        //    dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = popupForm.NumPadResult;
                        //}
                    }
                }
                
            }
        }
        private void HeaderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = sender as CheckBox;

            // 모든 행의 체크박스 상태 변경
            foreach (DataGridViewRow row in dataGridView_Recipe.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell;
                checkBoxCell.Value = headerCheckBox.Checked;
                dataGridView_Recipe.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

            // DataGridView 업데이트
            dataGridView_Recipe.RefreshEdit();
        }
        private void BTN_RECIPE_DOWN_REQ_Click(object sender, EventArgs e)
        {
            //DOWNLOAD REQ
            //string selectedItem = comboBox_RecipeList.SelectedItem.ToString();
            //string selectedItem = loadRecipeName;

            Globalo.ubisamForm.FormattedProcessProgramRequest(loadRecipeName);


            //TcpSocket.EquipmentData sendEqipData = new TcpSocket.EquipmentData();
            //sendEqipData.Command = "APS_RECIPE_DOWN";
            //sendEqipData.DataID = selectedItem;

            //Globalo.tcpManager.SendMessageToClient(sendEqipData);
        }

        private void BTN_RECIPE_SAVE_Click(object sender, EventArgs e)
        {
            //SAVE
            //string selectedItem = comboBox_RecipeList.SelectedItem.ToString();
            string selectedItem = loadRecipeName;


            if (selectedItem != Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName)
            {
                Globalo.LogPrint("MainControl", $"[{selectedItem}] 사용중인 RECIPE ID가 아닙니다.", Globalo.eMessageName.M_WARNING);
                return;
            }

            int ppIndex = GetRecipeList();	//2 = 값이 변경, 4 = 체크 변경


            if (ppIndex == 0)
            {
                Globalo.LogPrint("MainControl", "Recipe no Change", Globalo.eMessageName.M_INFO);
                return;
            }


            Globalo.dataManage.mesData.m_dPPChangeArr[0] = ppIndex;
            Globalo.dataManage.mesData.m_dPPChangeArr[1] = (int)Ubisam.ePP_CHANGE_ORDER_TYPE.eOperator;

            if (Globalo.dataManage.mesData.m_dPPChangeArr[0] == (int)Ubisam.ePP_CHANGE_STATE.eEdited)
            {
                if(reciveSpec.RECIPE.Version.Length < 1)
                {
                    reciveSpec.RECIPE.Version = "1";
                }
                int tempVer = Convert.ToInt32(reciveSpec.RECIPE.Version);
                tempVer++;
                reciveSpec.RECIPE.Version = tempVer.ToString();
                string logData = $"[Rerpot] Process Program State Changed Report - Edited{reciveSpec.RECIPE.Version}";
                Globalo.LogPrint("MainControl", logData);
            }
            else
            {
                string logData = "[Rerpot] Process Program State Changed Report - UploadListChanged";
                Globalo.LogPrint("MainControl", logData);
            }

            Globalo.yamlManager.recipeData.RecipeSave(reciveSpec);     //TODO: 여기도 바궈야된다. 담기는 변수로 저장

            Globalo.LogPrint("MainControl", "[Rerpot] Process Program State Changed Report - Created");

            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601, loadRecipeName);//ppRs.RECIPE.Ppid);

            Globalo.yamlManager.recipeData.vPPRecipeSpecEquip = Globalo.yamlManager.recipeData.RecipeLoad(Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName);

            Globalo.tcpManager.SendRecipeName(Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName);



            //레시피 값이 변경 저장되면 Test 프로그램으로 보내줘야된다.
            Http.HttpService.RecipeSend(0);     //Aoi pc1
            Http.HttpService.RecipeSend(1);     //Aoi pc2
            
            //Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601, selectedItem);


            //TcpSocket.EquipmentData sendEqipData = new TcpSocket.EquipmentData();
            //sendEqipData.Command = "APS_RECIPE_SAVE";
            //sendEqipData.DataID = selectedItem;
            //sendEqipData.CommandParameter = new List<TcpSocket.EquipmentParameterInfo>();
            //TcpSocket.EquipmentParameterInfo pInfo = new TcpSocket.EquipmentParameterInfo();
            ////
            //sendEqipData.CommandParameter.Add(new TcpSocket.EquipmentParameterInfo
            //{
            //    Name = "PP_STATE1",
            //    Value = Globalo.dataManage.mesData.m_dPPChangeArr[0].ToString()
            //});
            //sendEqipData.CommandParameter.Add(new TcpSocket.EquipmentParameterInfo
            //{
            //    Name = "PP_STATE2",
            //    Value = Globalo.dataManage.mesData.m_dPPChangeArr[1].ToString()
            //});


            //Globalo.tcpManager.SendMessageToClient(sendEqipData);
        }

        private void BTN_RECIPE_CLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RecipePopup_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ShowRecipeGrid();
            }
        }
    }
}
