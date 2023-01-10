#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using System;

namespace PocketLearn.Shared.Core.Learning
{
    [Serializable]
    public class ProjectConfig
    {
        public float HardFactor { get; set; } = 1f;
        public float MediumFactor { get; set; } = 2f;
        public float OKFactor { get; set; } = 5f;
        public float EasyFactor { get; set; } = 20f;
        public float NotLearnedFactor { get; set; } = 1.5f;

        public DateTime LastEdit { get; set; }
    }
}
