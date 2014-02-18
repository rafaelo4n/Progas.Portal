using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Domain.Entities
{
    public class TipoPedido : IAggregateRoot
    {        
        public virtual string Codigo { get; protected set; }
        public virtual string Descricao { get; protected set; }

        protected TipoPedido() { }

        public TipoPedido(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public virtual void AtualizarDescricao(string descricao)
        {
            Descricao = descricao;
        }
    }
}
