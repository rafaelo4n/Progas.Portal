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
            Query = Query.Where(f => (f.Codigo_eliminacao == null || !f.Codigo_eliminacao.Equals("X")));
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

        public IFornecedores ComCnpj(string cnpj)
        {
            if (!string.IsNullOrEmpty(cnpj))
            {
                Query = Query.Where(x => x.Cnpj == cnpj);
            }
            return this;

        }

        public IFornecedores ComCpf(string cpf)
        {
            if (!string.IsNullOrEmpty(cpf))
            {
                Query = Query.Where(x => x.Cpf == cpf);
            }
            return this;
        }

        public IFornecedores SomenteTransportadora()
        {
            Query = Query.Where(f => f.Grupo_contas == "ZTRA");
            return this;
        }


        public IFornecedores MunicipioContendo(string municipio)
        {
            if (!string.IsNullOrEmpty(municipio))
            {
                Query = Query.Where(x => x.Municipio.ToLower().Contains(municipio.ToLower()));

            }
            return this;
        }

    }
}
