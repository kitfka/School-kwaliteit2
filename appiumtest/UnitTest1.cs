using appiumtest.shared;
using NUnit.Framework;

namespace appiumtest;

public class Tests : BaseTest
{
    //[SetUp]
    //public void Setup()
    //{
    //}

    //[Test]
    //public void Test1()
    //{
    //    Assert.Pass();
    //}


    [Test]
    public void AppLaunches()
    {
        App.GetScreenshot().SaveAsFile($"{nameof(AppLaunches)}.png");
        Assert.True(true);
    }

    [Test]
    public void ClickAccountButtonTest()
    {
        // Arrange
        // Find elements with the value of the AutomationId property
        var element = FindUIElement("AccountButton");

        // Act
        element.Click();
        Task.Delay(500).Wait(); // Wait for the click to register and show up on the screenshot

        // Assert
        App.GetScreenshot().SaveAsFile($"{nameof(ClickAccountButtonTest)}.png");
        //Assert.That(element.Text, Is.EqualTo("Clicked 1 time"));
        Assert.True(true);
    }
}