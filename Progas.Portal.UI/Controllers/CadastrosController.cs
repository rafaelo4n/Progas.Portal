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
    public class CadastrosController : Controller
    {
        private readonly IConsultaUnidadeDeMedida   _consultaUnidadeDeMedida;
        //private readonly IConsultaTipoPedido        _consultaTipoPedido;
        private readonly IConsultaCondicaoPagamento _consultaCondicaoPagamento;
        //private readonly IConsultaListaPreco        _consultaListaPreco;
        private readonly IConsultaMaterial          _consultaMaterial;
        //private readonly IConsultaIncoterm          _consultaIncoterm;
        private readonly IConsultaCliente           _consultaCliente;
        private readonly IConsultaFornecedor        _consultaFornecedor;
        //private readonly IConsultaPedidoVenda       _consultaPedidoVenda;

        
        // implementa os valores dos repositorios que serao usados nas views (Lista de valrores dos campos)
        public CadastrosController   (IConsultaUnidadeDeMedida   consultaUnidadeDeMedida, 
                                      IConsultaCondicaoPagamento consultaCondicaoPagamento,
                                      IConsultaMaterial          consultaMaterial,
                                      IConsultaCliente           consultaCliente,
                                      IConsultaFornecedor        consultaFornecedor
                                    )                
            {
                _consultaUnidadeDeMedida   = consultaUnidadeDeMedida;
                _consultaCondicaoPagamento = consultaCondicaoPagamento;
                _consultaMaterial          = consultaMaterial;               
                _consultaCliente           = consultaCliente;
                _consultaFornecedor        = consultaFornecedor;                
            }
        //
        // GET: /Cadastros/

        #region Consultar Material

        public ActionResult Index()
        {
            //return View();
            ViewBag.Materiais = _consultaMaterial.ListarTodas();
            return View("_Material");
        }      

        // Lista Material
        [HttpGet]
        public JsonResult ListarMaterial(PaginacaoVm paginacaoVm, MaterialFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaMaterial.Listar(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Consultar Condicao de Pagamento

        // Consultar Condicao de Pagamento        
        public ActionResult ConsultaCondicaoPagamento()
        {
            return View("_CondicaoPagamento");
        }

        //Listar Condicao de Pagamento 
        [HttpGet]
        public JsonResult ListarCondicaoPagamento(PaginacaoVm paginacaoVm, CondicaoPagamentoFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaCondicaoPagamento.Listar(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Consultar Cliente

        // Consultar Cliente
        public ActionResult ConsultaCliente()
        {
            return View("_Cliente");
        }

        // ListarCliente
        [HttpGet]
        public JsonResult ListarCliente(PaginacaoVm paginacaoVm, ClienteFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaCliente.Listar(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarClienteParaSelecao(PaginacaoVm paginacaoVm, ClienteFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaCliente.ListarParaSelecao(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Consultar Fornecedor

        // Consultar Fornecedor
        public ActionResult ConsultaFornecedor()
        {
            return View("_Fornecedor");
        }

        // Lista Fornecedor
        [HttpGet]
        public JsonResult ListarFornecedor(PaginacaoVm paginacaoVm, FornecedorFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaFornecedor.Listar(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Consultar Unidade Medida

        // Consulta Unidade Medida
        public ActionResult ConsultaUnidadeMedida()
        {
            return View("_UnidadeMedida");
        }

        // Lista Unidade Medida
        [HttpGet]
        public JsonResult ListarUnidadeMedida(PaginacaoVm paginacaoVm, UnidadeDeMedidaFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaUnidadeDeMedida.Listar(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
