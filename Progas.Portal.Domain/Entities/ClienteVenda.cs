using System;

namespace Progas.Portal.Domain.Entities
{
    public class ClienteVenda : IAggregateRoot 
    {   
        public virtual int Id { get; set; }
        public virtual string Id_cliente    { get; set; }
        public virtual string Org_vendas    { get; set; }
        public virtual string Can_dist      { get; set; }
        public virtual string Set_ativ      { get; set; }
        public virtual string Grupo_cli     { get; set; }
        public virtual string Id_fornecedor { get; set; }
        public virtual string Pacote        { get; set; }
        public virtual string Hora_criacao  { get; set; }
        public virtual DateTime Data_criacao{ get; set; }
        public virtual string Denominacao { get; set; }
    }
}