using System.Diagnostics;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class FootprintModel : BaseNotifyModel
    {
        private string _Ingredient;

        private int _LandUsage;

        private int _Farming;

        private int _AimalFeed;

        private int _Processing;

        private int _Transport;

        private int _Retail;

        private int _Packaging;

        private int _waste;

        private int _totalCO2;


        public string Ingredient
        {
            get { return _Ingredient; }
            set { SetProperty(ref _Ingredient, value); }
        }

        public int LandUsage
        {
            get { return _LandUsage; }
            set { SetProperty(ref _LandUsage, value); }
        }

        public int Farming
        {
            get { return _Farming; }
            set { SetProperty(ref _Farming, value); }
        }

        public int AimalFeed
        {
            get { return _AimalFeed; }
            set { SetProperty(ref _AimalFeed, value); }
        }

        public int Processing
        {
            get { return _Processing; }
            set { SetProperty(ref _Processing, value); }
        }

        public int Transport
        {
            get { return _Transport; }
            set { SetProperty(ref _Transport, value); }
        }

        public int Retail
        {
            get { return _Retail; }
            set { SetProperty(ref _Retail, value); }
        }

        public int Packaging
        {
            get { return _Packaging; }
            set { SetProperty(ref _Packaging, value); }
        }

        public int Waste
        {
            get { return _waste; }
            set { SetProperty(ref _waste, value); }
        }

        public int TotalCO2
        {
            get { return _totalCO2; }
            set { SetProperty(ref _totalCO2, value); }
        }

        private string GetDebuggerDisplay()
        {
            return $"{Ingredient} - {TotalCO2}";
        }
    }

}
