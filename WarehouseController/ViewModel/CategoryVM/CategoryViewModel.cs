using WarehouseController.ViewModel.Abstract;
using WarehouseController.Model;
using WarehouseController.View.CategoryView;
using System.Diagnostics;

namespace WarehouseController.ViewModel.CategoryVM
{
    public partial class CategoryViewModel : AItemListViewModel<Category>
    {
        
        private Category? _selectedCategory;
        public Command RefreshCommand { get; }
        public Command<Category> AboutCommand { get; }
        public CategoryViewModel():base("Categories")
        {
            RefreshCommand = LoadItemsCommand;
            AboutCommand = new Command<Category>(OnSelectedCategory);
        }

        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }
        }
        async void OnSelectedCategory(Category category)
        {
            await GoToDetailsPage(category);
        }

        public override async Task GoToAddPage()
        {
            await Shell.Current.GoToAsync(nameof(AddCategoryPage));
        }
        public override async Task GoToDetailsPage(Category category)
        {
            if (category == null) { return; }
            SelectedCategory = category;

            await Shell.Current.GoToAsync($"{nameof(DetailsCategoryPage)}?{nameof(DetailsCategoryViewModel.ItemId)}={category.Id}");
        }
    }
}
