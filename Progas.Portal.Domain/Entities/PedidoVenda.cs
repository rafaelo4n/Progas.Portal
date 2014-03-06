using System;
using System.Collections.Generic;

namespace Progas.Portal.Domain.Entities
{
    public class PedidoVenda : IAggregateRoot
    {
        // pro_vcab  
        public virtual string Tipo { get; set; }
        public virtual string Id_cotacao { get; set; }
        public virtual string TipoPedido { get; set; }
        public virtual string Id_centro { get; set; }
        public virtual string Id_cliente { get; set; }
        public virtual DateTime Datacp { get; set; }
        public virtual string Id_pedido { get; set; }
        public virtual DateTime Datap { get; set; }
        public virtual string Condpgto { get; set; }
        public virtual string Inco1 { get; set; }
        public virtual string Inco2 { get; set; }
        public virtual string Trans { get; set; }
        public virtual string Transred { get; set; }
        public virtual string Transredcif { get; set; }
        public virtual string Id_repre { get; set; }
        public virtual string Obs { get; set; }
        public virtual string Motrec { get; set; }
        public virtual string Status { get; set; }
        public virtual decimal Vlrtot { get; set; }
        public virtual ClienteVenda AreaDeVenda{ get; set; }
        public virtual List<PedidoVendaLinha> Itens { get; protected set; }


        protected PedidoVenda()
        {
            Itens = new List<PedidoVendaLinha>();
        } 

        public PedidoVenda( string tipo,
                            string id_cotacao, 
                            string tipoPedido, 
                            string id_centro, 
                            string id_cliente,
                            ClienteVenda areaDeVenda,
                            DateTime datacp,
                            string id_pedido,
                            DateTime datap,
                            string condpgto,
                            string inco1,
                            string inco2,
                            string trans,
                            string transred,
                            string transredcif, 
                            string id_repre, 
                            string obs
                            //string motrec, string status, decimal vlrtot, string tipo
                            ):this()            
        {
            Tipo        = tipo;
            Id_cotacao  = id_cotacao;
            TipoPedido  = tipoPedido;
            Id_centro   = id_centro;
            Id_cliente  = id_cliente;
            AreaDeVenda = areaDeVenda;
            Datacp      = datacp;
            Id_pedido   = id_pedido;
            Datap       = datap;
            Condpgto    = condpgto;
            Inco1       = inco1;
            Inco2       = inco2;
            Trans       = trans;
            Transred    = transred;
            Transredcif = transredcif;  
            Id_repre    = id_repre;
            Obs         = obs;
        }

        public void AdicionarItem(PedidoVendaLinha item)
        {
            Itens.Add(item);
        }
             
    }
}
