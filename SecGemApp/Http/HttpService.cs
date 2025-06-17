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
            Event.EventManager.PgExitCall += OnPgExitCall;
        }
        private void OnPgExitCall(object sender, EventArgs e)
        {
            //await HttpService.Stop();
            _ = HttpService.Stop();
        }
        //Apd Report
        //Tester 1  ===>  SecsGem
        //Tester 2  ===>  SecsGem
        public async static void RecipeSend(int index)      //aoi Tester 프로그램에서 레시피 요청시 대응
        {
            //Globalo.yamlManager.recipeData.vPPRecipeSpecEquip

            var recipe = new
            {
                NAME = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.Ppid,
                O_RING = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["O_RING"].value,
                CONE = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["CONE"].value,
                KEYTYPE = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["KEYTYPE"].value,
                HEIGHT_LH_MIN = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["HEIGHT_LH_MIN"].value,
                HEIGHT_LH_MAX = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["HEIGHT_LH_MAX"].value,
                HEIGHT_MH_MIN = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["HEIGHT_MH_MIN"].value,
                HEIGHT_MH_MAX = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["HEIGHT_MH_MAX"].value,
                HEIGHT_RH_MIN = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["HEIGHT_RH_MIN"].value,
                HEIGHT_RH_MAX = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["HEIGHT_RH_MAX"].value,
                CONCENTRICITY_IN_MIN = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["CONCENTRICITY_IN_MIN"].value,
                CONCENTRICITY_IN_MAX = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["CONCENTRICITY_IN_MAX"].value,
                CONCENTRICITY_OUT_MIN = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["CONCENTRICITY_OUT_MIN"].value,
                CONCENTRICITY_OUT_MAX = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["CONCENTRICITY_OUT_MAX"].value,
                GASKET_MIN = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["GASKET_MIN"].value,
                GASKET_MAX = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["GASKET_MAX"].value,
                DENT_MIN = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["DENT_MIN"].value,
                DENT_MAX = Globalo.yamlManager.recipeData.vPPRecipeSpecEquip.RECIPE.ParamMap["DENT_MAX"].value
            };

            string json = JsonConvert.SerializeObject(recipe);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
                //PC Ip:
                //Handler : 192.168.100.100
                //SecsGem : 192.168.100.100
                //Verify Handler = 192.168.100.101

                //Tester Pc : 192.168.100.{1, 2, 3, 4, 5, 6, 7, 8}

                //검사 PC Port: 4001로 전부 고정하면될듯

                string url = string.Empty;
                int port = 4001;    //SecsGem ------->>>  T Pg 4001
                if (Program.NORIN_MODE == true)
                {
                    url = $"http://192.168.100.{index}:{port}/set-recipe";
                }
                else
                {
                    url = $"http://127.0.0.1:{port}/set-recipe";
                }

                Console.WriteLine($"Recipe Send : {url}");


                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Recipe Send Complete!");
                }
                else
                {
                    Console.WriteLine($"Recipe Send Fail: ErrCode {(int)response.StatusCode} {response.ReasonPhrase}");
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Server Response Content: " + errorContent);
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

            Console.WriteLine("Http Listener Start: http://+:8080/");

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
            else if (path == "/ObjectReport")   //Return : /LotValidation  으로 리턴
            {
                //검사 pc에서 Object 보고 들어오는 곳 :
                //EEprom Verify 공정에서만 들어올 듯 , 다른 공정은 Handler에서 바코드 스캔하면서 바로 보냄
                //착공하고 결과를 다시 verify 공정으로 보내줘야될듯 
            }
            else if (path == "/ApdReport")
            {
                //검사 pc에서 Apd 보고 들어오는 곳
                //Lot Processing Completed Processing 받고 , Handler로 결과 전송?

                using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    string body = reader.ReadToEnd();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);

                    foreach (var pair in data)
                    {
                        Data.ApdData apddata = new Data.ApdData();
                        string key = pair.Key;
                        object value = pair.Value;

                        apddata.DATANAME = pair.Key;
                        apddata.DATAVALUE = pair.Value.ToString();
                        Globalo.dataManage.mesData.vMesApdData.Add(apddata);

                        Console.WriteLine($"{key} = {value}");
                    }
                    //Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap["O_RING"].value = data["O_RING"].ToString();


                }

            }
            else if (path == "/reqRecipe")
            {
                //aoi만 레시피 파일 있음.
                RecipeSend(1);  //aoi pc1
                RecipeSend(2);  //aoi pc2
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
