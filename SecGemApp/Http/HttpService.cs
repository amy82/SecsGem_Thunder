using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecGemApp.Http
{
    public class HttpService
    {
        static string recipeFolder = @"D:\Recipes"; // 실제 경로로 바꾸세요
        private static CancellationTokenSource _HttpCts;
        private static HttpListener _listener;
        private static Task _serverTask;

        private static readonly string[] RecipeFiles = new string[]
        {
            "RecipeA.json", "RecipeB.json", "RecipeC.json"
        };
        public HttpService()
        {

        }
        public async static void RecipySend(int index)
        {
            var recipe = new
            {
                O_RING = "1",
                CONE = "0",
                KEYTYPE = "C",
                HEIGHT_LH_MIN = 10.5,
                HEIGHT_LH_MAX = 12.3,
                HEIGHT_MH_MIN = 10.5,
                HEIGHT_MH_MAX = 12.3,
                HEIGHT_RH_MIN = 10.5,
                HEIGHT_RH_MAX = 12.3,
                CONCENTRICITY_IN_MIN = 10.5,
                CONCENTRICITY_IN_MAX = 12.3,
                CONCENTRICITY_OUT_MIN = 10.5,
                CONCENTRICITY_OUT_MAX = 12.3,
                GASKET_MIN = 10.5,
                GASKET_MAX = 12.3,
                DENT_MIN = 10.5,
                DENT_MAX = 12.3
            };

            string json = JsonConvert.SerializeObject(recipe);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
                int port = 4000 + index;
                string url = $"http://127.0.0.1:{port}/set-recipe";
                var response = await client.PostAsync(url, content);
                //var response = await client.PostAsync("http://127.0.0.1:4001/set-recipe", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("레시피 전송 성공!");
                }
                else
                {
                    Console.WriteLine($"전송 실패: 상태 코드 {(int)response.StatusCode} {response.ReasonPhrase}");
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("서버 응답 내용: " + errorContent);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HTTP 요청 중 오류 발생: " + ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("요청 시간이 초과되었습니다: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("알 수 없는 예외: " + ex.Message);
            }
        }
        public static void Start()
        {
            _HttpCts = new CancellationTokenSource();
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://+:3001/"); // PC1 주소에서 8080 포트로 요청 받음
            _listener.Start();
            //Start에서 오류나면 관리자 권한으로 시작하거나
            //cmd 관리자 권한에서 실행하기=>netsh http add urlacl url=http://+:3001/ user=Everyone

            Console.WriteLine("HTTP 서버 시작됨: http://+:8080/");

            _serverTask = Task.Run(() => RunServerLoop(_HttpCts.Token));
        }
        private static async Task RunServerLoop(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var contextTask = _listener.GetContextAsync();

                    // 취소 가능하도록 Task.WhenAny 사용
                    var completedTask = await Task.WhenAny(contextTask, Task.Delay(-1, token));

                    if (completedTask == contextTask)
                    {
                        var context = contextTask.Result;
                        _ = Task.Run(() => HandleRequest(context)); // 비동기로 응답 처리
                    }
                    else
                    {
                        // 취소 요청됨
                        break;
                    }
                }
            }
            catch (HttpListenerException ex) when (ex.ErrorCode == 995 || ex.ErrorCode == 500)
            {
                Console.WriteLine("HTTP 리스너 중단됨.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("서버 루프 종료 요청 받음.");
            }
            finally
            {
                _listener?.Close();
            }

            Console.WriteLine("HTTP 서버 루프 종료.");
        }
        private static void HandleRequest(HttpListenerContext context)
        {
            string path = context.Request.Url.AbsolutePath;
            var query = context.Request.QueryString;

            if (path == "/recipes")
            {
                string json = JsonConvert.SerializeObject(RecipeFiles);
                byte[] buffer = Encoding.UTF8.GetBytes(json);
                context.Response.ContentType = "application/json";
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else if (path == "/req")
            {
                //aoi만 레시피 파일 있음.
                RecipySend(0);  //aoi pc1
                RecipySend(1);  //aoi pc2
            }
            else if (path == "/recipe")
            {
                string name = query["name"];

                if (!string.IsNullOrEmpty(name))
                {
                    string filePath = Path.Combine("C:\\Recipes", name); // 실제 레시피 폴더 경로

                    if (File.Exists(filePath))
                    {
                        string content = File.ReadAllText(filePath);
                        byte[] buffer = Encoding.UTF8.GetBytes(content);
                        context.Response.ContentType = "application/json";
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                        byte[] buffer = Encoding.UTF8.GetBytes("File not found..");
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            }
            else
            {
                context.Response.StatusCode = 404;
            }

            context.Response.Close();
        }
        public static async Task Stop()
        {
            if (_HttpCts != null)
            {
                Console.WriteLine("HTTP 서버 종료 중...");
                _HttpCts.Cancel();

                try
                {
                    await _serverTask;
                }
                catch (TaskCanceledException)
                {
                    // 무시해도 됨
                }

                _HttpCts.Dispose();
                _HttpCts = null;

                Console.WriteLine("HTTP 서버 종료 완료.");
            }
        }
    }


}
