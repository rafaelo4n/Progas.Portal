using System;

namespace Progas.Portal.Domain.Entities
{
    public class IncotermLinha : IAggregateRoot
    {
        protected IncotermLinha() { }

        public virtual int Id { get; protected set; }
        public virtual string CodigoIncotermCab { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual DateTime DataDeCriacao { get; protected set; }
        public virtual string Pacote { get; protected set; }
        public virtual string HoraDeCriacao { get; protected set; }
        public virtual bool ExigeTransportadoraDeRedespachoCif { get; protected set; }
        public virtual bool ExigeTransportadoraDeRedespachoFob { get; protected set; }
    

   }
}
