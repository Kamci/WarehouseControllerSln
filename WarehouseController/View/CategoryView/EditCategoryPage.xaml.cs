using WarehouseController.ViewModel.CategoryVM;

namespace WarehouseController.View.CategoryView;

public partial class EditCategoryPage : ContentPage
{
	public EditCategoryPage()
	{
		BindingContext = new EditCategoryViewModel();
        InitializeComponent();
	}
}