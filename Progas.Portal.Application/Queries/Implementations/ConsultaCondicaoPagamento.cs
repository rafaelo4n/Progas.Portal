using System.Linq;
using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

//using StructureMap;

namespace Progas.Portal.Application.Queries.Implementations

{
    public class ConsultaCondicaoPagamento : IConsultaCondicaoPagamento
    {
        private readonly ICondicoesDePagamento _condicoesDePagamento;
        private readonly IBuilder<CondicaoDePagamento, CondicaoDePagamentoCadastroVm> _builder;

        public ConsultaCondicaoPagamento(ICondicoesDePagamento condicoesDePagamento, IBuilder<CondicaoDePagamento, CondicaoDePagamentoCadastroVm> builder)
        {
            _condicoesDePagamento = condicoesDePagamento;
            _builder = builder;
        }

        // pesquisa da tela
        public KendoGridVm Listar(PaginacaoVm paginacaoVm, CondicaoPagamentoFiltroVm filtro)
        {
            _condicoesDePagamento
                .CodigoContendo(filtro.Codigo)
                .NomeContendo(filtro.Nome);
            var kendoGridVmn = new KendoGridVm()
            {
                QuantidadeDeRegistros = _condicoesDePagamento.Count(),
                Registros =
                    _builder.BuildList(_condicoesDePagamento.Skip(paginacaoVm.Skip)
                    .Take(paginacaoVm.Take).List())
                            .Cast<ListagemVm>()
                            .ToList()

            };
            return kendoGridVmn;
        }

        public IList<CondicaoDePagamentoCadastroVm> Listar(PaginacaoVm paginacaoVm, CondicaoDePagamentoCadastroVm filtro)
        {
            if (!string.IsNullOrEmpty(filtro.Codigo))
            {
                _condicoesDePagamento.BuscaPeloCodigo(filtro.Codigo);

            }

            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                _condicoesDePagamento.FiltraPelaDescricao(filtro.Descricao);
            }
            int skip = (paginacaoVm.Page - 1) * paginacaoVm.PageSize;

            return _builder.BuildList(_condicoesDePagamento.Skip(skip).Take(paginacaoVm.Take).List());
            
        }

       
    }
}
