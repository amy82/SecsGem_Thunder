using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace SecGemApp.Data
{
    public class CSFRInfo
    {
        private int i;
        private double dOffsetX, dOffsetY;
        public int[] m_nSizeX = new int[Globalo.CHART_ROI_COUNT];
        public int[] m_nSizeY = new int[Globalo.CHART_ROI_COUNT];
        public Point[] m_clPtOffset = new Point[Globalo.CHART_ROI_COUNT];
        public Rectangle[] m_clRectCircle = new Rectangle[Globalo.CHART_ROI_COUNT];

        public int[] m_MTF_Direction = new int[Globalo.MTF_ROI_COUNT];
        public void Init(int _w, int _h)
        {
            for (i = 0; i < Globalo.CHART_ROI_COUNT; i++)
            {
                m_nSizeX[i] = 80;
                m_nSizeY[i] = 80;
                switch (i)
                {
                    case 0: dOffsetX = 2.15; dOffsetY = 2.31; break;
                    case 1: dOffsetX = 4.25; dOffsetY = 4.82; break;
                    case 2: dOffsetX = 1.38; dOffsetY = 4.82; break;
                    case 3: dOffsetX = 1.38; dOffsetY = 1.53; break;
                    case 4: dOffsetX = 4.25; dOffsetY = 1.53; break;
                    case 5: dOffsetX = 1.51; dOffsetY = 2.41; break;
                    case 6: dOffsetX = 3.20; dOffsetY = 2.41; break;
                    case 7: dOffsetX = 19.2; dOffsetY = 20.6; break;
                    case 8: dOffsetX = 1.15; dOffsetY = 19.0; break;
                    case 9: dOffsetX = 1.15; dOffsetY = 1.27; break;
                    case 10: dOffsetX = 20.9; dOffsetY = 1.28; break;
                    case 11: dOffsetX = 2.19; dOffsetY = 20.6; break;
                    case 12: dOffsetX = 2.19; dOffsetY = 1.23; break;
                        /*case 13:	dOffsetX = 5.61;	dOffsetY = 1.51;	break;
                        case 14:	dOffsetX = 11.1;	dOffsetY = 2.41;	break;
                        case 15:	dOffsetX = 19.2;	dOffsetY = 20.6;	break;
                        case 16:	dOffsetX = 1.15;	dOffsetY = 19.0;	break;
                        case 17:	dOffsetX = 1.15;	dOffsetY = 1.27;	break;
                        case 18:	dOffsetX = 20.9;	dOffsetY = 1.28;	break;*/
                }

                m_clPtOffset[i].X = (int)((double)_w / dOffsetX);
                m_clPtOffset[i].Y = (int)((double)_h / dOffsetY);
            }
        }
        public CSFRInfo()
        {

        }

    }
    public class WorkData
    {
        //private static Data uniqueInstance = null;
        private static readonly object padlock = new object();
        public bool bConnected = false;

        public uint[] uInData;
        public uint[] uOutData;

        public int[] ArrInModuleCh;
        public int[] ArrOutModuleCh;
        public int dTotalReadModuleCount = 0;   //in io 모듈 총 개수
        public int dTotalOutModuleCount = 0;   //out io 모듈 총 개수

        public int dCurReadModuleCh = 0;     //현재 in io 채널
        public int dCurOutModuleCh = 0;     //현재 in io 채널

        public const int BUFF_SIZE = 10;

        public DataTable dataTable = new DataTable();
        public CSFRInfo m_clSfrInfo = new CSFRInfo();

        public Rectangle[] m_CircleP = new Rectangle[4];
        //private Form MainDlg;
        public WorkData()
        {
           
        }
        public void DataLoad()
        {
            //int i = 0;
            using (StreamReader reader = File.OpenText(CPath.BASE_ENV_PATH + "\\Data.toml"))
            {
                // Parse the table
                //TomlTable table = TOML.Parse(reader);
                ////
                //TomlNode node = table["CCD"]["SfrPosX"];
                //for (i = 0; i < node.ChildrenCount; i++)
                //{
                //    Debug.WriteLine(node[i]);
                //    m_clSfrInfo.m_clPtOffset[i].X = (int)node[i];
                //}
                //node = table["CCD"]["SfrPosY"];
                //for (i = 0; i < node.ChildrenCount; i++)
                //{
                //    Debug.WriteLine(node[i]);
                //    m_clSfrInfo.m_clPtOffset[i].Y = (int)node[i];
                //}

                //node = table["CCD"]["SfrSizeX"];
                //for (i = 0; i < node.ChildrenCount; i++)
                //{
                //    Debug.WriteLine(node[i]);
                //    m_clSfrInfo.m_nSizeX[i] = (int)node[i];
                //}
                //node = table["CCD"]["SfrSizeY"];
                //for (i = 0; i < node.ChildrenCount; i++)
                //{
                //    Debug.WriteLine(node[i]);
                //    m_clSfrInfo.m_nSizeY[i] = (int)node[i];
                //}

                //
                //
                //원형마크
                //node = table["CCD"]["CirClePosX"];
                //for (i = 0; i < node.ChildrenCount; i++)
                //{
                //    m_CircleP[i].X = (int)node[i];
                //}
                //node = table["CCD"]["CirClePosY"];
                //for (i = 0; i < node.ChildrenCount; i++)
                //{
                //    m_CircleP[i].Y = (int)node[i];
                //}
                //node = table["CCD"]["CirCleSizeX"];
                //for (i = 0; i < node.ChildrenCount; i++)
                //{
                //    m_CircleP[i].Width = (int)node[i];
                //}
                //node = table["CCD"]["CirCleSizeY"];
                //for (i = 0; i < node.ChildrenCount; i++)
                //{
                //    m_CircleP[i].Height = (int)node[i];
                //}
            }
        }
        public void DataSave()
        {
           // int i = 0;
            //TomlNode[] mNode1 = new TomlNode[4];
            //TomlNode[] mCircleNode = new TomlNode[4];
            //for (i = 0; i < 4; i++)
            //{
            //    mNode1[i] = new TomlNode[oGlobal.CHART_ROI_COUNT];
            //    mCircleNode[i] = new TomlNode[4];
            //}

            //for (i = 0; i < Globalo.CHART_ROI_COUNT; i++)
            //{
            //    mNode1[0][i] = m_clSfrInfo.m_clPtOffset[i].X;
            //    mNode1[1][i] = m_clSfrInfo.m_clPtOffset[i].Y;
            //    mNode1[2][i] = m_clSfrInfo.m_nSizeX[i];
            //    mNode1[3][i] = m_clSfrInfo.m_nSizeY[i];
            //}
            //for (i = 0; i < 4; i++)
            //{
            //    mCircleNode[0][i] = m_CircleP[i].X;
            //    mCircleNode[1][i] = m_CircleP[i].Y;
            //    mCircleNode[2][i] = m_CircleP[i].Width;
            //    mCircleNode[3][i] = m_CircleP[i].Height;
            //}

            //TomlTable toml = new TomlTable
            //{
            //    ["title"] = "CHART ROI SET",
            //    ["CCD"] = new TomlTable
            //    {
            //        IsInline = false,
            //        ["SfrPosX"] = mNode1[0],
            //        ["SfrPosY"] = mNode1[1],
            //        ["SfrSizeX"] = mNode1[2],
            //        ["SfrSizeY"] = mNode1[3],
            //        ["CirClePosX"] = mCircleNode[0],
            //        ["CirClePosY"] = mCircleNode[1],
            //        ["CirCleSizeX"] = mCircleNode[2],
            //        ["CirCleSizeY"] = mCircleNode[3]
            //    }
            //};


            //using (StreamWriter writer = File.CreateText(CPath.BASE_DATA_PATH + "\\Data.toml"))
            //{
            //    toml.WriteTo(writer);
            //    // Remember to flush the data if needed!
            //    writer.Flush();
            //}
        }
    }
}
