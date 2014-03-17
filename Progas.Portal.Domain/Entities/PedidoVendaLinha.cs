using System;
using System.Collections.Generic;

namespace Progas.Portal.Domain.Entities
{
    public class PedidoVendaLinha
    {
        // pro_vitem
        public virtual int Id { get; set; }
        public virtual string  Numero { get; set; }
        public virtual Material Material { get; set; }
        public virtual decimal Quantidade { get; set; }
        public virtual ListaPreco ListaDePreco { get; set; }
        public virtual decimal ValorTabela { get; set; }
        public virtual decimal ValorPolitica { get; set; }
        public virtual decimal DescontoManual { get; set; }
        //public virtual decimal Valfin { get; set; }
        public virtual string Status { get; protected set; }
        public virtual MotivoDeRecusa  MotivoDeRecusa { get; set; }
        public virtual IList<CondicaoDePreco> CondicoesDePreco  { get; protected set; }

        protected PedidoVendaLinha()
        {
            CondicoesDePreco = new List<CondicaoDePreco>();
        }

        public PedidoVendaLinha(
            string id_item,
            Material material,
            decimal quantidade,
            ListaPreco listaDePreco,
            decimal valorTabela,
            decimal valorPolitica,
            decimal descontoManual,
            MotivoDeRecusa motivoDeRecusa
            ) : this()
        {
            //Id_cotacao = id_cotacao;
            Numero = id_item;
            Material = material;
            Quantidade = quantidade;
            ListaDePreco = listaDePreco;
            ValorTabela = valorTabela;
            ValorPolitica = valorPolitica;
            DescontoManual = descontoManual;
            MotivoDeRecusa = motivoDeRecusa;
        }

        public virtual void AdicionarCondicao(CondicaoDePreco condicaoDePreco)
        {
            CondicoesDePreco.Add(condicaoDePreco);
        }

    }
}
