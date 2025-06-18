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
    public partial class SecsGemStatusControl : UserControl
    {
        private Label[] TesterLabel;
        public SecsGemStatusControl()
        {
            InitializeComponent();

            label_CommunicationStateVal.Text = "Not Communication";
            label_ControlStateVal.Text = "Equipment Offline";
            label_OperatorId.Text = "000000000001";
            label_EquipStatusVal.Text = "Disconnected";
            

            label_CommunicationStateVal.ForeColor = Color.White;
            label_ControlStateVal.ForeColor = Color.White;
            label_OperatorId.ForeColor = Color.White;
            label_EquipStatusVal.ForeColor = Color.White;

            label_CommunicationStateVal.Size = new Size(label_ControlStateVal.Size.Width, 23);
            label_ControlStateVal.Size = new Size(label_ControlStateVal.Size.Width, 23);
            label_OperatorId.Size = new Size(label_ControlStateVal.Size.Width, 23);
            label_EquipStatusVal.Size = new Size(label_ControlStateVal.Size.Width, 23);

            TesterLabel = new Label[] { label_Tester1, label_Tester2, label_Tester3, label_Tester4 };
        }

        public void Set_SecsGemC_State(string state, string state2, int Connected)
        {
            label_CommunicationStateVal.Text = state;
            label_ControlStateVal.Text = state2;
            if(Connected == 1)
            {
                label_CommunicationStateVal.BackColor = Globalo.ConnectColor;
                if (state2 == "EquipmentOffline")
                {
                    label_ControlStateVal.BackColor = Globalo.DisconnectColor;
                }
                else
                {
                    label_ControlStateVal.BackColor = Globalo.ConnectColor;
                }
            }
            else
            {
                label_CommunicationStateVal.BackColor = Globalo.DisconnectColor;
                label_ControlStateVal.BackColor = Globalo.DisconnectColor;
            }
        }
        public void Set_TesterConnected(int index, bool Connected = true)
        {
            int tNum = index;
            if (tNum > TesterLabel.Length - 1)
            {
                return;
            }
            if (Connected == true)
            {
                TesterLabel[tNum].BackColor = Color.Moccasin;
                TesterLabel[tNum].ForeColor = Color.Black;
            }
            else
            {
                TesterLabel[tNum].BackColor = Color.Transparent;
                TesterLabel[tNum].ForeColor = Color.White;
            }
        }
        public void Set_OperatorId(string opid)
        {
            label_OperatorId.Text = opid;
            Globalo.dataManage.mesData.m_sMesOperatorID = opid;

            Globalo.yamlManager.mesManager.MesData.SecGemData.OperatorId = opid;
            Globalo.yamlManager.mesManager.MesSave();
        }
        public void Set_Equip_State(string state, int Connected)
        {
            label_EquipStatusVal.Text = state;
            if (Connected == 1)
            {

                label_EquipStatusVal.BackColor = Globalo.ConnectColor;
            }
            else
            {
                label_EquipStatusVal.BackColor = Globalo.DisconnectColor;
            }
        }

        private void label_OperatorId_Click(object sender, EventArgs e)
        {
            InputForm inputForm = new InputForm("OPERATOR ID", "OPERATOR ID INPUT:", label_OperatorId.Text, 0);
            //
            DialogResult result = inputForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string OPID = inputForm.InputText;
                if (OPID.Length < 1)
                {
                    return;
                }
                Set_OperatorId(OPID);
                
            }
        }
    }
}
