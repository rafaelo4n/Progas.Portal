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
        private readonly IBuilder<Fornecedor, FornecedorCadastroVm> _builderFornecedor;

        public ConsultaFornecedor(IFornecedores fornecedores, IBuilder<Fornecedor, FornecedorCadastroVm> builderFornecedor, IClienteVendas clienteVendas)
        {
            _builderFornecedor = builderFornecedor;
            _clienteVendas = clienteVendas;
            _fornecedores = fornecedores;
        }       

        public KendoGridVm Listar(PaginacaoVm paginacaoVm, FornecedorFiltroVm filtro)
        {
            var unitOfWorkNh = ObjectFactory.GetInstance<IUnitOfWorkNh>();

            Fornecedor fornecedor = null;
            IQueryOver<Fornecedor,Fornecedor> queryOver = unitOfWorkNh.Session.QueryOver(() => fornecedor)
                .Where(f => f.Eliminacao == null || f.Eliminacao != "X");

            if (filtro.SomenteTransportadora)
            {
                queryOver = queryOver.And(f => f.Grupo_contas == "ZTRA");
            }

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

        public FornecedorCadastroVm ConsultaPorCodigo(string codigoDoFornecedor)
        {
            return _builderFornecedor.BuildSingle(_fornecedores.BuscaPeloCodigo(codigoDoFornecedor));
        }

    }
}
