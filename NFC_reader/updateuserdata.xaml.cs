using System;
using System.Collections.Generic;
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

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=234238

namespace NFC_reader
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class updateuserdata : Page
    {
        public updateuserdata()
        {
            this.InitializeComponent();
        }

        private async void add_butten_Click(object sender, RoutedEventArgs e)
        {
            if (userid.Text != ""&&(username.Text != ""||userclass.Text != ""))
            {
                try
                {
                    UserData userData = new UserData()
                    {
                        ID = Convert.ToInt32(userid.Text),
                        Name = username.Text == "" ? "null" : username.Text,
                        grade = userclass.Text == "" ? 500:Convert.ToInt32(userclass.Text)
                    };
                    String err = await httpclientdata.updata(userData);
                    switch (err.Replace("\"",""))
                    {
                        case "scuess":
                            message.Text = "以更新資料";
                            break;
                        case "ID":
                            message.Text = "未找到此ID";
                            break;
                        case "freeze":
                            message.Text = "此學號存在，但已被凍結，請聯絡管理人員";
                            break;
                        default:
                            message.Text = "資料不可為空";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    message.Text = ex.Message;
                }
            }
            else
            {
                message.Text = "輸入錯誤";
            }
        }

        private void userid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                add_butten_Click(sender, e);
            }
        }

        private void userclass_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                add_butten_Click(sender, e);
            }
        }

        private void username_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                add_butten_Click(sender, e);
            }
        }

        private void clean_butten_Click(object sender, RoutedEventArgs e)
        {
            userid.Text = "";
            userclass.Text = "";
            username.Text = "";

        }
    }
}
