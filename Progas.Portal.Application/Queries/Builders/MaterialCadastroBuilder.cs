using NHibernate.Criterion;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    public class MaterialCadastroBuilder : Builder<Material, MaterialCadastroVm>
    {
        public override MaterialCadastroVm BuildSingle(Material material)
        {
            return new MaterialCadastroVm()
            {
                Id = material.pro_id_material,
                Id_material = material.Id_material,
                Descricao = material.Descricao,
                Centro = material.Id_centro,
                Tipo = material.Tip_mat,
                UnidadeMedida = material.UnidadeDeMedida.Id_unidademedida
            };
        }
    }
}
