﻿using System.Linq;
using System.Security.Policy;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Model;
using Progas.Portal.Infra.Repositories.Contracts;
using StructureMap;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class Clientes :CompleteRepositoryNh<Cliente>,  IClientes
    {
        public Clientes(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
            Query = Query.OrderBy(x => x.Nome);
        }

        public Cliente BuscaPeloCodigo(string codigoSap)
        {
            return Query.SingleOrDefault(x => x.Id_cliente == codigoSap);
        }

        public IClientes FiltraPelaDescricao(string descricao)
        {
            Query = Query.Where(x => x.Nome.ToLower().Contains(descricao.ToLower()));
            return this;
        }

        public IClientes CodigoContendo(string filtroCodigo)
        {
            if (!string.IsNullOrEmpty(filtroCodigo))
            {
                Query = Query.Where(x => x.Id_cliente.ToLower().Contains(filtroCodigo.ToLower()));
            }

            return this;
        }

        // Consulta Cliente pelo Id no repositório
        public Cliente ConsultaClientes(string id_cliente)
        {
            return Query.SingleOrDefault(x => x.Id_cliente == id_cliente);            
        }

        public IClientes NomeContendo(string filtroNome)
        {
            if (!string.IsNullOrEmpty(filtroNome))
            {
                Query = Query.Where(x => x.Nome.ToLower().Contains(filtroNome.ToLower()));

            }
            return this;
        }

        public IClientes ComCnpj(string cnpj)
        {
            if (!string.IsNullOrEmpty(cnpj))
            {
                Query = Query.Where(x => x.Cnpj == cnpj);
            }
            return this;

        }

        public IClientes MunicipioContendo(string municipio)
        {
            if (!string.IsNullOrEmpty(municipio))
            {
                Query = Query.Where(x => x.Municipio.ToLower().Contains(municipio.ToLower()));

            }
            return this;
        }

        public IClientes ComCpf(string cpf)
        {
            if (!string.IsNullOrEmpty(cpf))
            {
                Query = Query.Where(x => x.Cpf == cpf);
            }
            return this;
        }

    }
}
