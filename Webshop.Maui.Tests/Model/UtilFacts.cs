using Webshop.Maui;

namespace Webshop.Maui.Tests.Model;

public class UtilFacts
{
    [Fact]
    public void IsDebugAndIsNotDebug_inverted()
    {
        Assert.NotEqual(Webshop.Maui.Model.Util.IsDebug, Webshop.Maui.Model.Util.IsNotDebug);
    }
}
