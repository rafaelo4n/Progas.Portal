using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class IncotermsCabMap : ClassMap<IncotermCab>
    {
        public IncotermsCabMap()
        {
            Table("pro_incotermcab");
            Id(x => x.pro_id_incotermCab);
            Map(x => x.CodigoIncotermCab);
            Map(x => x.Descricao);
            Map(x => x.Data_criacao);
            Map(x => x.Pacote);
            Map(x => x.Hora_criacao);
        }
    }
}
