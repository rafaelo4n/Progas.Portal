using System.Collections.Generic;
using Progas.Portal.DTO;

namespace Progas.Portal.ViewModel
{
    public class PedidoVendaLinhaCadastroVm
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public int IdMaterial { get; set; }

        public string CodigoMaterial{ get; set; }

        public string DescricaoMaterial { get; set; }

        public decimal Quantidade { get; set; }

        public string CodigoUnidadeMedida { get; set; }

        public string CodigoListaPreco { get; set; }

        public string DescricaoListaPreco { get; set; }

        public decimal Desconto { get; set; }

        public decimal ValorTabela { get; set; }

        public decimal ValorPolitica { get; set; }

        public string CodigoDoMotivoDeRecusa { get; set; }

        public string DescricaoDoMotivoDeRecusa { get; set; }

        public IEnumerable<CondicaoDePrecoDTO> CondicoesDePreco { get; set; }
       
    }

    
}
