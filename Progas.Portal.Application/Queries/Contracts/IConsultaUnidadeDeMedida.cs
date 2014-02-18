using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaUnidadeDeMedida
    {

        KendoGridVm Listar(PaginacaoVm paginacaoVm, UnidadeDeMedidaFiltroVm filtro);
        IList<UnidadeDeMedidaCadastroVm> Listar(PaginacaoVm paginacaoVm, UnidadeDeMedidaCadastroVm filtro);
        IList<UnidadeDeMedidaCadastroVm> ListarTodos();
    }

}
