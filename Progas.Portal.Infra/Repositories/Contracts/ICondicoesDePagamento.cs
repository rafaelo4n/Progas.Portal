using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface ICondicoesDePagamento: ICompleteRepository<CondicaoDePagamento>
    {
        CondicaoDePagamento   BuscaPeloCodigo(string codigoSap);
        ICondicoesDePagamento FiltraPelaDescricao(string descricao);
        ICondicoesDePagamento FiltraPorListaDeCodigos(string[] codigos);
        ICondicoesDePagamento CodigoContendo(string filtroCodigo);
        ICondicoesDePagamento NomeContendo(string filtroNome);
    }
}
