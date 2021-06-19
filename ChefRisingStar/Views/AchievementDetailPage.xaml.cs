using ChefRisingStar.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class AchievementDetailPage : ContentPage
    {
        public AchievementDetailPage()
        {
            InitializeComponent();
            BindingContext = new AchievementDetailViewModel();
        }
    }
}