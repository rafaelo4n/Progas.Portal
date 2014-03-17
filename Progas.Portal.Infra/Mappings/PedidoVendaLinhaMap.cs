using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class PedidoVendaLinhaMap : ClassMap<PedidoVendaLinha>
    {
        public PedidoVendaLinhaMap()
        {
            Table("pro_vitem");
            Id(x => x.Id,"pro_id_item");
            Map(x => x.Numero,"id_item");
            Map(x => x.Status);
            Map(x => x.Quantidade,"Quant");
            Map(x => x.ValorTabela,"Valtab");
            Map(x => x.ValorPolitica,"Valpol");
            Map(x => x.DescontoManual,"Descma");
            //Map(x => x.Valfin);
            References(x => x.MotivoDeRecusa,"Motrec");
            References(x => x.Material, "pro_id_material");
            References(x => x.ListaDePreco,"Listpre");

            HasMany(x => x.CondicoesDePreco)
                .KeyColumn("pro_id_item")
                .Not.Inverse()
                .Not.KeyNullable()
                .Not.KeyUpdate()
                .Cascade.AllDeleteOrphan();
        }
    }
}
