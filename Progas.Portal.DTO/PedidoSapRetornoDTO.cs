﻿using System.Collections.Generic;

namespace Progas.Portal.DTO
{

    public class PedidoSapRetornoDTO
    {
        public string IdDoPedido { get; set; }
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }
        public IList<PedidoSapItemRetornoDTO> Itens { get; set; }
    }

    public class PedidoSapItemRetornoDTO
    {
        public int Id { get; set; }
        public string NumeroDoItem { get; set; }
        public decimal ValorDeTabela { get; set; }
        public decimal ValorPolitica { get; set; }
        public string CodigoDoMotivoDeRecusa { get; set; }
        public string DescricaoDoMotivoDeRecusa { get; set; }
        public IList<CondicaoDePrecoDTO> CondicoesDePreco { get; set; }
    }
}
