using Progas.Portal.Common;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{

    public class UsuarioConsultaBuilder : Builder<Usuario, UsuarioConsultaVm>
    {
        public override UsuarioConsultaVm BuildSingle(Usuario model)
        {
            return new UsuarioConsultaVm()
            {
                Login = model.Login,
                Nome = model.Nome,
                Email = model.Email,
                CodigoFornecedor = model.CodigoFornecedor,
                Status = model.Status.Descricao()
            };
        }

    }
}
