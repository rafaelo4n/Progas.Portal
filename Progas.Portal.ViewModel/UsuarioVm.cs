﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.ViewModel
{
    public class UsuarioVm
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public string Email { get; set; }
        public string CodigoFornecedor { get; set; }
        public int? CodigoPerfil { get; set; }
        public string DescricaoPerfil { get; set; }
    }
}
