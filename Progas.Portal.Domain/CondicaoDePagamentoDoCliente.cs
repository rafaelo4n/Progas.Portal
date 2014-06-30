using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Domain
{
    public class CondicaoDePagamentoDoCliente
    {
        protected CondicaoDePagamentoDoCliente(){}

        public virtual Cliente Cliente { get; set; }
        public virtual CondicaoDePagamento CondicaoDePagamento { get; set; }
        public virtual DateTime DataDeValidade { get; set; }
        public virtual string Eliminacao { get; set; }

        protected bool Equals(CondicaoDePagamentoDoCliente other)
        {
            return Equals(Cliente, other.Cliente) && Equals(CondicaoDePagamento, other.CondicaoDePagamento);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CondicaoDePagamentoDoCliente) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Cliente != null ? Cliente.GetHashCode() : 0)*397) ^ (CondicaoDePagamento != null ? CondicaoDePagamento.GetHashCode() : 0);
            }
        }
    }
}
