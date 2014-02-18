using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class TipoPedidoCadastroVm
    {
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descricao { get; set; }
    }
    [CollectionDataContract]
    public class ListaTipoPedido : List<TipoPedidoCadastroVm>{}
}
