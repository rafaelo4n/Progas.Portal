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
                Id = incotermLinhas.pro_id_incotermLinha,
                IncotermLinha = incotermLinhas.IncotermLinha
            };
        }

        public IList<IncotermLinhasCadastroVm> BuildList(IList<IncotermLinhas> incotermLinhas)
        {
            return incotermLinhas.Select(BuildSingle).ToList();
        }
    }
}
