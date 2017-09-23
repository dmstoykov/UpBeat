using System;
using System.Collections.Generic;
using System.Data.Entity;
using UpBeat.Data.Contracts;

namespace UpBeat.Data
{
    public class UpBeatDbContext: DbContext, IDbContext
    {
        public UpBeatDbContext()
            : base("DefaultConnection")
        {
        }


    }
}
