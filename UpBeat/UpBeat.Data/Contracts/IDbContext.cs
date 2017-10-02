using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace UpBeat.Data.Contracts
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry Entry<T>(T entity) where T : class;

        int SaveChanges();

        void Dispose();
    }
}
