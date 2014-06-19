using System.Text;

namespace Progas.Portal.ViewModel
{
    public class PedidoVendaFiltroVm
    {
        public string CodigoDoCliente { get; set; }
        public string CodigoDoRepresentante { get; set; }
        public string datacp { get; set; }
        public string id_pedido  {   get; set; }
        public string datap { get; set; }
        public int? IdDoMaterial   { get; set; }
        public string Status { get; set; }
    }
}
