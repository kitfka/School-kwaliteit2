using Webshop.Model.Config;

namespace Webshop.Tests;
public class ConfigFacts
{
    [Fact]
    public void TestString_Works()
    {
        // Arrange
        Config.SetTestMode(true);
        string expected = "TestString_Works";
        string actual;

        // Act
        Config.TestString = expected;
        actual = Config.TestString;

        // Assert.
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TestBool_Works(bool expected)
    {
        // Arrange
        Config.SetTestMode(true);
        bool actual;

        // Act
        Config.TestBool = expected;
        actual = Config.TestBool;

        // Assert.
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(999999999)]
    public void TestInt_Works(int expected)
    {
        // Arrange
        Config.SetTestMode(true);
        int actual;

        // Act
        Config.TestInt = expected;
        actual = Config.TestInt;

        // Assert.
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestList_Works()
    {
        // Arrange
        Config.SetTestMode(true);
        List<string> expected = ["a", "b", "c"];
        List<string> actual;

        // Act
        Config.TestList = expected;
        actual = Config.TestList;

        // Assert.
        Assert.Equal(expected, actual);
    }
}
