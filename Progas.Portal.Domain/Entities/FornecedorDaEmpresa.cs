namespace Progas.Portal.Domain.Entities
{
    public class FornecedorDaEmpresa
    {
        protected FornecedorDaEmpresa() { }

        public virtual string Empresa { get; protected set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual string Eliminacao { get; set; }

        protected bool Equals(FornecedorDaEmpresa other)
        {
            return string.Equals(Empresa, other.Empresa) && Equals(Fornecedor, other.Fornecedor);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FornecedorDaEmpresa) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Empresa != null ? Empresa.GetHashCode() : 0)*397) ^ (Fornecedor != null ? Fornecedor.GetHashCode() : 0);
            }
        }
    }
}
