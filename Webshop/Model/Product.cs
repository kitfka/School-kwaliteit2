namespace Webshop.Model;

public class Product : BaseEntity
{
    public Product() { }
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public decimal Price { get; set; } = decimal.Zero;

    public Guid SellerId { get; set; } 

    public string ImageSource { get; set; } = "sky.jpg";

    public int Stock { get; set; }
}
