using ChefRisingStar.ViewModels;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}