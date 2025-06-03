using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SecGemApp.FThread
{
    public class LogThread : BaseThread
    {
        private ListBox logListBox;
        //private Thread thread;
        //public bool threadLogRun = false;

        public DirectoryInfo dif = new DirectoryInfo(@"C:\logg"); // 디렉토리 경로
        public string Fpath = @"LOG.txt"; // 파일 경로 


        public Queue<string> logQueue = new Queue<string>();

        private readonly object writeLock = new object();

        public LogThread()
        {
            this.logListBox = Globalo.logControl.listBox1;
        }
        protected override void ThreadInit()
        {
            if (!dif.Exists) // 디렉토리 체크
            {
                dif.Create();
            }
            if (!Directory.Exists(Data.CPath.BASE_LOG_PATH))
            {
                Directory.CreateDirectory(Data.CPath.BASE_LOG_PATH);
            }
            if (!Directory.Exists(Data.CPath.BASE_LOG_CLIENT_PATH))
            {
                Directory.CreateDirectory(Data.CPath.BASE_LOG_CLIENT_PATH);
            }

        }
        
        //protected override void ProcessRun()
        protected override void ThreadRun()
        {
            //threadLogRun = true;
            
            lock (writeLock)
            {
                if (logQueue.Count > 0)
                {
                    DateTime currentDate = DateTime.Now;

                    string year = currentDate.ToString("yyyy");
                    string month = currentDate.ToString("MM");
                    string day = currentDate.ToString("dd");

                    string fileName = DateTime.Now.ToString("yyyyMMdd_HH") + "_client.log"; // 년월일_시간 형식 파일명

                    string basePath = Path.Combine(Data.CPath.BASE_LOG_CLIENT_PATH, year, month, day);

                    string fullPath = Path.Combine(basePath, fileName);

                    if (!Directory.Exists(basePath)) // 폴더가 존재하지 않으면
                    {
                        Directory.CreateDirectory(basePath); // 폴더 생성
                    }
                    try
                    {
                        using (StreamWriter fw = new StreamWriter(fullPath, append: true))
                        {
                            string LogInfo = logQueue.Dequeue();// + "\n";

                            if (this.logListBox != null)
                            {
                                if (this.logListBox.InvokeRequired)
                                {
                                    this.logListBox.Invoke(new MethodInvoker(delegate { this.logListBox.Items.Add(LogInfo); }));
                                    this.logListBox.Invoke(new MethodInvoker(delegate { this.logListBox.SelectedIndex = this.logListBox.Items.Count - 1; }));

                                }
                                else
                                {
                                    this.logListBox.Items.Add(LogInfo);
                                }
                            }
                            fw.WriteLine(LogInfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("파일 생성 오류: " + ex.Message);
                    }
                    
                }
            }
        }

        
    }
}
