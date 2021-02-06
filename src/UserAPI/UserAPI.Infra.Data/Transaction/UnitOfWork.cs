using System.Threading.Tasks;
using UserAPI.Domain.Interfaces.Transactions;
using UserAPI.Infra.Data.Context;

namespace UserAPI.Infra.Data.Transaction
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserApiContext _context;

        public UnitOfWork(UserApiContext context)
        {
            _context = context;
        }
        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            //data still in memory
        }
    }
}
