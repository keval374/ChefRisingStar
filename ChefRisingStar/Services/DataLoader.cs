using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ChefRisingStar.Services
{
    internal class DataLoader
    {
        public void LoadData()
        {
            IDataStore<Achievement, int>  achievmentDs = DependencyService.Get<MockAchievementDataStore>();
            IDataStore<AchievementStep, int>  achievmentsConditionDs = DependencyService.Get<MockAchievementConditionDataStore>();

            ReadOnlyCollection<Achievement> achievments = achievmentDs.GetItems();
            ReadOnlyCollection<AchievementStep> achievmentConditions = achievmentsConditionDs.GetItems();

            achievments[2].AchievementSteps.Add(achievmentConditions[0]);
            achievments[2].AchievementSteps.Add(achievmentConditions[2]);
            achievments[2].AchievementSteps.Add(achievmentConditions[4]);
            achievments[3].AchievementSteps.Add(achievmentConditions[1]);

            //Not needed
            //achievmentDs.UpdateItemAsync(achievments[2]);
            //achievmentDs.UpdateItemAsync(achievments[3]);
        }
    }
}
