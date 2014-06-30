using System.Collections.Generic;
using Progas.Portal.DTO;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaStatusDoPedidoDeVenda
    {
        IList<StatusDoPedidoDeVendaDTO> ListarTodos();
    }
}