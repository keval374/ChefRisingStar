using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}