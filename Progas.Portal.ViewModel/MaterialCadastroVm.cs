using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class MaterialCadastroVm : ListagemVm
    {
        [DataMember]
        public string Id_material { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public string Centro { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string UnidadeMedida { get; set; }
    }
    [CollectionDataContract]
    public class ListaMaterial : List<MaterialCadastroVm> { }
}
