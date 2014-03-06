using Progas.Portal.Infra.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Model;
using StructureMap;


namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class ClienteVendas : CompleteRepositoryNh<ClienteVenda>, IClienteVendas
    {
        public ClienteVendas(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
            Query = Query.OrderBy(x => x.Id_cliente);
        }

        public ClienteVenda ConsultaAtivDistribuicao(string cliente, string centro)
        {
            return Query.SingleOrDefault(x => x.Id_cliente == cliente && x.Org_vendas == centro);
        }

        public IClienteVendas DoCliente(string idDoCliente)
        {
            Query = Query.Where(x => x.Id_cliente == idDoCliente);
            return this;
        }

        public ClienteVenda ObterPorId(int idDaAreaDeVenda)
        {
            return Query.Single(x => x.Id == idDaAreaDeVenda);
        }
    }
}
