using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaTipoPedido : IConsultaTipoPedido
    {
        private readonly ITipoPedidos _tipoPedidos;
        private readonly IBuilder<TipoPedido, TipoPedidoCadastroVm> _builder;

        // Recebe dados Interface do repositorio do Tipo pedido e monta a lista com a Entidade + ViewModel
        public ConsultaTipoPedido(ITipoPedidos tipoPedido, IBuilder<TipoPedido, TipoPedidoCadastroVm> builder)
        {
            _tipoPedidos = tipoPedido;
            _builder    = builder;    
        }

        public IList<TipoPedidoCadastroVm> Listar(PaginacaoVm paginacaoVm, TipoPedidoCadastroVm filtro)
        {
            if (!string.IsNullOrEmpty(filtro.Codigo))
            {
                _tipoPedidos.BuscaPeloCodigo(filtro.Codigo);

            }

            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                _tipoPedidos.FiltraPelaDescricao(filtro.Descricao);
            }
            int skip = (paginacaoVm.Page - 1) * paginacaoVm.PageSize;

            //paginacaoVm.TotalRecords = _condicoesDePagamento.Count();

            return _builder.BuildList(_tipoPedidos.Skip(skip).Take(paginacaoVm.Take).List());

        }

        public IList<TipoPedidoCadastroVm> ListarTodas()
        {
            return _builder.BuildList(_tipoPedidos.List());
        }
    }
}
