using System;
using System.Web.Mvc;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class PedidoVendaController : Controller
    {
        #region Repositorios

        private readonly IConsultaTipoPedido _consultaTipoPedido;
        private readonly IConsultaCondicaoPagamento _consultaCondicaoPagamento;
        private readonly IConsultaListaPreco _consultaListaPreco;
        private readonly IConsultaMaterial _consultaMaterial;
        private readonly IConsultaIncotermCab _consultaIncotermCab;
        private readonly IConsultaPedidoVenda _consultaPedidoVenda;

        private readonly IConsultaMotivoDeRecusa _consultaMotivoDeRecusa;

        // implementa os valores dos repositorios que serao usados nas views (Lista de valrores dos campos)
        public PedidoVendaController(
            IConsultaCondicaoPagamento consultaCondicaoPagamento,
            IConsultaTipoPedido consultaTipoPedido,
            IConsultaListaPreco consultaListaPreco,
            IConsultaMaterial consultaMaterial,
            IConsultaIncotermCab consultaIncotermCab,
            IConsultaPedidoVenda consultaPedidoVenda, IConsultaMotivoDeRecusa consultaMotivoDeRecusa)
        {
            _consultaCondicaoPagamento = consultaCondicaoPagamento;
            _consultaTipoPedido = consultaTipoPedido;
            _consultaListaPreco = consultaListaPreco;
            _consultaMaterial = consultaMaterial;
            _consultaIncotermCab = consultaIncotermCab;
            _consultaPedidoVenda = consultaPedidoVenda;
            _consultaMotivoDeRecusa = consultaMotivoDeRecusa;
        }

        #endregion

        #region Criar e Editar Pedido de Venda

        private void PrepararViewBagParaTelaDeCadastroDePedido()
        {
            ViewBag.CondicoesDePagamento = _consultaCondicaoPagamento.ListarTodas();
            ViewBag.TipoPedidos = _consultaTipoPedido.ListarTodas();
            ViewBag.ListaPreco = _consultaListaPreco.ListarTodas();
            ViewBag.Centro = _consultaMaterial.ListarCentro();
            ViewBag.Incoterms = _consultaIncotermCab.ListarTodas();
            ViewBag.MotivosDeRecusa = _consultaMotivoDeRecusa.ListarTodas();

        }

        // Criacao de um novo pedido de venda
        [HttpGet]
        public ActionResult Index()
        {
            PrepararViewBagParaTelaDeCadastroDePedido();
            ViewBag.TituloDaPagina = "Criar Pedido de Venda";
            return View("_CriarPedidoVenda");

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

        [HttpGet]
        public ActionResult CopiarPedido(string idDoPedido)
        {
            PrepararViewBagParaTelaDeCadastroDePedido();
            ViewBag.TituloDaPagina = "Criar Pedido de Venda";
            PedidoVendaCadastroVm pedidoVendaCadastroVm = _consultaPedidoVenda.Consultar(idDoPedido);
            pedidoVendaCadastroVm.Copia = true;
            pedidoVendaCadastroVm.id_pedido = null;
            pedidoVendaCadastroVm.status = null;
            return View("_CriarPedidoVenda", pedidoVendaCadastroVm);
            
        }


        #endregion

        #region Consultar Pedidos

        // Consulta Pedido de Venda
        public ActionResult Consultar()
        {
            return View("_ConsultarPedidoVenda");
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

        #endregion
        [HttpGet]
        public ActionResult ListarCondicoesDePreco(PedidoVendaLinhaChaveVm item)
        {
            KendoGridVm kendoGridVm = _consultaPedidoVenda.ListarCondicoesDePreco(item);

            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CondicoesDePreco(PedidoVendaLinhaChaveVm item)
        {
            return PartialView("CondicaoDePreco", item);
        }
    }
}
