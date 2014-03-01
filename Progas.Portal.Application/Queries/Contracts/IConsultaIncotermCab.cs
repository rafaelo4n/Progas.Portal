using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaIncotermCab
    {
        IList<IncotermsCabCadastroVm> ListarTodas();
    }
}
