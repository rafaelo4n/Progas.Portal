using System.Collections.Generic;

namespace Progas.Portal.ViewModel
{
    public class KendoGridVm
    {
        public int QuantidadeDeRegistros   { get; set; }        
        public IList<ListagemVm> Registros { get; set; }
    }
}
