using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.DTO
{
    public class PedidoVendaItemImprimirDto
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoDeTabela { get; set; }
        public decimal ValorLiquido { get; set; }
        public decimal PercentualDeIpi { get; set; }
    }

    public class PedidoVendaImprimirDto
    {
        public string Empresa { get; set; }
        public string NumeroDaCotacao { get; set; }
        public string NumeroDoPedido{ get; set; }
        public string DataDeCriacao { get; set; }
        public string Representante { get; set; }
        public string Cliente { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CondicaoDePagamento { get; set; }
        public string TipoDeFrete { get; set; }
        public string ModeloDeFrete { get; set; }
        public string Transportadora { get; set; }
        public string TransportadoraDeRedespachoFob { get; set; }
        public string TransportadoraDeRedespachoCif { get; set; }
        public string Observacao { get; set; }
        public decimal TotalLiquido { get; set; }
        public decimal TotalTabela { get; set; }
        public IList<PedidoVendaItemImprimirDto> Itens { get; set; }

    }
}
