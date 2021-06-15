using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class MockAchievementDataStore : IDataStore<Achievement>
    {
        readonly List<Achievement> items;

        public MockAchievementDataStore()
        {
            items = new List<Achievement>()
            {
                new Achievement(1, 10, "First Achievement", "This is an Achievement description.", "trophy64.png", AchievementTypes.Skill, DateTime.Now.AddMonths(-1)),
                new Achievement(2, 20, "Second Achievement", "This is an Achievement description.", "trophy64.png", AchievementTypes.Progress),
                new Achievement(3, 30, "Thrid Achievement", "This is an Achievement description.", "trophy64.png", AchievementTypes.Social),
                new Achievement(4, 40, "Forth Achievement", "This is an Achievement description.", "trophy64.png", AchievementTypes.Skill),
            };
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

        public async Task<bool> DeleteItemAsync(string stringId)
        {
            //NOTE: not clean but by contract
            int id = int.Parse(stringId);
            var oldItem = items.Where((Achievement arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Achievement> GetItemAsync(string stringId)
        {
            //NOTE: not clean but by contract
            int id = int.Parse(stringId);
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Achievement>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}