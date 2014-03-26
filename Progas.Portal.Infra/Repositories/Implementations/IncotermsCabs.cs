using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;


namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class IncotermsCabs : CompleteRepositoryNh<IncotermCab>, IIncotermsCabs
    {
        public IncotermsCabs(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public IIncotermsCabs FiltraPorId(int id)
        {
            Query = Query.Where(x => x.pro_id_incotermCab == id);
            return this;
        }
    }
}
