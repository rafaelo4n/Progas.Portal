using Progas.Portal.Common.Exceptions;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Model;
using Progas.Portal.Infra.Services.Contracts;
using StructureMap;

namespace Progas.Portal.Infra.Services.Implementations
{
    public class GeradorDeEmail : IGeradorDeEmail
    {
        //private readonly ContaDeEmail _contaDeEmail ;
        private readonly GeradorDeMensagemDeEmail _geradorDeMensagemDeEmail;

        public GeradorDeEmail(GeradorDeMensagemDeEmail geradorDeMensagemDeEmail)
        {
            _geradorDeMensagemDeEmail = geradorDeMensagemDeEmail;
        }

        public void CriacaoAutomaticaDeSenha(Usuario usuario, string novaSenha)
        {
            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new UsuarioSemEmailException(usuario.Nome);
            }
            var contaDeEmail = ObjectFactory.GetNamedInstance<ContaDeEmail>("ContaDeEmailProgas");
            var emailService = new EmailService(contaDeEmail);
            emailService.Enviar(usuario.Email, _geradorDeMensagemDeEmail.CriacaoAutomaticaDeSenha(usuario, novaSenha));
        }

    }
}