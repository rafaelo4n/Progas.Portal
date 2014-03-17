using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaMotivoDeRecusa : IConsultaMotivoDeRecusa
    {

        private readonly IMotivosDeRecusa _motivosDeRecusa;

        public ConsultaMotivoDeRecusa(IMotivosDeRecusa motivosDeRecusa)
        {
            _motivosDeRecusa = motivosDeRecusa;
        }

        public IList<MotivoDeRecusaVm> ListarTodas()
        {
            return _motivosDeRecusa.GetQuery().Select(motivo => new MotivoDeRecusaVm
            {
                Codigo = motivo.Codigo,
                Descricao = motivo.Descricao
            }).ToList();
        }
    }
}