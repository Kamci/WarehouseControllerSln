using Microcharts;
using SkiaSharp;
using Entry = Microcharts.ChartEntry;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Implementations;
using WarehouseController.ViewModel.Abstract;
namespace WarehouseController.ViewModel.Dashboard

{
    public partial class DashboardViewModel : BaseViewModel
    {


        // Właściwości i metody do obsługi wykresów
        private readonly ProductService _productService = new();
        private readonly OrderService _orderService = new();

        private int productsQty;
        public int ProductsQty
        {
            get => productsQty;
            private set => SetProperty(ref productsQty, value);    // podnosi PropertyChanged
        }

        private int openOrdersQty;
        public int OpenOrdersQty
        {
            get => openOrdersQty;
            set => SetProperty(ref openOrdersQty, value);
        }
        private int lowStockItemCount;
        public int LowStockItemCount
        {
            get => lowStockItemCount;
            set => SetProperty(ref lowStockItemCount, value);
        }

        private ObservableCollection<OrderDto> recentOrders;
        public ObservableCollection<OrderDto> RecentOrders
        {
            get => recentOrders;
            set => SetProperty(ref recentOrders, value);
        }

        private List<Entry> chartEntries;
        public List<Entry> ChartEntries
        {
            get => chartEntries;
            set => SetProperty(ref chartEntries, value);
        }

        public async Task LoadTopProductsChartAsync()
        {
            if (SelectedWarehouse == null)
                return;

            var topProducts = await new ProductService().GetTopProductsAsync(SelectedWarehouse.Id);

            if (topProducts == null || !topProducts.Any())
            {
                ChartEntries = new List<Entry>
                {
                            new Entry(0)
                            {
                            Label = "",
                            ValueLabel = "",
                            Color = SKColor.Parse("#7102f0")
                            }
                };

            await Application.Current.MainPage.DisplayAlert("Information", "No products in the selected warehouse.", "OK");
            return;
            }

            var colors = GetRandomWarmColors(topProducts.Count);

            ChartEntries = topProducts.Select((p, index) => new Entry(p.StockQuantity)
            {
                Label = p.Name,
                ValueLabel = p.StockQuantity.ToString(),
                Color = colors[index],
                TextColor = SKColors.Black,
                ValueLabelColor = SKColors.Black
            }).ToList();
        }
        public string SelectedWarehouseName => SelectedWarehouse?.Name ?? "None";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        private readonly ReferenceDataHelper _refHelper = new();
        public ObservableCollection<Warehouse> Warehouses { get; } = new();


        private Warehouse selectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get => selectedWarehouse;
            set
            {
                if (SetProperty(ref selectedWarehouse, value) && value != null)
                {
                    Console.WriteLine($"📌 Wybrano magazyn: {value.Name} (ID={value.Id})");
                }
            }
        }

        public ICommand LoadWarehouseReportCommand { get; }

        public DashboardViewModel()
        {
            LoadWarehouseReportCommand = new Command(OnLoadWarehouseReport);
        }

        public async Task LoadWarehousesAsync()
        {
            try
            {
                Console.WriteLine("→ LoadWarehousesAsync start");

                Warehouses.Clear();
                await _refHelper.LoadWarehousesAsync(Warehouses);

                Debug.WriteLine($"Liczba magazynów: {Warehouses.Count}");
                foreach (var wh in Warehouses)
                {
                    Debug.WriteLine($"✅ Magazyn: {wh.Id} - {wh.Name}");
                }

                Console.WriteLine("✔ LoadWarehousesAsync zakończone");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Błąd podczas ładowania magazynów: {ex.Message}");
            }

        }

        private async void OnLoadWarehouseReport()
        {
            if (SelectedWarehouse is null)
                return;                       // nic nie wybrano

            await LoadWarehouseMetricsAsync(); // ładujesz metryki
            await LoadTopProductsChartAsync();
        }

        private async Task LoadWarehouseMetricsAsync()
        {
            try
            {
                // pobierasz tylko to, co potrzeba – np. jedno zapytanie do API
                ProductsQty = await _productService.GetProductsCountAsync(SelectedWarehouse.Id);
                OpenOrdersQty = await _orderService.GetOpenOrdersCountAsync(SelectedWarehouse.Id);
                LowStockItemCount = await _productService.GetLowStockItemCountAsync(SelectedWarehouse.Id);
                await LoadRecentOrdersAsync();
                await LoadRecentShipmentsAsync();
                // Jeżeli chcesz więcej metryk (np. OrdersOpen, LowStockQty …)
                // dodajesz kolejne pola i ładujesz je w tym samym miejscu.
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Błąd liczenia produktów: {ex.Message}");
                ProductsQty = 0;
                OpenOrdersQty = 0;
                LowStockItemCount = 0;
            }
        }

        private List<SKColor> GetRandomWarmColors(int count)
        {
            string[] warmHexColors = new[]
            {
        "#c2255c", "#e827e8", "#ed1818", "#260ceb",
        "#02ccf0", "#02f016", "#dcf002", "#f0a102", "#f03602"
            };

            var rng = new Random();
            return warmHexColors
                .OrderBy(_ => rng.Next())
                .Take(count)
                .Select(hex => SKColor.Parse(hex))
                .ToList();
        }

        public async Task LoadRecentOrdersAsync()
        {
            if (SelectedWarehouse == null)
                return;

            var orders = await _orderService.GetRecentOrdersAsync(SelectedWarehouse.Id);
            RecentOrders = new ObservableCollection<OrderDto>(orders);
        }

        private ObservableCollection<RecentShipmentDto> recentShipments;
        public ObservableCollection<RecentShipmentDto> RecentShipments
        {
            get => recentShipments;
            set => SetProperty(ref recentShipments, value);
        }

        public async Task LoadRecentShipmentsAsync()
        {
            if (SelectedWarehouse == null)
                return;

            var service = new ShipmentService();
            var list = await service.GetRecentShipmentsViewModelAsync(SelectedWarehouse.Id);
            RecentShipments = new ObservableCollection<RecentShipmentDto>(list);
        }
    }
}

