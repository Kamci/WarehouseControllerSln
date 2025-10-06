using System.Diagnostics;
using WarehouseController.Model;
using WarehouseController.ViewModel.SupplierVM;

namespace WarehouseController.View.SupplierView;

public partial class AddSupplierPage : ContentPage
{
    public Supplier Item { get; set; }
    public AddSupplierPage()
	{
		InitializeComponent();
        BindingContext = new AddSupplierViewModel();
    }
}