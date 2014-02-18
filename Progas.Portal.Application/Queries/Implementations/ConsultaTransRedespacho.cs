using System.Linq;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    //class ConsultaTransRedespacho
    public class ConsultaTransRedespacho : IConsultaTransRedespacho
    {
        private readonly IFornecedores _fornecedores;
        private readonly IBuilder<Fornecedor, TransRedespachoCadastroVm> _builderFornecedor;

        public ConsultaTransRedespacho(IFornecedores fornecedores, IBuilder<Fornecedor, TransRedespachoCadastroVm> builderFornecedor)
        {
            _builderFornecedor = builderFornecedor;            
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

        public TransRedespachoCadastroVm ConsultaPorCodigo(string codigoDoFornecedor)
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
