using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class IncotermLinhaMap : ClassMap<IncotermLinha>
    {
        public IncotermLinhaMap()
        {
            Table("pro_incotermlinha");
            Id(x => x.Id, "pro_id_incotermLinha");
            Map(x => x.CodigoIncotermCab);
            Map(x => x.Descricao,"IncotermLinha");
            Map(x => x.DataDeCriacao,"Data_criacao");
            Map(x => x.Pacote);
            Map(x => x.HoraDeCriacao,"Hora_criacao");
            Map(x => x.ExigeTransportadoraDeRedespachoCif,"parc_redesp_cif");
            Map(x => x.ExigeTransportadoraDeRedespachoFob,"parc_redesp_fob");
        }
    }
}
