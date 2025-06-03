using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecGemApp
{
    static class Program
    {
        public static bool NORIN_MODE = false;            //TODO:  ★★테스트 모드 설정, 실행파일 배포 할 때 false로 변경
        public static string _Ver = "Ver. 1.0.0.1";
        public static string _Build = "25/06/03";

        [STAThread]
        static void Main()
        {
            if (!Debugger.IsAttached)
            {
                NORIN_MODE = false;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new MainForm());
            // 뮤텍스를 생성하여 애플리케이션이 이미 실행 중인지 확인
            using (Mutex mutex = new Mutex(true, Assembly.GetExecutingAssembly().GetName().Name, out bool isAppAlreadyRunning))
            {
                if (isAppAlreadyRunning)
                {
                    // 애플리케이션이 처음 실행될 때
                    Application.Run(new MainForm());
                }
                else
                {
                    // 이미 실행 중이면 사용자에게 메시지 표시
                    MessageBox.Show("이 애플리케이션은 이미 실행 중입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //aps 250311
        //aps start 250306
        // TODO : 설비 PC에서 SECS/GEM 정보를 받아야 해서 1.MES -> CLINET 접속 - > 설비 PC 순으로 접속 확인 코드 확인후 검사 진행하게 해야된다. 250309
    }
}
//250427 in home
//250427 in home tag 추가