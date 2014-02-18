using Progas.Portal.Common;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IPedidosVendaLinha : ICompleteRepository<PedidoVendaLinha>
    {
        IPedidosVendaLinha CotacaoPedidoContendo(string filtroCotacao);
    }
}
