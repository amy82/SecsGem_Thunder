using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SecGemApp.TcpSocket
{
    public class TcpClientHandler   //검사 프로그램과 연결
    {
        private readonly SynchronizationContext _syncContext;
        private readonly TcpManager _tcpManager;  // TcpManager 참조

        private TcpClient _client;
        private NetworkStream _stream;
        private bool bConnected;
        private readonly string _ip;
        private readonly int _port;

        //public event Action<string> OnMessageReceived;
        public event Func<string, Task> OnMessageReceivedAsync; // 비동기 이벤트

        public event Action OnDisconnected;
        public event Action OnReconnecting;

        private CancellationTokenSource _cancellationTokenSource;// = new CancellationTokenSource();
        private bool _isReconnecting = false;
        private readonly int _reconnectInterval = 10000; // 재접속 시도 간격 (밀리초)
        private System.Timers.Timer _reconnectTimer;


        public TcpClientHandler(string ip, int port, TcpManager manager)
        {
            _tcpManager = manager;
            _syncContext = SynchronizationContext.Current;
            _ip = ip;
            _port = port;
            bConnected = false;

            Event.EventManager.PgExitCall += OnPgExitCall;
            InitializeReconnectTimer();
        }
        private void OnPgExitCall(object sender, EventArgs e)
        {
            // 이벤트 처리
            Console.WriteLine("TcpClientHandler - OnPgExitCall");
            if (_reconnectTimer != null)
            {
                _reconnectTimer.Stop();
                _reconnectTimer.Dispose();
                _reconnectTimer = null;
            }
        }
        public bool bHostConnectedState()
        {
            return bConnected;
        }
        public bool Connect()
        {
            try
            {
                _isReconnecting = false;
                _reconnectTimer.Stop();
                _client = new TcpClient(_ip, _port);
                _stream = _client.GetStream();
                _cancellationTokenSource = new CancellationTokenSource();

                //StartListening();
                newStartListening(_client);
                _syncContext.Send(_ =>
                {
                    bConnected = true;
                    Globalo.secsGemStatusControl.Set_Equip_State("Connected", 1);
                }, null);


                _tcpManager.CmdPPid();
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                StartReconnecting();
                return false;
            }
        }
        public void Disconnect(bool retry = false)
        {
            try
            {
                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Cancel();
                }

                _stream?.Close();
                _client?.Close();
                OnDisconnected?.Invoke();
                if (retry)
                {
                    StartReconnecting();
                }
                else
                {
                    StopReconnecting();
                }
                _syncContext.Send(_ =>
                {
                    bConnected = false;
                    Globalo.secsGemStatusControl.Set_Equip_State("Disconnected", 0);
                }, null);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnect failed: {ex.Message}");
                StartReconnecting();
            }
            
            
        }
        // 데이터 전송 메서드
        public async Task SendDataAsync(string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);

            // 데이터 전송
            await _stream.WriteAsync(buffer, 0, buffer.Length);
            Console.WriteLine("데이터가 서버로 전송되었습니다.");

            //using (TcpClient client = new TcpClient(ip, port))
            //{
            //    using (NetworkStream stream = client.GetStream())
            //    {
            //        byte[] buffer = Encoding.UTF8.GetBytes(data);

            //        // 데이터 전송
            //        await stream.WriteAsync(buffer, 0, buffer.Length);
            //        Console.WriteLine("데이터가 서버로 전송되었습니다.");
            //    }
            //}
        }
        public void SendMessage(string message)
        {
            if (_stream == null || !_client.Connected) return;

            byte[] data = Encoding.UTF8.GetBytes(message);

            _stream.Write(data, 0, data.Length);
        }
        private async void newStartListening(TcpClient client)
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            StringBuilder sb = new StringBuilder(); // 여러 개의 JSON 조각을 합치기 위한 StringBuilder

            try
            {
                while (client.Connected)
                {
                    bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, _cancellationTokenSource.Token);

                    // CancellationToken이 취소되었는지 체크
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        Console.WriteLine("취소 요청이 발생했습니다.");
                        break;
                    }

                    if (bytesRead > 0)
                    {
                        string receivedChunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        sb.Append(receivedChunk); // JSON 조각을 합침

                        // JSON이 닫혔는지 확인 (한 개의 JSON이 완성되었는지 확인)
                        if (receivedChunk.TrimEnd().EndsWith("}"))
                        {
                            string receivedData = sb.ToString();
                            sb.Clear(); // StringBuilder 초기화 (다음 JSON 수신을 위해)

                            // 메시지 수신 시 비동기 이벤트 호출
                            if (OnMessageReceivedAsync != null)
                            {
                                await OnMessageReceivedAsync.Invoke(receivedData);
                            }
                        }
                    }
                    else
                    {
                        // ⚠️ 서버가 연결을 끊었을 때
                        Console.WriteLine("서버에서 연결을 끊었습니다.");
                        Disconnect(true);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving data: {ex.Message}");
                Disconnect(true);
            }
            
        }
        private async void StartListening()
        {
            byte[] buffer = new byte[1024];
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, _cancellationTokenSource.Token);

                    // CancellationToken이 취소되었는지 체크
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        Console.WriteLine("취소 요청이 발생했습니다.");
                        break;
                    }

                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        //OnMessageReceived?.Invoke(receivedData);


                        // ✅ 메시지 수신 시 비동기 이벤트 호출
                        if (OnMessageReceivedAsync != null)
                        {
                            await OnMessageReceivedAsync.Invoke(receivedData);
                        }
                    }
                    else
                    {
                        // ⚠️ 서버가 연결을 끊었을 때
                        Console.WriteLine("서버에서 연결을 끊었습니다.");
                        Disconnect(true);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error receiving data: {ex.Message}");
                    Disconnect(true);
                    break;
                }
            }
        }
        private void InitializeReconnectTimer()
        {
            _reconnectTimer = new System.Timers.Timer(_reconnectInterval);
            _reconnectTimer.Elapsed += (sender, e) => AttemptReconnect();
            _reconnectTimer.AutoReset = true;
        }
        private void StartReconnecting()
        {
            if (_isReconnecting)
            {
                return;
            }

            _isReconnecting = true;
            OnReconnecting?.Invoke();
            if (_reconnectTimer != null)
            {
                _reconnectTimer.Start();
            }
            
        }
        private void StopReconnecting()
        {
            if (_reconnectTimer != null)
            {
                _reconnectTimer.Stop();
            }
                
        }
        private void AttemptReconnect()
        {
            Console.WriteLine($"Attempting to reconnect to {_ip}:{_port}...");
            if (Connect())
            {
                Console.WriteLine($"Attempting to reconnect to Ok");

                
                return;
            }
            Console.WriteLine($"Attempting to reconnect to Fail");
        }
    }
}
