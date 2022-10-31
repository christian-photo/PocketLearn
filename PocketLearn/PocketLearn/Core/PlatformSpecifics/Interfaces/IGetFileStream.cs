using System.IO;

namespace PocketLearn.Core.PlatformSpecifics.Interfaces
{
    public interface IGetFileStream
    {
        MemoryStream GetStream();
    }
}
