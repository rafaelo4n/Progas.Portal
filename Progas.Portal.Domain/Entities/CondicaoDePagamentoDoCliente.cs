using System;

namespace Progas.Portal.Domain.Entities
{
    public class CondicaoDePagamentoDoCliente
    {
        protected CondicaoDePagamentoDoCliente() { }

        public virtual Cliente Cliente { get; protected set; }
        public virtual CondicaoDePagamento CondicaoDePagamento { get; protected set; }
        public virtual DateTime DataDeValidade { get; protected set; }
        public virtual string Eliminacao { get; protected set; }

        protected bool Equals(CondicaoDePagamentoDoCliente other)
        {
            return Cliente.Equals(other.Cliente) && CondicaoDePagamento.Equals(other.CondicaoDePagamento);
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
                return (Cliente.GetHashCode()*397) ^ CondicaoDePagamento.GetHashCode();
            }
        }
    }
}
