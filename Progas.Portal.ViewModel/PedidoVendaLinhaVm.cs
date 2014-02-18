using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progas.Portal.ViewModel
{
    /// <summary>
    /// classe utilizada para Salvar as Linhas do Pedido
    /// </summary>
    public class PedidoVendaLinhaVm
    {        
        public string id_material { get; set; }
        public decimal Quant { get; set; }
        public string CodigoUnidadeMedida { get; set; }
        public string listpre { get; set; }
        // Recebara o resultado da rfc
        public string valtab { get; set; }
        // Recebara o resultado da rfc
        public string valpol { get; set; }
        // Desconto Manual
        public string descma { get; set; }
        // Recebara o resultado da rfc
        public string valfin { get; set; }
        // Recebara o resultado da rfc
        public string ordem { get; set; }
    }
}
