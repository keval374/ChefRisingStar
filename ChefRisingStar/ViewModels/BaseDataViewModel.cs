using ChefRisingStar.Models;
using ChefRisingStar.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public abstract class BaseDataViewModel<T, K> : BaseViewModel, INotifyPropertyChanged
    {
        public abstract IDataStore<T, K> DataStore { get; protected set; }
    }
}
