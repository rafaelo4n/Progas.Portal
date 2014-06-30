using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IRepositorioDeStatusDoPedidoDeVenda: IReadOnlyRepository<StatusDoPedidoDeVenda>
    {
        IRepositorioDeStatusDoPedidoDeVenda BuscaPorCodigo(string codigo);
    }
}
