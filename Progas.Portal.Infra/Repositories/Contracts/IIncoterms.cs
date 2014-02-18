using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IIncoterms : ICompleteRepository<Incoterm>
    {
        Incoterm   BuscaPeloCodigo(string codigoSap);
        IIncoterms FiltraPelaDescricao(string descricao);
        IIncoterms FiltraPorListaDeCodigos(string[] codigos);
    }
}
