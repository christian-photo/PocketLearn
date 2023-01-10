#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using Newtonsoft.Json;
using PocketLearn.Shared.Interfaces;
using PocketLearn.Win.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace PocketLearn.Public.Core.Config
{
    public class WinConfig : IConfig
    {
        public int Port { get; set; } = 4242;

        public List<(TimeSpan, TimeSpan)> LearnTimes { get; set; } = new List<(TimeSpan, TimeSpan)>();

        [JsonIgnore]
        private static WinConfig instance;

        [JsonIgnore]
        private static string path = Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Config.json");

        public static WinConfig Get()
        {
            if ((instance is null) && File.Exists(path))
            {
                instance = JsonConvert.DeserializeObject<WinConfig>(File.ReadAllText(path));
                return instance;
            }
            if (instance is not null)
            {
                return instance;
            }

            instance = new WinConfig();
            return instance;
        }

        public void Save()
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
