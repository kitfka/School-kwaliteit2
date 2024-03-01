using Webshop.Service;
using Webshop.ViewModel;

namespace Webshop.Maui.Pages;

[QueryProperty(nameof(Guid), "id")]
public partial class EditProductPage : ContentPage
{
    EditProductViewModel viewModel => BindingContext as EditProductViewModel;

    public Guid Guid
    {
        set
        {
            viewModel.SetProduct(value);
        }
    }

    public EditProductPage(EditProductViewModel editProductViewModel)
	{
        BindingContext = editProductViewModel;
		InitializeComponent();
	}

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        viewModel.SaveProduct();
    }
}