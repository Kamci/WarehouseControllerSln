using WarehouseController.Model;
using WarehouseController.ViewModel.CategoryVM;

namespace WarehouseController;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CategoryPage : ContentPage
{
	public CategoryViewModel _viewModel;
    public CategoryPage()
	{
		BindingContext = _viewModel = new CategoryViewModel();
        InitializeComponent();
	}
    private async void CategoriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CategoriesList.SelectedItem is Category selectedCategory)
        {
            bool isConfirmed = await DisplayAlert("Product Selected", $"You selected: {selectedCategory.Name}", "OK", "Cancel");

            if (isConfirmed)
            {
                _viewModel.SelectedCategory = selectedCategory;
            }
            else
            {
                CategoriesList.SelectedItem = null;
            }

        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CategoryViewModel vm)
            vm.LoadItemsCommand.Execute(null);
        _viewModel.OnAppearing();
    }

}