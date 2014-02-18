using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaUsuario
    {
        KendoGridVm Listar(PaginacaoVm paginacaoVm, UsuarioFiltroVm usuarioFiltroVm);
        UsuarioConsultaVm ConsultaPorLogin(string login);
        IList<PerfilVm> PerfisDoUsuario(string login);
        string ConfirmaLogin(string login);
    }
}