namespace Progas.Portal.Domain.Entities
{
    public class TransportadoraDoCliente
    {
        protected TransportadoraDoCliente() { }
        public virtual int Id { get; protected set; }
        public virtual Cliente Cliente { get; protected set; }
        public virtual Fornecedor Transportadora{ get; protected set; }
        public virtual string TipoDeParceiro { get; set; }
    }
}
