using System.Collections.Generic;
using Progas.Portal.Common;
using Progas.Portal.Common.Exceptions;

namespace Progas.Portal.Domain.Entities
{
    public class Usuario : IAggregateRoot
    {
        public virtual string Nome { get; protected set; }
        public virtual string Login { get; protected set; }
        public virtual string Senha { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string CodigoFornecedor { get; protected set; }
        public virtual Enumeradores.StatusUsuario Status { get; protected set; }
        public virtual IList< Enumeradores.Perfil> Perfis { get; set; }

        public Usuario(string nome, string login, string email, string codigoFornecedor)
            : this()
        {
            Nome = nome;
            Login = login;
            Email = email;
            CodigoFornecedor = codigoFornecedor;
            Status = Enumeradores.StatusUsuario.Ativo;
        }

        protected Usuario()
        {
            Perfis = new List<Enumeradores.Perfil>();
        }


        public virtual void Alterar(string nome, string email, string codigoFornecedor)
        {
            Nome = nome;
            Email = email;
            CodigoFornecedor = codigoFornecedor;
        }

        public virtual void CriarSenha(string senhaCriptografada)
        {
            Senha = senhaCriptografada;
        }

        public virtual void AdicionarPerfil(Enumeradores.Perfil perfil)
        {
            Perfis.Add(perfil);
            
        }
        public virtual void RemoverPerfil(Enumeradores.Perfil perfil)
        {
            Perfis.Remove(perfil);

        }

        public virtual void Bloquear()
        {
            Status = Enumeradores.StatusUsuario.Bloqueado;
        }

        public virtual void Ativar()
        {
            Status = Enumeradores.StatusUsuario.Ativo;
        }

        public virtual void AlterarSenha(string senhaAtualCriptografada, string senhaNovaCriptografada)
        {
            if (senhaAtualCriptografada != Senha)
            {
                throw new SenhaIncorretaException("A senha atual informada está incorreta");
            }
            Senha = senhaNovaCriptografada;
        }
    }
}
