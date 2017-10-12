using System.Collections.Generic;

namespace UpBeat.Services.Contracts
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Update(T entity);
    }
}
