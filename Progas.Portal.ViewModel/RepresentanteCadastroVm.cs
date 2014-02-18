using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class RepresentanteCadastroVm : ListagemVm
    {
        [DataMember]
        [Display(Name = "Código: ")]
        public string Codigo { get; set; }
        [DataMember]
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }
        [DataMember]
        [Display(Name = "E-mail: ")]
        public string Email { get; set; }
        [DataMember]
        [Display(Name = "CNPJ: ")]
        public string Cnpj { get; set; }
        [DataMember]
        [Display(Name = "Municipio: ")]
        public string Municipio { get; set; }
        [DataMember]
        [Display(Name = "UF: ")]
        public string Uf { get; set; }

        [DataMember]
        [Display(Name = "Transportadora: ")]
        public string Transportadora { get; set; }
    }

    [CollectionDataContract]
    public class ListaRepresentante : List<RepresentanteCadastroVm> { }
}
