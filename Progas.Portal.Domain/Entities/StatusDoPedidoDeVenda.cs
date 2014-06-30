namespace Progas.Portal.Domain.Entities
{
    public class StatusDoPedidoDeVenda: IAggregateRoot
    {
        protected StatusDoPedidoDeVenda() { }

        public virtual string Codigo { get; protected set; }
        public virtual string Descricao { get; protected set; }
    }
}
