using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    public class PedidoVendaLinhaCadastroVm : ListagemVm
    {
        //
        // linhas
        [Display(Name = "Cotacao:")]
        public string id_cotacao{ get; set; }

        // linhas
        [Display(Name = "item:")]
        public string id_item { get; set; }

        // linhas
        [Display(Name = "pedido:")]
        public string id_pedido{ get; set; }

        [Display(Name = "Material:")]
        public string id_material { get; set; }

        [Display(Name = "Quant.:")]
        [Required(ErrorMessage = "Quantidade do Material é obrigatória")]
        public decimal Quant { get; set; }

        [Display(Name = "UM:")]
        public string CodigoUnidadeMedida { get; set; }

        [Display(Name = "Lista:")]
        public string listpre { get; set; }

        // Recebara o resultado da rfc
        public decimal valtab { get; set; }

        // Recebara o resultado da rfc
        public decimal valpol { get; set; }

        // Desconto Manual
        [Display(Name = "Desconto:")]
        public decimal descma { get; set; }

        // Recebara o resultado da rfc
        public decimal valfin { get; set; }
        
    }

    [CollectionDataContract]
    public class ListaLinhasPedido : List<PedidoVendaLinhaCadastroVm> { }
    
}
