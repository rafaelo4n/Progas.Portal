using System;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Model;
using Progas.Portal.Infra.Services.Contracts;

namespace Progas.Portal.Infra.Services.Implementations
{
    public class GeradorDeMensagemDeEmail : IGeradorDeMensagemDeEmail
    {
        public MensagemDeEmail CriacaoAutomaticaDeSenha(Usuario usuario, string novaSenha)
        {
            string mensagem = "Prezado(a) " + usuario.Nome + Environment.NewLine + Environment.NewLine +
            "Conforme foi solicitado através do Portal de Vendas da PROGAS, segue abaixo a sua nova senha de acesso ao site. " + 
            "Esta senha foi gerada automaticamente no momento da sua solicitação. "+
            "Recomenda-se que acesse o site http://portaldevendas.progas.com.br/ e altere a senha para uma de sua preferência." + Environment.NewLine + Environment.NewLine +
            "Dados de Acesso:" + Environment.NewLine + Environment.NewLine + 
            "Login: " + usuario.Login + Environment.NewLine +
            "Nova Senha: " + novaSenha + Environment.NewLine +
            "Atenciosamente," + Environment.NewLine +
            "PROGAS" + Environment.NewLine + Environment.NewLine +
            "Esta é uma mensagem gerada automaticamente, portanto, não deve ser respondida." + Environment.NewLine +
            "© PROGAS. Todos os direitos reservados. Termos e Condições e Política de Privacidade." + Environment.NewLine;

            return new MensagemDeEmail("Geração automática de senha",mensagem);
        }            
    }
}