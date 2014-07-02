using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;


namespace Progas.Portal.Application.Queries.Builders
{
    public class IncotermsLinhasCadastroBuilder : IBuilder<IncotermLinha, IncotermLinhasCadastroVm>
    {
        public IncotermLinhasCadastroVm BuildSingle(IncotermLinha incotermLinhas)
        {
            return new IncotermLinhasCadastroVm()
            {
                Id = incotermLinhas.Id,
                IncotermLinha = incotermLinhas.Descricao
            };
        }

        public IList<IncotermLinhasCadastroVm> BuildList(IList<IncotermLinha> incotermLinhas)
        {
            return incotermLinhas.Select(BuildSingle).ToList();
        }
    }
}
