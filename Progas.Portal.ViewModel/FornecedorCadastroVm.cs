using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class FornecedorCadastroVm : ListagemVm
    {
        public int Id { get; set; }
        [DataMember]
        [Display(Name = "Código: ")]
        public string Codigo { get; set; }
        [DataMember]
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }
        [DataMember]
        [Display(Name = "CPF: ")]
        public string Cpf { get; set; }
        [DataMember]
        [Display(Name = "CNPJ: ")]
        public string Cnpj { get; set; }
        [DataMember]
        [Display(Name = "Insc. Est.: ")]
        public string nr_ie_for { get; set; }
        [DataMember]
        [Display(Name = "Cep ")]
        public string cep { get; set; }                
        [DataMember]
        [Display(Name = "Endereco: ")]
        public string endereco { get; set; }
        [DataMember]
        [Display(Name = "Numero: ")]
        public string numero { get; set; }
        [DataMember]
        [Display(Name = "Complemento: ")]
        public string complemento { get; set; }
        [DataMember]
        [Display(Name = "Cidade: ")]
        public string municipio { get; set; }
        [DataMember]
        [Display(Name = "bairro: ")]
        public string bairro { get; set; }
        [DataMember]
        [Display(Name = "UF: ")]
        public string uf { get; set; }
        [DataMember]
        [Display(Name = "Pais: ")]
        public string pais { get; set; }
        [DataMember]
        [Display(Name = "Telefone Res: ")]
        public string tel_res { get; set; }
        [DataMember]
        [Display(Name = "Telefone Cel: ")]
        public string tel_cel { get; set; }
        [DataMember]
        [Display(Name = "Email:")]
        public string email { get; set; }

    }

    [CollectionDataContract]
    public class ListaFornecedores:List<FornecedorCadastroVm>{}
}
