using System.Threading.Tasks;

namespace UserAPI.Domain.Interfaces.Transactions
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
        void Rollback();
    }
}
