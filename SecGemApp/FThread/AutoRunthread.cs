using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecGemApp.FThread
{
    public class AutoRunthread : BaseThread
    {
        //public event delLogSender eLogSender;       //외부에서 호출할때 사용
        


        private Process.LotProcess lotProcess = new Process.LotProcess();
        public AutoRunthread()
        {
            //thread = null;
        }
        protected override void ThreadInit()
        {
            Globalo.tcpManager.IdleStateChange(true);       //ThreadInit
        }
        protected override void ThreadDestructor()
        {
            //여기서 idle보내면 될듯
            Globalo.tcpManager.IdleStateChange(true);       //ThreadDestructor
        }


        protected override void ThreadRun()
        {
            if (Globalo.taskWork.m_nCurrentStep >= Globalo.taskWork.m_nStartStep && Globalo.taskWork.m_nCurrentStep < Globalo.taskWork.m_nEndStep)
            {

                if (Globalo.taskWork.m_nCurrentStep >= 100 && Globalo.taskWork.m_nCurrentStep < 1000)
                {
                    Globalo.taskWork.m_nCurrentStep = lotProcess.ObjectIdProcessAsync(Globalo.taskWork.m_nCurrentStep);
                }
                else if (Globalo.taskWork.m_nCurrentStep >= 1000 && Globalo.taskWork.m_nCurrentStep < 2000)
                {
                    Globalo.taskWork.m_nCurrentStep = lotProcess.ApdProcessAsync(Globalo.taskWork.m_nCurrentStep);
                }

            }
            else
            {
                
                //Globalo.MainForm.StopAutoProcess();
                // TODO:   STOP 버튼 함수 살려야된다. 250219 왜? 250220
                //cts.Cancel();
                Stop();
            }
        }

    }
}
