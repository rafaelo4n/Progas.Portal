using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class Materiais : CompleteRepositoryNh<Material>, IMateriais
    {
        public Materiais(IUnitOfWorkNh unitOfWork)
            : base(unitOfWork)
        {
            //Query = Query.OrderBy(x => x.Descricao);
        }

        public Material BuscaPeloCodigo(string codigoSap)
        {
            return Query.SingleOrDefault(x => x.Id_centro == codigoSap);
        }

        public IMateriais FiltraPelaDescricao(string descricao)
        {
            Query = Query.Where(x => x.Descricao.ToLower().Contains(descricao.ToLower()));
            return this;
        }


        public IMateriais FiltraPorListaDeCodigos(string[] codigos)
        {
            Query = Query.Where(x => codigos.Contains(x.Id_centro));
            return this;
        }

        public IMateriais CodigoContendo(string filtroCodigo)
        {
            if (!string.IsNullOrEmpty(filtroCodigo))
            {
                Query = Query.Where(x => x.Id_material.ToLower().Contains(filtroCodigo.ToLower()));
            }

            return this;
        }

        public IMateriais NomeContendo(string filtroNome)
        {
            if (!string.IsNullOrEmpty(filtroNome))
            {
                Query = Query.Where(x => x.Descricao.ToLower().Contains(filtroNome.ToLower()));

            }
            return this;
        }

    }
}
