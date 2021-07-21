using ChefRisingStar.Behaviours;
using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
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

        private async void AchievementStepPressed(object sender, EventArgs e)
        {
            LongPressBehaviour behave = sender as LongPressBehaviour;

            AchievementStep step;

            if (behave != null)
            {
                step = behave.AttachedButton.CommandParameter as AchievementStep;
            }
            else
            {
                ImageButton button = sender as ImageButton;
                step = button.CommandParameter as AchievementStep;
            }

            if (step.IsComplete)
            {
                var result1 = await DisplayAlert("Remove objective", "Do you want to clear this objective?", "Clear", "Cancel");

                if (result1)
                {
                    step.CompletionDate = DateTime.MinValue;
                    step.ImageSrc = AchievementStep.IncompleteImage;
                }

                return;
            }


            var result = await DisplayAlert("Claim objective", "Do you want to claim this objective?", "Claim", "Cancel");

            if (result)
            {
                step.CompletionDate = DateTime.Now;
                step.ImageSrc = AchievementStep.CompleteImage;
            }
        }
    }
}