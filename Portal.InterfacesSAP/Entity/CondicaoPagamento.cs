using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class CondicaoPagamento
    {
        public virtual string Codigo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual DateTime data_criacao { get; set; }
        public virtual string pacote { get; set; }
        public virtual string hora_criacao { get; set; }
        public virtual string Eliminacao { get; set; }
    }
}
