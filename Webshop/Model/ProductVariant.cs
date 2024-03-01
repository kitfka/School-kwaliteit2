namespace Webshop.Model;

public class ProductVariant : BaseEntity
{

    public Guid ProductId { get; set; }

    public string ImageSource { get; set; } = "sky.jpg";

    public decimal Price { get; set; }
}
