using Progas.Portal.Common;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{

    public class UsuarioConsultaBuilder : Builder<Usuario, UsuarioConsultaVm>
    {
        public override UsuarioConsultaVm BuildSingle(Usuario model)
        {
            var usuarioConsultaVm = new UsuarioConsultaVm()
            {
                Login = model.Login,
                Nome = model.Nome,
                Email = model.Email,
                Status = model.Status.Descricao()
            };

            if (model.Fornecedor != null)
            {
                usuarioConsultaVm.CodigoFornecedor = model.Fornecedor.Codigo;
                usuarioConsultaVm.NomeDoRepresentante = model.Fornecedor.Nome;
            }

            return usuarioConsultaVm;

        }
    }
}
