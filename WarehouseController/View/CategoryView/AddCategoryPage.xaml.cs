using WarehouseController.ViewModel.CategoryVM;

namespace WarehouseController.View.CategoryView;

public partial class AddCategoryPage : ContentPage
{
	public AddCategoryPage()
	{
		BindingContext = new AddCategoryViewModel();
        InitializeComponent();
	}
}