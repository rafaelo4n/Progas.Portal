using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;
using StructureMap;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaCliente : IConsultaCliente
    {
        private readonly IClientes _clientes;
        private readonly IUsuarios _usuarios;
        private readonly IUnitOfWorkNh _unitOfWorkNh;
        private readonly IBuilder<Cliente, ClienteCadastroVm> _builderCliente;

        public ConsultaCliente(IClientes clientes, IBuilder<Cliente, ClienteCadastroVm> builder, IUsuarios usuarios, IUnitOfWorkNh unitOfWorkNh)
        {
            _clientes = clientes;
            _builderCliente = builder;
            _usuarios = usuarios;
            _unitOfWorkNh = unitOfWorkNh;
        }

        public KendoGridVm Listar(PaginacaoVm paginacaoVm, ClienteFiltroVm filtro)
        {
            _clientes
                .CodigoContendo(filtro.Codigo)
                .NomeContendo(filtro.Nome)
                .ComCnpj(filtro.Cnpj)
                .ComCpf(filtro.Cpf)
                .MunicipioContendo(filtro.Municipio);

            var kendoGridVmn = new KendoGridVm()
            {
                QuantidadeDeRegistros = _clientes.Count(),
                Registros =
                    _builderCliente.BuildList(_clientes.Skip(paginacaoVm.Skip)
                    .Take(paginacaoVm.Take).List())
                            .Cast<ListagemVm>()
                            .ToList()

            };
            return kendoGridVmn;
        }

        public KendoGridVm ListarParaSelecao(PaginacaoVm paginacaoVm, ClienteFiltroVm filtro)
        {
            Usuario usuarioConectado = _usuarios.UsuarioConectado();
            var unitOfWorkNh = ObjectFactory.GetInstance<IUnitOfWorkNh>();

            ClienteVenda areaDeVenda = null;
            Cliente cliente = null;

            ClienteParaSelecaoVm clienteVm = null;

            IQueryOver<Cliente, Cliente> queryOver = unitOfWorkNh.Session.QueryOver<Cliente>(() => cliente);

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                queryOver = queryOver.Where(x => x.Nome.IsInsensitiveLike(filtro.Nome, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(filtro.Cnpj))
            {
                queryOver = queryOver.Where(x => x.Cnpj.IsInsensitiveLike(filtro.Cnpj, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(filtro.Cpf))
            {
                queryOver = queryOver.Where(x => x.Cpf.IsInsensitiveLike(filtro.Cpf,MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(filtro.Codigo))
            {
                queryOver = queryOver.Where(x => x.Id_cliente.IsInsensitiveLike(filtro.Codigo, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(filtro.Municipio))
            {
                queryOver = queryOver.Where(x => x.Municipio.IsInsensitiveLike(filtro.Municipio, MatchMode.Anywhere));
            }

            QueryOver<ClienteVenda, ClienteVenda> subQuery = QueryOver
                .Of<ClienteVenda>(() => areaDeVenda)
                .Where(Restrictions.EqProperty(Projections.Property(() => areaDeVenda.Cliente.Id_cliente),
                    Projections.Property(() => cliente.Id_cliente)))
                .Where(() => areaDeVenda.Fornecedor.Codigo == usuarioConectado.CodigoDoFornecedor)
                .Select(Projections.Property(() => areaDeVenda.Id));

            queryOver.WithSubquery.WhereExists(subQuery);

            queryOver.SelectList(lista => lista
                .Select(x => cliente.Id_cliente).WithAlias(() => clienteVm.Codigo)
                .Select(x => cliente.Nome).WithAlias(() => clienteVm.Nome)
                .Select(x => cliente.Cnpj).WithAlias(() => clienteVm.Cnpj)
                .Select(x => cliente.Cpf).WithAlias(() => clienteVm.Cpf)
                .Select(x => cliente.Municipio).WithAlias(() => clienteVm.Municipio)
                .Select(x => cliente.Uf).WithAlias(() => clienteVm.Uf)
                .Select(x => cliente.Tel_res).WithAlias(() => clienteVm.Telefone)
                );

            var kendoGridVm = new KendoGridVm()
            {
                QuantidadeDeRegistros = queryOver.RowCount(),
                Registros = queryOver
                    .TransformUsing(Transformers.AliasToBean<ClienteParaSelecaoVm>())
                    .Skip(paginacaoVm.Skip)
                    .Take(paginacaoVm.Take)
                    .List<ClienteParaSelecaoVm>()
                    .Cast<ListagemVm>()
                    .ToList()


            };

            return kendoGridVm;
        }

        public int ConsultaDeTeste()
        {
            Usuario usuarioConectado = _usuarios.UsuarioConectado();
            var unitOfWorkNh = ObjectFactory.GetInstance<IUnitOfWorkNh>();

            ClienteVenda areaDeVenda = null;
            Cliente cliente = null;

            QueryOver<ClienteVenda, ClienteVenda> subQuery = QueryOver
                .Of<ClienteVenda>(() => areaDeVenda)
                .Where(Restrictions.EqProperty(Projections.Property(() => areaDeVenda.Cliente.Id_cliente),
                    Projections.Property(() => cliente.Id_cliente)))
                .Where(() => areaDeVenda.Fornecedor.Codigo == usuarioConectado.CodigoDoFornecedor)
                .Select(Projections.Property(() => areaDeVenda.Id));

            IQueryOver<Cliente,Cliente> queryOver = unitOfWorkNh.Session.QueryOver<Cliente>(() => cliente);
            queryOver.WithSubquery.WhereExists(subQuery);

            return queryOver.RowCount();

        }

        public IList<ClienteCadastroVm> Listar(PaginacaoVm paginacaoVm, ClienteCadastroVm filtro)
        {
            if (!string.IsNullOrEmpty(filtro.id_cliente))
            {
                _clientes.BuscaPeloCodigo(filtro.id_cliente);

            }

            if (!string.IsNullOrEmpty(filtro.nome))
            {
                _clientes.FiltraPelaDescricao(filtro.nome);
            }
            int skip = (paginacaoVm.Page - 1) * paginacaoVm.PageSize;

            return _builderCliente.BuildList(_clientes.Skip(skip).Take(paginacaoVm.Take).List());

        }


        public IList<ClienteCadastroVm> Listar()
        {
            return _builderCliente.BuildList(_clientes.List());

        }

    }
}
