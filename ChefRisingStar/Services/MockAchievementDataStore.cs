using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChefRisingStar.Services
{
    public class MockAchievementDataStore : IDataStore<Achievement, int>
    {
        readonly List<Achievement> items;

        public MockAchievementDataStore()
        {
            items = new List<Achievement>()
            {
                new Achievement(1, 10, "First Achievement", "You earn this Achievement just by downloading the App and starting to use it! Well done!", "trophy64.png", AchievementTypes.Progress, DateTime.Now.AddDays(-1)),
                new Achievement(2, 20, "Register Achievement", "Register and add your preferences.", "trophy64.png", AchievementTypes.Progress),
                new Achievement(3, 30, "Post a cooking selfie!", "Show off your cooking outfit and earn this badge", "trophy64.png", AchievementTypes.Social),
                new Achievement(4, 40, "Start cooking!", "Cook a your first meal this week to get this achievement. Start your cooking adventure and make your first meal!", "trophy64.png", AchievementTypes.Skill),
                new Achievement(5, 80, "Continue cooking", "Cook a two meals this week to get this achievement.", "trophy64.png", AchievementTypes.Skill),
                new Achievement(6, 200, "Sustainable cooking", "Cook a cook a meal with a smaller eco footprint to get this achievement.", "trophy64.png", AchievementTypes.Skill),
            };

            IDataStore<AchievementStep, int> achievmentsConditionDs = DependencyService.Get<MockAchievementConditionDataStore>();
            ReadOnlyCollection<AchievementStep> achievmentConditions = achievmentsConditionDs.GetItems();

            items[1].AchievementSteps.Add(achievmentConditions[9]);
            items[1].AchievementSteps.Add(achievmentConditions[10]);
            items[2].AchievementSteps.Add(achievmentConditions[0]);
            items[3].AchievementSteps.Add(achievmentConditions[2]);
            items[4].AchievementSteps.Add(achievmentConditions[2]);
            items[4].AchievementSteps.Add(achievmentConditions[3]);
            items[5].AchievementSteps.Add(achievmentConditions[12]);
            items[5].AchievementSteps.Add(achievmentConditions[13]);
        }

        public async Task<bool> AddItemAsync(Achievement item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Achievement item)
        {
            var oldItem = items.Where((Achievement arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Achievement arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Achievement> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Achievement>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<Achievement> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }
    }
}