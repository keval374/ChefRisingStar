using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class MockAchievementConditionDataStore : IDataStore<AchievementStep, int>
    {
        readonly List<AchievementStep> items;

        public MockAchievementConditionDataStore()
        {
            items = new List<AchievementStep>()
            {
                new AchievementStep(1, "Post a selfie", "Share a photo of yourself cooking"),
                new AchievementStep(2, "Cook your first meal", "Start your cooking adventure and make your first meal"),
                new AchievementStep(3, "Cook twice in one week", "Cooking twice in one week to get the habbit rolling"),
                new AchievementStep(4, "Make a new dish", "Try something you have never made before"),
                new AchievementStep(5, "Share a photo", "Share a photo of yourself cooking"),
                new AchievementStep(6, "Watch a video", "Watch an instructional video to learn a new skill"),
                new AchievementStep(7, "Read a new recipe", "Read about a new recipe"),
                new AchievementStep(8, "Teach another", "Show someone else a skill your learned and howto properly do it"),
            };
        }

        public async Task<bool> AddItemAsync(AchievementStep item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(AchievementStep item)
        {
            var oldItem = items.Where((AchievementStep arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((AchievementStep arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<AchievementStep> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<AchievementStep>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<AchievementStep> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }
    }
}