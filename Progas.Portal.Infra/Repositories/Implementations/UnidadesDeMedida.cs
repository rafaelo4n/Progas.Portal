using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class UnidadesDeMedida:CompleteRepositoryNh<UnidadeDeMedida>, IUnidadesDeMedida
    {
        public UnidadesDeMedida(IUnitOfWorkNh unitOfWork)
            : base(unitOfWork)
        {
        }

        public UnidadeDeMedida BuscaPeloCodigo(string codigoSap)
        {
            return Query.SingleOrDefault(x => x.Id_unidademedida == codigoSap);
        }

        public IUnidadesDeMedida FiltraPelaDescricao(string descricao)
        {
            Query = Query.Where(x => x.Descricao.ToLower().Contains(descricao.ToLower()));
            return this;
        }

        public IUnidadesDeMedida CodigoContendo(string filtroCodigo)
        {
            if (!string.IsNullOrEmpty(filtroCodigo))
            {
                Query = Query.Where(x => x.Id_unidademedida.ToLower().Contains(filtroCodigo.ToLower()));
            }

            return this;
        }

        public IUnidadesDeMedida NomeContendo(string filtroNome)
        {
            if (!string.IsNullOrEmpty(filtroNome))
            {
                Query = Query.Where(x => x.Descricao.ToLower().Contains(filtroNome.ToLower()));

            }
            return this;
        }

        public IUnidadesDeMedida BuscaPeloCodigoInterno(string codigoInterno)
        {
            Query = Query.Where(x => x.Id_unidademedida == codigoInterno);
            return this;
        }

        public IUnidadesDeMedida FiltraPorListaDeCodigosInternos(string[] codigos)
        {
            Query = Query.Where(x => codigos.Contains(x.Id_unidademedida));
            return this;
        }
    }
}
