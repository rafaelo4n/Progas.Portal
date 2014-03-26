using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IMateriais : ICompleteRepository<Material>
    {
        Material   BuscaPeloCodigo(string codigoSap);
        IMateriais FiltraPelaDescricao(string descricao);
        IMateriais FiltraPorListaDeCodigos(string[] codigos);
        IMateriais CodigoContendo(string filtroCodigo);
        IMateriais NomeContendo(string filtroNome);
        IMateriais DoTipo(string tipo);
        IMateriais DoCentro(string centro);
        IMateriais BuscarLista(int[] idDosMateriais);
    }
}
