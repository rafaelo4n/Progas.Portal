using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class CondicaoDePrecoCliente
    {
        public virtual int Id                        { get; set; }
        public virtual string Org_vendas             { get; set; }
        public virtual string Can_dist               { get; set; }
        public virtual string Id_cliente             { get; set; }
        public virtual string Id_material            { get; set; }
        public virtual string NumeroRegistroCondicao { get; set; }
        public virtual decimal Montante              { get; set; }
        public virtual string UnidadeCondicao        { get; set; }
        public virtual string Pacote { get; set; }
        public virtual DateTime Data_criacao         { get; set; }        
        public virtual string Hora_criacao           { get; set; }
    }
}
