using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class IncotermsLinhas : CompleteRepositoryNh<IncotermLinhas>, IIncotermsLinhas
    {
        public IncotermsLinhas(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public IIncotermsLinhas FiltraPorId(int id)
        {
            Query = Query.Where(x => x.pro_id_incotermLinha == id);
            return this;
        }

        public IIncotermsLinhas DoCabecalho(string codigoDoCabecalho)
        {
            Query = Query.Where(x => x.CodigoIncotermCab == codigoDoCabecalho);
            return this;
        }
    }
}
