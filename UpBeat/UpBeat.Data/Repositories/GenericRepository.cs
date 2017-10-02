using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Bytes2you.Validation;
using UpBeat.Data.Contracts;
using UpBeat.Data.Models.Contracts;

namespace UpBeat.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class, IDeletable
    {
        private readonly IDbContext context;

        public GenericRepository(IDbContext context)
        {
            Guard.WhenArgument(context, nameof(context)).IsNull().Throw();

            this.context = context;
        }

        public IQueryable<T> All
        {
            get
            {
                return this.context.Set<T>().Where(x => !x.IsDeleted);
            }
        }

        public IQueryable<T> AllAndDeleted
        {
            get
            {
                return this.context.Set<T>();
            }
        }

        public void Add(T entity)
        {
            Guard.WhenArgument(entity, nameof(entity)).IsNull().Throw();

            DbEntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.context.Set<T>().Add(entity);
            }
        }

        public void Delete(T entity)
        {
            Guard.WhenArgument(entity, nameof(entity)).IsNull().Throw();

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            DbEntityEntry entry = this.context.Entry(entity);

            entry.State = EntityState.Modified;
        }

        public void Update(T entity)
        {
            Guard.WhenArgument(entity, nameof(entity)).IsNull().Throw();

            DbEntityEntry entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
