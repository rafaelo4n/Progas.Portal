using Progas.Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaCliente
    {
        KendoGridVm Listar(PaginacaoVm paginacaoVm, ClienteFiltroVm filtro);

        IList<ClienteCadastroVm> Listar(PaginacaoVm paginacaoVm, ClienteCadastroVm filtro);        
        IList<ClienteCadastroVm> Listar();
    }
}
