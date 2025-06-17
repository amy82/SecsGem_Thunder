using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecGemApp
{
    public partial class RecipeControl : UserControl
    {
        public int selectedNo;
        public int RecipeGridRowViewCount = 3;
        private int SelectedCellRow = 0;
        private int SelectedCellCol = 0;
        public RecipeControl()
        {
            InitializeComponent();
            InitRecipeGrid();
        }
        public string SelectedRecipedName()
        {
            if (SelectedCellRow < 0)
            {
                return "";
            }
            if(dataGridView_Recipe.RowCount < 1 || SelectedCellRow > dataGridView_Recipe.RowCount - 1)
            {
                return "";
            }
            string strData = dataGridView_Recipe.Rows[SelectedCellRow].Cells[0].Value.ToString();
            return strData;
        }
        public void SetRecipeListView()
        {
            int i = 0;
            // MaterialListView 설정

            //if (Globalo.yamlManager.MesData == null)
            //{
            //    Console.WriteLine($"[ModelControl] Globalo.yamlManager.MesData: null");
            //    return;
            //}
            //int ModelCount = 0;// Globalo.yamlManager.MesData.SecGemData.Modellist.Count;
            int recipeCount = Globalo.yamlManager.recipeData.recipeInventory.recipeYamlFiles.Count();
            if (recipeCount < 0)
            {

                return;
            }

            dataGridView_Recipe.Rows.Clear();


            int gridViewCount = recipeCount;
            if (gridViewCount < RecipeGridRowViewCount)
            {
                gridViewCount = RecipeGridRowViewCount;
            }

            int recipeNoChk = -1;
            bool exists = Globalo.yamlManager.recipeData.recipeInventory.recipeYamlFiles.Contains(Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName);

            for (i = 0; i < gridViewCount; i++)
            {
                string _ppid = "";

                if (exists == false && i == 0)
                {
                    //리스트 중에 사용중인 레시피가 없다.
                    Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName = Globalo.yamlManager.recipeData.recipeInventory.recipeYamlFiles[0];
                    Globalo.yamlManager.mesManager.MesSave();
                }

                if (i < recipeCount)
                {
                    _ppid = Globalo.yamlManager.recipeData.recipeInventory.recipeYamlFiles[i];
                    dataGridView_Recipe.Rows.Add(_ppid);

                    if (_ppid == Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName)
                    {
                        SelectedCellRow = i;
                        dataGridView_Recipe.CurrentCell = dataGridView_Recipe.Rows[i].Cells[0];  // 4번째 행, 3번째 열로 이동

                        dataGridView_Recipe.Rows[i].Cells[0].Style.BackColor = Color.YellowGreen; // 1번 열
                        dataGridView_Recipe.Rows[i].Cells[0].Style.ForeColor = Color.Black; // 1번 열
                        dataGridView_Recipe.Rows[i].Cells[0].Style.Font = new Font(dataGridView_Recipe.DefaultCellStyle.Font, FontStyle.Bold);
                    }
                    else
                    {
                        dataGridView_Recipe.Rows[i].Cells[0].Style.BackColor = Color.White; // 1번 열
                        dataGridView_Recipe.Rows[i].Cells[0].Style.ForeColor = Color.Black; // 1번 열
                        dataGridView_Recipe.Rows[i].Cells[0].Style.Font = new Font(dataGridView_Recipe.DefaultCellStyle.Font, FontStyle.Regular);
                    }
                }
                else
                {
                    dataGridView_Recipe.Rows.Add(""); // 행 추가
                    dataGridView_Recipe.Rows[i].Cells[0].Style.BackColor = Color.White; // 1번 열
                    dataGridView_Recipe.Rows[i].Cells[0].Style.ForeColor = Color.Black; // 1번 열
                    dataGridView_Recipe.Rows[i].Cells[0].Style.Font = new Font(dataGridView_Recipe.DefaultCellStyle.Font, FontStyle.Regular);
                }
            }
        }

        private void InitRecipeGrid()
        {
            int i = 0;
            int j = 0;

            dataGridView_Recipe.ColumnHeadersVisible = false;
            dataGridView_Recipe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView_Recipe.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; // 모든 셀에 맞게 자동 조정
            dataGridView_Recipe.AllowUserToResizeColumns = false;
            dataGridView_Recipe.AllowUserToResizeRows = false;
            dataGridView_Recipe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 열 헤더의 크기 변경을 방지

            // 헤더의 기본 스타일을 설정하여 크기 조정 불가
            dataGridView_Recipe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView_Recipe.ReadOnly = true;
            dataGridView_Recipe.MultiSelect = false;             // 여러 개 선택 불가능
            dataGridView_Recipe.AllowUserToAddRows = false;      // 빈 행 추가 방지

            //foreach (DataGridViewColumn column in dataGridView_Model.Columns)
            //{
            //    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}

            //dataGridView_Model.Columns.Add("", "Model");
            //dataGridView_Model.Rows.Add("aaaaaaaaaa1");
            //dataGridView_Model.Rows.Add("bbbbbbbbbb2");

            dataGridView_Recipe.ColumnHeadersHeight = 20;
            int _w = dataGridView_Recipe.Width - dataGridView_Recipe.ColumnHeadersHeight - 13;


            int GridRowHeight = 25;
            DataGridViewTextBoxColumn[] textColumns = new DataGridViewTextBoxColumn[1];
            textColumns[0] = new DataGridViewTextBoxColumn();
            textColumns[0].HeaderText = "No";
            dataGridView_Recipe.Columns.Add(textColumns[0]);

            for (i = 0; i < RecipeGridRowViewCount; i++)
            {

                string text = $"예시 텍스트 {i}"; // 예시 텍스트 생성
                //bool isChecked = (i % 2 == 0); // 짝수인 경우 체크박스가 체크됨
                dataGridView_Recipe.Rows.Add(""); // 행 추가
                dataGridView_Recipe.Rows[i].Height = GridRowHeight;

                for (j = 0; j < dataGridView_Recipe.ColumnCount; j++)
                {
                    dataGridView_Recipe.Columns[j].Resizable = DataGridViewTriState.False;

                }
            }

            for (i = 0; i < dataGridView_Recipe.ColumnCount; i++)
            {
                //dataGridView_Model.Columns[i].Resizable = DataGridViewTriState.False;
                //dataGridView_Model.Columns[0].Width = _w;
            }


            dataGridView_Recipe.ScrollBars = ScrollBars.Vertical;      //가로 스크롤 안보이게 설정

            // 버튼 클릭 이벤트 등록
            dataGridView_Recipe.CellClick += new DataGridViewCellEventHandler(GridView_CellClick); //textbox 한번 클릭으로 바로 수정되게 추가
            dataGridView_Recipe.SelectionChanged += SelectionChanged;


            dataGridView_Recipe.Columns[0].DefaultCellStyle.BackColor = Color.LightGray; // 배경색 설정
            dataGridView_Recipe.Columns[0].DefaultCellStyle.ForeColor = Color.Yellow; // 배경색 설정
            dataGridView_Recipe.Columns[0].DefaultCellStyle.Font = new Font("나눔고딕", 9F, FontStyle.Bold); // 굵은 글씨
            dataGridView_Recipe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 가운데 정렬

            //dataGridView_Model.ClearSelection();
        }
        private void GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            SelectedCellCol = e.ColumnIndex;
            SelectedCellRow = e.RowIndex;           //세로

            if (e.RowIndex < 0) return;

            int nRow = dataGridView_Recipe.RowCount;
            int dataCount = Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist.Count();

            if (e.ColumnIndex == 1 && SelectedCellRow < dataCount)
            {
                // 셀이 클릭되었을 때 편집 모드로 전환
                dataGridView_Recipe.CurrentCell = dataGridView_Recipe.Rows[e.RowIndex].Cells[e.ColumnIndex];

                dataGridView_Recipe.BeginEdit(true);
            }
        }
        private void SelectionChanged(object sender, EventArgs e)
        {
            // 현재 선택된 행의 인덱스 가져오기
            if (dataGridView_Recipe.SelectedCells.Count > 0)
            {
                // 첫 번째 선택된 셀을 가져오기
                DataGridViewCell selectedCell = dataGridView_Recipe.SelectedCells[0];

                // 행(Row) 인덱스
                int selectedRowIndex = selectedCell.RowIndex;

                // 열(Column) 인덱스
                int selectedColumnIndex = selectedCell.ColumnIndex;
                //if (selectedColumnIndex == 0 || selectedRowIndex == Globalo.yamlManager.MesData.SecGemData.ModelNo)
                if (selectedColumnIndex == 0 || selectedCell.Value.ToString() == Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName)
                {
                    dataGridView_Recipe.ClearSelection();
                }

            }
        }

        private void crownButton_Recipe_Change_Click(object sender, EventArgs e)
        {
            //레시피 변경
            if (SelectedCellRow < 0)
            {
                return;
            }

            string strData = dataGridView_Recipe.Rows[SelectedCellRow].Cells[0].Value.ToString();

            string logStr = $"[{strData}] 레시피 변경 하시겠습니까 ?";

            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");
            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, logStr);
            DialogResult result = messagePopUp.ShowDialog();

            if (result != DialogResult.Yes)
            {
                return;
            }
            if (SelectedCellRow < Globalo.yamlManager.recipeData.recipeInventory.recipeYamlFiles.Count())
            {
                if (strData.Length < 1)
                {
                    return;
                }


                Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName = strData;
                Globalo.yamlManager.mesManager.MesSave();

                string logData = $"[Recipe] Change : {strData}";
                Globalo.LogPrint("Recipe", logData);

                //tester로 레시피명 , 파라미터 보내기
                Globalo.tcpManager.SendRecipeName(Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName);

                Globalo.yamlManager.recipeData.vPPRecipeSpecEquip = Globalo.yamlManager.recipeData.RecipeLoad(Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentRecipeName);


                Http.HttpService.RecipeSend(1);     //Aoi pc1
                Http.HttpService.RecipeSend(2);     //Aoi pc2
            }
            SetRecipeListView();
        }

        private void crownButton_Recipe_View_Click(object sender, EventArgs e)
        {
            //string selectedItem = textBox_Recipe.Text;


            string selectedItem = dataGridView_Recipe.Rows[SelectedCellRow].Cells[0].Value.ToString();
            Dlg.RecipePopup recipePopup = new Dlg.RecipePopup(selectedItem);
            recipePopup.ShowDialog();
        }
    }
}
