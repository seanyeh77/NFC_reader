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
    public sealed partial class deletecard : Page
    {
        UserData userdata;
        public static deletecard Current;
        public deletecard()
        {
            this.InitializeComponent();
            Current = this;
        }
        public async Task showname(string UID)
        {
            if(UID != "")
            {
                message.Text = "處理中";
                try
                {
                    userdata = await httpclientusercard.Get(UID);
                    if(userdata == null)
                    {
                        switch (httpclientusercard.err.Replace("\"", ""))
                        {
                            case "UID":
                                message.Text = "未找到此卡";
                                break;
                        }
                    }
                    else
                    {
                        deluserID.Text = userdata.ID.ToString("D6");
                        message.Text = "查詢成功";
                    }
                    
                }
                catch (Exception ex)
                {
                    message.Text = ex.Message;
                }
            }
            else
            {
                deluserID.Text = "";
                message.Text = "請刷卡";
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (deluserID.Text != "")
            {
                string err = await httpclientusercard.delete(MainPage.UID);
                switch (err.Replace("\"", ""))
                {
                    case "UID":
                        message.Text = "未找到此卡";
                        break;
                    case "scuess":
                        message.Text = "刪除卡片成功";
                        break;
                }
            }
            else
            {
                message.Text = "請刷卡";
            }
        }
        public void notcard()
        {
            message.Text = "請再次刷卡";
        }
    }
}
