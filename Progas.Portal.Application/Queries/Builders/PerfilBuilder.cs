using Progas.Portal.Common;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    public class PerfilBuilder: Builder<Enumeradores.Perfil, PerfilVm>
    {
        public override PerfilVm BuildSingle(Enumeradores.Perfil model)
        {
            return new PerfilVm()
                {
                    Codigo = (int) model,
                    Descricao = model.Descricao()
                };
        }
    }
}
