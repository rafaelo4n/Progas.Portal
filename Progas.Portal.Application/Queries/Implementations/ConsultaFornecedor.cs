using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;
using StructureMap;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaFornecedor: IConsultaFornecedor
    {
        private readonly IFornecedores _fornecedores;
        private readonly IClienteVendas _clienteVendas;
        private readonly IUsuarios _usuarios;
        private readonly IBuilder<Fornecedor, FornecedorCadastroVm> _builderFornecedor;

        public ConsultaFornecedor(IFornecedores fornecedores, IBuilder<Fornecedor, FornecedorCadastroVm> builderFornecedor, IClienteVendas clienteVendas, IUsuarios usuarios)
        {
            _builderFornecedor = builderFornecedor;
            _clienteVendas = clienteVendas;
            _usuarios = usuarios;
            _fornecedores = fornecedores;
        }

        private IQueryOver<Fornecedor, Fornecedor> AplicarFiltros(FornecedorFiltroVm filtro)
        {
            var unitOfWorkNh = ObjectFactory.GetInstance<IUnitOfWorkNh>();

            Fornecedor fornecedor = null;
            IQueryOver<Fornecedor, Fornecedor> queryOver = unitOfWorkNh.Session.QueryOver(() => fornecedor)
                .Where(f => f.Eliminacao == null || f.Eliminacao != "X");

            if (!string.IsNullOrEmpty(filtro.Codigo))
            {
                queryOver = queryOver.And(f => f.Codigo.IsInsensitiveLike(filtro.Codigo, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                queryOver = queryOver.And(f => f.Nome.IsInsensitiveLike(filtro.Nome, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(filtro.Cnpj))
            {
                queryOver = queryOver.And(f => f.Cnpj == filtro.Cnpj);
            }

            if (!string.IsNullOrEmpty(filtro.Cpf))
            {
                queryOver = queryOver.And(f => f.Cpf == filtro.Cpf);
            }

            if (filtro.IdDaAreaDeVenda.HasValue)
            {
                ClienteVenda areaDeVenda = _clienteVendas.ObterPorId(filtro.IdDaAreaDeVenda.Value);
                FornecedorDaEmpresa fornecedorDaEmpresa = null;

                QueryOver<FornecedorDaEmpresa, FornecedorDaEmpresa> subQuery = QueryOver.Of(() => fornecedorDaEmpresa)
                    .Where(Restrictions.EqProperty(Projections.Property(() => fornecedor.Codigo), Projections.Property(() => fornecedorDaEmpresa.Fornecedor.Codigo)))
                    .And(x => x.Empresa == areaDeVenda.Org_vendas)
                    .Select(Projections.Property(() => fornecedorDaEmpresa.Empresa));

                queryOver.WithSubquery.WhereExists(subQuery);
            }

            return queryOver;
        }

        public KendoGridVm Listar(PaginacaoVm paginacaoVm, FornecedorFiltroVm filtro)
        {
            IQueryOver<Fornecedor, Fornecedor> queryOver = AplicarFiltros(filtro);

            var kendoGridVm = new KendoGridVm
            {
                QuantidadeDeRegistros = queryOver.RowCount(),
                Registros =
                    _builderFornecedor.BuildList(queryOver.Skip(paginacaoVm.Skip).Take(paginacaoVm.Take).List())
                        .Cast<ListagemVm>()
                        .ToList()

            };
            return kendoGridVm;
        }

        public KendoGridVm ListarTransportadoras(PaginacaoVm paginacaoVm, TransportadoraFiltroVm filtro)
        {
            IQueryOver<Fornecedor, Fornecedor> queryOverBase = AplicarFiltros(filtro);
            queryOverBase = queryOverBase.And(f => f.Grupo_contas == "ZTRA");

            Fornecedor fornecedor = null;

            TransportadoraDoCliente transportadoraDoCliente = null;

            QueryOver<TransportadoraDoCliente, TransportadoraDoCliente> subQueryCliente = QueryOver.Of(() => transportadoraDoCliente)
                .Where(Restrictions.EqProperty(Projections.Property(() => fornecedor.Codigo), Projections.Property(() => transportadoraDoCliente.Transportadora.Codigo)))
                .And(x => x.TipoDeParceiro == filtro.TipoDeParceiro)
                .And(x => x.Cliente.Id_cliente == filtro.IdDoCliente)
                .Select(Projections.Property(() => transportadoraDoCliente.TipoDeParceiro));

            var queryCliente =  queryOverBase.Clone().WithSubquery.WhereExists(subQueryCliente);

            var kendoGridVm = new KendoGridVm
            {
                QuantidadeDeRegistros = queryCliente.RowCount(),
                Registros =
                    _builderFornecedor.BuildList(queryCliente.Skip(paginacaoVm.Skip).Take(paginacaoVm.Take).List())
                        .Cast<ListagemVm>()
                        .ToList()

            };

            if (kendoGridVm.QuantidadeDeRegistros > 0)
            {
                return kendoGridVm;
            }

            Usuario usuarioConectado = _usuarios.UsuarioConectado();

            if (!string.IsNullOrEmpty(usuarioConectado.CodigoDoFornecedor))
            {

                TransportadoraDoRepresentante transportadoraDoRepresentante = null;

                QueryOver<TransportadoraDoRepresentante, TransportadoraDoRepresentante> subQuery = QueryOver.Of(() => transportadoraDoRepresentante)
                    .Where(Restrictions.EqProperty(Projections.Property(() => fornecedor.Codigo), Projections.Property(() => transportadoraDoRepresentante.Transportadora.Codigo)))
                    .And(x => x.TipoDeParceiro == filtro.TipoDeParceiro)
                    .And(x => x.Representante.Codigo == usuarioConectado.CodigoDoFornecedor)
                    .Select(Projections.Property(() => transportadoraDoRepresentante.TipoDeParceiro));

                var queryOverRepresentante = queryOverBase.Clone().WithSubquery.WhereExists(subQuery);

                kendoGridVm = new KendoGridVm
                {
                    QuantidadeDeRegistros = queryOverRepresentante.RowCount(),
                    Registros =
                        _builderFornecedor.BuildList(queryOverRepresentante.Skip(paginacaoVm.Skip).Take(paginacaoVm.Take).List())
                            .Cast<ListagemVm>()
                            .ToList()

                };

                if (kendoGridVm.QuantidadeDeRegistros > 0)
                {
                    return kendoGridVm;
                }


            }

            kendoGridVm = new KendoGridVm
            {
                QuantidadeDeRegistros = queryOverBase.RowCount(),
                Registros =
                    _builderFornecedor.BuildList(queryOverBase.Skip(paginacaoVm.Skip).Take(paginacaoVm.Take).List())
                        .Cast<ListagemVm>()
                        .ToList()

            };

            return kendoGridVm;

        }

        public FornecedorCadastroVm ConsultaPorCodigo(string codigoDoFornecedor)
        {
            return _builderFornecedor.BuildSingle(_fornecedores.BuscaPeloCodigo(codigoDoFornecedor));
        }

    }
}
