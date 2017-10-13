using System.Collections.Generic;

namespace UpBeat.Services.Contracts
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
