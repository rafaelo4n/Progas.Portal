using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    public class IncotermsCabCadastroBuilder : IBuilder<IncotermCab, IncotermsCabCadastroVm>
    {
        public IncotermsCabCadastroVm BuildSingle(IncotermCab incotermCab)
        {
            return new IncotermsCabCadastroVm()
            {
                CodigoIncotermCab = incotermCab.CodigoIncotermCab,
                Descricao         = incotermCab.Descricao
            };
        }

        public IList<IncotermsCabCadastroVm> BuildList(IList<IncotermCab> incotermCab)
        {
            return incotermCab.Select(incoterm => new IncotermsCabCadastroVm()
            {
                CodigoIncotermCab = incoterm.CodigoIncotermCab,
                Descricao         = incoterm.Descricao
            }).ToList();
        }
    }
}
