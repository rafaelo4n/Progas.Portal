using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IConsultaCliente _consultaCliente;

        public ClienteController(IConsultaCliente consultaCliente)
        {
            _consultaCliente = consultaCliente;
        }

        [HttpGet]
        public ActionResult ListarCondicoesDePagamento(string idDoCliente,string  codigoDaCondicaoDePagamento)
        {
            try
            {
                IList<CondicaoDePagamentoCadastroVm> condicoesDePagamento = _consultaCliente.ListarCondicoesDePagamento(idDoCliente,codigoDaCondicaoDePagamento);
                return Json(new { Sucesso = true, condicoesDePagamento}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {Sucesso = false, Mensagem = ex.Message}, JsonRequestBehavior.AllowGet);

            }
            
        }
    }
}