using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Webshop.Api.Tests;

public class MockConfiguration : IConfiguration
{
    private readonly Dictionary<string, string> _configuration = new()
    {
        { "Logging:LogLevel:Default", "Information" },
        { "Logging:LogLevel:Microsoft.AspNetCore", "Warning" },
        { "AllowedHosts", "*" },
        { "JWT:SecretKey", "asecretkeyyouneedtoreplace" },
        { "JWT:Issuer", "TestIssuer" },
        { "JWT:Audience", "TestAudience" },
        { "ConnectionStrings:LiteDB", ":memory:" },
    };
    public string this[string key]
    {
        get => _configuration[key];
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        set
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            _configuration[key] = value;
        }
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        throw new NotImplementedException();
    }

    public IChangeToken GetReloadToken()
    {
        throw new NotImplementedException();
    }

    public IConfigurationSection GetSection(string key)
    {
        if (key == "ConnectionStrings")
        {
            return new MockConfigurationSection() { mockConfiguration = this, Key = key, Path = "", Value = ":temp:" };
        }
        throw new NotImplementedException();
    }
}

public class MockConfigurationSection : IConfigurationSection
{
    public MockConfiguration mockConfiguration { get; set; } = new();

    public string? this[string key] { get => mockConfiguration[$"{Key}:{key}"]; set => throw new NotImplementedException(); }

    public string Key { get; set; } = "";

    public string Path { get; set; } = "";

    public string? Value { get; set; } = "";

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        throw new NotImplementedException();
    }

    public IChangeToken GetReloadToken()
    {
        throw new NotImplementedException();
    }

    public IConfigurationSection GetSection(string key)
    {
        throw new NotImplementedException();
    }
}
