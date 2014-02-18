using System;
using System.Linq;
using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;
using NHibernate.Criterion;
//using StructureMap;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaFornecedor: IConsultaFornecedor
    {
        private readonly IFornecedores _fornecedores;
        private readonly IBuilder<Fornecedor, FornecedorCadastroVm> _builderFornecedor;
        //private readonly IBuilder<Produto, ProdutoCadastroVm> _builderProduto;

        public ConsultaFornecedor(IFornecedores fornecedores, IBuilder<Fornecedor, FornecedorCadastroVm> builderFornecedor)
        {
            _builderFornecedor = builderFornecedor;
            //_builderProduto = builderProduto;
            _fornecedores = fornecedores;
        }       

        public KendoGridVm Listar(PaginacaoVm paginacaoVm, FornecedorFiltroVm filtro)
        {
            _fornecedores
                .CodigoContendo(filtro.Codigo)
                .NomeContendo(filtro.Nome);
            var kendoGridVmn = new KendoGridVm()
                {
                    QuantidadeDeRegistros = _fornecedores.Count(),
                    Registros =
                        _builderFornecedor.BuildList(_fornecedores.Skip(paginacaoVm.Skip).Take(paginacaoVm.Take).List())
                                .Cast<ListagemVm>()
                                .ToList()

                };
            return kendoGridVmn;
        }

        public IList<FornecedorCadastroVm> Listar(PaginacaoVm paginacaoVm, FornecedorCadastroVm filtro)
        {
            if (!string.IsNullOrEmpty(filtro.Codigo))
            {
                _fornecedores.BuscaPeloCodigo(filtro.Codigo);

            }

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                _fornecedores.FiltraPelaDescricao(filtro.Nome);
            }
            int skip = (paginacaoVm.Page - 1) * paginacaoVm.PageSize;

            //paginacaoVm.TotalRecords = _condicoesDePagamento.Count();

            return _builderFornecedor.BuildList(_fornecedores.Skip(skip).Take(paginacaoVm.Take).List());

        }

        public FornecedorCadastroVm ConsultaPorCodigo(string codigoDoFornecedor)
        {
            return _builderFornecedor.BuildSingle(_fornecedores.BuscaPeloCodigo(codigoDoFornecedor));
        }

        public string ConsultaPorCnpj(string cnpj)
        {
            _fornecedores.BuscaPeloCnpj(cnpj);
            return (from fornecedor in _fornecedores.GetQuery() select fornecedor.Nome).FirstOrDefault();
        }
    }
}
