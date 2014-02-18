using System.Web.Mvc;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class SelecionarRepresentanteController : Controller
    {
        public ActionResult Selecionar(RepresentanteCadastroVm fornecedorCadastroVm)
        {
            return PartialView("_SelecionarRepresentante", fornecedorCadastroVm);
        }

    }
}
