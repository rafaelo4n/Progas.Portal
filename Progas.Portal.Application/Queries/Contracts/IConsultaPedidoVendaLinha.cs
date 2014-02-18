using Progas.Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaPedidoVendaLinha
    {
        KendoGridVm Listar(PaginacaoVm paginacaoVm, PedidoVendaLinhaCadastroVm filtro);
    }
}
