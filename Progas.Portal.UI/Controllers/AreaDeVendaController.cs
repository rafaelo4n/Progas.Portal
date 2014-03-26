using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class AreaDeVendaController : Controller
    {
        private readonly IConsultaAreasDeVenda _consultaAreasDeVenda;

        public AreaDeVendaController(IConsultaAreasDeVenda consultaAreasDeVenda)
        {
            _consultaAreasDeVenda = consultaAreasDeVenda;
        }

        [HttpGet]
        public ActionResult ListarPorCliente(string idDoCliente)
        {
            try
            {
                IList<AreaDeVendaVm> areasDeVenda = _consultaAreasDeVenda.ListarPorCliente(idDoCliente);
                return Json(new{Sucesso = true, areasDeVenda}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {Sucesso = false, Mensagem = ex.Message}, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}
