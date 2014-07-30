using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.DTO;
using Progas.Portal.UI.App_Start;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class PedidoVendaController : Controller
    {
        #region Repositorios

        private readonly IConsultaTipoPedido _consultaTipoPedido;
        private readonly IConsultaListaPreco _consultaListaPreco;
        private readonly IConsultaMaterial _consultaMaterial;
        private readonly IConsultaIncotermCab _consultaIncotermCab;
        private readonly IConsultaPedidoVenda _consultaPedidoVenda;
        private readonly IConsultaStatusDoPedidoDeVenda _consultaStatusDoPedidoDeVenda;
        private readonly IConsultaMotivoDeRecusa _consultaMotivoDeRecusa;
        private readonly IConsultaUsuario _consultaUsuario;

        // implementa os valores dos repositorios que serao usados nas views (Lista de valrores dos campos)
        public PedidoVendaController(IConsultaTipoPedido consultaTipoPedido,
            IConsultaListaPreco consultaListaPreco,
            IConsultaMaterial consultaMaterial,
            IConsultaIncotermCab consultaIncotermCab,
            IConsultaPedidoVenda consultaPedidoVenda, IConsultaMotivoDeRecusa consultaMotivoDeRecusa,
            IConsultaStatusDoPedidoDeVenda consultaStatusDoPedidoDeVenda, IConsultaUsuario consultaUsuario)
        {
            _consultaTipoPedido = consultaTipoPedido;
            _consultaListaPreco = consultaListaPreco;
            _consultaMaterial = consultaMaterial;
            _consultaIncotermCab = consultaIncotermCab;
            _consultaPedidoVenda = consultaPedidoVenda;
            _consultaMotivoDeRecusa = consultaMotivoDeRecusa;
            _consultaStatusDoPedidoDeVenda = consultaStatusDoPedidoDeVenda;
            _consultaUsuario = consultaUsuario;
        }

        #endregion

        #region Criar e Editar Pedido de Venda

        private void PrepararViewBagParaTelaDeCadastroDePedido()
        {
            ViewBag.CondicoesDePagamento = new List<CondicaoDePagamentoCadastroVm>();
            ViewBag.TipoPedidos = _consultaTipoPedido.ListarTodas();
            ViewBag.ListaPreco = _consultaListaPreco.ListarTodas();
            ViewBag.Centro = _consultaMaterial.ListarCentro();
            ViewBag.Incoterms = _consultaIncotermCab.ListarTodas();
            ViewBag.MotivosDeRecusa = _consultaMotivoDeRecusa.ListarTodas();

        }

        [HttpGet]
        public ActionResult Index()
        {
            PrepararViewBagParaTelaDeCadastroDePedido();
            ViewBag.TituloDaPagina = "Criar Pedido de Venda";
            PedidoVendaCadastroVm pedidoVendaCadastroVm = TempData["PedidoVenda"] as PedidoVendaCadastroVm ?? 
                new PedidoVendaCadastroVm { datap = DateTime.Today.ToShortDateString() };
            return View("_CriarPedidoVenda", pedidoVendaCadastroVm);
        }

        [HttpGet]
        public ActionResult CopiarPedido(string idDoPedido)
        {
            PedidoVendaCadastroVm pedidoVenda = _consultaPedidoVenda.Consultar(idDoPedido);
            pedidoVenda.Copia = true;
            pedidoVenda.id_pedido = "";
            pedidoVenda.NumeroPedidoDoCliente = "";
            pedidoVenda.status = "";
            
            TempData["PedidoVenda"] = pedidoVenda;

            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult EditarPedido(string idDaCotacao)
        {
            PrepararViewBagParaTelaDeCadastroDePedido();
            ViewBag.TituloDaPagina = "Editar Pedido de Venda";
            PedidoVendaCadastroVm pedidoVendaCadastroVm = _consultaPedidoVenda.Consultar(idDaCotacao);
            return View("_CriarPedidoVenda", pedidoVendaCadastroVm);
        }

        [HttpGet]
        public ActionResult VisualizarPedido(string idDaCotacao)
        {
            PrepararViewBagParaTelaDeCadastroDePedido();
            ViewBag.TituloDaPagina = "Visualizar Pedido de Venda";
            PedidoVendaCadastroVm pedidoVendaCadastroVm = _consultaPedidoVenda.Consultar(idDaCotacao);
            pedidoVendaCadastroVm.SomenteLeitura = true;
            return View("_CriarPedidoVenda", pedidoVendaCadastroVm);

        }

        [HttpGet]
        public ActionResult PedidoExiste(string idDoPedido)
        {
            bool pedidoExiste = _consultaPedidoVenda.PedidoExiste(idDoPedido);
            return Json(pedidoExiste, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Consultar Pedidos

        // Consulta Pedido de Venda
        public ActionResult Consultar()
        {
            ViewBag.ListaDeStatus = _consultaStatusDoPedidoDeVenda.ListarTodos().Select(status =>
                new SelectListItem
                {
                    Value = status.Codigo,
                    Text = status.Descricao,
                    Selected = false

                });

            RepresentanteDTO representanteDTO = _consultaUsuario.RepresentanteDoUsuarioLogado();

            return View("_ConsultarPedidoVenda",representanteDTO);
        }

        // Listar Pedido Venda
        [HttpGet]
        public JsonResult ListarPedidoVenda(PaginacaoVm paginacaoVm, PedidoVendaFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaPedidoVenda.Listar(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Consultar Cotacao


        // Consultar Linhas Cotacao
        [HttpGet]
        public JsonResult ConsultarItensDoPedido(string idDoPedido)
        {
            try
            {

                var itens = _consultaPedidoVenda.ListarItensDoPedido(idDoPedido);
                return Json(new {Sucesso = true, Itens = itens}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Imprimir(string idDaCotacao)
        {
            PedidoVendaImprimirDto pedidoVendaImprimirDto = _consultaPedidoVenda.Impressao(idDaCotacao);
            return View(pedidoVendaImprimirDto);
        }

        #endregion


    }
}
