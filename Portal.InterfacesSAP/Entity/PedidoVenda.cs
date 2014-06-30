using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DadosSap.Entity
{
    public class PedidoVenda
    {
        // pro_vcab  
        public virtual string Tipo { get; set; }
        public virtual string Id_cotacao { get; set; }
        public virtual string TipoPedido { get; set; }
        public virtual string Id_centro { get; set; }
        public virtual string Cliente { get; set; }
        public virtual DateTime Datacp { get; set; }
        public virtual string NumeroDoPedido { get; set; }
        public virtual DateTime Datap { get; set; }
        public virtual string Condpgto { get; set; }
        public virtual int Incoterm1 { get; set; }
        public virtual int Incoterm2 { get; set; }
        public virtual string Transportadora { get; set; }
        public virtual string TransportadoraDeRedespacho { get; set; }
        public virtual string TransportadoraDeRedespachoCif { get; set; }
        public virtual string Id_repre { get; set; }
        public virtual string Observacao { get; set; }
        public virtual string Status { get; set; }
        public virtual decimal ValorTotal { get; set; }
        public virtual int AreaDeVenda { get; set; }
        public virtual string NumeroDoPedidoDoCliente { get; set; }
        //public virtual IList<PedidoVendaLinha> Itens { get; protected set; }
    }
}
