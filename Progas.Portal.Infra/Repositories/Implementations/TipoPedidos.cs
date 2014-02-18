using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;


namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class TipoPedidos : CompleteRepositoryNh<TipoPedido>, ITipoPedidos
    {
        public TipoPedidos(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public TipoPedido BuscaPeloCodigo(string codigoSap)
        {
            return Query.SingleOrDefault(x => x.Codigo == codigoSap);
        }

        public ITipoPedidos FiltraPelaDescricao(string descricao)
        {
            Query = Query.Where(x => x.Descricao.ToLower().Contains(descricao.ToLower()));
            return this;
        }

        public ITipoPedidos FiltraPorListaDeCodigos(string[] codigos)
        {
            Query = Query.Where(x => codigos.Contains(x.Codigo));
            return this;
        }
        
    }
}
