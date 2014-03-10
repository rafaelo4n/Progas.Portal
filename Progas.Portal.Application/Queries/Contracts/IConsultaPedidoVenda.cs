using Progas.Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaPedidoVenda
    {
        // pesquisa da tela
        KendoGridVm Listar(PaginacaoVm paginacaoVm, PedidoVendaFiltroVm filtro);

        PedidoVendaCadastroVm ListarLinhasPedido(string id_cotacao);

        //PedidoVendaCadastroVm totalValfin(string id_cotacao);
        KendoGridVm ListarLinhasPedido(PaginacaoVm paginacaoVm, string cotacao);

        IList<PedidoVendaLinhaCadastroVm> ListarLinhasCotacao(string cotacao);

        PedidoVendaCadastroVm Consultar(string idDoPedido);
    }
}
