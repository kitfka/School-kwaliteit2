using LiteDB;

namespace Webshop.Api;


/// <summary>
/// Very simple wrapper for the LiteDatabase.
/// </summary>
public class LiteDBService(IConfiguration configuration)
{
    public readonly ILiteDatabase data = new LiteDatabase(configuration.GetConnectionString("LiteDB"));
    public ILiteStorage<string> FileStorage => data.FileStorage;
}
