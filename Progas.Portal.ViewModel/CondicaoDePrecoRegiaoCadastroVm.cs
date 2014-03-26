using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class CondicaoDePrecoRegiaoCadastroVm : ListagemVm
    {
        [DataMember]
        public string Regiao { get; set; }
        [DataMember]
        public string Id_material { get; set; }
        [DataMember]
        public string NumeroRegistroCondicao { get; set; }
        [DataMember]
        public decimal Montante { get; set; }
    }

    [CollectionDataContract]
    public class ListaCondicaoDePrecoRegiao : List<CondicaoDePrecoRegiaoCadastroVm> { }
}
