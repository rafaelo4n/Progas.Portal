using Progas.Portal.DTO;
using Progas.Portal.ViewModel;
using System.Collections.Generic;


namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaPedidoVenda
    {
        KendoGridVm Listar(PaginacaoVm paginacaoVm, PedidoVendaFiltroVm filtro);

        IList<PedidoVendaLinhaCadastroVm> ListarItensDoPedido(string idDoPedido);

        PedidoVendaCadastroVm Consultar(string idDoPedido);
        bool PedidoExiste(string idDoPedido);

        PedidoVendaImprimirDto Impressao(string idDaCotacao);
    }
}
