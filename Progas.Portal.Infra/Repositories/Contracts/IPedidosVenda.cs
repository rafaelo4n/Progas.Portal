using Progas.Portal.Common;
using Progas.Portal.Domain.Entities;
using System;

namespace Progas.Portal.Infra.Repositories.Contracts
{
    /// <summary>
    /// interface utilizada para salvar os dados do Cabecalho de pedido no banco
    /// </summary>
    public interface IPedidosVenda: ICompleteRepository<PedidoVenda>
    {
        IPedidosVenda ClienteCodigoContendo  (string filtroCodigo);
        IPedidosVenda DataCriacaoContendo    (string filtroDataCriacao);
        IPedidosVenda PedidoCodigoContendo   (string filtroPedido);
        IPedidosVenda DataPedidoContendo     (string filtroDataPedido);
        PedidoVenda   CotacaoPedidoContendo  (string filtroCotacao, string representante);
        IPedidosVenda CotacaoRepresentante(string id_representante);    
        //IPedidosVenda MaterialCodigoContendo (string filtroMaterial);  
    }
}
