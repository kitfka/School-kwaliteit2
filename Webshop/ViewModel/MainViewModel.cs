using Webshop.Model;
using Webshop.Model.Interfaces;

namespace Webshop.ViewModel;


public class MainViewModel : ViewModelBase
{

    private int Counter = 0;

    private string bindingText = "Hello World!";

    public string BindingText
    {
        get { return bindingText; }
        set
        {
            bindingText = value;
            NotifyPropertyChanged();
        }
    }

    public MainViewModel()
    {
    }

    public void IncreaseCounter()
    {
        Counter++;
        BindingText = $"Counter: {Counter}";
    }
}
