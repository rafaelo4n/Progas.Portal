using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Services.Contracts
{
    public interface IGeradorDeEmail
    {
        void CriacaoAutomaticaDeSenha(Usuario usuario, string novaSenha);
    }
}