using WarehouseController.ViewModel;
namespace WarehouseController;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
    public string Warehouse { get; set; }
}

public partial class ProductsPage : ContentPage
{
    public ProductsPage()
    {
        InitializeComponent();
        BindingContext = new ProductViewModel();
       

        var products = new List<Product>
        {
            new Product { Name = "Laptop", Price = 4999.99m, Quantity = 12, Category = "Electronics", Warehouse = "Warehouse A" },
            new Product { Name = "Mouse", Price = 99.99m, Quantity = 50, Category = "Peripherals", Warehouse = "Warehouse B" },
            new Product { Name = "Monitor", Price = 899.99m, Quantity = 8, Category = "Electronics", Warehouse = "Warehouse A" },
        };

        ProductsList.ItemsSource = products;
    }

    private void ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ProductsList.SelectedItem != null)
        {
            var selectedProduct = (Product)ProductsList.SelectedItem;
            DisplayAlert("Product Selected", $"You selected: {selectedProduct.Name}", "OK");
        }
    }

   
}