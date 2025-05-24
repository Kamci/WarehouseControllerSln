using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Services.Abstract;

namespace WarehouseController.ViewModel.Abstract
{
    public abstract class AItemListViewModel<T> : BaseViewModel
        where T : class
    {

        #region Fields
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
        private T _selectedItem = default!;
        #endregion
        #region Properties
        public ObservableCollection<T> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<T> ItemTapped { get; }
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        #endregion
        protected AItemListViewModel(string title)
        {
            Title = title;
            Items = new ObservableCollection<T>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<T>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);

                if (items == null)
                {
                    Debug.WriteLine("Received NULL from DataStore");
                }
                else
                {
                    foreach (var item in items)
                    {
                        Items.Add(item);
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Error loading items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }


        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = default!;
        }
        private async void OnAddItem(object obj)
            => await GoToAddPage();
        public abstract Task GoToAddPage();
        public abstract Task GoToDetailsPage(T item);
        async void OnItemSelected(T item)
        {
            if (item == null)
                return;
            await GoToDetailsPage(item);
        }
    }
}
