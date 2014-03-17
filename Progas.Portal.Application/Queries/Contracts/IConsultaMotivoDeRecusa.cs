using System.Collections.Generic;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Contracts
{
    public interface IConsultaMotivoDeRecusa
    {
        IList<MotivoDeRecusaVm> ListarTodas();
    }
}