using System;
using System.Collections.Generic;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Common;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Services.Implementations
{
    public class CadastroUsuario: ICadastroUsuario
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarios _usuarios;
        private readonly IFornecedores _fornecedores;

        public CadastroUsuario(IUnitOfWork unitOfWork, IUsuarios usuarios, IFornecedores fornecedores)
        {
            _unitOfWork = unitOfWork;
            _usuarios = usuarios;
            _fornecedores = fornecedores;
        }

        public void Novo(UsuarioCadastroVm usuarioVm)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                Fornecedor fornecedor = _fornecedores.BuscaPeloCodigo(usuarioVm.CodigoFornecedor);
                var novoUsuario = new Usuario(usuarioVm.Nome, usuarioVm.Login, usuarioVm.Email, fornecedor);
                _usuarios.Save(novoUsuario);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        private Usuario AtualizarUsuario(UsuarioCadastroVm usuarioCadastroVm)
        {
            Usuario usuario = _usuarios.BuscaPorLogin(usuarioCadastroVm.Login);
            Fornecedor fornecedor = _fornecedores.BuscaPeloCodigo(usuarioCadastroVm.CodigoFornecedor);
            if (usuario != null)
            {
                usuario.Alterar(usuarioCadastroVm.Nome, usuarioCadastroVm.Email, fornecedor);
            }
            else
            {
                usuario = new Usuario(usuarioCadastroVm.Nome, usuarioCadastroVm.Login,usuarioCadastroVm.Email, fornecedor);
            }

            return usuario;
        }

        public void AtualizarUsuarios(IList<UsuarioCadastroVm> usuarios)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                foreach (var usuarioCadastroVm in usuarios)
                {
                    Usuario usuario = AtualizarUsuario(usuarioCadastroVm);
                    _usuarios.Save(usuario);
                }

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        public void AtualizarUsuario(UsuarioCadastroVm usuarioVm, IList<Enumeradores.Perfil> perfis)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                Usuario usuario = AtualizarUsuario(usuarioVm);
                usuario.AlterarPerfis(perfis);

                _usuarios.Save(usuario);

                _unitOfWork.Commit();

            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}
