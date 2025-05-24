using WarehouseController.Model;
using WarehouseController.ViewModel;
using WarehouseController.ViewModel.ProductVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace WarehouseController.View.ProductView;

public partial class AddProductPage : ContentPage
{
	public Product Item { get; set; }
	public AddProductPage()
    {
        InitializeComponent();
        BindingContext = new AddProductViewModel();
      
       
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AddProductViewModel vm)
        {
            await vm.LoadDataAsync();
        }
    }
}