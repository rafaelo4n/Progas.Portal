using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Domain.Entities
{
    public class CondicaoDePrecoCliente : IAggregateRoot
    {
        protected CondicaoDePrecoCliente() { }

        public CondicaoDePrecoCliente(string org_vendas, string can_dist, string id_cliente, string id_material, string numeroRegistroCondicao, decimal montante, string unidadeCondicao)
        {
            Org_vendas             = org_vendas;
            Can_dist               = can_dist;
            Id_cliente             = id_cliente;    
            Id_material            = id_material;
            NumeroRegistroCondicao = numeroRegistroCondicao;
            Montante               = montante;
            UnidadeCondicao        = unidadeCondicao;            
        }

        public virtual int Id { get; set; }
        public virtual string   Org_vendas { get; set; }
        public virtual string   Can_dist { get; set; }
        public virtual string   Id_cliente { get; set; }
        public virtual string   Id_material { get; set; }
        public virtual string   NumeroRegistroCondicao { get; set; }
        public virtual decimal  Montante { get; set; }
        public virtual string   UnidadeCondicao { get; set; }
        public virtual string   Pacote { get; set; }
        public virtual DateTime Data_criacao { get; set; }
        public virtual string   Hora_criacao { get; set; }
    }
}
