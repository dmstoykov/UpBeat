using Bytes2you.Validation;
using UpBeat.Data.Contracts;

namespace UpBeat.Data.UnitOfWork
{
    public class SaveChanges : ISaveChanges
    {
        private readonly IDbContext context;

        public SaveChanges(IDbContext context)
        {
            Guard.WhenArgument(context, context.GetType().Name).IsNull().Throw();
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
