using System.Diagnostics;
using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;
using WarehouseController.ViewModel.CategoryVM;
namespace WarehouseController.View.CategoryView;

public partial class DetailsCategoryPage : ContentPage
{
	public DetailsCategoryPage()
	{
		BindingContext = new DetailsCategoryViewModel();
        InitializeComponent();
	}
    
}