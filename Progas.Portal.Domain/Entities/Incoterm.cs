using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Domain.Entities
{
    public class Incoterm : IAggregateRoot
    {
        public virtual string CodigoIncoterm { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual string Tipo { get; protected set; }

        protected Incoterm() { }

        public Incoterm(string codigoIncoterm, string descricao, string tipo)
        {
            CodigoIncoterm = codigoIncoterm;
            Descricao      = descricao;
            Tipo           = tipo;
        }

        public virtual void AtualizarDescricao(string descricao)
        {
            Descricao = descricao;
        }
    }
}
