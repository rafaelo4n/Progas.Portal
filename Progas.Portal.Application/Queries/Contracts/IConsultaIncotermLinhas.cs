using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaIncotermLinhas
    {
        IList<IncotermLinhasCadastroVm> ListarTodas();
        IList<IncotermLinhasCadastroVm> ListarPorCabecalho(string codigoDoCabecalho);
    }
}
