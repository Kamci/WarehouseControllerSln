using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Services.Abstract;

namespace WarehouseController.ViewModel.Abstract
{
    public abstract class ANewItemViewModel<T> : BaseViewModel
    {
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
        public ANewItemViewModel(string title)
        {
            Title = title;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        public abstract bool ValidateSave();
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        private async void OnCancel()
            => await Shell.Current.GoToAsync("..");
        public abstract T SetItem();
        private async void OnSave()
        {
            try
            {
                var item = SetItem();

                var json = System.Text.Json.JsonSerializer.Serialize(item);
                Debug.WriteLine("Sending JSON to API:");
                Debug.WriteLine(json);

                await DataStore.AddItemAsync(item);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to save item.", "OK");
            }
        }
    }
}
