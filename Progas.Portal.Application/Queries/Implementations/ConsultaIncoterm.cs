using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaIncoterm : IConsultaIncoterm
    {
        private readonly IIncoterms _incoterm;
        private readonly IBuilder<Incoterm , IncotermCadastroVm> _builder;

        // Recebe dados Interface do repositorio do Tipo pedido e monta a lista com a Entidade + ViewModel
        public ConsultaIncoterm(IIncoterms incoterm, IBuilder<Incoterm, IncotermCadastroVm> builder)
        {
            _incoterm    = incoterm;
            _builder     = builder;
        }

        public IList<IncotermCadastroVm> Listar(PaginacaoVm paginacaoVm, IncotermCadastroVm filtro)
        {
            if (!string.IsNullOrEmpty(filtro.CodigoIncoterm))
            {
                _incoterm.BuscaPeloCodigo(filtro.CodigoIncoterm);

            }

            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                _incoterm.FiltraPelaDescricao(filtro.Descricao);
            }
            int skip = (paginacaoVm.Page - 1) * paginacaoVm.PageSize;

            //paginacaoVm.TotalRecords = _condicoesDePagamento.Count();

            return _builder.BuildList(_incoterm.Skip(skip).Take(paginacaoVm.Take).List());

        }

        public IList<IncotermCadastroVm> ListarTodas()
        {
            return _builder.BuildList(_incoterm.List());
        }
    }
}
