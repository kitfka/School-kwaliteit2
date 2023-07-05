using GitServer.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GitServer.Infrastructure.LiteDB;
public interface IDatabaseService<TEntity>
        where TEntity : BaseEntity, new()
{
    void Insert(TEntity entity);
}