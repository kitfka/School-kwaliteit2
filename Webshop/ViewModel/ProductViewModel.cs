using Webshop.Model;

namespace Webshop.ViewModel;

public class ProductViewModel : ViewModelBase
{
    private Product product = new();

    public Product Product
    {
        get { return product; }
        set
        {
            product = value;
            NotifyPropertyChanged();
        }
    }



    public ProductViewModel()
    {

    }
}
