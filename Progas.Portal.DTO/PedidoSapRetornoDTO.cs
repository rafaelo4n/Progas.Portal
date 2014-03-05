using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.DTO
{
    public class PedidoSapRetornoDTO
    {
        List<PedidoSapItemRetornoDTO> ItensDoPedido { get; set; }
    }

    public class PedidoSapCondicaoRetornoDTO
    {
    }

    public class PedidoSapItemRetornoDTO
    {
        public int NumeroDoItem { get; set; }
        public string Status { get; set; }
        public decimal ValorDeTabela { get; set; }
        public decimal ValorPolitica { get; set; }
        List<PedidoSapCondicaoRetornoDTO> CondicoesDePreco { get; set; }
    }
}
