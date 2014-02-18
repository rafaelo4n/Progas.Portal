using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Progas.Portal.ViewModel
{
    [DataContract]
    public class ClienteCadastroVm : ListagemVm
    {
        [DataMember]
        public string id_cliente { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string Cnpj { get; set; }
        [DataMember]
        public string nr_ie_for { get; set; }
        [DataMember]
        public string Cpf { get; set; }
        [DataMember]
        public string endereco { get; set; }
        [DataMember]
        public string numero { get; set; }
        [DataMember]
        public string complemento { get; set; }
        [DataMember]
        public string municipio { get; set; }
        [DataMember]
        public string uf { get; set; }
        [DataMember]
        public string pais { get; set; }
        [DataMember]
        public string tel_res { get; set; }
        [DataMember]
        public string tel_cel { get; set; }
        [DataMember]
        public string email { get; set; }        
    }

    [CollectionDataContract]
    public class ListaCliente : List<ClienteCadastroVm> { }
}