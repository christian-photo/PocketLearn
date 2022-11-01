using PocketLearn.Core.PlatformSpecifics.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZXing.Mobile;

namespace PocketLearn.Droid.Platform
{
    public class AndroidQrScanner : IQrScanner
    {
        private bool isScanning = false;
        private CancellationTokenSource token;

        public void CancelScan()
        {
            if (isScanning)
                token.Cancel();
        }

        public async Task<ZXing.Result> StartScan()
        {
            CancelScan();
            token = new CancellationTokenSource();

            var options = new MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.QR_CODE };

            MobileBarcodeScanner scanner = new MobileBarcodeScanner();
            scanner.TopText = "Scanning for Qr Code";
            return await scanner.Scan(options);
        }
    }
}