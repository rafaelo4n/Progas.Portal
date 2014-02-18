using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class ClienteVendas
    {
        public virtual int pro_id_cliente_vendas { get; set; }
        public virtual string Id_cliente { get; set; }
        public virtual string Org_vendas { get; set; }
        public virtual string Can_dist { get; set; }
        public virtual string Set_ativ { get; set; }
        public virtual string Grupo_cli { get; set; }
        public virtual string Id_fornecedor { get; set; }
        public virtual DateTime Data_criacao { get; set; }
        public virtual string Pacote { get; set; }
        public virtual string Hora_criacao { get; set; }
    }
}
