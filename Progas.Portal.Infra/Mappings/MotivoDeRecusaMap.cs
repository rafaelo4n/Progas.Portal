using FluentNHibernate.Mapping;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Mappings
{
    public class MotivoDeRecusaMap: ClassMap<MotivoDeRecusa>
    {
        public MotivoDeRecusaMap()
        {
            Table("MotivoDeRecusa");
            Id(x => x.Codigo);
            Map(x => x.Descricao);
        }
    }
}
