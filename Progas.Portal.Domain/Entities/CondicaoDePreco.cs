namespace Progas.Portal.Domain.Entities
{
    public class CondicaoDePreco
    {
        public virtual int Id { get; set; }
        public virtual string Nivel{ get; protected set; }
        public virtual string Tipo { get; set; }
        public virtual decimal Base { get; set; }
        public virtual decimal Montante { get; set; }
        public virtual decimal Valor { get; set; }
    }
}
