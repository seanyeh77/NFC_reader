using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Threading.Tasks;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=234238

namespace NFC_reader
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class addcard : Page
    {
        public static addcard Current;
        public addcard()
        {
            this.InitializeComponent();
            Current = this;
        }
        private async void add_butten_Click(object sender, RoutedEventArgs e)
        {
            bool userDatabool = userid.Text != "" && usergrade.Text != "" && username.Text != "";
            bool userCardbool = userid.Text != "" && MainPage.UID.ToString() != "";
            message.Text = "處理中";
            try
            {
                UserData userData = null;
                UserCard userCard = new UserCard{};
                if (userDatabool) 
                {
                    userData = new UserData
                    {
                        ID = Convert.ToInt32(userid.Text),
                        Name = username.Text,
                        grade = Convert.ToInt32(usergrade.Text)
                    };
                }
                if (userCardbool)
                {
                    userCard = new UserCard
                    {
                        UID = MainPage.UID.ToString(),
                        ID = Convert.ToInt32(userid.Text)
                    };
                }
                String[] err = await httpclientdata.Post(userData, userCard);
                if (err[0] == "true" && err[1] == "false")
                {
                    message.Text = "新增個人資料成功";
                }
                else if (err[0] == "false" && err[1] == "false")
                {
                    message.Text = "欄位輸入錯誤";
                }
                else if (err[0] == "true" && err[1] == "false")
                {
                    message.Text = "新增個人資料、卡片成功";
                }
                else if (err[0] == "\"isID\"" && err[1] == "\"UID\"")
                {
                    message.Text = "此個人資料、卡已存在";
                }
                else if (err[0] == "\"isID\"" && err[1] == "false")
                {
                    message.Text = "此個人資料已存在";
                }
                else if (err[0] == "false" && err[1] == "\"ID\"")
                {
                    message.Text = "請輸入個人資料";
                }
                else if (err[0] == "false" && err[1] == "true")
                {
                    message.Text = "新增卡片成功";
                }
                else if (err[0] == "false" && err[1] == "\"UID\"")
                {
                    message.Text = "此卡已存在";
                }
            }
            catch (Exception ex)
            { 
                message.Text = ex.Message; 
            }
        }

        private void username_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                add_butten_Click(sender,e);
            }
        }
        private void id_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                add_butten_Click(sender, e);
            }
        }
        private void usergrade_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                add_butten_Click(sender, e);
            }
        }
        public void userlog(string msg)
        {
            if(msg != "")
            {
                message.Text = "讀取卡片成功";
            }
            else
            {
                message.Text = "未讀取到卡片";
            }
            
        }
        public void notcard()
        {
            message.Text = "讀取卡片失敗";
        }

        private void clean_butten_Click(object sender, RoutedEventArgs e)
        {
            username.Text = "";
            usergrade.Text = "";
            userid.Text = "";
            message.Text = "";
        }
    }
}
