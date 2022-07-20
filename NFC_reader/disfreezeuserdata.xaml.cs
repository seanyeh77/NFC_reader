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
    public sealed partial class disfreezeuserdata : Page
    {
        public disfreezeuserdata()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                message.Text = "處理中";
                if (userid.Text != "")
                {
                    string err = await httpclientdata.disfreeze(Convert.ToInt32(userid.Text));
                    switch (err.Replace("\"",""))
                    { 
                        case "scuess":
                            message.Text = "資料解除成功";
                            break;
                        case "ID":
                            message.Text = "找不到此資料";
                            break;
                        case "disfreeze":
                            message.Text = "此用戶沒有被凍結";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    message.Text = "請輸入學號";
                }
            }
            catch (Exception ex)
            {

                message.Text = ex.Message;
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
