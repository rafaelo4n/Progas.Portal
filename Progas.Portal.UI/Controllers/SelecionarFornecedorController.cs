using System.Web.Mvc;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class SelecionarFornecedorController : Controller
    {
        public ActionResult Selecionar(FornecedorCadastroVm fornecedorCadastroVm)
        {
            return PartialView("_SelecionarFornecedor",fornecedorCadastroVm);            
        }

    }
}
