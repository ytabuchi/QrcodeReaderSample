using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrcodeReader.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;
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
            // 必要に応じてスキャンオプションをカスタマイズ
            var scanOptions = new MobileBarcodeScanningOptions
            {
                AutoRotate = true,
                PossibleFormats = new List<BarcodeFormat>
                {
                    BarcodeFormat.CODE_39
                },
            };
            // ScannerPageを作成し、resultで読み取り結果を取得
            _scannerPage = new ZXingScannerPage();

            // 読み取るとOnScanResultイベント発火
            _scannerPage.OnScanResult += (result) =>
            {
                _scannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();
                    await DisplayAlert("Scanned", result.Text, "OK");

                    var qrContents = result.Text.Split(',');

                    var formData = new FormData
                    {
                        Category = qrContents[0],
                        Machine = qrContents[1],
                        SerialNumber = qrContents[2]
                    };

                    await Navigation.PushAsync(new FormPage(formData));
                });
            };
            
            // ScannerPage表示
            await Navigation.PushModalAsync(_scannerPage);
        }
    }
}