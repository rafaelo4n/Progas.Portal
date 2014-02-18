using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class IncotermCadastroVm
    {
        [DataMember]
        public string CodigoIncoterm { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public string Tipo { get; set; }
    }
    [CollectionDataContract]
    public class ListaIncoterm: List<IncotermCadastroVm> { }
}
