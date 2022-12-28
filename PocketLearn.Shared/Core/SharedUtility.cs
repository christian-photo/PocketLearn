using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace PocketLearn.Shared.Core
{
    public static class SharedUtility
    {
        public static int GetSizeFactor(int dimension, int targetDimension)
        {
            return dimension / targetDimension;
        }

        public static T MakeDeepCopy<T>(this T obj)
        {
            T o;
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                o = (T)new BinaryFormatter().Deserialize(ms);
            }
            return o;
        }
    }
}
