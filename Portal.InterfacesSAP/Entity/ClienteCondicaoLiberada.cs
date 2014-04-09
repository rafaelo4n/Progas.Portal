using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class ClienteCondicaoLiberada
    {
        public virtual string   Chave_condicao      { get; set; }        
        public virtual string   Id_cliente          { get; set; }
        public virtual DateTime Data_fim_condicao   { get; set; }
        public virtual string   Pacote              { get; set; }
        public virtual DateTime Data_criacao        { get; set; }
        public virtual string   Hora_criacao        { get; set; }
        public virtual string   Eliminacao          { get; set; }
        public virtual IList<Cliente> ListaClientes { get; set; }

        public override bool Equals(Object o)
        {
            if (o == null)
            {
                return false;
            }
            if (this.GetType() == o.GetType())
            {
                return o.GetHashCode() == this.GetHashCode();
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
