using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;


namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class ListasPreco : CompleteRepositoryNh<ListaPreco>, IListasPreco
    {
        public ListasPreco(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public ListaPreco BuscaPeloCodigo(string codigoSap)
        {
            return Query.SingleOrDefault(x => x.Codigo == codigoSap);
        }

        public IListasPreco FiltraPelaDescricao(string descricao)
        {
            Query = Query.Where(x => x.Descricao.ToLower().Contains(descricao.ToLower()));
            return this;
        }

        public IListasPreco FiltraPorListaDeCodigos(string[] codigos)
        {
            Query = Query.Where(x => codigos.Contains(x.Codigo));
            return this;
        }

    }
}
