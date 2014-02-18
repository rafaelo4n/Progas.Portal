using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class CondicaoDePagamentoCadastroVm : ListagemVm
    {
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descricao { get; set; }
    }

    [CollectionDataContract]
    public class ListaCondicaoPagamento: List<CondicaoDePagamentoCadastroVm>{}
}
