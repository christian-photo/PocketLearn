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

    }
}
