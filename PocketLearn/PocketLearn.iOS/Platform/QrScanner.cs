using Foundation;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UIKit;
using ZXing.Mobile;
using Xamarin.Essentials;

namespace PocketLearn.iOS.Platform
{
    public class QrScanner : IQrScanner
    {
        private bool isScanning = false;
        MobileBarcodeScanner scanner;

        public void CancelScan()
        {
            if (isScanning)
                scanner?.Cancel();
        }

        public async Task<ZXing.Result> StartScan()
        {
            CancelScan();

            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.Camera>();
            }

            var options = new MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.QR_CODE };

            scanner = new MobileBarcodeScanner();
            scanner.TopText = "Scanning for Qr Code";
            return await scanner.Scan(options);
        }
    }
}