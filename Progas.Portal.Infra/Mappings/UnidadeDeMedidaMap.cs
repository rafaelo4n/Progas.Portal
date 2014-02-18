using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class UnidadeDeMedidaMap:ClassMap<UnidadeDeMedida>
    {
        public UnidadeDeMedidaMap()
        {
            Table("pro_unidademedida");
            Id(x => x.Id_unidademedida);            
            Map(x => x.Descricao);
            Map(x => x.Dimensao);
            Map(x => x.Aprestecnica);
            Map(x => x.pacote);
            Map(x => x.hora_criacao);
            Map(x => x.data_criacao);
        }
    }
}
