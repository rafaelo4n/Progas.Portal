using Progas.Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Progas.Portal.Infra.Repositories.Contracts
{
    public interface IClienteVendas : ICompleteRepository<ClienteVenda>
    {
        ClienteVenda ConsultaAtivDistribuicao(string cliente, string centro);
        IClienteVendas DoCliente(string idDoCliente);
        IList<ClienteVenda> CarregarSempre(int idDaAreaDeVenda);
        ClienteVenda ObterPorId(int idDaAreaDeVenda);
    }
}
