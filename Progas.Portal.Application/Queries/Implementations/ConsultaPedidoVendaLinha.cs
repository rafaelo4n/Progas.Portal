using System;
using System.Linq;
using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;
using NHibernate.Criterion;
//using StructureMap;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaPedidoVendaLinha
    {
        private readonly IPedidosVendaLinha _pedidosVendaLinha;
        private readonly IBuilder <PedidoVendaLinha, PedidoVendaLinhaCadastroVm> _builderPedidoVendaLinha;

        public ConsultaPedidoVendaLinha(IPedidosVendaLinha pedidosVendaLinha,
                                        IBuilder<PedidoVendaLinha, PedidoVendaLinhaCadastroVm> builderPedidoVendaLinha)
        {
            _pedidosVendaLinha       = pedidosVendaLinha;
            _builderPedidoVendaLinha = builderPedidoVendaLinha;
        }
    }
}
