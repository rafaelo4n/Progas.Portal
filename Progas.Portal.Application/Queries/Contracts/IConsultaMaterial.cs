using Progas.Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaMaterial
    {
        KendoGridVm Listar(PaginacaoVm paginacaoVm, MaterialFiltroVm filtro);

        IList<MaterialCadastroVm> Listar(PaginacaoVm paginacaoVm, MaterialCadastroVm filtro);

        IList<MaterialCadastroVm> ListarTodas();
        IList<MaterialCadastroVm> ListarCentro();
    }
}
