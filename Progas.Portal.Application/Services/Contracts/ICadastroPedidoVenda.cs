using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Services.Contracts
{
    public interface ICadastroPedidoVenda
    {
        void Salvar(PedidoVendaSalvarVm pedido);
    }
}
