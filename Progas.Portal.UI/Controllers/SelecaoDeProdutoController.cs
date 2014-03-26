using System.Web.Mvc;
using Progas.Portal.UI.Filters;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class SelecaoDeProdutoController : Controller
    {
        public ActionResult Selecionar()
        {
            return PartialView("_SelecionarProduto");
        }

    }
}