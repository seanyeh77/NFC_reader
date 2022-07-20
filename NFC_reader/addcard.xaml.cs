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
            message.Text = "處理中";
            bool userCardbool = userid.Text != "" && MainPage.UID.ToString() != "";
            try
            {
                UserCard userCard = new UserCard{};
                if (!userCardbool)
                {
                    message.Text = "資料讀取失敗";
                    return;
                }
                userCard = new UserCard
                {
                    UID = MainPage.UID.ToString(),
                    ID = Convert.ToInt32(userid.Text)
                };
                String err = await httpclientusercard.Post(userCard);
                if (err == "\"UID\"")
                {
                    message.Text = "已有此卡的資料";
                }
                else if(err == "\"ID\"")
                {
                    message.Text = "尚未加入個人資料";
                }
                else if(err == "\"freeze\"")
                {
                    message.Text = "已有此卡的資料，但已被凍結";
                }
                else if( err == "scuess")
                {
                    message.Text = "卡片加入成功";
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
        public async Task showdata(string UID)
        {
            try
            {
                if (UID != "")
                {
                    UserData userdata = await httpclientusercard.Get(UID);
                    if (userdata == null)
                    {
                        switch (httpclientusercard.err.Replace("\"",""))
                        {
                            case "UID":
                                message.Text = "未找到此卡";
                                break;
                            case "ID":
                                message.Text = "未找到此人";
                                break;
                            case "freeze":
                                message.Text = "卡片無法使用，請聯絡管理員";
                                break;
                        }
                    }
                    else
                    {
                        message.Text = "成功";
                    }
                }
                else
                {
                    message.Text = "未讀取到卡片";
                }
            }
            catch (Exception ex)
            {
                message.Text = ex.Message;
            }
        }
        public void notcard()
        {
            message.Text = "讀取卡片失敗";
        }

        private void clean_butten_Click(object sender, RoutedEventArgs e)
        {
            userid.Text = "";
            message.Text = "";
        }
    }
}
