using System;

namespace PocketLearn.Core.Learning
{
    public class Difficulty
    {
        private Difficulty() { }

        public string Name { get; set; }
        public TimeSpan TimeToAskAgain { get; set; }


        public static Difficulty Easy = new Difficulty() { Name = "Easy", TimeToAskAgain = TimeSpan.FromDays(1) };
        public static Difficulty Ok = new Difficulty() { Name = "Ok", TimeToAskAgain = TimeSpan.FromHours(4) };
        public static Difficulty Medium = new Difficulty() { Name = "Medium", TimeToAskAgain = TimeSpan.FromHours(2) };
        public static Difficulty Hard = new Difficulty() { Name = "Hard", TimeToAskAgain = TimeSpan.FromMinutes(20) };
    }
}
