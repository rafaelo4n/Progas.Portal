namespace Progas.Portal.ViewModel
{
    // View que recebera os valores da pagina que serão utilizados no CRUD
    public class PedidoVendaSalvarItemVm
    {
        public string CodigoMaterial { get; set; }
        public decimal Quantidade { get; set; }
        public string CodigoDaListaDePreco { get; set; }
        public decimal Desconto { get; set; }
        public string CodigoDoMotivoDeRecusa { get; set; }
    }
}
