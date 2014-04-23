using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.DTO;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaStatusDoPedidoDeVenda : IConsultaStatusDoPedidoDeVenda
    {
        private readonly IRepositorioDeStatusDoPedidoDeVenda _repositorioDeStatusDoPedidoDeVenda;

        public ConsultaStatusDoPedidoDeVenda(IRepositorioDeStatusDoPedidoDeVenda repositorioDeStatusDoPedidoDeVenda)
        {
            _repositorioDeStatusDoPedidoDeVenda = repositorioDeStatusDoPedidoDeVenda;
        }

        public IList<StatusDoPedidoDeVendaDTO> ListarTodos()
        {
            return _repositorioDeStatusDoPedidoDeVenda.List().Select(status =>
                new StatusDoPedidoDeVendaDTO
                {
                    Codigo = status.Codigo,
                    Descricao = status.Descricao
                }
                ).ToList();
        }
    }
}