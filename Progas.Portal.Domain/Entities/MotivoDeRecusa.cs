namespace Progas.Portal.Domain.Entities
{
    public class MotivoDeRecusa: IAggregateRoot
    {

        protected MotivoDeRecusa() { }

        public virtual string Codigo { get; protected set; }
        public virtual string Descricao { get; protected set; }
    }

}
