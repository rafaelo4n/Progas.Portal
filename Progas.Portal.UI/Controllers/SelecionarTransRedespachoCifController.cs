using System.Web.Mvc;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class SelecionarTransRedespachoCifController : Controller
    {
        public ActionResult Selecionar(TransRedespachoCifCadastroVm fornecedorCadastroVm)
        {
            return PartialView("_SelecionarTransRedespacho", fornecedorCadastroVm);
        }
    }
}
