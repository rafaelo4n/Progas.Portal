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
        public virtual DateTime Data_criacao { get; set; }
        public virtual string Pacote { get; set; }
        public virtual string Hora_criacao { get; set; }
        public virtual string Eliminacao { get; set; }
    }
}
