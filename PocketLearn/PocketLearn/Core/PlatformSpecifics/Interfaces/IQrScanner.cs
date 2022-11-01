using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace PocketLearn.Core.PlatformSpecifics.Interfaces
{
    public interface IQrScanner
    {
        Task<Result> StartScan();
        void CancelScan();
    }
}
