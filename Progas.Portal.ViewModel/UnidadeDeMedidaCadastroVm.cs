using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class UnidadeDeMedidaCadastroVm : ListagemVm
    {
        [DataMember]
        public string Id_unidademedida { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public string Dimensao { get; set; }
        [DataMember]
        public string Aprestecnica { get; set; }
    }

    [CollectionDataContract]
    public class ListaUnidadesDeMedida:List<UnidadeDeMedidaCadastroVm>{}
}
