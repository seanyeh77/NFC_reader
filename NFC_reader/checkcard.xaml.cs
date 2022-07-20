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
    public sealed partial class checkcard : Page
    {
        public static checkcard Current;
        private static List<UserData> userDatas;
        private static UserData userdata;

        public checkcard()
        {
            this.InitializeComponent();
            Current = this;
        }
        public async Task showdata(string UID)
        {
            try
            {
                if (UID != "")
                {
                    UserData userdata = await httpclientusercard.Get(UID);
                    string err = httpclientusercard.err;
                    if(userdata == null)
                    {
                        switch (err.Replace("\"", ""))
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
                        ID.Text = userdata.ID.ToString("D6");
                        name.Text = userdata.Name;
                        grade.Text = userdata.grade.ToString();
                        state.Text = userdata.state.ToString();
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
            message.Text = "請重新刷卡";
        }
    }
}
