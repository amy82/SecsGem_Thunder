using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecGemApp.TcpSocket
{
    public enum ClientSlotIndex
    {
        Tester1 = 5,    // IP 뒷자리 5  ex)192.168.100.1
        Tester2 = 6,    // IP 뒷자리 6  ex)192.168.100.2
        Tester3 = 7,    // IP 뒷자리 7
        Tester4 = 8,    // IP 뒷자리 8
        Tester5 = 1,    // IP 뒷자리 1
    }
    public class TcpServer
    {
        private TcpListener _listener;
        //private bool _isRunning;
        private bool bConnected;

        private bool[] bConnectedClient = new bool[10];

 
        private readonly TcpClient[] _clients = new TcpClient[5];       //eeprom verify 만 통신 - 착/완공받기
        private readonly Dictionary<int, ClientSlotIndex> ipToSlotIndex = new Dictionary<int, ClientSlotIndex>
        {
            { 5, ClientSlotIndex.Tester1 },     //여기 앞에 숫자를 ip주소 끝자리와 맞혀야 된다.
            { 6, ClientSlotIndex.Tester2 },
            { 7, ClientSlotIndex.Tester3 },
            { 8, ClientSlotIndex.Tester4 },
            { 1, ClientSlotIndex.Tester5 }
        };
        public event Func<string,int, Task> OnServerMessageReceivedAsync; // 비동기 이벤트

        public TcpServer(string ip, int port)
        {
            bConnected = false;
            _listener = new TcpListener(IPAddress.Any, port);//IPAddress.Parse(ip), port);

            for (int i = 0; i < 5; i++)
            {
                bConnectedClient[i] = false;
            }
            string logData = $"[tcp] Server Create:{ip} / {port}";
            Globalo.LogPrint("CCdControl", logData);
        }
        public bool bClientConnectedState(int index)
        {
            return bConnectedClient[index];
            //return bConnected;
        }
        // 🎯 **클라이언트로 메시지 보내는 함수**
        public async Task SendMessageAsync(TcpClient client, string message)
        {
            if(_clients.Length < 1)
            {
                return;
            }
            try
            {
                if (client != null && client.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await client.GetStream().WriteAsync(data, 0, data.Length);


                    //Console.WriteLine($"클라이언트에게 전송: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"메시지 전송 오류: {ex.Message}");
            }
        }
        // 🎯 **모든 클라이언트에게 메시지 보내는 함수**
        public async Task BroadcastMessageAsync(string message, int ClientNum = -1)
        {
            List<int> disconnectedClientKeys = new List<int>();
            int i = 0;

            for (i = 0; i < _clients.Length; i++)
            {
                if (ClientNum > -1)
                {
                    if (i != ClientNum)
                    {
                        continue;
                    }
                }
                var client = _clients[i];
                if (client != null && client.Connected)
                {
                    await SendMessageAsync(client, message);
                }
                else
                {
                    // 연결 끊긴 클라이언트 처리
                    bConnectedClient[i] = false;
                    _clients[i] = null;
                }
            }
        }
        // 서버 시작
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _listener.Start();
            Console.WriteLine("서버가 시작되었습니다.");
            string logData = $"[tcp] Server Start";
            Globalo.LogPrint("CCdControl", logData);
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (_listener.Pending()) // 대기 중인 연결이 있는지 확인
                    {
                        TcpClient client = await _listener.AcceptTcpClientAsync();

                        // 클라이언트 IP 주소 가져오기
                        IPEndPoint remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                        string clientIP = remoteIpEndPoint?.Address.ToString();

                        // 출력
                        
                        // IP 주소의 마지막 자리 추출
                        int lastOctet = -1;
                        if (!string.IsNullOrEmpty(clientIP))
                        {
                            string[] parts = clientIP.Split('.');
                            if (parts.Length == 4 && int.TryParse(parts[3], out int parsed))
                            {
                                lastOctet = parsed;
                                Console.WriteLine($"Client Connect IP: {clientIP},ID: {lastOctet}");
                            }
                        }
                        //_clientsList.Add(client); // 클라이언트 추가
                        //// _clientMap[lastOctet] = client;
                        // 클라이언트 접속 시 (IP 뒷자리 lastOctet)
                        int clientNo = -1;
                        if (ipToSlotIndex.TryGetValue(lastOctet, out ClientSlotIndex slot))
                        {
                            clientNo = (int)slot;
                            _clients[(int)slot] = client; // 배열 인덱스에 저장
                        }
                        else
                        {
                            // 정의되지 않은 IP 뒷자리 처리
                        }

                        if(clientNo == -1)
                        {
                            Console.WriteLine($"Client Connect Fail:{clientNo}");
                            return;
                        }
                        
                        bConnected = true;
                        bConnectedClient[clientNo] = true;
                        logData = $"[tcp] Client Connected";
                        Globalo.LogPrint("CCdControl", logData);

                        //if (clientNo == (int)ClientSlotIndex.SecsGem)
                        //{
                        //    //Globalo.MainForm.ClientConnected(true);
                        //}

                        _ = HandleClientAsync(client, clientNo, cancellationToken); // 클라이언트 연결 처리
                    }
                    await Task.Delay(100); // CPU 점유율을 낮추기 위해 약간의 대기
                }
            }
            catch (Exception ex)
            {
                bConnected = false;
                Console.WriteLine($"서버 예외 발생: {ex.Message}");
            }
        }
        
        // 클라이언트 연결 처리
        private async Task HandleClientAsync(TcpClient client, int clientIndex, CancellationToken cancellationToken)
        {
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                StringBuilder sb = new StringBuilder(); // 여러 개의 JSON 조각을 합치기 위한 StringBuilder
                try
                {
                    //while (true) // 연결이 유지되는 동안 계속 읽음
                    while (client.Connected)
                    {
                        bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);

                        // 서버에서 연결을 종료하면 종료
                        if (bytesRead == 0)
                        {
                            Console.WriteLine("서버와의 연결이 종료되었습니다.");
                            break;
                        }

                        string receivedChunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        sb.Append(receivedChunk); // JSON 조각을 합침

                        // JSON이 닫혔는지 확인 (한 개의 JSON이 완성되었는지 확인)
                        if (receivedChunk.TrimEnd().EndsWith("}"))
                        {
                            string receivedData = sb.ToString();
                            sb.Clear(); // StringBuilder 초기화 (다음 JSON 수신을 위해)

                            // 메시지 수신 이벤트 호출
                            // 메시지 수신 시 비동기 이벤트 호출
                            if (OnServerMessageReceivedAsync != null)
                            {
                                await OnServerMessageReceivedAsync.Invoke(receivedData, clientIndex);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"클라이언트 처리 중 예외 발생: {ex.Message}");
                }
            }

            // 클라이언트 연결 종료 시 배열에서 null 처리 및 소켓 닫기
            if (_clients[clientIndex] == client)
            {
                client.Close();
                _clients[clientIndex] = null;
                Console.WriteLine($"클라이언트 인덱스 {clientIndex} 연결 종료, 배열에서 제거");
            }

            bConnected = false;
            string logData = $"[tcp] Client DisConnected";
            Globalo.LogPrint("CCdControl", logData);
            //Globalo.MainForm.ClientConnected(false);
            Console.WriteLine("클라이언트 연결이 종료되었습니다.");
        }

        // 서버 중지
        public void Stop()
        {
            _listener.Stop();
            
            Console.WriteLine("서버가 중지되었습니다.");
        }
    }
}
