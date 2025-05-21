using WarehouseController.ViewModel.SupplierVM;

namespace WarehouseController.View.SupplierView;

public partial class DetailsSupplierPage : ContentPage
{
	public DetailsSupplierPage()
	{
		InitializeComponent();
		BindingContext = new DetailSupplierViewModel();
    }
}