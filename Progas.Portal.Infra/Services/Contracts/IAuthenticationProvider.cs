using Progas.Portal.Infra.Model;

namespace Progas.Portal.Infra.Services.Contracts
{
    public interface IAuthenticationProvider
    {
        void Autenticar(UsuarioConectado usuarioConectado);
        void Desconectar();
    }
}