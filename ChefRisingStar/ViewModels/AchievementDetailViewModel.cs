using ChefRisingStar.Models;
using ChefRisingStar.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class AchievementDetailViewModel : BaseDataViewModel<Achievement, int>
    {
        private int _id;
        private string _description;
        private Achievement _achievement;

        public override IDataStore<Achievement, int> DataStore { get; protected set; }

        //TODO: Make readonly
        public ObservableCollection<AchievementStep> AchievementSteps { get; protected set; }

        //public List<AchievementStep> AchievementSteps
        //{
        //    get => _achievementConditions;
        //    set => SetProperty(ref _achievementConditions, value);
        //}

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    LoadItemId(value);
                }
            }
        }

        public AchievementDetailViewModel()
        {
            DataStore = DependencyService.Get<IDataStore<Achievement, int>>();
            AchievementSteps = new ObservableCollection<AchievementStep>();
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                _achievement = item;
                Id = item.Id;
                Title = item.Name;
                Description = item.Description;

                foreach(AchievementStep ac in item.AchievementSteps)
                    AchievementSteps.Add(ac);
            }
            catch (Exception)
            {
                Debug.WriteLine($"Failed to Load Item: {itemId}");
            }
        }
    }
}
