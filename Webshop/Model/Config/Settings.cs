using Webshop.Data;

namespace Webshop.Model.Config;

public partial class Config
{
    public static UITheme UITheme
    {
        get => ReadSetting(nameof(UITheme), UITheme.Unspecified);
        set => WriteSetting(nameof(UITheme), value);
    }

    public static BaseUrlOption BaseUrlOption
    {
        get => ReadSetting(nameof(BaseUrlOption), BaseUrlOption.Unspecified);
        set => WriteSetting(nameof(BaseUrlOption), value);
    }

    /// <summary>
    /// USED FOR TESTING PERPOSES.
    /// The next are just variables to test if writing and reading settings works.
    /// </summary>
    #region ConfigTestVariables
    public static string TestString
    {
        get => ReadSetting(nameof(TestString), "Default");
        set => WriteSetting(nameof(TestString), value);
    }
    public static int TestInt
    {
        get => ReadSetting(nameof(TestInt), 0);
        set => WriteSetting(nameof(TestInt), value);
    }
    public static bool TestBool
    {
        get => ReadSetting(nameof(TestBool), false);
        set => WriteSetting(nameof(TestBool), value);
    }
    public static List<string> TestList
    {
        get => ReadSetting(nameof(TestList), new List<string>());
        set => WriteSetting(nameof(TestList), value);
    }
    #endregion
}