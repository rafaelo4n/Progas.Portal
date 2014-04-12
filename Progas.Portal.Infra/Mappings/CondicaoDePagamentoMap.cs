using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class CondicaoDePagamentoMap : ClassMap<CondicaoDePagamento>
    {
        public CondicaoDePagamentoMap()
        {
            Table("pro_condpgto");
            Id(x => x.Codigo);
            Map(x => x.Descricao);
            Map(x => x.pacote);
            Map(x => x.hora_criacao);
            Map(x => x.data_criacao);
            Map(x => x.Eliminacao);
        }
    }
}
