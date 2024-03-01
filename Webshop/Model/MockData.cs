namespace Webshop.Model;

public static class MockData
{
    public static readonly IList<Product> Products = new List<Product>()
    {
        new ()
        {
            Id = Guid.Parse("46295400-FA7E-46BC-8A8B-E70ECECD5692"),
            Name = "Product 1",
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lacus viverra vitae congue eu consequat. Tortor vitae purus faucibus ornare suspendisse sed nisi. Posuere morbi leo urna molestie. Nunc mattis enim ut tellus elementum sagittis vitae et. Quis auctor elit sed vulputate mi sit amet mauris. Quis varius quam quisque id diam vel.",
            Price = 1,
        },
        new ()
        {
            Id = Guid.Parse("179741E9-246D-44CF-BE92-38C34419B9DC"),
            Name = "Product 2",
            Description = "Consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lacus viverra vitae congue eu consequat. Tortor vitae purus faucibus ornare suspendisse sed nisi. Posuere morbi leo urna molestie. Nunc mattis enim ut tellus elementum sagittis vitae et. Quis auctor elit sed vulputate mi sit amet mauris. Quis varius quam quisque id diam vel.",
            Price = 22,
        },
        new ()
        {
            Id = Guid.Parse("A9320339-B35B-4F3D-B4B0-EE14BCD9076F"),
            Name = "Product 3",
            Description = "Lacus viverra vitae congue eu consequat. Tortor vitae purus faucibus ornare suspendisse sed nisi. Posuere morbi leo urna molestie. Nunc mattis enim ut tellus elementum sagittis vitae et. Quis auctor elit sed vulputate mi sit amet mauris. Quis varius quam quisque id diam vel.",
            Price = 333,
        },
        new ()
        {
            Id = Guid.Parse("4A9C0D4A-8BA6-4646-8301-6E8CEA939FDB"),
            Name = "koffie kopje",
            Description = "Quis varius quam quisque id diam vel.",
            Price = 4.50m,
            ImageSource = "https://cith.nl/Admin/Webshop/raw/main/Webshop.Maui/Resources/Images/ProductPictures/13/IMG20231011124056.jpg",
        }
    };

    public static readonly IList<ProductVariant> ProductVariants = new List<ProductVariant>()
    {
        new ()
        {
            Id = Guid.Parse("7D613777-F54E-468E-98CE-3E3CB5268615"),
            ImageSource = "img20231011120337.jpg",
            Price = 21,
            ProductId = Guid.Parse("46295400-FA7E-46BC-8A8B-E70ECECD5692"),
        },
        new ()
        {
            Id = Guid.Parse("B850102E-994E-4A65-92A3-56ABDEAF7D48"),
            ImageSource = "img20231011120341.jpg",
            Price = 23,
            ProductId = Guid.Parse("46295400-FA7E-46BC-8A8B-E70ECECD5692"),
        }
    };
}
