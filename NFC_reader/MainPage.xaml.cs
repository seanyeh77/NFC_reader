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
using Windows.Devices.SmartCards;
using Pcsc.Common;
using System.Threading.Tasks;
using Windows.Storage;
using Pcsc;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace NFC_reader
{
    
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static string page = "selectcard";
        public static string UID = "";
        public MainPage()
        {
            this.InitializeComponent();
        }
        SmartCardReader m_cardReader;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {// First try to find a reader that advertises as being NFC
            Windows.Devices.Enumeration.DeviceInformation deviceInfo = await Pcsc.SmartCardReaderUtils.GetFirstSmartCardReaderInfo(SmartCardReaderKind.Nfc);
            if (deviceInfo == null)
            {
                // If we didn't find an NFC reader, let's see if there's a "generic" reader meaning we're not sure what type it is
                deviceInfo = await Pcsc.SmartCardReaderUtils.GetFirstSmartCardReaderInfo(SmartCardReaderKind.Any);
            }

            if (deviceInfo == null)
            {
                textblock.Text = "NFC card reader mode not supported on this device";
                //LogMessage("NFC card reader mode not supported on this device", NotifyType.ErrorMessage);
                return;
            }

            if (!deviceInfo.IsEnabled)
            {
                var msgbox = new Windows.UI.Popups.MessageDialog("Your NFC proximity setting is turned off, you will be taken to the NFC proximity control panel to turn it on");
                msgbox.Commands.Add(new Windows.UI.Popups.UICommand("OK"));
                await msgbox.ShowAsync();

                //// This URI will navigate the user to the NFC proximity control panel
                //NfcUtils.LaunchNfcProximitySettingsPage();
                return;
            }

            if (m_cardReader == null)
            {
                m_cardReader = await SmartCardReader.FromIdAsync(deviceInfo.Id);
                m_cardReader.CardAdded += cardReader_CardAdded;
                m_cardReader.CardRemoved += cardReader_CardRemoved;
            }

        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            // Clean up
            if (m_cardReader != null)
            {
                m_cardReader.CardAdded -= cardReader_CardAdded;
                m_cardReader.CardRemoved -= cardReader_CardRemoved;
                m_cardReader = null;
            }

            base.OnNavigatingFrom(e);
        }
        private void cardReader_CardRemoved(SmartCardReader sender, CardRemovedEventArgs args)
        {
            UID = "";
            if (!this.Dispatcher.HasThreadAccess)
            {
                var ignored = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { util.push_data(UID, page); });
                return;
            }
        }
        private async void cardReader_CardAdded(SmartCardReader sender, CardAddedEventArgs args)
        {
            await HandleCard(args.SmartCard);
        }
        private async Task HandleCard(SmartCard card)
        {
            try
            {
                
                //util.push_data("iiiii");
                // Connect to the card
                using (SmartCardConnection connection = await card.ConnectAsync())
                {
                    // Try to identify what type of card it was
                    IccDetection cardIdentification = new IccDetection(card);
                    await cardIdentification.DetectCardTypeAync();
                    var apduRes = await connection.TransceiveAsync(new Pcsc.GetUid());
                    UID = BitConverter.ToString(apduRes.ResponseData);
                }

                if (!this.Dispatcher.HasThreadAccess)
                {
                    var ignored = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { util.push_data(UID, page); });
                    return;
                }
            }
            catch
            {
                if (!this.Dispatcher.HasThreadAccess)
                {
                    var ignored = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { util.notcard(UID, page); });
                    return;
                }
            }
        }
        private void MainPage1_Loading(FrameworkElement sender, object args)
        {
            ScrollViewer.Navigate(typeof(selectcard));
        }

        private void MainPage1_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if(args.IsSettingsSelected)
            {

            }
            else 
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                switch (item.Tag.ToString())
                {
                    case "selectcard":
                        ScrollViewer.Navigate(typeof(selectcard));
                        page = "selectcard";
                        break;
                    case "addcard":
                        ScrollViewer.Navigate(typeof(addcard));
                        page = "addcard";
                        break;
                    case "adduserdata":
                        ScrollViewer.Navigate(typeof(adduserdata));
                        page = "adduserdata";
                        break;
                    
                    case "checktuserdata":
                        ScrollViewer.Navigate(typeof(checktuserdata));
                        page = "checktuserdata";
                        break;
                    case "checkcard":
                        ScrollViewer.Navigate(typeof(checkcard));
                        page = "checkcard";
                        break;
                    case "deletecard":
                        ScrollViewer.Navigate(typeof(deletecard));
                        page = "deletecard";
                        break;
                    case "deleteuaserdata":
                        ScrollViewer.Navigate(typeof(deleteuaserdata));
                        page = "deleteuaserdata";
                        break;
                    case "updateuserdata":
                        ScrollViewer.Navigate(typeof(updateuserdata));
                        page = "updateuserdata";
                        break;
                    //case "freezeuserdata":
                    //    ScrollViewer.Navigate(typeof(freezeuserdata));
                    //    page = "freezeuserdata";
                    //    break;
                    //case "disfreezeuserdata":
                    //    ScrollViewer.Navigate(typeof(disfreezeuserdata));
                    //    page = "freezeuserdata";
                    //    break;
                }
            }
        }

       
    }
}
