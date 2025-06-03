using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecGemApp.TcpSocket
{
    public class TcpLotTask
    {
        private CancellationTokenSource token ;


        public TcpLotTask()
        {
            

        }

        public void InitializeTask()
        {
            token = new CancellationTokenSource();
        }
        // ✅ 실행 중인지 확인하는 함수
        public bool IsRunning()
        {
            return !token.IsCancellationRequested;
        }
        // ✅ 실행 중인 작업 강제 종료
        public void StopTask()
        {
            token.Cancel();
        }

        
    }
}
