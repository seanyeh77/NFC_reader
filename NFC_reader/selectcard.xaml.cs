using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public sealed partial class selectcard : Page, INotifyPropertyChanged
    {
        static List<UserData> userDatas = new List<UserData>();
        int counter = 0;
        UserData userdata = new UserData();
        UserCard usercard = new UserCard();
        public static selectcard Current;
        public event PropertyChangedEventHandler PropertyChanged;
        private string userName = "姓名";
        private string userTime = "時間";

        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //public string UserTime
        //{
        //    get { return userName; }
        //    set
        //    {
        //        if (value != this.userName)
        //        {
        //            this.userName = value;
        //            NotifyPropertyChanged();
        //        }

        //    }
        //}

        //public string UserName
        //{
        //    get { return userName; }
        //    set
        //    {
        //        if (value != this.userName)
        //        {
        //            this.userName = value;
        //            NotifyPropertyChanged();
        //        }

        //    }
        //} 

       
        public selectcard()
        {
            this.InitializeComponent();
            Current = this;
        }
        public async Task userlog(string UID)
        {
            try
            {
                if(UID != "")
                {
                    message.Text = "處理中"; 
                    UserLog userLog = new UserLog()
                    {
                        UID = UID,
                        time = DateTime.Now
                    };
                    String err = await httpclientlog.Post(userLog);
                    switch (err.Replace("\"",""))
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
                        default:
                            userdata = await httpclientusercard.Get(UID);
                            ID.Text = userdata.ID;
                            name.Text = userdata.ChineseName;
                            time.Text = userLog.time.ToString();
                            state.Text = userdata.state?"簽到":"簽退";
                            message.Text = "成功";
                            break;
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
            ID.Text = "靠卡失敗";
            userName = "靠卡失敗";
            userTime = "靠卡失敗";
            message.Text = "靠卡失敗";
        }

        internal void userlog(byte[] msg)
        {
            throw new NotImplementedException();
        }
    }
}
