using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Domain.Services.Contracts
{
    public interface IAtualizadorDeItensDoPedidoDeVenda
    {
        void Atualizar(PedidoVenda pedidoVenda, PedidoVendaSalvarVm pedidoAlterado);
    }
}