using Progas.Portal.Infra.Model;

namespace Progas.Portal.Infra.Services.Contracts
{
    public interface IValidadorUsuario
    {
        UsuarioConectado Validar(string usuario, string senha); 
    }
}