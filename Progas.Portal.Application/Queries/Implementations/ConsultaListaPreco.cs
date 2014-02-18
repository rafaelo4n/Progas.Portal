using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaListaPreco : IConsultaListaPreco
    {
        private readonly IListasPreco _listaPreco;
        private readonly IBuilder<ListaPreco, ListaPrecoCadastroVm> _builder;

        // Recebe dados Interface do repositorio do Tipo pedido e monta a lista com a Entidade + ViewModel
        public ConsultaListaPreco(IListasPreco listaPreco, IBuilder<ListaPreco, ListaPrecoCadastroVm> builder)
        {
            _listaPreco = listaPreco;
            _builder = builder;
        }

        public IList<ListaPrecoCadastroVm> Listar(PaginacaoVm paginacaoVm, ListaPrecoCadastroVm filtro)
        {
            if (!string.IsNullOrEmpty(filtro.Codigo))
            {
                _listaPreco.BuscaPeloCodigo(filtro.Codigo);

            }

            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                _listaPreco.FiltraPelaDescricao(filtro.Descricao);
            }
            int skip = (paginacaoVm.Page - 1) * paginacaoVm.PageSize;

            //paginacaoVm.TotalRecords = _condicoesDePagamento.Count();

            return _builder.BuildList(_listaPreco.Skip(skip).Take(paginacaoVm.Take).List());

        }

        public IList<ListaPrecoCadastroVm> ListarTodas()
        {
            return _builder.BuildList(_listaPreco.List());
        }
    }
}
