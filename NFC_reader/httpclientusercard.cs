using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NFC_reader
{
    public class httpclientusercard
    {
        public static string err = "";
        //public static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7021/") };
        public static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://userdatawebapi20220502202651.azurewebsites.net/") };
        public static UserData result = new UserData();
        public static async Task<UserData> Get(string UID)
        {
            try
            {
                //使用 async 方法從網路 url 上取得回應
                var response = await client.GetAsync($"UserCard/{UID}");
                err = await response.Content.ReadAsStringAsync();
                //如果 httpstatus code 不是 200 時會直接丟出 expection
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsAsync<UserData>();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public static async Task<string> count(int ID)
        {
            try
            {
                //使用 async 方法從網路 url 上取得回應
                var response = await client.GetAsync($"UserCard/cardcount/{ID}");
                err = await response.Content.ReadAsStringAsync();
                //如果 httpstatus code 不是 200 時會直接丟出 expection
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }
        public static async Task<String> Post(UserCard userCard)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            string json = JsonConvert.SerializeObject(userCard);
            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
            // 發出 post 並取得結果
            HttpResponseMessage response = client.PostAsync("UserCard", contentPost).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "scuess";
            }
        }
        public static async Task<string> delete(string UID)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            // 發出 post 並取得結果
            HttpResponseMessage response = client.DeleteAsync("UserCard/" + UID).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                string err = await response.Content.ReadAsStringAsync();
                return err;
            }
            else
            {
                return "scuess";
            }
        }
    }
}
