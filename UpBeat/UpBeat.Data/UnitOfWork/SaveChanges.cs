using Bytes2you.Validation;
using UpBeat.Data.Contracts;

namespace UpBeat.Data.UnitOfWork
{
    public class SaveChanges : ISaveChanges
    {
        private readonly IDbContext context;

        public SaveChanges(IDbContext context)
        {
            Guard.WhenArgument(context, "IDbContext").IsNull().Throw();
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
