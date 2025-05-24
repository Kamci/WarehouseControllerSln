using System.Diagnostics;
using WarehouseController.Model;
using WarehouseController.ViewModel.WarehouseVM;

namespace WarehouseController.View.WarehouseView;

public partial class AddWarehousePage : ContentPage
{
    public Warehouse Item { get; set; }
    public AddWarehousePage()
    {
        InitializeComponent();
        BindingContext = new AddWarehouseViewModel();


    }
   
}