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
            Debug.WriteLine("CategoryViewModel constructed");
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
                    Debug.WriteLine($"SelectedCategory set to: {value?.Name} (Id: {value?.Id})");
                    // Notify that the selected category has changed
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }
        }
        async void OnSelectedCategory(Category category)
        {
            Debug.WriteLine($"OnSelectedCategory called with: {category?.Name} (Id: {category?.Id})");
            await GoToDetailsPage(category);
        }

        public override async Task GoToAddPage()
        {
            Debug.WriteLine("Navigating to AddCategoryPage");
            // Załaduj nową stronę przez Shell
            await Shell.Current.GoToAsync(nameof(AddCategoryPage));
        }
        public override async Task GoToDetailsPage(Category category)
        {
            if (category == null) { Debug.WriteLine("Selected category is null"); return; }
            Debug.WriteLine($"GoToDetailsPage navigating to details of category: {category.Name} (Id: {category.Id})");
            SelectedCategory = category;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DetailsCategoryPage)}?{nameof(DetailsCategoryViewModel.ItemId)}={category.Id}");
        }
    }
}
