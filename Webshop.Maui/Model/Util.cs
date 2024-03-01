namespace Webshop.Maui.Model;

public static class Util
{
    public static bool IsNotDebug => !IsDebug;
    public static bool IsDebug
    {
        get
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
