using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaIncotermLinhas : IConsultaIncotermLinhas
    {
       private readonly IIcontermslinhas _incotermsLinhas;
        private readonly IBuilder<IncotermLinhas, IncotermLinhasCadastroVm> _builder;

        // Recebe dados Interface do repositorio do Tipo pedido e monta a lista com a Entidade + ViewModel
        public ConsultaIncotermLinhas(IIcontermslinhas incotermsLinhas, IBuilder<IncotermLinhas, IncotermLinhasCadastroVm> builder)
        {
            _incotermsLinhas = incotermsLinhas;
            _builder         = builder;

        }
        public IList<IncotermLinhasCadastroVm> ListarTodas()
        {
            return _builder.BuildList(_incotermsLinhas.List());
        }

        public IList<IncotermLinhasCadastroVm> ListarPorCabecalho(string codigoDoCabecalho)
        {
            return _incotermsLinhas
                .DoCabecalho(codigoDoCabecalho)
                .GetQuery()
                .Select(x => new IncotermLinhasCadastroVm
                {
                    Id = x.pro_id_incotermLinha,
                    IncotermLinha = x.IncotermLinha
                }).ToList();
        }
    }
}
