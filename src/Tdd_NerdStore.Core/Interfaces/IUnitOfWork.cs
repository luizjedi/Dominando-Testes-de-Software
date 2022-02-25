using System.Threading.Tasks;

namespace Tdd_NerdStore.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
