using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class Fornecedores:CompleteRepositoryNh<Fornecedor>,  IFornecedores
    {
        public Fornecedores(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public Fornecedor BuscaPeloCodigo(string codigoSap)
        {
            return Query.SingleOrDefault(x => x.Codigo == codigoSap);
        }

        public IFornecedores BuscaPeloCnpj(string cnpj)
        {
            Query = Query.Where(x => x.Cnpj == cnpj);
            return this;
        }

        public IFornecedores BuscaListaPorCodigo(string[] codigoDosFornecedores)
        {
            Query = Query.Where(x => codigoDosFornecedores.Contains(x.Codigo));
            return this;
        }

        public IFornecedores NomeContendo(string filtroNome)
        {
            if (!string.IsNullOrEmpty(filtroNome))
            {
                Query = Query.Where(x => x.Nome.ToLower().Contains(filtroNome.ToLower()));
                
            }
            return this;
        }

        public IFornecedores CodigoContendo(string filtroCodigo)
        {
            if (!string.IsNullOrEmpty(filtroCodigo))
            {
                Query = Query.Where(x => x.Codigo.ToLower().Contains(filtroCodigo.ToLower()));
            }

            return this;
        }

        public IFornecedores FiltraPelaDescricao(string descricao)
        {
            Query = Query.Where(x => x.Nome.ToLower().Contains(descricao.ToLower()));
            return this;
        }

        public IFornecedores BuscaListaPorIds(IList<int> ids)
        {
            Query = Query.Where(x => ids.Contains(x.Id));
            return this;
        }
    }
}
