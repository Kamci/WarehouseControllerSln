using WarehouseController.ViewModel.SupplierVM;

namespace WarehouseController.View.SupplierView;

public partial class EditSupplierPage : ContentPage
{
	public EditSupplierPage()
	{
		BindingContext = new EditSupplierViewModel();
        InitializeComponent();
	}
}