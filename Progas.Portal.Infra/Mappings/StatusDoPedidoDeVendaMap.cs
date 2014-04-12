using FluentNHibernate.Mapping;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Mappings
{
    public class StatusDoPedidoDeVendaMap: ClassMap<StatusDoPedidoDeVenda>
    {
        public StatusDoPedidoDeVendaMap()
        {
            Table("StatusDoPedidoDeVenda");
            Id(x => x.Codigo);
            Map(x => x.Descricao);

        }
    }
}
