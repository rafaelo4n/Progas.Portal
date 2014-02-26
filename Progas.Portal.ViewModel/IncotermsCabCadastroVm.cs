using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class IncotermsCabCadastroVm
    {
        [DataMember]
        public string CodigoIncotermCab { get; set; }
        [DataMember]
        public string Descricao { get; set; }
    }
    [CollectionDataContract]
    public class ListaIncotermCab : List<IncotermsCabCadastroVm> { }
}
