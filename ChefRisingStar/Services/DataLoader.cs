using ChefRisingStar.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ChefRisingStar.Services
{
    internal class DataLoader
    {
        public void LoadData()
        {
            IDataStore<Achievement, int> achievmentDs = DependencyService.Get<MockAchievementDataStore>();
            IDataStore<AchievementStep, int> achievmentsConditionDs = DependencyService.Get<MockAchievementConditionDataStore>();

            ReadOnlyCollection<Achievement> achievments = achievmentDs.GetItems();
            ReadOnlyCollection<AchievementStep> achievmentConditions = achievmentsConditionDs.GetItems();

            achievments[1].AchievementSteps.Add(achievmentConditions[9]);
            achievments[1].AchievementSteps.Add(achievmentConditions[10]);
            achievments[2].AchievementSteps.Add(achievmentConditions[1]);
            achievments[3].AchievementSteps.Add(achievmentConditions[1]);
            achievments[4].AchievementSteps.Add(achievmentConditions[4]);


            //Not needed
            //achievmentDs.UpdateItemAsync(achievments[2]);
            //achievmentDs.UpdateItemAsync(achievments[3]);
        }
    }
}
