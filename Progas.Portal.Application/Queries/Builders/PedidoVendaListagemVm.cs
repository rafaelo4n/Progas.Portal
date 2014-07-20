using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Builders
{
    public class PedidoVendaListagemVm: ListagemVm
    {
        public string IdDaCotacao { get; set; }
        public string Status { get; set; }
        public string NumeroDoPedido { get; set; }
        public string NomeDoCliente { get; set; }
        public string DataDeCriacao { get; set; }
        public string DataDoPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public bool ExibirBotaoDeImpressao { get; set; }
    }
}
