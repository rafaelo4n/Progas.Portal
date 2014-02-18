using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class TipoPedidoMap : ClassMap<TipoPedido>
    {
        public TipoPedidoMap()
        {
            Table("pro_tipopedido");
            Id(x => x.Codigo);
            Map(x => x.Descricao);
        }
    }
}
