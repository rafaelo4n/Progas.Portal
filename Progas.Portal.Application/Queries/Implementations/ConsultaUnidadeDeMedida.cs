using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;
using System.Linq;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaUnidadeDeMedida : IConsultaUnidadeDeMedida
    {
        private readonly IUnidadesDeMedida _ivas;
        private readonly IBuilder<UnidadeDeMedida, UnidadeDeMedidaCadastroVm> _builder;

        public ConsultaUnidadeDeMedida(IUnidadesDeMedida unidadesDeMedida, IBuilder<UnidadeDeMedida, UnidadeDeMedidaCadastroVm> builder)
        {
            _ivas = unidadesDeMedida;
            _builder = builder;
        }


        // pesquisa da tela
        public KendoGridVm Listar(PaginacaoVm paginacaoVm, UnidadeDeMedidaFiltroVm filtro)
        {
            _ivas
                .CodigoContendo(filtro.Codigo)
                .NomeContendo(filtro.Descricao);
            var kendoGridVmn = new KendoGridVm()
            {
                QuantidadeDeRegistros = _ivas.Count(),
                Registros =
                    _builder.BuildList(_ivas.Skip(paginacaoVm.Skip)
                    .Take(paginacaoVm.Take).List())
                            .Cast<ListagemVm>()
                            .ToList()

            };
            return kendoGridVmn;
        }
        //



        public IList<UnidadeDeMedidaCadastroVm> Listar(PaginacaoVm paginacaoVm, UnidadeDeMedidaCadastroVm filtro)
        {
            if (!string.IsNullOrEmpty(filtro.Id_unidademedida))
            {
                _ivas.BuscaPeloCodigo(filtro.Id_unidademedida);

            }

            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                _ivas.FiltraPelaDescricao(filtro.Descricao);
            }
            int skip = (paginacaoVm.Page - 1) * paginacaoVm.PageSize;

            //paginacaoVm.TotalRecords = _condicoesDePagamento.Count();

            return _builder.BuildList(_ivas.Skip(skip).Take(paginacaoVm.Take).List());

        }



        public IList<UnidadeDeMedidaCadastroVm> ListarTodos()
        {
            return _builder.BuildList(_ivas.GetQuery().OrderBy(x => x.Descricao).ToList());
        }
    }
}
