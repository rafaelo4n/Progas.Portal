using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class PedidoVendaLinhaMap : ClassMap<PedidoVendaLinha>
    {
        public PedidoVendaLinhaMap()
        {
            Table("pro_vitem");
            Id(x => x.pro_id_item);
            Map(x => x.Id_cotacao);
            Map(x => x.Id_item);
            Map(x => x.Id_pedido);
            Map(x => x.Id_material);
            Map(x => x.Quant);
            Map(x => x.Unimed);
            Map(x => x.Listpre);
            Map(x => x.Valtab);
            Map(x => x.Valpol);
            Map(x => x.Descma);
            Map(x => x.Valfin);
            Map(x => x.Motrec);
        }
    }
}
