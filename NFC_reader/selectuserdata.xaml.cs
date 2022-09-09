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
    public sealed partial class checktuserdata : Page
    {
        public checktuserdata()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (userid.Text != "")
                {
                    UserData userData = await httpclientdata.Get(userid.Text);
                    if (userData != null)
                    {
                        username.Text = userData.ChineseName;
                        cardcount.Text = await httpclientusercard.count(userid.Text);
                        userstate.Text = userData.state?"在":"不在";
                        message.Text = "查詢資料成功";
                    }
                    else
                    {
                        switch (httpclientdata.err.Replace("\"", ""))
                        {
                            case "ID":
                                message.Text = "未找到此人";
                                break;
                            case "freeze":
                                message.Text = "你已被凍結，請聯絡管理員";
                                break;
                        }
                    }
                }
                else
                {
                    message.Text = "請輸入學號";
                }
            }
            catch (Exception ex)
            {
                message.Text = ex.ToString();
            }
        }

        private void userid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Button_Click(sender, e);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            userid.Text = "";
        }
    }
}
