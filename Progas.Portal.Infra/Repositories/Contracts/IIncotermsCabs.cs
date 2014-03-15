using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IIncotermsCabs : ICompleteRepository<IncotermCab>
    {
        IIncotermsCabs FiltraPorId(int id);
    }
}
