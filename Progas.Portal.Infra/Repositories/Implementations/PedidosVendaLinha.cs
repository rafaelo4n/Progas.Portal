using System.Linq;
using Progas.Portal.Common;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class PedidosVendaLinha : CompleteRepositoryNh<PedidoVendaLinha>, IPedidosVendaLinha
    {
        public PedidosVendaLinha(IUnitOfWorkNh unitOfWork): base(unitOfWork)
        {
        }

        public IPedidosVendaLinha CotacaoPedidoContendo(string filtroCotacao)
        {
            //Query = Query.Where(x => x.Id_cotacao == filtroCotacao);
            return this;
        }


    }
}
