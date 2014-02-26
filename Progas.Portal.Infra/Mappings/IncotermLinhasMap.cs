using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class IncotermLinhasMap : ClassMap<IncotermLinhas>
    {
        public IncotermLinhasMap()
        {
            Table("pro_incotermlinha");
            Id(x => x.pro_id_incotermLinha);
            Map(x => x.CodigoIncotermCab);
            Map(x => x.IncotermLinha);
            Map(x => x.Data_criacao);
            Map(x => x.Pacote);
            Map(x => x.Hora_criacao);
        }
    }
}
