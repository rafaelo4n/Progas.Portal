using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class PedidoVendaLinha
    {
        public virtual int Id { get; set; }
        public virtual string  Id_cotacao { get; set; }
        public virtual string  Id_item { get; set; }
        public virtual int     Material { get; set; }
        public virtual decimal Quantidade { get; set; }
        public virtual string  ListaDePreco { get; set; }
        public virtual decimal ValorTabela { get; set; }
        public virtual decimal ValorPolitica { get; set; }
        public virtual decimal DescontoManual { get; set; }
        public virtual string  MotivoDeRecusa { get; set; }
        public virtual string  Status { get; set; }
    }
}
