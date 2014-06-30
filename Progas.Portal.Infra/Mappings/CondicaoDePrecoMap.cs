using FluentNHibernate.Mapping;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Mappings
{
    public class CondicaoDePrecoMap: ClassMap<CondicaoDePreco>
    {
        public CondicaoDePrecoMap()
        {
            Table("pro_item_condicaopreco");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nivel);
            Map(x => x.Descricao);
            Map(x => x.Tipo);
            Map(x => x.Base);
            Map(x => x.Montante);
            Map(x => x.Valor);
        }
    }
}
