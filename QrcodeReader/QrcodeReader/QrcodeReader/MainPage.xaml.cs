using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace QrcodeReader
{
    public partial class MainPage : ContentPage
    {
        private ZXingScannerPage _scannerPage;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            // ScannerPageを作成し、resultで読み取り結果を取得
            _scannerPage = new ZXingScannerPage();
            // 必要に応じてページをここでカスタマイズ

            _scannerPage.OnScanResult += (result) =>
            {
                _scannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Scanned", result.Text, "OK");
                });
            };

            // ScannerPage表示
            await Navigation.PushAsync(_scannerPage);
        }
    }
}