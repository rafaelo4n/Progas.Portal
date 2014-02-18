using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IUnidadesDeMedida: ICompleteRepository<UnidadeDeMedida>
    {
        IUnidadesDeMedida BuscaPeloCodigoInterno(string codigoInterno);
        IUnidadesDeMedida FiltraPorListaDeCodigosInternos(string[] codigos);
        UnidadeDeMedida   BuscaPeloCodigo(string codigoSap);
        IUnidadesDeMedida FiltraPelaDescricao(string descricao);
        IUnidadesDeMedida CodigoContendo(string filtroCodigo);
        IUnidadesDeMedida NomeContendo(string filtroNome);
    }
}
