using System.ComponentModel.DataAnnotations;

namespace Progas.Portal.ViewModel
{
    public class ClienteDoPedidoDeVendaVm
    {

        [Display(Name = "Cliente: ")]
        [Required(ErrorMessage = "Cliente é obrigatório")]
        public string Codigo { get; set; }
        [Display(Name = "Nome:")]
        public string Nome { get; set; }
        [Display(Name = "CNPJ/CPF:")]
        public string Cnpj { get; set; }
        [Display(Name = "Telefone:")]
        public string Telefone { get; set; }
    }

}
