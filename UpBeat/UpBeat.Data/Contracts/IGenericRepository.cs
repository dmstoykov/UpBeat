using System;
using System.Linq;
using UpBeat.Data.Models.Contracts;

namespace UpBeat.Data.Contracts
{
    public interface IGenericRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }

        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
