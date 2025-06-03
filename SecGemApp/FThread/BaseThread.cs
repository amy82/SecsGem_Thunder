using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecGemApp.FThread
{
    public class BaseThread
    {
        protected Thread thread;
        protected bool m_bPause = false;
        protected CancellationTokenSource cts;
        public event Action<bool> ThreadCompleted; // 쓰레드 종료 이벤트
        private bool _result;

        public int threadCount = 0;

        public BaseThread()
        {
            thread = null;
            cts = null;
        }

        protected virtual void ThreadInit() { }
        protected virtual void ThreadDestructor() { }
        protected virtual void ThreadRun(){ }

        private void ProcessRun()
        {
            try
            {
                ThreadInit();
                while (!cts.Token.IsCancellationRequested)
                {
                    if (!m_bPause)
                    {
                        ThreadRun();
                    }
                    Thread.Sleep(Globalo.BASE_THREAD_INTERVAL);
                }

                ThreadDestructor();
                _result = true;
            }
            catch (ThreadAbortException e)
            {
                ThreadDestructor();
                // Console.WriteLine("Thread - caught ThreadAbortException - resetting.");
                Console.WriteLine("Exception message: {0}", e.Message);
                _result = false;
            }
            finally
            {
                cts = null;
                thread = null;
                //Console.WriteLine("ProcessRun mesfinallysage");
            }


            Console.WriteLine("BaseThread ProcessRun 종료됨.");
            ThreadCompleted?.Invoke(_result); // 쓰레드 종료 시 이벤트 호출
        }


        public void Pause()
        {
            m_bPause = true;
        }
        
        
        public bool Start()
        {
            try
            {
                if (thread == null)
                {

                    cts = null;
                    cts = new CancellationTokenSource();
                    //Console.WriteLine("Thread Start #1.");
                    threadCount++;
                    thread = new Thread(() => ProcessRun());//cts.Token));
                    thread.Start();
                    //Console.WriteLine("Thread Start #2.");
                }
                else
                {
                    if (m_bPause == false)
                    {
                        if (thread.IsAlive)
                        {
                            Console.WriteLine($"==Start Fail : thread.IsAlive {thread.IsAlive}");
                            return false;
                        }
                        else if (thread != null)
                        {
                            Console.WriteLine($"==Start Fail : thread not null");
                            return false;
                        }
                        else
                        {
                            bool Rtn = thread.Join(100);  // 쓰레드가 종료될 때까지 기다림
                            if (Rtn)
                            {
                                cts = null;
                                cts = new CancellationTokenSource();
                                thread = null;  // 종료 후 thread를 null로 설정
                                thread = new Thread(() => ProcessRun());// cts.Token));
                                thread.Start();
                            }
                            else
                            {
                                Abort();
                                return false;
                            }
                        }
                        
                    }
                    else
                    {
                        Resume();   // 정지 상태일 때만 실행
                        return true;
                    }
                }
                
                
            }
            catch (ThreadStateException ex)
            {
                Console.WriteLine($"[ERR] ThreadStateException: {ex.Message}");
                return false;
            }
            return true;
        }
        public void Stop()
        {
            if (thread != null && cts != null)
            {
                //Console.WriteLine("Base Thread Stop() #1");
                
                cts.Cancel();
                m_bPause = false;       //일시정지 해제 cts.Cancel 보다 m_bPause를 먼저하면 ThreadRun 에서 일시 정지로 빠진다.

                //Console.WriteLine("Base Thread Stop() #End");
            }
        }
       
        public bool GetThreadRun()
        {
            if (thread != null)
            {
                Console.WriteLine($"GetThreadRun() : {thread.IsAlive}");

                return thread.IsAlive;    //thread 동작 중
            }
            return false;
            //return thread?.IsAlive ?? false;  // thread가 null이면 false 반환
        }

        public bool GetThreadPause()
        {
            return m_bPause;
        }
        private void Resume()
        {
            m_bPause = false;
            Console.WriteLine("thread Resume call");
        }
        private void Abort()
        {
            thread.Abort();
            Console.WriteLine("thread Abort call");
        }
    }
}
