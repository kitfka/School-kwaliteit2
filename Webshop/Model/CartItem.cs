namespace Webshop.Model;


public class CartItem
{
    public Guid ProductId { get; set; }
    public Guid Variant { get; set; } = Guid.Empty;
    public int Count { get; set; } = 0;
}