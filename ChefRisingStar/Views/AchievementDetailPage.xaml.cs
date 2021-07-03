using ChefRisingStar.Behaviours;
using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
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

        private async void MyButton_LongPressed(object sender, System.EventArgs e)
        {
            LongPressBehaviour behave = sender as LongPressBehaviour;
            var step = behave.AttachedButton.CommandParameter as AchievementStep;

            if (step.IsComplete)
            {
                var result1 = await DisplayAlert("Remove objective", "Do you want to clear this objective?", "Clear", "Cancel");

                if (result1)
                {
                    behave.AttachedButton.Source = AchievementStep.IncompleteImage;
                    step.CompletionDate = DateTime.MinValue;
                }

                return;
            }


            var result = await DisplayAlert("Claim objective", "Do you want to claim this objective?", "Claim", "Cancel");

            if(result)
            {
                behave.AttachedButton.Source = AchievementStep.CompleteImage;
                step.CompletionDate = DateTime.Now;
            }
        }
    }
}