using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class MotivosDeRecusa : ReadOnlyRepositoryNh<MotivoDeRecusa>, IMotivosDeRecusa
    {
        public MotivosDeRecusa(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public IMotivosDeRecusa BuscarLista(string[] codigoDosMotivosDeRecusa)
        {
            Query = Query.Where(x => codigoDosMotivosDeRecusa.Contains(x.Codigo));
            return this;
        }
    }
}