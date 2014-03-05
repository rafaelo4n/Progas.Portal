using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class IncotermController : Controller
    {
        private readonly IConsultaIncotermLinhas _consultaIncotermLinhas;

        public IncotermController(IConsultaIncotermLinhas consultaIncotermLinhas)
        {
            _consultaIncotermLinhas = consultaIncotermLinhas;
        }

        [HttpGet]
        public ActionResult ListarIncotermsDoCabecalho(string codigoDoCabecalho)
        {
            try
            {
                IList<IncotermLinhasCadastroVm> incoterms = _consultaIncotermLinhas.ListarPorCabecalho(codigoDoCabecalho);

                return Json(new { Sucesso = true, incoterms }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new {Sucesso = false, Mensagem = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
