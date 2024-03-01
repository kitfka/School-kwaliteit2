namespace Webshop.Model;

public class UserOrder : BaseEntity
{
    public Guid UserId { get; set; }
    public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public decimal TotalPrice { get; set; }

}
