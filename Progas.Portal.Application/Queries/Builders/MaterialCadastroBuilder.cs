using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.Application.Queries.Builders
{
    public class MaterialCadastroBuilder : Builder<Material, MaterialCadastroVm>
    {
        public override MaterialCadastroVm BuildSingle(Material material)
        {
            return new MaterialCadastroVm()
            {
                Id_material     = material.Id_material,
                Descricao       = material.Descricao,
                Centro          = material.Id_centro,
                Tipo            = material.Tip_mat,
                UnidadeMedida   = material.Uni_med
            };
        }
    }
}
