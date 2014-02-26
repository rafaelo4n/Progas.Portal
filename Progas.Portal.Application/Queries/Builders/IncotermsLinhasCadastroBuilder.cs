using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;


namespace Progas.Portal.Application.Queries.Builders
{
    public class IncotermsLinhasCadastroBuilder : IBuilder<IncotermLinhas, IncotermLinhasCadastroVm>
    {
        public IncotermLinhasCadastroVm BuildSingle(IncotermLinhas incotermLinhas)
        {
            return new IncotermLinhasCadastroVm()
            {
                CodigoIncotermCab = incotermLinhas.CodigoIncotermCab,
                IncotermLinha     = incotermLinhas.IncotermLinha
            };
        }

        public IList<IncotermLinhasCadastroVm> BuildList(IList<IncotermLinhas> incotermLinhas)
        {
            return incotermLinhas.Select(incoterm => new IncotermLinhasCadastroVm()
            {
                CodigoIncotermCab = incoterm.CodigoIncotermCab,
                IncotermLinha     = incoterm.IncotermLinha
            }).ToList();
        }
    }
}
