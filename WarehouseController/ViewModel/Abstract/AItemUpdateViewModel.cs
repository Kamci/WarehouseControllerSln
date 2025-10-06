using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Services.Abstract;

namespace WarehouseController.ViewModel.Abstract
{
    public abstract class AItemUpdateViewModel<T> : BaseViewModel
    {
        private int itemId;
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
        public AItemUpdateViewModel(string title)
        {
            Title = title;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        public abstract bool ValidateSave();
        public int ItemId
        {
            get => itemId;
            set
            {
                if (itemId != value)
                {
                    itemId = value;
                    LoadItem(itemId);
                }
            }
        }
     
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        private async void OnCancel()
            => await Shell.Current.GoToAsync("..");
        public abstract T SetItem();
        public abstract Task LoadItem(int id);
        private async void OnSave()
        {
            await DataStore.UpdateItemAsync(SetItem());
            await Shell.Current.GoToAsync("../..");
        }
    }
}
