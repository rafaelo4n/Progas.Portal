using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IFornecedores: ICompleteRepository<Fornecedor>
    {
        Fornecedor BuscaPeloCodigo(string codigo);
        IFornecedores BuscaPeloCnpj(string cnpj);
        IFornecedores BuscaListaPorCodigo(string[] codigoDosFornecedores);
        //IFornecedores FornecedoresNaoVinculadosAoProduto(string codigoProduto);
        IFornecedores NomeContendo(string filtroNome);
        IFornecedores CodigoContendo(string filtroCodigo);
        IFornecedores FiltraPelaDescricao(string descricao);
    }
}