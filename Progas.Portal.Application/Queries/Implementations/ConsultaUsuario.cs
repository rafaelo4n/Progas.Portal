using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Common;
using Progas.Portal.Domain.Entities;
using Progas.Portal.DTO;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaUsuario : IConsultaUsuario
    {
        private readonly IUsuarios _usuarios;
        private readonly IBuilder<Usuario, UsuarioConsultaVm> _builderUsuario;
        private readonly IBuilder<Enumeradores.Perfil, PerfilVm> _builderPerfil;
        public ConsultaUsuario(IUsuarios usuarios, IBuilder<Usuario, UsuarioConsultaVm> builderUsuario, IBuilder<Enumeradores.Perfil, PerfilVm> builderPerfil)
        {
            _usuarios = usuarios;
            _builderUsuario = builderUsuario;
            _builderPerfil = builderPerfil;
        }

        public KendoGridVm Listar(PaginacaoVm paginacaoVm, UsuarioFiltroVm usuarioFiltroVm)
        {
            _usuarios
                .LoginContendo(usuarioFiltroVm.Login)
                .NomeContendo(usuarioFiltroVm.Nome)
                .OrdenarPorNome();
            
            var kendoGridVmn = new KendoGridVm()
            {
                QuantidadeDeRegistros = _usuarios.Count(),
                Registros =
                    _builderUsuario.BuildList(_usuarios.Skip(paginacaoVm.Skip).Take(paginacaoVm.Take).List())
                            .Cast<ListagemVm>()
                            .ToList()

            };
            return kendoGridVmn;

        }

        public UsuarioConsultaVm ConsultaPorLogin(string login)
        {
            return _builderUsuario.BuildSingle(_usuarios.BuscaPorLogin(login));
        }

        public IList<PerfilVm> PerfisDoUsuario(string login)
        {
            return _builderPerfil.BuildList(_usuarios.BuscaPorLogin(login).Perfis);
        }

        public string ConfirmaLogin(string login)
        {

            var usuario = _usuarios.BuscaPorLogin(login);

            return usuario != null ? login : null;

        }

        public RepresentanteDTO RepresentanteDoUsuarioLogado()
        {
            Usuario usuarioConectado = _usuarios.UsuarioConectado();

            if (usuarioConectado.Fornecedor == null)
            {
                return null;
            }
            return new RepresentanteDTO
            {
                Codigo = usuarioConectado.Fornecedor.Codigo,
                Nome = usuarioConectado.Fornecedor.Nome
            };
             
        }
    }
}