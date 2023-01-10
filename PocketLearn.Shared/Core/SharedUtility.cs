#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace PocketLearn.Shared.Core
{
    public static class SharedUtility
    {

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
