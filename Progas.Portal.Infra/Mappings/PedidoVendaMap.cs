using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class PedidoVendaMap : ClassMap<PedidoVenda>
    {
        public PedidoVendaMap()
        {
            Table("pro_vcab");
            Id(x => x.Id_cotacao).Column("id_cotacao");
            Map(x => x.TipoPedido);
            Map(x => x.Id_centro);
            Map(x => x.Datacp);
            Map(x => x.NumeroDoPedido, "Id_pedido");
            Map(x => x.Datap);
            Map(x => x.Condpgto);
            Map(x => x.Id_repre);
            Map(x => x.Observacao,"Obs");
            Map(x => x.Status);
            Map(x => x.ValorTotal,"Vlrtot");
            Map(x => x.Tipo);

            References(x => x.Cliente, "Id_cliente");
            References(x => x.AreaDeVenda, "pro_id_cliente_vendas");
            References(x => x.Transportadora,"CodigoDaTransportadora");
            References(x => x.TransportadoraDeRedespacho, "CodigoDaTransportadoraDeRedespacho");
            References(x => x.TransportadoraDeRedespachoCif,"CodigoDaTransportadoraDeRedespachoCif");
            References(x => x.Incoterm1, "pro_id_incotermCab");
            References(x => x.Incoterm2, "pro_id_incotermLinha");

            HasMany(x => x.Itens)
                .KeyColumn("Id_cotacao")
                .Not.Inverse()
                .Not.KeyNullable()
                .Not.KeyUpdate()
                .Cascade.AllDeleteOrphan();

        }
    }
}
