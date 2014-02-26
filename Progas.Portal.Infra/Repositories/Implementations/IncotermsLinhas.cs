using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class IncotermsLinhas : CompleteRepositoryNh<IncotermLinhas>, IIcontermslinhas
    {
        public IncotermsLinhas(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }
    }
}
