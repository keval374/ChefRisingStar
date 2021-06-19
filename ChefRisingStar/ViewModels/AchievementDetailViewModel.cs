using ChefRisingStar.Models;
using ChefRisingStar.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class AchievementDetailViewModel : BaseDataViewModel<Achievement, int>
    {
        private int id;
        private string text;
        private string description;

        public override IDataStore<Achievement, int> DataStore { get; protected set; }
               
        
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                LoadItemId(value);
            }
        }

        public AchievementDetailViewModel()
        {
            DataStore = DependencyService.Get<IDataStore<Achievement, int>>();
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Description;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
