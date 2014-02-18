using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    //public class TipoPedidoCadastroBuilder : Builder<TipoPedido, TipoPedidoCadastroVm>
    public class TipoPedidoCadastroBuilder : IBuilder<TipoPedido, TipoPedidoCadastroVm>
    {
        public TipoPedidoCadastroVm BuildSingle(TipoPedido tipoPedido)
        {
            return new TipoPedidoCadastroVm()
            {
                Codigo = tipoPedido.Codigo,
                Descricao = tipoPedido.Descricao
            };
        }

        public IList<TipoPedidoCadastroVm> BuildList(IList<TipoPedido> tipoPedidos)
        {
            return tipoPedidos.Select(tipoPedido => new TipoPedidoCadastroVm()
            {
                Codigo = tipoPedido.Codigo,
                Descricao = tipoPedido.Descricao

            }).ToList();
        }
    }


}
