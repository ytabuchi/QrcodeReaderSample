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

namespace QrcodeReader.Views
{
    public partial class MainPage : ContentPage
    {
        private ZXingScannerPage _scannerPage;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void ScanButton_OnClicked(object sender, EventArgs e)
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
                // Scan停止
                _scannerPage.IsScanning = false;

                // OnScanResultはバックグラウンドスレッドで処理されているので、UIスレッドで処理をさせる
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Scanned", result.Text, "OK");

                    // QRコードの仕様により適宜変更
                    var qrContents = result.Text;

                    var formData = new FormData
                    {
                        SerialNumber = qrContents
                    };

                    await Navigation.PushAsync(new FormPage(formData));
                });
            };
            
            // ScannerPage表示
            await Navigation.PushAsync(_scannerPage);
        }

        private async void ShowLogButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LogsPage());
        }
    }
}