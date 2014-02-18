using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Domain.Entities
{
    public class CondicaoPagamento
    {
        public virtual string id_condpgto { get; set; }
        public virtual string descricao { get; set; }
    }
}
