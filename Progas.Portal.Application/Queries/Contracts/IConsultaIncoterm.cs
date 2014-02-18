using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaIncoterm
    {
        IList<IncotermCadastroVm> Listar(PaginacaoVm paginacaoVm, IncotermCadastroVm filtro);
        IList<IncotermCadastroVm> ListarTodas();
    }
}
