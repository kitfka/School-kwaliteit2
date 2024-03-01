namespace Webshop.Data;

public enum BaseUrlOption
{
    Unspecified, // Local in debug, VM on Release mode.
    Local,
    VM,
    Online, // Use cloudflared address. Won't work after project is compleeted (see documentation section API for more info)
}
