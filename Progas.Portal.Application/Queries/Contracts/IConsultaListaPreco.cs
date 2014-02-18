using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaListaPreco
    {
        IList<ListaPrecoCadastroVm> Listar(PaginacaoVm paginacaoVm, ListaPrecoCadastroVm filtro);
        IList<ListaPrecoCadastroVm> ListarTodas();
    }
}
