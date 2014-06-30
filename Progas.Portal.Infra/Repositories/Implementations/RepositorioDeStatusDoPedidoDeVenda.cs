using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class RepositorioDeStatusDoPedidoDeVenda : ReadOnlyRepositoryNh<StatusDoPedidoDeVenda>, IRepositorioDeStatusDoPedidoDeVenda
    {
        public RepositorioDeStatusDoPedidoDeVenda(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public IRepositorioDeStatusDoPedidoDeVenda BuscaPorCodigo(string codigo)
        {
            Query = Query.Where(x => x.Codigo == codigo);
            return this;
        }
    }
}