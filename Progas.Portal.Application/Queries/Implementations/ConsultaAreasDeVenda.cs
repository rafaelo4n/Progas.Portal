using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaAreasDeVenda : IConsultaAreasDeVenda
    {

        private readonly IClienteVendas _clienteVendas;

        public ConsultaAreasDeVenda(IClienteVendas clienteVendas)
        {
            _clienteVendas = clienteVendas;
        }

        public IList<AreaDeVendaVm> ListarPorCliente(string idDoCliente)
        {
            IQueryable<ClienteVenda> queryable = _clienteVendas
                .DoCliente(idDoCliente)
                .GetQuery();

            return queryable.Select(c => new AreaDeVendaVm
            {
                Id = c.Id,
                Descricao = string.Format("{0}-{1}-{2}",c.Org_vendas,c.Can_dist,c.Set_ativ)
            }).ToList();

        }
    }
}