#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

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