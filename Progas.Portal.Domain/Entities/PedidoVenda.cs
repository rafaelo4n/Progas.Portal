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
        public virtual Cliente Cliente { get; set; }
        public virtual DateTime Datacp { get; set; }
        public virtual string Id_pedido { get; set; }
        public virtual DateTime Datap { get; set; }
        public virtual string Condpgto { get; set; }
        public virtual string Inco1 { get; set; }
        public virtual string Inco2 { get; set; }
        public virtual Fornecedor Transportadora { get; set; }
        public virtual Fornecedor TransportadoraDeRedespacho { get; set; }
        public virtual Fornecedor TransportadoraDeRedespachoCif { get; set; }
        public virtual string Id_repre { get; set; }
        public virtual string Obs { get; set; }
        public virtual string Motrec { get; set; }
        public virtual string Status { get; set; }
        public virtual decimal Vlrtot { get; set; }
        public virtual ClienteVenda AreaDeVenda{ get; set; }
        public virtual IList<PedidoVendaLinha> Itens { get; protected set; }


        protected PedidoVenda()
        {
            Itens = new List<PedidoVendaLinha>();
        }

        public PedidoVenda(string tipo,
            string id_cotacao,
            string tipoPedido,
            string id_centro,
            Cliente cliente,
            ClienteVenda areaDeVenda,
            DateTime datacp,
            string id_pedido,
            DateTime datap,
            string condpgto,
            string inco1,
            string inco2,
            Fornecedor transportadora,
            Fornecedor transportadoraDeRedespacho,
            Fornecedor transportadoraDeRedespachoCif,
            string id_repre,
            string obs,
            string status
            //string motrec, string status, decimal vlrtot, string tipo
            ) : this()
        {
            Tipo = tipo;
            Id_cotacao = id_cotacao;
            TipoPedido = tipoPedido;
            Id_centro = id_centro;
            Cliente = cliente;
            AreaDeVenda = areaDeVenda;
            Datacp = datacp;
            Id_pedido = id_pedido;
            Datap = datap;
            Condpgto = condpgto;
            Inco1 = inco1;
            Inco2 = inco2;
            Transportadora = transportadora;
            TransportadoraDeRedespacho = transportadoraDeRedespacho;
            TransportadoraDeRedespachoCif = transportadoraDeRedespachoCif;
            Id_repre = id_repre;
            Obs = obs;
            Status = status;
        }

        public virtual void AdicionarItem(PedidoVendaLinha item)
        {
            Itens.Add(item);
        }
             
    }
}
