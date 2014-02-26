using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaIncotermCab : IConsultaIncotermCab 
    {
        private readonly IIncotermsCabs _incotermsCabs;
        private readonly IBuilder<IncotermCab, IncotermsCabCadastroVm> _builder;

        // Recebe dados Interface do repositorio do Tipo pedido e monta a lista com a Entidade + ViewModel
        public ConsultaIncotermCab(IIncotermsCabs incotermCab, IBuilder<IncotermCab, IncotermsCabCadastroVm> builder)
        {
            _incotermsCabs = incotermCab;
            _builder       = builder;

        }
        public IList<IncotermsCabCadastroVm> ListarTodas()
        {
            return _builder.BuildList(_incotermsCabs.List());
        }
    }
}
