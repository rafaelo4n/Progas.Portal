using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    public class UnidadeDeMedidaCadastroBuilder : Builder<UnidadeDeMedida, UnidadeDeMedidaCadastroVm>
    {
        public override UnidadeDeMedidaCadastroVm BuildSingle(UnidadeDeMedida unidadeDeMedida)
        {
            return new UnidadeDeMedidaCadastroVm()
                {
                    Id_unidademedida = unidadeDeMedida.Id_unidademedida,
                    Descricao        = unidadeDeMedida.Descricao,
                    Dimensao         = unidadeDeMedida.Dimensao,
                    Aprestecnica     = unidadeDeMedida.Aprestecnica                   
                };
        }
    }
}
