
using System.Collections.ObjectModel;
using WarehouseController.ViewModel.Dashboard;


namespace WarehouseController
{
    public partial class MainPage : ContentPage
    {

        public MainPage() { InitializeComponent(); }
        public ObservableCollection<string> Warehouses { get; } =
        new() { "A", "B", "C" };

        string? selectedWarehouse;
        public string? SelectedWarehouse
        {
            get => selectedWarehouse;
            set
            {
                if (selectedWarehouse == value) return;
                selectedWarehouse = value;
                OnPropertyChanged();
            }
        }

    }
}



