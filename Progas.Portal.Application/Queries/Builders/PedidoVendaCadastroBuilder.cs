using System;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    public class PedidoVendaCadastroBuilder : Builder<PedidoVenda, PedidoVendaCadastroVm>
    {
        public override PedidoVendaCadastroVm BuildSingle(PedidoVenda pedidoVenda)
        {

            return new PedidoVendaCadastroVm()
            {                            
                Tipo             = pedidoVenda.Tipo,
                id_cotacao       = pedidoVenda.Id_cotacao,
                CodigoTipoPedido = pedidoVenda.TipoPedido,
                id_centro        = pedidoVenda.Id_centro,
                //id_cliente       = pedidoVenda.Id_cliente,
                datacp           = Convert.ToString(pedidoVenda.Datacp),
                datap            = pedidoVenda.Datap.ToString("dd/MM/yyyy"),
                id_pedido        = pedidoVenda.NumeroDoPedidoDoRepresentante,
                condpgto         = pedidoVenda.Condpgto,
                IdDoIncoterm1            = pedidoVenda.ModeloDeFrete.pro_id_incotermCab.ToString(),
                IdDoIncoterm2            = pedidoVenda.TipoDeFrete.Id.ToString(),
                //trans            = pedidoVenda.Trans,
                //transred         = pedidoVenda.Transred,
                //transredcif      = pedidoVenda.Transredcif, 
                id_repre         = pedidoVenda.Id_repre,
                obs              = pedidoVenda.Observacao//,
                //valfin           = pedidoVenda.Vlrtot,  
                //vlrtot           = pedidoVenda.Vlrtot
                //tipo             = pedidoVendTransportadorao,
            };
        }
    }
}
