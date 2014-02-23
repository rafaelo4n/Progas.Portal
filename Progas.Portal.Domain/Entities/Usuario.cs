using System.Collections.Generic;
using System.Linq;
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
        public virtual Fornecedor Fornecedor { get; protected set; }
        public virtual Enumeradores.StatusUsuario Status { get; protected set; }
        public virtual IList< Enumeradores.Perfil> Perfis { get; set; }

        public Usuario(string nome, string login, string email, Fornecedor fornecedor)
            : this()
        {
            Nome = nome;
            Login = login;
            Email = email;
            Fornecedor = fornecedor;
            Status = Enumeradores.StatusUsuario.Ativo;
        }

        public virtual string CodigoDoFornecedor
        {
            get { return Fornecedor != null ? Fornecedor.Codigo : ""; }
        }

        protected Usuario()
        {
            Perfis = new List<Enumeradores.Perfil>();
        }


        public virtual void Alterar(string nome, string email, Fornecedor fornecedor)
        {
            Nome = nome;
            Email = email;
            Fornecedor = fornecedor;
        }

        public virtual void CriarSenha(string senhaCriptografada)
        {
            Senha = senhaCriptografada;
        }

        private void AdicionarPerfil(Enumeradores.Perfil perfil)
        {
            Perfis.Add(perfil);
            
        }
        private void RemoverPerfil(Enumeradores.Perfil perfil)
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

        public virtual void AlterarPerfis(IList<Enumeradores.Perfil> perfis )
        {
            if (perfis == null)
            {
                perfis = new List<Enumeradores.Perfil>();
            }
            IList<Enumeradores.Perfil> perfisRemovidos = Perfis.Except(perfis).ToList();
            IList<Enumeradores.Perfil> perfisParaAdicionar = perfis.Except(Perfis).ToList();
            foreach (var perfilRemovido in perfisRemovidos)
            {
                RemoverPerfil(perfilRemovido);
            }

            foreach (var perfilParaAdicionar in perfisParaAdicionar)
            {
                AdicionarPerfil(perfilParaAdicionar);
            }
            
        }
    }
}
