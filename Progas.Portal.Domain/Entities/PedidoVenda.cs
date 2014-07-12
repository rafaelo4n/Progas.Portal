using System;
using System.Collections.Generic;
using System.Linq;

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
        public virtual string NumeroDoPedidoDoRepresentante { get; set; }
        public virtual string NumeroDoPedidoDoCliente { get; set; }
        public virtual DateTime Datap { get; set; }
        public virtual string Condpgto { get; set; }
        public virtual IncotermCab ModeloDeFrete { get; set; }
        public virtual IncotermLinha TipoDeFrete { get; set; }
        public virtual Fornecedor Transportadora { get; set; }
        public virtual Fornecedor TransportadoraDeRedespachoFob { get; set; }
        public virtual Fornecedor TransportadoraDeRedespachoCif { get; set; }
        public virtual string Id_repre { get; set; }
        public virtual string Observacao { get; set; }
        public virtual StatusDoPedidoDeVenda Status { get; set; }
        public virtual decimal ValorTotal { get; set; }
        public virtual ClienteVenda AreaDeVenda{ get; set; }
        public virtual IList<PedidoVendaLinha> Itens { get; protected set; }


        protected PedidoVenda()
        {
            Itens = new List<PedidoVendaLinha>();
        }

        public PedidoVenda(string tipo,
            //string id_cotacao,
            string tipoPedido,
            string id_centro,
            Cliente cliente,
            ClienteVenda areaDeVenda,
            DateTime datacp,
            string numeroDoPedidoDoRepresentante,
            string numeroDoPedidoDoCliente,
            DateTime datap,
            string condpgto,
            IncotermCab modeloDeFrete,
            IncotermLinha tipoDeFrete,
            Fornecedor transportadora,
            Fornecedor transportadoraDeRedespachoFob,
            Fornecedor transportadoraDeRedespachoCif,
            string id_repre,
            string observacao
            ) : this()
        {

            if (tipoDeFrete.ExigeTransportadoraDeRedespachoFob && transportadoraDeRedespachoFob == null)
            {
                throw new Exception("É necessário informar a Transportadora de Redespacho FOB");
            }

            if (!tipoDeFrete.ExigeTransportadoraDeRedespachoFob && transportadoraDeRedespachoFob != null)
            {
                throw new Exception("A Transportadora de Redespacho FOB não deve ser informada");
            }


            if (!tipoDeFrete.ExigeTransportadoraDeRedespachoCif && transportadoraDeRedespachoCif != null)
            {
                throw new Exception("A Transportadora de Redespacho CIF não deve ser informada");
            }

            if (tipoDeFrete.ExigeTransportadoraDeRedespachoCif && transportadoraDeRedespachoCif == null)
            {
                throw new Exception("É necessário informar a Transportadora de Redespacho CIF");
            }


            Tipo = tipo;
            TipoPedido = tipoPedido;
            Id_centro = id_centro;
            Cliente = cliente;
            AreaDeVenda = areaDeVenda;
            Datacp = datacp;
            NumeroDoPedidoDoRepresentante = numeroDoPedidoDoRepresentante;
            NumeroDoPedidoDoCliente = numeroDoPedidoDoCliente;
            Datap = datap;
            Condpgto = condpgto;
            ModeloDeFrete = modeloDeFrete;
            TipoDeFrete = tipoDeFrete;
            Transportadora = transportadora;
            TransportadoraDeRedespachoFob = transportadoraDeRedespachoFob;
            TransportadoraDeRedespachoCif = transportadoraDeRedespachoCif;
            Id_repre = id_repre;
            Observacao = observacao;
        }

        public virtual void AdicionarItem(PedidoVendaLinha item)
        {
            Itens.Add(item);
        }

        public virtual decimal CalcularTotal()
        {
            ValorTotal = Itens.Sum(item => item.ValorPolitica);
            return ValorTotal;
        }

        public virtual void Alterar(string numeroDaCotacao, StatusDoPedidoDeVenda status)
        {
            this.Id_cotacao = numeroDaCotacao;
            this.Status = status;
        }

        public virtual PedidoVenda AlterarCliente(ClienteVenda areaDeVEnda, Cliente cliente)
        {
            this.AreaDeVenda = areaDeVEnda;
            this.Cliente = cliente;
            return this;
        }

        public virtual PedidoVenda AlterarTransportadora(Fornecedor transportadora, Fornecedor transportadoraDeRedespacho, Fornecedor transportadoraDeRedespachoCif)
        {
            this.Transportadora = transportadora;
            this.TransportadoraDeRedespachoFob = transportadoraDeRedespacho;
            this.TransportadoraDeRedespachoCif = transportadoraDeRedespachoCif;

            return this;
        }

        public virtual PedidoVenda AlterarIncoterm(IncotermCab incoterm1, IncotermLinha incoterm2)
        {
            this.ModeloDeFrete = incoterm1;
            this.TipoDeFrete = incoterm2;

            return this;
        }

        public virtual PedidoVenda AlterarDados(string numeroDoPedidoDoRepresentante, string numeroDoPedidoDoCliente, DateTime dataDoPedido, string codigoDaCondicaoDePagamento, string observacao)
        {
            this.NumeroDoPedidoDoRepresentante = numeroDoPedidoDoRepresentante;
            this.NumeroDoPedidoDoCliente = numeroDoPedidoDoCliente;
            this.Datap = dataDoPedido;
            this.Condpgto = codigoDaCondicaoDePagamento;
            this.Observacao = observacao;

            return this;
        }

        public virtual PedidoVenda AlterarTipo(string tipo)
        {
            this.Tipo = tipo;

            return this;
        }
    }
}
