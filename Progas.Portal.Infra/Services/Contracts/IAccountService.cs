using Progas.Portal.Infra.Model;

namespace Progas.Portal.Infra.Services.Contracts
{
    public interface IAccountService
    {
        UsuarioConectado Login(string usuario, string senha);
        void Logout();
    }
}