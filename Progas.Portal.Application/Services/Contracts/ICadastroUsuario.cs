﻿using System.Collections.Generic;
using Progas.Portal.Common;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Services.Contracts
{
    public interface ICadastroUsuario
    {
        void Novo(UsuarioCadastroVm usuarioVm);
        void AtualizarUsuarios(IList<UsuarioCadastroVm> usuarios);
        void AtualizarUsuario(UsuarioCadastroVm usuarioVm, IList<Enumeradores.Perfil> perfis);
    }
}