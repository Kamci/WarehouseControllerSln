using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Services;

namespace WarehouseController.ViewModel.Abstract
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public abstract class AItemDetailsViewModel<T> : BaseViewModel
        where T : class
    {
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
        private int itemId;
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

        public AItemDetailsViewModel(string title)
        {
            Title = title;
            CancelCommand = new Command(OnCancel);
            DeleteCommand = new Command(OnDelete);
            UpdateCommand = new Command(OnUpdate);
        }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }
        public Command UpdateCommand { get; }
        private async void OnCancel()
            => await Shell.Current.GoToAsync("..");
        private async void OnDelete()
        {
            await DataStore.DeleteItemAsync(ItemId);
            await Shell.Current.GoToAsync("..");
        }
        private async void OnUpdate()
            => await GoToUpdatePage();
        protected abstract Task GoToUpdatePage();
        public abstract Task LoadItem(int id);

    }

}