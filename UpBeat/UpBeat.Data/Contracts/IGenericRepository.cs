using System;
using System.Collections.Generic;
using System.Linq;
using UpBeat.Data.Models.Contracts;

namespace UpBeat.Data.Contracts
{
    public interface IGenericRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }

        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
