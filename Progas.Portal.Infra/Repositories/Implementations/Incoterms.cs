using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;


namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class Incoterms : CompleteRepositoryNh<Incoterm>, IIncoterms
    {
        public Incoterms(IUnitOfWorkNh unitOfWork)
            : base(unitOfWork)
        {
        }

        public Incoterm BuscaPeloCodigo(string codigoSap)
        {
            return Query.SingleOrDefault(x => x.CodigoIncoterm == codigoSap);
        }

        public IIncoterms FiltraPelaDescricao(string descricao)
        {
            Query = Query.Where(x => x.Descricao.ToLower().Contains(descricao.ToLower()));
            return this;
        }

        public IIncoterms FiltraPorListaDeCodigos(string[] codigos)
        {
            Query = Query.Where(x => codigos.Contains(x.CodigoIncoterm));
            return this;
        }

    }
}
