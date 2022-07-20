using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class adduserdata : Page
    {
        public static adduserdata Current;
        public adduserdata()
        {
            Current = this;
            this.InitializeComponent();
        }

        private void clean_butten_Click(object sender, RoutedEventArgs e)
        {
            userid.Text = "";
            userclass.Text = "";
            username.Text = "";
        }

        private async void add_butten_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool userdatabool = userid.Text != "" && username.Text != "" && userclass.Text != "";
                if (userdatabool)
                {
                    UserData userData = new UserData()
                    {
                        ID = Convert.ToInt32(userid.Text),
                        Name = username.Text,
                        grade = Convert.ToInt32(userclass.Text)
                    };
                    String err = await httpclientdata.Post(userData);
                    switch (err.Replace("\"", ""))
                    {
                        case "scuess":
                            message.Text = "以新增資料";
                            break;
                        case "isID":
                            message.Text = "此學號以存在";
                            break;
                        case "freeze":
                            message.Text = "此學號以存在，但已被凍結，請聯絡管理人員";
                            break;
                    }
                }
                else
                {
                    message.Text = "資料不可為空";
                }
            }
            catch(Exception ex)
            {
                message.Text = ex.Message;
            }
            
        }

        private void username_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                add_butten_Click(sender, e);
            }
        }
    }
}
