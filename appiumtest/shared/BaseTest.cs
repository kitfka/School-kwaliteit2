using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appiumtest.shared;
public abstract class BaseTest
{
    protected AppiumDriver App => AppiumSetup.App;

    // This could also be an extension method to AppiumDriver if you prefer
    protected AppiumElement FindUIElement(string id)
    {
        if (App is WindowsDriver)
        {
            return App.FindElement(MobileBy.AccessibilityId(id));
        }

        return App.FindElement(MobileBy.Id(id));
    }
}