using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
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
            Guard.WhenArgument(context, "IDbContext").IsNull().Throw();

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

        public T Get(int id)
        {
            return this.context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            Guard.WhenArgument(entity, "EntityToAdd").IsNull().Throw();

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

        public void AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.Add(entity);
            }
        }

        public void Remove(T entity)
        {
            Guard.WhenArgument(entity, "EntityToRemove").IsNull().Throw();

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            DbEntityEntry entry = this.context.Entry(entity);

            entry.State = EntityState.Modified;
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.Remove(entity);
            }
        }

        public void Update(T entity)
        {
            Guard.WhenArgument(entity, "EntityToUpdate").IsNull().Throw();

            DbEntityEntry entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
