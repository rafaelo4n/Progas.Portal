using System.Collections.Generic;

namespace Progas.Portal.Domain.Entities
{
    public class PedidoVendaLinha
    {
        // pro_vitem
        public virtual int pro_id_item { get; set; }
        //public virtual string  Id_cotacao { get; set; }
        public virtual string  Id_item { get; set; }
        public virtual string  Id_pedido { get; set; }
        public virtual Material  Material { get; set; }
        public virtual decimal Quant { get; set; }
        public virtual string  Listpre { get; set; }
        public virtual decimal Valtab { get; set; }
        public virtual decimal Valpol { get; set; }
        public virtual decimal Descma { get; set; }
        public virtual decimal Valfin { get; set; }
        public virtual string  Motrec { get; set; }
        public virtual IList<CondicaoDePreco> CondicoesDePreco  { get; protected set; }

        protected PedidoVendaLinha()
        {
            CondicoesDePreco = new List<CondicaoDePreco>();
        }

        public PedidoVendaLinha(//string id_cotacao,
            string id_item,
            string id_pedido,
            Material material,
            decimal quant,
            string listpre,
            decimal valtab,
            decimal valpol,
            decimal descma,
            string motrec
            ) : this()
        {
            //Id_cotacao = id_cotacao;
            Id_item = id_item;
            Id_pedido = id_pedido;
            Material = material;
            Quant = quant;
            Listpre = listpre;
            Valtab = valtab;
            Valpol = valpol;
            Descma = descma;
            Motrec = motrec;
        }

        public virtual void AdicionarCondicao(CondicaoDePreco condicaoDePreco)
        {
            CondicoesDePreco.Add(condicaoDePreco);
        }

    }
}
