using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class CondicaoDePrecoGeralCadastroVm : ListagemVm
    {
        [DataMember]
        public string Org_vendas { get; set; }
        [DataMember]
        public string Can_dist { get; set; }
        [DataMember]
        public string Id_material { get; set; }
        [DataMember]
        public string NumeroRegistroCondicao { get; set; }
        [DataMember]
        public decimal Montante { get; set; }
    }

    [CollectionDataContract]
    public class ListaCondicaoDePrecoGeral: List<CondicaoDePrecoGeralCadastroVm> { }
}
