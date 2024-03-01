using Webshop.Model;

namespace Webshop.Service;

public class CartService(ProductService productService)
{
    private readonly List<CartItem> cartItems = [];
    private readonly List<UserOrder> userOrders = [];

    private readonly ProductService productService = productService;

    public void AddToCart(Product product, int amount) => AddToCart(product, Guid.Empty, amount);
    public void AddToCart(Product product, Guid variantId, int Count)
    {
        foreach (var item in cartItems)
        {
            if (item.ProductId == product.Id && item.Variant == variantId)
            {
                item.Count += Count;
                return;
            }
        }

        cartItems.Add(new CartItem { ProductId = product.Id, Count = Count, Variant = variantId });
    }

    public IList<CartItem> GetCart()
    {
        return cartItems;
    }

    public Tuple<bool, string> Order()
    {
        if (cartItems.Count == 0)
        {

            return Tuple.Create(false, "Empty card");
        }
        decimal totalPrice = 0;
        UserOrder newUserOrder = new();

        foreach (var item in GetCart())
        {
            totalPrice += item.Count * productService.GetProduct(item.ProductId).Price;
            newUserOrder.OrderItems.Add(new OrderItem { Count = item.Count, ProductId = item.ProductId, Variant = item.Variant });
        }

        newUserOrder.TotalPrice = totalPrice;


        userOrders.Add(newUserOrder);
        cartItems.Clear();

        return Tuple.Create(true, "");
    }

    public IList<UserOrder> GetUserOrder()
    {
        return userOrders;
    }
}



