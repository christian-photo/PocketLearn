using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Shared.Core
{
    public static class SharedUtility
    {
        public static int GetSizeFactor(int dimension, int targetDimension)
        {
            return dimension / targetDimension;
        }
    }
}
