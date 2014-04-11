using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class MaterialMap : ClassMap<Material>
    {
        public MaterialMap()
        {
            Table("pro_material");
            Id(x => x.pro_id_material);
            Map(x => x.Id_material);
            Map(x => x.Id_cliente);
            Map(x => x.Id_centro);
            Map(x => x.Descricao);
            Map(x => x.Tip_mat);
            Map(x => x.Uni_med);
            Map(x => x.Peso_bru);
            Map(x => x.Peso_liq);
            Map(x => x.Volume);
            Map(x => x.Status_mat);
            Map(x => x.Data_criacao);
            Map(x => x.Pacote);
            Map(x => x.Hora_criacao);
            Map(x => x.Eliminacao);
        }
    }
}
