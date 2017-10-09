using System.Collections.Generic;

namespace UpBeat.Services.Contracts
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();
    }
}
