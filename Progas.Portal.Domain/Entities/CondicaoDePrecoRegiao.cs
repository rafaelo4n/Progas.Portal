using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Domain.Entities
{
    public class CondicaoDePrecoRegiao : IAggregateRoot
    {
        protected CondicaoDePrecoRegiao () { }

        public CondicaoDePrecoRegiao(string regiao, string id_material, string numeroRegistroCondicao, decimal montante, string unidadeCondicao)
        {
            Regiao                 = regiao;
            Id_material            = id_material;
            NumeroRegistroCondicao = numeroRegistroCondicao;
            Montante               = montante;
            UnidadeCondicao        = unidadeCondicao;            
        }

        public virtual int Id { get; set; }
        public virtual string Regiao { get; set; }
        public virtual string Id_material { get; set; }
        public virtual string NumeroRegistroCondicao { get; set; }
        public virtual decimal Montante { get; set; }
        public virtual string UnidadeCondicao { get; set; }
        public virtual DateTime Data_criacao { get; set; }
        public virtual string Pacote { get; set; }
        public virtual string Hora_criacao { get; set; }
    }
}
