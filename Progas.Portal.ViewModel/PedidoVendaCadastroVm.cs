using System.ComponentModel.DataAnnotations;

namespace Progas.Portal.ViewModel
{
    public class PedidoVendaCadastroVm : ListagemVm
    {   
        /// <summary>
        /// /indica se é uma cópia de outro pedido de venda que já foi salvo
        /// </summary>
        public bool Copia { get; set; }

        public bool SomenteLeitura { get; set; }

        // Campo de pesquisa
        [Display(Name = "N°  Cotação: ")]
        public string id_cotacao { get; set; }

        // Campo de pesquisa
        [Display(Name = "Status: ")]
        public string status { get; set; }

        //[Required(ErrorMessage = "Tipo do pedido é obrigatório")]
        [Display(Name = "Tipo do Pedido: ")]
        [Required(ErrorMessage = "Tipo do Pedido é obrigatório")]
        public string CodigoTipoPedido { get; set; }

        [Display(Name = "Centro: ")]
        public string id_centro { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Criação: ")]
        public string datacp { get; set; }

        [Display(Name = "Pedido Representante: ")]
        [Required(ErrorMessage = "Nº Pedido do Representante é obrigatório")]
        public string id_pedido { get; set; }

        [Display(Name = "OC Cliente:")]
        public string NumeroDoPedidoDoCliente { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Data do Pedido: ")]
        [Required(ErrorMessage = "Data do Pedido é obrigatório")]
        public string datap { get; set; }

        [Required(ErrorMessage = "Condição de Pagamento é obrigatória")]
        [Display(Name = "Condição de Pagamento")]
        public string condpgto { get; set; }

        [Display(Name = "Incoterm 1: ")]
        [Required(ErrorMessage = "Incoterm 1 é obrigatória")]
        public string IdDoIncoterm1 { get; set; }

        [Display(Name = "Incoterm 2: ")]
        [Required(ErrorMessage = "Incoterm 2 é obrigatória")]
        public string IdDoIncoterm2 { get; set; }

        [Display(Name = "Representante: ")]
        public string id_repre { get; set; }

        [Display(Name = "Observação: ")]
        public string obs { get; set; }

        // Esta disponivel no Cabeçalho do pedido
        // Caso seja preenchido será replicado para todos os itens do pedido
        [Display(Name = "Motivo de Recusa: ")]
        public string motrec { get; set; }

        [Display(Name = "Total Final: ")]
        public decimal vlrtot { get; set; }

        [Display(Name = "Tipo de Envio: G ou S")]
        public string Tipo { get; set; }

        [Display(Name = "Quantidade:")]
        [Required(ErrorMessage = "Quantidade é obrigatório")]
        public decimal Quant { get; set; }

        [Display(Name = "Unidade de Medida:")]
        public string CodigoUnidadeMedida { get; set; }

        [Required(ErrorMessage = "Lista é obrigatório")]
        [Display(Name = "Lista:")]
        public string CodigoDaListaDePreco { get; set; }

        // Desconto Manual
        [Display(Name = "Desconto:")]
        public string Desconto { get; set; }

        [Display(Name = "Área de Venda:")]
        public int IdDaAreaDeVenda { get; set; }

        public ClienteDoPedidoDeVendaVm Cliente { get; set; }
        public TransportadoraDoPedidoDeVenda Transportadora { get; set; }
        public TransportadoraDoPedidoDeVenda TransportadoraDeRedespacho { get; set; }
        public TransportadoraDoPedidoDeVenda TransportadoraDeRedespachoCif { get; set; }

        public int IdDoMaterial { get; set; }

        [Display(Name = "Código do Material:")]
        [Required(ErrorMessage = "Material é obrigatório")]
        public string CodigoDoMaterial { get; set; }


        [Display(Name = "Nome do Material:")]
        public string NomeDoMaterial { get; set; }

    }

}
