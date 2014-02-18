using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IUsuarios: ICompleteRepository<Usuario>
    {
        Usuario BuscaPorLogin(string login);
        Usuario BuscaPorCodigoRepresentante(string codigoRepresentante);
        IUsuarios NomeContendo(string filtroNome);
        IUsuarios LoginContendo(string login);
        IUsuarios FiltraPorListaDeLogins(string[] logins);
        Usuario UsuarioConectado();
        IUsuarios SemSenha();
    }
}