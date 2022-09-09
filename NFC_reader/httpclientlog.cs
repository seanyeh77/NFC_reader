using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace NFC_reader
{
    public class httpclientlog
    {
        public static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7021/") };
        //public static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://userdatawebapi20220829195800.azurewebsites.net/") };
        public static List<UserLog> result = new List<UserLog>();
        public static async Task<List<UserLog>> Get()
        {
            try
            {
                //使用 async 方法從網路 url 上取得回應
                var response = await client.GetAsync("UserLog");
                //如果 httpstatus code 不是 200 時會直接丟出 expection
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsAsync<List<UserLog>>();
                return result;
            }
            catch
            {
                result = new List<UserLog>();
                return result;
            }
        }
        public static async Task<String> Post(UserLog userLog)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            string json = JsonConvert.SerializeObject(userLog);
            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
            // 發出 post 並取得結果
            HttpResponseMessage response = client.PostAsync("UserLog", contentPost).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "scuess";
            }
        }
    }
}
