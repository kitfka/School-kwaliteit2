using GitServer.ApplicationCore.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitServer.Infrastructure.LiteDB;
public class LiteDBService<TEntity> : IDatabaseService<TEntity>
        where TEntity : BaseEntity, new()
{
    private string _stringConnection;
    private LiteDatabase _db;
    protected ILiteCollection<TEntity> _collection;

    //public DatabaseService()
    public LiteDBService()
    {
        //_stringConnection = string.Format("filename={0};journal=false", DependencyService.Get<IFileService>().GetLocalFilePath("LiteDB.db"));
        _stringConnection = "";

        _db = new LiteDatabase(_stringConnection);
        _collection = _db.GetCollection<TEntity>();
        
    }

    public void Insert(TEntity entity)
    {
        _collection.Insert(entity);
    }
}