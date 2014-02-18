using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface ITipoPedidos : ICompleteRepository<TipoPedido>
    {
        TipoPedido BuscaPeloCodigo(string codigoSap);
        ITipoPedidos FiltraPelaDescricao(string descricao);
        ITipoPedidos FiltraPorListaDeCodigos(string[] codigos);
    }
}
