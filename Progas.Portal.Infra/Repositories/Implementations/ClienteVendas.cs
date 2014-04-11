using Progas.Portal.Infra.Repositories.Contracts;
using System.Linq;
using Progas.Portal.Domain.Entities;


namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class ClienteVendas : CompleteRepositoryNh<ClienteVenda>, IClienteVendas
    {
        public ClienteVendas(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
            Query = Query
                .Where(x  => x.Eliminacao == null || x.Eliminacao != "X")
                .OrderBy(x => x.Cliente.Id_cliente);
        }

        public ClienteVenda ConsultaAtivDistribuicao(string cliente, string centro)
        {
            return Query.SingleOrDefault(x => x.Cliente.Id_cliente == cliente && x.Org_vendas == centro);
        }

        public IClienteVendas DoCliente(string idDoCliente)
        {
            Query = Query.Where(x => x.Cliente.Id_cliente == idDoCliente);
            return this;
        }

        public ClienteVenda ObterPorId(int idDaAreaDeVenda)
        {
            return Query.Single(x => x.Id == idDaAreaDeVenda);
        }
    }
}
