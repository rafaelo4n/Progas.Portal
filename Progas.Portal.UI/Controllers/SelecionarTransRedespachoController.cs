using System.Web.Mvc;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class SelecionarTransRedespachoController : Controller
    {
        public ActionResult Selecionar(TransRedespachoCadastroVm fornecedorCadastroVm)
        {
            return PartialView("_SelecionarTransRedespacho", fornecedorCadastroVm);
        }
    }
}
