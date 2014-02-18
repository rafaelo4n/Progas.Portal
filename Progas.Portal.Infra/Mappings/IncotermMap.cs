using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class IncotermMap : ClassMap<Incoterm>
    {
        public IncotermMap()
        {
            Table("pro_incoterms");
            Id(x => x.CodigoIncoterm);
            Map(x => x.Descricao);
            Map(x => x.Tipo);
        }
    }
}
