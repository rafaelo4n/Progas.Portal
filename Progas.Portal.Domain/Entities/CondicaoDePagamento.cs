using System;

namespace Progas.Portal.Domain.Entities
{
    public class CondicaoDePagamento : IAggregateRoot
    {
        public virtual string Codigo { get; protected set; }
        public virtual string Descricao { get; protected set;}
        public virtual string pacote { get; set; }
        public virtual string hora_criacao { get; set; }
        public virtual DateTime data_criacao { get; set; }
        public virtual string Eliminacao {get;  protected set; }

        protected CondicaoDePagamento(){}
        public CondicaoDePagamento(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public virtual void AtualizarDescricao(string descricao)
        {
            Descricao = descricao;
        }
    }
}
