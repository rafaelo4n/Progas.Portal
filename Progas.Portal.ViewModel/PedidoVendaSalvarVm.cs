using System;
using System.Collections.Generic;

namespace Progas.Portal.ViewModel
{
    public class PedidoVendaSalvarVm
    {
        public string IdDaCotacao { get; set; }
        public string Tipo { get; set; } 
        public string NumeroPedidoDoRepresentante { get; set; }
        public string NumeroPedidoDoCliente { get; set; } 
        public string CodigoDaCondicaoDePagamento { get; set; } 
        public DateTime DataDoPedido { get; set; }
        public string CodigoTipoPedido { get; set; } 
        public string CodigoDaTransportadora { get; set; } 
        public string CodigoDaTransportadoraDeRedespacho { get; set; } 
        public string CodigoDaTransportadoraDeRedespachoCif { get; set; } 
        public string CodigoDoCliente { get; set; } 
        public int IdDaAreaDeVenda { get; set; }
        public int IdDoIncoterm1 { get; set; } 
        public int IdDoIncoterm2 { get; set; } 
        public string Observacao { get; set; }              

        public IList<PedidoVendaSalvarItemVm> Itens { get; set; }

    }
}
