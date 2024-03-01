using Webshop.Model;

namespace Webshop.ViewModel;

public class CartViewModel : ViewModelBase
{

    private decimal totalPrice;

    public decimal TotalPrice
    {
        get { return totalPrice; }
        set
        {
            totalPrice = value;
            NotifyPropertyChanged();
        }
    }

}
