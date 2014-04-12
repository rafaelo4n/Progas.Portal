using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class CondicaoDePrecoClienteMap : ClassMap<CondicaoDePrecoCliente>
    {
        public CondicaoDePrecoClienteMap()
        {
            Table("pro_condicaopreco_cliente");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Org_vendas);
            Map(x => x.Can_dist);
            Map(x => x.Id_cliente);
            Map(x => x.Id_material);
            Map(x => x.NumeroRegistroCondicao);
            Map(x => x.Montante);
            Map(x => x.UnidadeCondicao);
            Map(x => x.Pacote);
            Map(x => x.Data_criacao);
            Map(x => x.Hora_criacao);
        }
    }
}
