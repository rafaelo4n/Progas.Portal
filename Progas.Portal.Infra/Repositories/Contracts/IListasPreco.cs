using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IListasPreco : ICompleteRepository<ListaPreco>
    {
        ListaPreco BuscaPeloCodigo(string codigoSap);
        IListasPreco FiltraPelaDescricao(string descricao);
        IListasPreco FiltraPorListaDeCodigos(string[] codigos);
    }
}
