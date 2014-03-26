using System;
using System.Collections.Generic;

namespace Progas.Portal.ViewModel
{
    // View que recebera os valores da pagina que serão utilizados no CRUD
    public class PedidoVendaSalvarVm
    {
        // cabecalho
        public string Tipo { get; set; } //
        public string NumeroPedido { get; set; } //
        public string CodigoDaCondicaoDePagamento { get; set; } //
        public string Centro { get; set; } //
        public DateTime DataDoPedido { get; set; }
        public string CodigoTipoPedido { get; set; } //
        public int? CodigoDaTransportadora { get; set; } //
        public int? CodigoDaTransportadoraDeRedespacho { get; set; } //
        public int? CodigoDaTransportadoraDeRedespachoCif { get; set; } //
        public int IdDoCliente { get; set; } //
        public int IdDaAreaDeVenda { get; set; }
        public int IdDoIncoterm1 { get; set; } //
        public int IdDoIncoterm2 { get; set; } //
        public string Observacao { get; set; } //             

        public IList<PedidoVendaSalvarItemVm> Itens { get; set; }

    }
}
