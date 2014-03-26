using System.Web.Mvc;

namespace Progas.Portal.UI.Controllers
{
    public class SelecionarClienteController : Controller
    {
        //
        // GET: /SelecionarCliente/
        [HttpGet]
        public ActionResult Selecionar()
        {
            return PartialView("_SelecionarCliente");
        }

    }
}
