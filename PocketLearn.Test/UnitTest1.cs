using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocketLearn.Core;
using PocketLearn.Droid.Platform;
using PocketLearn.Shared.Core.Learning;
using System;
using System.Collections.Generic;

namespace PocketLearn.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ProjectManager manager = ProjectManager.Create(string.Empty);
            manager.AddProject(new LearnProject(DateTime.Now, DateTime.Now.AddDays(5), new Guid("04f1e45e-93c2-4207-b31e-46472965568d"))
            {
                Cards = new() { new LearnCard() { 
                    CardContent1 = new CardContent(new List<CardContentItem>() { new CardContentItem("Test", CardContentItemType.Text)}),
                    CardContent2 = new CardContent(new List<CardContentItem>() { new CardContentItem("Hallo", CardContentItemType.Text)}),
                    CardType = CardType.OneWay,
                    Difficulty = CardDifficulty.NotLearned,
                    LastLearnedTime = DateTime.Now
                    } 
                },
                ProjectName = "Test_No_Images",
                LearnSubject = LearnSubject.English,
                ProjectConfig = new()
            });
            manager.AddProject(new LearnProject(DateTime.Now, DateTime.Now.AddDays(5), new Guid("eb8e9ef5-eddf-4bfc-b033-fd800165c26b"))
            {
                Cards = new() { new LearnCard() {
                    CardContent1 = new CardContent(new List<CardContentItem>() { new CardContentItem("Test", CardContentItemType.Text)}),
                    CardContent2 = new CardContent(new List<CardContentItem>() { new CardContentItem("Hallo", CardContentItemType.Text), new CardContentItem("ee1bd65f-3768-4766-aca0-e12d19a9a63a.jpg", CardContentItemType.Image) }),
                    CardType = CardType.OneWay,
                    Difficulty = CardDifficulty.NotLearned,
                    LastLearnedTime = DateTime.Now
                    }
                },
                ProjectName = "Test_Images",
                LearnSubject = LearnSubject.English,
                ProjectConfig = new()
            });
            DesktopSync.SyncProject("", true, manager, new AndroidConstants());
        }
    }
}
