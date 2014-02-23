using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class UsuarioCadastroVm : ListagemVm
    {
        [DataMember]
        [Required(ErrorMessage = "Login é obrigatório")]
        [Display(Name = "Login: ")]
        public string Login { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }

        [DataMember]
        [Display(Name = "E-mail: ")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Required(ErrorMessage = "E-mail é obrigatório")]
        public string Email { get; set; }

        [DataMember]
        [Display(Name = "Representante: ")]
        public string CodigoFornecedor { get; set; }

    }

    [CollectionDataContract]
    public class ListaUsuario:List<UsuarioCadastroVm>{}

    public class UsuarioConsultaVm: UsuarioCadastroVm
    {
        [Display(Name = "Status: ")]
        public string Status { get; set; }

        [Display(Name = "Representante: ")]
        public string NomeDoRepresentante { get; set; }

        public string UrlParaSalvar { get; set; }

    }

}
