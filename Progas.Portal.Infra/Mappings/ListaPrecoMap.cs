using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class ListaPrecoMap : ClassMap<ListaPreco>
    {
        public ListaPrecoMap()
        {
            Table("pro_listapreco");
            Id(x => x.Codigo);
            Map(x => x.Descricao);
        }
    }
}
