﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progas.Portal.Common;
using Progas.Portal.Common.Exceptions;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Model;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.Infra.Services.Contracts;

namespace Progas.Portal.Infra.Services.Implementations
{
    public class ValidadorUsuario: IValidadorUsuario
    {
        private readonly IUsuarios _usuarios;
        private readonly IProvedorDeCriptografia _provedorDeCriptografia;

        public ValidadorUsuario(IUsuarios usuarios, IProvedorDeCriptografia provedorDeCriptografia)
        {
            _usuarios = usuarios;
            _provedorDeCriptografia = provedorDeCriptografia;
        }

        public UsuarioConectado Validar(string login, string senha)
        {
            Usuario usuario = _usuarios.BuscaPorLogin(login);
            if (usuario == null)
            {
                throw new UsuarioNaoCadastradoException(login);
            }

            if (usuario.Status == Enumeradores.StatusUsuario.Bloqueado)
            {
                throw new UsuarioBloqueadoException(usuario.Login);
            }

            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new UsuarioSemSenhaException(usuario.Login);
            }

            if (!usuario.Perfis.Any())
            {
                 throw new UsuarioSemPerfilException(usuario.Login);
            }

            string senhaCriptografada = _provedorDeCriptografia.Criptografar(senha);
            if (usuario.Senha == senhaCriptografada)
            {
                return new UsuarioConectado(usuario.Login, usuario.Nome,usuario.Perfis);
                    
            }
            throw new SenhaIncorretaException("A senha informada está incorreta");
        }
    }
}
