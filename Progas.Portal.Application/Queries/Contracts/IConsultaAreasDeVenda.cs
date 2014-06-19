using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaAreasDeVenda
    {
        IList<AreaDeVendaVm> ListarPorCliente(string idDoCliente, int? idDaAreaDeVenda);
    }
}