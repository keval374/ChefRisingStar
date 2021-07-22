using ChefRisingStar.Services;
using System.ComponentModel;

namespace ChefRisingStar.ViewModels
{
    public abstract class BaseDataViewModel<T, K> : BaseViewModel, INotifyPropertyChanged
    {
        public abstract IDataStore<T, K> DataStore { get; protected set; }
    }
}
