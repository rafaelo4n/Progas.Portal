using System;
using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Common;
using Progas.Portal.Common.Exceptions;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Domain.Entities
{
    public class PedidoVendaLinha : IAggregateRoot
    {
        // pro_vitem
        public virtual int pro_id_item { get; set; }
        public virtual string  Id_cotacao { get; set; }
        public virtual string  Id_item { get; set; }
        public virtual string  Id_pedido { get; set; }
        public virtual string  Id_material { get; set; }
        public virtual decimal Quant { get; set; }
        public virtual string  Unimed { get; set; }
        public virtual string  Listpre { get; set; }
        public virtual decimal Valtab { get; set; }
        public virtual decimal Valpol { get; set; }
        public virtual decimal Descma { get; set; }
        public virtual decimal Valfin { get; set; }
        public virtual string  Motrec { get; set; }

        protected PedidoVendaLinha()
        {
        }

        public PedidoVendaLinha( string id_cotacao,
                                 string id_item, 
                                 string  id_pedido,
                                 string  id_material, 
                                 decimal quant,
                                 string  unimed, 
                                 string  listpre, 
                                 //decimal valtab, 
                                 //decimal valpol,
                                 decimal descma//, 
                                 //decimal valfin, 
                                 //string motrec
                                ):this()
        {
            Id_cotacao = id_cotacao;
            Id_item     = id_item;
            Id_pedido   = id_pedido;
            Id_material = id_material;
            Quant       = quant;
            Unimed      = unimed;
            Listpre     = listpre;
            //Valtab = valtab;
            //Valpol = valpol;
            Descma      = descma;
            //Valfin = valfin;
            //Motrec = motrec;
        }

        /*public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PedidoVendaLinha)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Id_cotacao != null ? Id_cotacao.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Id_item != null ? Id_item.GetHashCode() : 0);
                return hashCode;
            }
        }*/
    }
}
