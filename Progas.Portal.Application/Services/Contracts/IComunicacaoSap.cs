using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Application.Services.Contracts
{
    public interface IComunicacaoSap
    {
        void EnviarPedido(PedidoVenda pedidoVenda);
    }
}