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
    public partial class ReportControl : UserControl
    {
        public ReportControl()
        {
            InitializeComponent();
        }

        private void crownButton3_Click(object sender, EventArgs e)
        {
            string mid = metroTextBox4.Text;
            if (mid.Length < 1)
            {
                Globalo.LogPrint("[config]", $"[Report] 입력된 값이 없습니다. ", Globalo.eMessageName.M_WARNING);
                return;
            }

            Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialId = mid;

            Globalo.ubisamForm.EventReportSendFn(Ubisam.ReportConstants.MATERIAL_ID_REPORT_10713);

        }

        private void crownButton_Terminal_View_Click(object sender, EventArgs e)
        {
            Globalo.terminalMsgForm.Show();
        }
    }
}
