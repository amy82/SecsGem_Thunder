using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecGemApp
{
    public class ThreadControl
    {
        private FThread.LogThread logThread;
        //private FThread.TimeThread timeThread;
        
        public FThread.AutoRunthread autoRunthread;


        //public FThread.ManualThread manualThread;

        public ThreadControl()
        {
            logThread = new FThread.LogThread();
            //timeThread = new FThread.TimeThread();
            autoRunthread = new FThread.AutoRunthread();

            // 이벤트 핸들러 등록



            //manualThread = new FThread.ManualThread();
        }
        public void LogAdd(string logstr)
        {
            logThread.logQueue.Enqueue(logstr);
        }
        public void AllThreadStart()
        {
            logThread.Start();
            //timeThread.Start();

            
        }
        public void AllClose()
        {
            logThread.Stop();
            //timeThread.Stop();
            autoRunthread.Stop();

            //manualThread.Stop();
            //manualThread.Stop();
        }
    }
}
