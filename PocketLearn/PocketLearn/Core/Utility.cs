using System.Drawing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing;

namespace PocketLearn.Core
{
    public class Utility
    {
        public Bitmap CreateQRCode(string content, int size = 32)
        {
            BitMatrix matrix = new QRCodeWriter().encode(content, BarcodeFormat.QR_CODE, size, size);
            return new BarcodeWriter<Bitmap>() { Format = BarcodeFormat.QR_CODE }.Write(matrix);
        }
    }
}
