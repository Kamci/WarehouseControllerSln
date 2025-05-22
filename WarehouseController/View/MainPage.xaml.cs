using Microcharts;
using Microcharts.Maui;
using Microsoft.Maui.Devices;
using SkiaSharp;
using System.Diagnostics;
using WarehouseController.Model;

namespace WarehouseController;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        SetupChart();
        //LoadOrders();
        //LoadShipments();
    }

    private void SetupChart()
    {
        float labelSize;
        float valueSize;

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            labelSize = 36;
            valueSize = 36;
        }
        else
        {
            labelSize = 16;
            valueSize = 16;
        }

        var entries = new[]
        {
            new ChartEntry(212)
            {
                Label = "Product 1",
                ValueLabel = "212",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(248)
            {
                Label = "P2",
                ValueLabel = "248",
                Color = SKColor.Parse("#77d065")
            },
            new ChartEntry(128)
            {
                Label = "P3",
                ValueLabel = "128",
                Color = SKColor.Parse("#b455b6")
            },
            new ChartEntry(514)
            {
                Label = "P4",
                ValueLabel = "514",
                Color = SKColor.Parse("#3498db")
            },
            new ChartEntry(614)
            {
                Label = "P5",
                ValueLabel = "614",
                Color = SKColor.Parse("#8932a8")
            }
        };

        chartView.Chart = new BarChart
        {
            BackgroundColor = SKColors.LightPink,
            LabelTextSize = labelSize,
            ValueLabelTextSize = valueSize,
            IsAnimated = true,
            AnimationDuration = TimeSpan.FromSeconds(1),
            YAxisLinesPaint = new SKPaint
            {
                Color = SKColors.Black,
                StrokeWidth = 1,
                Style = SKPaintStyle.Stroke
            },
            Entries = entries
        };
    }
}

    //private void LoadOrders()
    //{
    //    var orders = new List<Order>
    //    {
    //        new Order { Name = "Order 1", Date = "10.04.2025", QtyOfProducts = 10 },
    //        new Order { Name = "Order 2", Date = "11.04.2025", QtyOfProducts = 20 },
    //        new Order { Name = "Order 3", Date = "12.04.2025", QtyOfProducts = 30 },
    //    };

    //    OrdersList.ItemsSource = orders;
    //}
    //private void LoadShipments()
    //{
    //    var shipments = new List<Shipment>
    //    {
    //        new Shipment { ShipmentDate = "10.04.2025", SupplierName = "Supplier 1", WarehouseId = "Warehouse 1" },
    //        new Shipment { ShipmentDate = "11.04.2025", SupplierName = "Supplier 2", WarehouseId = "Warehouse 2" },
    //        new Shipment { ShipmentDate = "12.04.2025", SupplierName = "Supplier 3", WarehouseId = "Warehouse 3" },
    //    };

    //    ShipmentsList.ItemsSource = shipments;
    //}


