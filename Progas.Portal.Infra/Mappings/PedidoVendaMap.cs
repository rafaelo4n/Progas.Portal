using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class PedidoVendaMap : ClassMap<PedidoVenda>
    {
        public PedidoVendaMap()
        {
            Table("pro_vcab");
            Id(x => x.Id_cotacao).Column("id_cotacao");//.GeneratedBy.Native("pro_id_pedido_venda");
            Map(x => x.TipoPedido);
            Map(x => x.Id_centro);
            Map(x => x.Id_cliente);
            Map(x => x.Datacp);
            Map(x => x.Id_pedido);
            Map(x => x.Datap);
            Map(x => x.Condpgto);
            Map(x => x.Inco1);
            Map(x => x.Inco2);
            Map(x => x.Trans);
            Map(x => x.Transred);
            Map(x => x.Transredcif);
            Map(x => x.Id_repre);
            Map(x => x.Obs);
            Map(x => x.Motrec);
            Map(x => x.Status);
            Map(x => x.Vlrtot);
            Map(x => x.Tipo);

            References(x => x.AreaDeVenda, "pro_id_cliente_vendas");

            HasMany(x => x.Itens)
                .KeyColumn("Id_cotacao")
                .Not.Inverse()
                .Not.KeyNullable()
                .Not.KeyUpdate()
                .Cascade.AllDeleteOrphan();

        }
    }
}
