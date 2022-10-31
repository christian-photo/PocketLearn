using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

namespace PocketLearn.ViewModels
{
    public class ScanQrViewModel : BaseViewModel
    {
        private ImageSource _image;
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        private bool isScanning = false;
        private CancellationTokenSource token;

        public ScanQrViewModel()
        {
            Title = "ScanVM";
        }

        public async Task<string> StartScan()
        {
            CancelScan();
            token = new CancellationTokenSource();

            var options = new MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.QR_CODE };

            MobileBarcodeScanner scanner = new MobileBarcodeScanner();
            scanner.CancelButtonText = "Cancel";
            scanner.TopText = "Scanning for Qr Code";
            var result = await scanner.Scan(options);

            return result.Text;
        }

        public void CancelScan()
        {
            if (isScanning)
                token.Cancel();
        }
    }
}
