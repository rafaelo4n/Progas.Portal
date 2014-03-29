using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class Fornecedor
    {       
        public virtual int pro_id_fornecedor { get; set; }
        public virtual string Codigo { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Cnpj { get; set; }
        public virtual string Nr_ie_for { get; set; }
        public virtual string Cep { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Municipio { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Uf { get; set; }
        public virtual string Pais { get; set; }
        public virtual string Tel_res { get; set; }
        public virtual string Tel_cel { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime Data_criacao { get; set; }
        public virtual string Pacote { get; set; }
        public virtual string Hora_criacao { get; set; }
        public virtual string Grupo_contas { get; set; }
        public virtual string Codigo_eliminacao { get; set; }        
    }
}
