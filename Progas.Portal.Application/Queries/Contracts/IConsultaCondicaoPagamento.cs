using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaCondicaoPagamento
    {
        

        IList<CondicaoDePagamentoCadastroVm> Listar(PaginacaoVm paginacaoVm, CondicaoDePagamentoCadastroVm filtro);
        IList<CondicaoDePagamentoCadastroVm> ListarTodas();
        KendoGridVm Listar(PaginacaoVm paginacaoVm, CondicaoPagamentoFiltroVm filtro);
    }

}
