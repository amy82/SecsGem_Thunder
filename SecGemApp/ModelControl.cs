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
    public partial class ModelControl : UserControl
    {
        public int selectedNo;
        public int ModelGridRowViewCount = 3;
        private int SelectedCellRow = 0;
        private int SelectedCellCol = 0;
        public ModelControl()
        {
            selectedNo = -1;
            InitializeComponent();
            InitModelGrid();
        }
        public string SelectedModelName()
        {
            if (SelectedCellRow < 0)
            {
                return "";
            }
            if (dataGridView_Model.RowCount < 1 || SelectedCellRow > dataGridView_Model.RowCount - 1)
            {
                return "";
            }
            string strData = dataGridView_Model.Rows[SelectedCellRow].Cells[0].Value.ToString();
            return strData;
        }
        private void InitModelGrid()
        {
            int i = 0;
            int j = 0;

            dataGridView_Model.ColumnHeadersVisible = false;
            dataGridView_Model.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView_Model.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; // 모든 셀에 맞게 자동 조정
            dataGridView_Model.AllowUserToResizeColumns = false;
            dataGridView_Model.AllowUserToResizeRows = false;
            dataGridView_Model.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 열 헤더의 크기 변경을 방지

            // 헤더의 기본 스타일을 설정하여 크기 조정 불가
            dataGridView_Model.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView_Model.ReadOnly = true;
            dataGridView_Model.MultiSelect = false;             // 여러 개 선택 불가능
            dataGridView_Model.AllowUserToAddRows = false;      // 빈 행 추가 방지

            //foreach (DataGridViewColumn column in dataGridView_Model.Columns)
            //{
            //    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    column.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}

            //dataGridView_Model.Columns.Add("", "Model");
            //dataGridView_Model.Rows.Add("aaaaaaaaaa1");
            //dataGridView_Model.Rows.Add("bbbbbbbbbb2");

            dataGridView_Model.ColumnHeadersHeight = 20;
            int _w = dataGridView_Model.Width - dataGridView_Model.ColumnHeadersHeight - 13;

            
            int GridRowHeight = 25;
            DataGridViewTextBoxColumn[] textColumns = new DataGridViewTextBoxColumn[1];
            textColumns[0] = new DataGridViewTextBoxColumn();
            textColumns[0].HeaderText = "No";
            dataGridView_Model.Columns.Add(textColumns[0]);

            for (i = 0; i < ModelGridRowViewCount; i++)
            {

                string text = $"예시 텍스트 {i}"; // 예시 텍스트 생성
                //bool isChecked = (i % 2 == 0); // 짝수인 경우 체크박스가 체크됨
                dataGridView_Model.Rows.Add(""); // 행 추가
                dataGridView_Model.Rows[i].Height = GridRowHeight;

                for (j = 0; j < dataGridView_Model.ColumnCount; j++)
                {
                    dataGridView_Model.Columns[j].Resizable = DataGridViewTriState.False;

                }
            }

            for (i = 0; i < dataGridView_Model.ColumnCount; i++)
            {
                //dataGridView_Model.Columns[i].Resizable = DataGridViewTriState.False;
                //dataGridView_Model.Columns[0].Width = _w;
            }

            dataGridView_Model.ScrollBars = ScrollBars.Vertical;      //가로 스크롤 안보이게 설정

            // 버튼 클릭 이벤트 등록
            dataGridView_Model.CellClick += new DataGridViewCellEventHandler(GridView_CellClick); //textbox 한번 클릭으로 바로 수정되게 추가
            dataGridView_Model.SelectionChanged += SelectionChanged;


            dataGridView_Model.Columns[0].DefaultCellStyle.BackColor = Color.LightGray; // 배경색 설정
            dataGridView_Model.Columns[0].DefaultCellStyle.ForeColor = Color.Yellow; // 배경색 설정
            dataGridView_Model.Columns[0].DefaultCellStyle.Font = new Font("나눔고딕", 9F, FontStyle.Bold); // 굵은 글씨
            dataGridView_Model.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 가운데 정렬

            //dataGridView_Model.ClearSelection();
        }
        private void GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            SelectedCellCol = e.ColumnIndex;
            SelectedCellRow = e.RowIndex;           //세로

            if (e.RowIndex < 0) return;

            int nRow = dataGridView_Model.RowCount;
            int dataCount = Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist.Count();

            if (e.ColumnIndex == 1 && SelectedCellRow < dataCount)
            {
                // 셀이 클릭되었을 때 편집 모드로 전환
                dataGridView_Model.CurrentCell = dataGridView_Model.Rows[e.RowIndex].Cells[e.ColumnIndex];

                dataGridView_Model.BeginEdit(true);
            }
        }
        private void SelectionChanged(object sender, EventArgs e)
        {
            // 현재 선택된 행의 인덱스 가져오기
            if (dataGridView_Model.SelectedCells.Count > 0)
            {
                // 첫 번째 선택된 셀을 가져오기
                DataGridViewCell selectedCell = dataGridView_Model.SelectedCells[0];

                // 행(Row) 인덱스
                int selectedRowIndex = selectedCell.RowIndex;

                // 열(Column) 인덱스
                int selectedColumnIndex = selectedCell.ColumnIndex;
                if (selectedColumnIndex == 0 || selectedRowIndex == Globalo.yamlManager.mesManager.MesData.SecGemData.ModelNo)
                {
                    dataGridView_Model.ClearSelection();
                }

            }
        }
        public void SetModelListView()
        {
            //materialListView1
            //materialListView1
            int i = 0;
            // MaterialListView 설정

            if(Globalo.yamlManager.mesManager.MesData == null)
            {
                Console.WriteLine($"[ModelControl] Globalo.yamlManager.mesManager.MesData: null");
                return;
            }
            int ModelCount = Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist.Count;
            if(ModelCount < 0)
            {
                
                return;
            }

            dataGridView_Model.Rows.Clear();


            int gridViewCount = ModelCount;
            if (gridViewCount < ModelGridRowViewCount)
            {
                gridViewCount = ModelGridRowViewCount;
            }
            string tempModel = "";
            for (i = 0; i < gridViewCount; i++)
            {
                if (i < ModelCount)
                {
                    tempModel = Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist[i];
                    dataGridView_Model.Rows.Add(tempModel);

                    //if (i == Globalo.yamlManager.mesManager.MesData.SecGemData.ModelNo)
                    if (tempModel == Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentModelName)
                    {
                        SelectedCellRow = i;
                        dataGridView_Model.CurrentCell = dataGridView_Model.Rows[i].Cells[0];  // 4번째 행, 3번째 열로 이동

                        dataGridView_Model.Rows[i].Cells[0].Style.BackColor = Color.YellowGreen; // 1번 열
                        dataGridView_Model.Rows[i].Cells[0].Style.ForeColor = Color.Black; // 1번 열
                        dataGridView_Model.Rows[i].Cells[0].Style.Font = new Font(dataGridView_Model.DefaultCellStyle.Font, FontStyle.Bold);
                    }
                    else
                    {
                        dataGridView_Model.Rows[i].Cells[0].Style.BackColor = Color.White; // 1번 열
                        dataGridView_Model.Rows[i].Cells[0].Style.ForeColor = Color.Black; // 1번 열
                        dataGridView_Model.Rows[i].Cells[0].Style.Font = new Font(dataGridView_Model.DefaultCellStyle.Font, FontStyle.Regular);
                    }
                }
                else
                {
                    dataGridView_Model.Rows.Add(""); // 행 추가
                    dataGridView_Model.Rows[i].Cells[0].Style.BackColor = Color.White; // 1번 열
                    dataGridView_Model.Rows[i].Cells[0].Style.ForeColor = Color.Black; // 1번 열
                    dataGridView_Model.Rows[i].Cells[0].Style.Font = new Font(dataGridView_Model.DefaultCellStyle.Font, FontStyle.Regular);
                }
            }
        }


        public void SetModelAdd()
        {

        }
        public void SetModelDel()
        {

        }
        public void SetModelRename()
        {
            //x
        }
        private void crownButton_Model_Change_Click(object sender, EventArgs e)
        {
            //모델 변경
            if (SelectedCellRow < 0)
            {
                return;
            }
            string strData = dataGridView_Model.Rows[SelectedCellRow].Cells[0].Value.ToString();
            string logStr = $"[{strData}]모델 변경 하시겠습니까 ?";

            MessagePopUpForm messagePopUp = new MessagePopUpForm("", "YES", "NO");
            messagePopUp.MessageSet(Globalo.eMessageName.M_ASK, logStr);
            DialogResult result = messagePopUp.ShowDialog();

            if (result != DialogResult.Yes)
            {
                return;
            }
            if (SelectedCellRow < Globalo.yamlManager.mesManager.MesData.SecGemData.Modellist.Count())
            {
                
                Globalo.yamlManager.mesManager.MesData.SecGemData.ModelNo = SelectedCellRow;
                Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentModelName = strData;
                Globalo.yamlManager.mesManager.MesSave();



                string logData = $"[Model] Change : {strData}";
                Globalo.LogPrint("Model", logData);

                //Host로 선택 모델명 보내기
                Globalo.tcpManager.SendModelName(Globalo.yamlManager.mesManager.MesData.SecGemData.CurrentModelName);

            }

            //Globalo.yamlManager.MesLoad();

            SetModelListView();
        }

        
    }
}
