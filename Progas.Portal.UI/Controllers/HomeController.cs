using System;
using System.Web.Mvc;
using Progas.Portal.Infra.Builders;
using Progas.Portal.Infra.Model;
using Progas.Portal.UI.Filters;
using StructureMap;

namespace Progas.Portal.UI.Controllers
{
    public class HomeController : Controller
    {
        [SecurityFilter]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
            try
            {
                var usuarioConectado = ObjectFactory.GetInstance<UsuarioConectado>();
                ViewBag.UsuarioConectado = usuarioConectado;
                var menuUsuarioBuilder = new MenuUsuarioBuilder(usuarioConectado.Perfis);
                return View("_Menu", menuUsuarioBuilder.Construct());

            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
