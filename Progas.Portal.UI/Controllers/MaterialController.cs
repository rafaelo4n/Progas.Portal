using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Common;
using Progas.Portal.Infra.Model;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;
using StructureMap;


namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class MaterialController : Controller
    {
        private readonly IConsultaMaterial _consultaMaterial;
        //
        // GET: /Material/

        public MaterialController(IConsultaMaterial consultaMaterial)
        {
            _consultaMaterial = consultaMaterial;
        }

        public ActionResult Index()
        {
            ViewBag.Materiais = _consultaMaterial.ListarTodas();
            return View("_Material");
        }

        [HttpGet]
        public JsonResult Listar(PaginacaoVm paginacaoVm, MaterialFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaMaterial.Listar(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

    }
}
