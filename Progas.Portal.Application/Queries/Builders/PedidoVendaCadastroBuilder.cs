using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    public class PedidoVendaCadastroBuilder : Builder<PedidoVenda, PedidoVendaCadastroVm>
    {
        public override PedidoVendaCadastroVm BuildSingle(PedidoVenda pedidoVenda)
        {
            //string datap = pedidoVenda.Datap.ToString("dd/MM/yyyy");
            //string datacp;
            string datap;   

            return new PedidoVendaCadastroVm()
            {                            
                Tipo             = pedidoVenda.Tipo,
                id_cotacao       = pedidoVenda.Id_cotacao,
                CodigoTipoPedido = pedidoVenda.TipoPedido,
                id_centro        = pedidoVenda.Id_centro,
                id_cliente       = pedidoVenda.Id_cliente,
                datacp           = Convert.ToString(pedidoVenda.Datacp),
                datap            = datap = pedidoVenda.Datap.ToString("dd/MM/yyyy"),
                id_pedido        = pedidoVenda.Id_pedido,
                condpgto         = pedidoVenda.Condpgto,
                inco1            = pedidoVenda.Inco1,
                inco2            = pedidoVenda.Inco2,
                trans            = pedidoVenda.Trans,
                transred         = pedidoVenda.Transred,
                transredcif      = pedidoVenda.Transredcif, 
                id_repre         = pedidoVenda.Id_repre,
                obs              = pedidoVenda.Obs//,
                //valfin           = pedidoVenda.Vlrtot,  
                //vlrtot           = pedidoVenda.Vlrtot
                //tipo             = pedidoVenda.Tipo,
            };
        }
    }
}
