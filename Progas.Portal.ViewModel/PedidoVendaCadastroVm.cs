using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class PedidoVendaCadastroVm : ListagemVm
    {   
        // Cabecalho

        // Campo de pesquisa
        [Display(Name = "N°  Cotação: ")]
        public string id_cotacao { get; set; }

        // Campo de pesquisa
        [Display(Name = "Status: ")]
        public string status { get; set; }

        //[Required(ErrorMessage = "Tipo do pedido é obrigatório")]
        [Display(Name = "Tipo do Pedido: ")]
        public string CodigoTipoPedido { get; set; }

        [Display(Name = "Centro: ")]
        public string id_centro { get; set; }

        [Display(Name = "Cliente: ")]
        public string id_cliente { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Criação: ")]
        public string datacp { get; set; }

        //[Required(ErrorMessage = "O numero do Pedido é obrigatório")]
        [Display(Name = "N° Pedido: ")]
        public string id_pedido { get; set; }

        [DataType(DataType.Date)]
        //[Required(ErrorMessage = "Data do Pedido é Obrigatória")]
        [Display(Name = "Data do Pedido: ")]
        public string datap { get; set; }

        //[Required(ErrorMessage = "Condição de Pagamento é Obrigatória")]
        [Display(Name = "Cond. Pag.")]
        public string condpgto { get; set; }

        [Display(Name = "incoterm1: ")]
        public string inco1 { get; set; }

        [Display(Name = "incoterm2: ")]
        public string inco2 { get; set; }

        [Display(Name = "Trans.: ")]
        public string trans { get; set; }

        [Display(Name = "Trans. Redes. :")]
        public string transred { get; set; }

        [Display(Name = "Trans. CIF: ")]
        public string transredcif { get; set; }

        [Display(Name = "Representante: ")]
        public string id_repre { get; set; }

        [Display(Name = "Observação: ")]
        public string obs { get; set; }

        // Esta disponivel no Cabeçalho do pedido
        // Caso seja preenchido será replicado para todos os itens do pedido
        [Display(Name = "Mot. Recusa: ")]
        public string motrec { get; set; }

        [Display(Name = "Total Final: ")]
        public string vlrtot { get; set; }

        [Display(Name = "Tipo de Envio: G ou S")]
        public string Tipo { get; set; }

        //
        // linhas
        [Display(Name = "Material:")]
        public string id_material { get; set; }

        [Display(Name = "Quant.:")]
        public decimal Quant { get; set; }

        [Display(Name = "UM:")]
        public string CodigoUnidadeMedida { get; set; }

        [Display(Name = "Lista:")]
        public string listpre { get; set; }

        // Recebara o resultado da rfc
        public string valtab { get; set; }

        // Recebara o resultado da rfc
        public string valpol { get; set; }

        // Desconto Manual
        [Display(Name = "Desconto:")]
        public string descma { get; set; }

        // Recebara o resultado da rfc
        public string valfin { get; set; }

        // Recebara o resultado da rfc
        public string ordem { get; set; }
        
    }

    [CollectionDataContract]
    public class ListaPedido : List<PedidoVendaCadastroVm> { }
}
