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
    }
}
