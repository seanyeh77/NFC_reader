using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Net.Http.Headers;

namespace NFC_reader
{
    public class httpclientdata
    {
        public static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7021/") };
        //public static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://userdatawebapi20220502202651.azurewebsites.net/") };
        public static List<UserData> result = new List<UserData>();
        public static UserData userdata = new UserData();
        public static async Task<List<UserData>> GetAll()
        {
            try
            {
                //使用 async 方法從網路 url 上取得回應
                var response = await client.GetAsync("UserData");
                //如果 httpstatus code 不是 200 時會直接丟出 expection
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsAsync<List<UserData>>();
                return result;
            }
            catch
            {
                result = new List<UserData>();
                return result;
            }
        }
        public static async Task<UserData> Get(int ID)
        {
            try
            {
                //使用 async 方法從網路 url 上取得回應
                var response = await client.GetAsync($"UserData/{ID}");
                //如果 httpstatus code 不是 200 時會直接丟出 expection
                response.EnsureSuccessStatusCode();
                userdata = await response.Content.ReadAsAsync<UserData>();
                return userdata;
            }
            catch
            {
                userdata = new UserData();
                return userdata;
            }
        }
        public static async Task<String[]> Post(UserData userData,UserCard userCard)
        {
            string jsonuserData = "";
            string jsonuserCard = "";
            string[] err = new string[2];
            HttpContent contentPost = null;
            HttpResponseMessage response = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            if(userData != null)
            {
                jsonuserData = JsonConvert.SerializeObject(userData);
                // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
                contentPost = new StringContent(jsonuserData, Encoding.UTF8, "application/json");
                // 發出 post 並取得結果
                response = client.PostAsync("UserData", contentPost).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    err[0] = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    err[0] = "true";
                }
            }
            else
            {
                err[0] = "false";
            }
            if (MainPage.UID!="" && userCard != null)
            {
                jsonuserCard = JsonConvert.SerializeObject(userCard);
                contentPost = new StringContent(jsonuserCard, Encoding.UTF8, "application/json");
                // 發出 post 並取得結果
                response = client.PostAsync("UserCard", contentPost).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    err[1] = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    err[1] = "true";
                }
            }
            else
            {
                err[1] = "false";
            }
            return err;
        }
        public static async Task<string> delete(string atr)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            // 發出 post 並取得結果
            HttpResponseMessage response = client.DeleteAsync("UserData/"+atr).GetAwaiter().GetResult();
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
