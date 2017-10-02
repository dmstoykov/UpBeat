using System;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using UpBeat.Data.Models;
using UpBeat.Data.Models.Contracts;
using UpBeat.Data.Contracts;
using System.Data.Entity.Infrastructure;

namespace UpBeat.Data
{
    public class MsSqlDbContext : IdentityDbContext<User>, IDbContext
    {
        public MsSqlDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        public static MsSqlDbContext Create()
        {
            return new MsSqlDbContext();
        }

        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return this.Set<TEntity>();
        }

        DbEntityEntry IDbContext.Entry<T>(T entity)
        {
            return this.Entry<T>(entity);
        }
    }
}
