namespace Progas.Portal.ViewModel
{
    public class PedidoVendaSalvarItemVm
    {
        public int IdDoItem { get; set; }
        public int IdMaterial { get; set; }
        public decimal Quantidade { get; set; }
        public string CodigoDaListaDePreco { get; set; }
        public decimal Desconto { get; set; }
        public string CodigoDoMotivoDeRecusa { get; set; }
    }
}
