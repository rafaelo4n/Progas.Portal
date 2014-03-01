using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class IncotermLinhasCadastroVm
    {
        [DataMember]
        public string CodigoIncotermCab { get; set; }
        [DataMember]
        public string IncotermLinha     { get; set; }        
    }
    [CollectionDataContract]
    public class ListaIncotermLinhas : List<IncotermLinhasCadastroVm> { }
}
