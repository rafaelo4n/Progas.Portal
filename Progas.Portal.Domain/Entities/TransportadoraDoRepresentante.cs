namespace Progas.Portal.Domain.Entities
{
    public class TransportadoraDoRepresentante
    {
        protected TransportadoraDoRepresentante() { }
        public virtual int Id { get; protected set; }
        public virtual Fornecedor Representante { get; protected set; }
        public virtual Fornecedor Transportadora { get; protected set; }
        public virtual string TipoDeParceiro { get; set; }
    }
}
