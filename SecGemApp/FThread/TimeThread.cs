using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecGemApp.FThread
{
    public class TimeThread : BaseThread
    {
        private Label timeLabel;
        //private Thread thread;
        public bool threadTimeRun = false;
        private DateTime dTime;
        private string sTime = "";
        public TimeThread()
        {
            timeLabel = new Label();
            //this.timeLabel = Globalo.MainForm.TimeLabel;
            // thread = new Thread(Run);
        }
        //protected override void ProcessRun()

        protected override void ThreadInit()
        {

        }
        protected override void ThreadRun()
        {
            dTime = DateTime.Now;
            sTime = $"{dTime:hh : mm : ss}";
            if (this.timeLabel.InvokeRequired)
            {
                //timeLabel.Invoke(new TimeTextCallback(setTimeLabel), sTime);        //<--사용가능 #1
                this.timeLabel.Invoke(new MethodInvoker(delegate { setTimeLabel(sTime); }));
                //TimeLabel.Invoke(new MethodInvoker(delegate { TimeLabel.Text = sTime; }));//<--사용가능 #2

            }

            Thread.Sleep(100);

        }
        public void setTimeLabel(string sTime)
        {
            timeLabel.Text = sTime;
        }
       
    }
}
