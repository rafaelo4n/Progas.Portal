using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class CondicoesDePagamento: CompleteRepositoryNh<CondicaoDePagamento>, ICondicoesDePagamento
    {
        public CondicoesDePagamento(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
            Query = Query.OrderBy(x => x.Codigo);
        }

        public CondicaoDePagamento BuscaPeloCodigo(string codigoSap)
        {
            return Query.SingleOrDefault(x => x.Codigo == codigoSap);
        }

        public ICondicoesDePagamento FiltraPelaDescricao(string descricao)
        {
            Query = Query.Where(x => x.Descricao.ToLower().Contains(descricao.ToLower()));
            return this;
        }

        public ICondicoesDePagamento FiltraPorListaDeCodigos(string[] codigos)
        {
            Query = Query.Where(x => codigos.Contains(x.Codigo));
            return this;
        }

        public ICondicoesDePagamento CodigoContendo(string filtroCodigo)
        {
            if (!string.IsNullOrEmpty(filtroCodigo))
            {
                Query = Query.Where(x => x.Codigo.ToLower().Contains(filtroCodigo.ToLower()));
            }

            return this;
        }

        public ICondicoesDePagamento NomeContendo(string filtroNome)
        {
            if (!string.IsNullOrEmpty(filtroNome))
            {
                Query = Query.Where(x => x.Descricao.ToLower().Contains(filtroNome.ToLower()));

            }
            return this;
        }
    }
}
