using System;
using System.Collections.Generic;
using Progas.Portal.Domain.ValueObjects;

namespace Progas.Portal.Domain.Entities
{
    public class PedidoVendaLinha
    {

        public virtual int Id { get; set; }
        public virtual string  Numero { get; set; }
        public virtual Material Material { get; set; }

        public virtual decimal Quantidade
        {
            get { return this._quantidade; }
            set {
                if (value <= 0)
                {
                    throw new Exception("Quantidade deve ser maior que 0");
                }

                this._quantidade = value;
            }
        }
        public virtual ListaPreco ListaDePreco { get; set; }
        public virtual decimal ValorTabela { get; set; }
        public virtual decimal ValorPolitica { get; set; }
        public virtual decimal DescontoManual { get; set; }
        public virtual MotivoDeRecusa  MotivoDeRecusa { get; set; }
        public virtual IList<CondicaoDePreco> CondicoesDePreco  { get; protected set; }

        private decimal _quantidade;

        protected PedidoVendaLinha()
        {
            CondicoesDePreco = new List<CondicaoDePreco>();
        }

        public PedidoVendaLinha(string numero, Material material, decimal quantidade, ListaPreco listaDePreco, decimal descontoManual, MotivoDeRecusa motivoDeRecusa) : this()
        {
            Numero = numero;
            Material = material;
            Quantidade = quantidade;
            ListaDePreco = listaDePreco;
            DescontoManual = descontoManual;
            MotivoDeRecusa = motivoDeRecusa;
        }

        public virtual void AdicionarCondicao(CondicaoDePreco condicaoDePreco)
        {
            CondicoesDePreco.Add(condicaoDePreco);
        }

        public virtual void Alterar(string numeroDoPedido, decimal valorPolitica, decimal valorTabela, MotivoDeRecusa motivoDeRecusa)
        {
            this.Numero = numeroDoPedido;
            this.ValorPolitica = valorPolitica;
            this.ValorTabela = valorTabela;
            this.MotivoDeRecusa = motivoDeRecusa;
        }

        public virtual void Alterar(ItemDeVenda item)
        {
            this.Material = item.Material;
            this.Quantidade = item.Quantidade;
            this.ListaDePreco = item.ListaPreco;
            this.DescontoManual = item.DescontoManual;
            this.MotivoDeRecusa = item.MotivoDeRecusa;
        }
    }
}
