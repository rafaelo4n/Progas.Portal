using Progas.Portal.ViewModel;
using System.Collections.Generic;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaMaterial
    {
        KendoGridVm Listar(PaginacaoVm paginacaoVm, MaterialFiltroVm filtro);
        IList<MaterialCadastroVm> ListarTodas();
        IList<MaterialCadastroVm> ListarCentro();
    }
}
