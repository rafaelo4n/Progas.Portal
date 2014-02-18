using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaTipoPedido
    {
        IList<TipoPedidoCadastroVm> Listar(PaginacaoVm paginacaoVm, TipoPedidoCadastroVm filtro);
        IList<TipoPedidoCadastroVm> ListarTodas();     
    }
}
