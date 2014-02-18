using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class ListaPrecoCadastroVm
    {
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descricao { get; set; }
    }
    [CollectionDataContract]
    public class ListaListaPreco : List<ListaPrecoCadastroVm> { }
}
