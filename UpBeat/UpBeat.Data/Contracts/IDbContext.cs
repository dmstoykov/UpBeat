using System;

namespace UpBeat.Data.Contracts
{
    public interface IDbContext
    {
        int SaveChanges();
    }
}
