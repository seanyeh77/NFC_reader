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
using Microsoft.Extensions.Configuration;

namespace NFC_reader
{
    public class httpclientdata
    {
        private readonly IConfiguration _configuration;
        public httpclientdata(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string err = "";
        //public static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7021/") };
        public static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://userdatawebapi20220502202651.azurewebsites.net/") };
        public static List<UserData> result = new List<UserData>();
        public static UserData userdata = new UserData();
        

        public static async Task<List<UserData>> GetAll()
        {
            try
            {
                var response = await client.GetAsync("UserData");
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
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync($"UserData/{ID}");
                err = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                userdata= await response.Content.ReadAsAsync<UserData>();
                return userdata;
            }
            catch
            {
                return null;
            }
        }
        public static async Task<String> Post(UserData userData)
        {
            HttpContent contentPost = null;
            HttpResponseMessage response = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            string json = JsonConvert.SerializeObject(userData);
            contentPost = new StringContent(json, Encoding.UTF8, "application/json");
            response = client.PostAsync("UserData", contentPost).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "scuess";
            }
        }
        public static async Task<string> delete(int ID)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync("UserData/"+ ID).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "scuess";
            }
        }
        public static async Task<string> updata(UserData userData)
        {
            HttpContent contentPost = null;
            HttpResponseMessage response = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            string json = JsonConvert.SerializeObject(userData);
            contentPost = new StringContent(json, Encoding.UTF8, "application/json");
            response = client.PutAsync("UserData", contentPost).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "scuess";
            }
        }
        public static async Task<string> freeze(int ID)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync("UserData/freeze/" + ID).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "scuess";
            }
        }
        public static async Task<string> disfreeze(int ID)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.DeleteAsync("UserData/disfreeze/" + ID).GetAwaiter().GetResult();
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
