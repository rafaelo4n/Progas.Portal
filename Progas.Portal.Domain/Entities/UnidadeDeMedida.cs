using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Domain.Entities
{
    public class UnidadeDeMedida : IAggregateRoot
    {   
        public virtual string Id_unidademedida { get; protected set; }
        public virtual string Descricao        { get; protected set; }
        public virtual string Dimensao         { get; protected set; }
        public virtual string Aprestecnica     { get; protected set; }
        public virtual string pacote           { get; protected set; }
        public virtual string hora_criacao     { get; protected set; }
        public virtual DateTime data_criacao   { get; protected set; }

        public UnidadeDeMedida( string id_unidademedida,
                                string descricao,
                                string dimensao,
                                string aprestecnica):this()
        {
            Id_unidademedida = id_unidademedida;
            Descricao        = descricao;
            Dimensao         = dimensao;
            Aprestecnica     = aprestecnica;
        }

        public UnidadeDeMedida() 
        {

        }
        
    }
}
