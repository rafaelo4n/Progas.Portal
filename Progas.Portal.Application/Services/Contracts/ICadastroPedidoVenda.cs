using Progas.Portal.Domain.Entities;
using Progas.Portal.DTO;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Services.Contracts
{
    public interface ICadastroPedidoVenda
    {
        PedidoSapRetornoDTO Salvar(PedidoVendaSalvarVm pedido);
    }
}
