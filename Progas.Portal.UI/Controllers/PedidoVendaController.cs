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
    public class PedidoVendaController : Controller
    {
        #region Repositorios

        private readonly IConsultaUnidadeDeMedida   _consultaUnidadeDeMedida;
        private readonly IConsultaTipoPedido        _consultaTipoPedido;
        private readonly IConsultaCondicaoPagamento _consultaCondicaoPagamento;
        private readonly IConsultaListaPreco        _consultaListaPreco;
        private readonly IConsultaMaterial          _consultaMaterial;
        private readonly IConsultaIncoterm          _consultaIncoterm;
        private readonly IConsultaCliente           _consultaCliente;
        private readonly IConsultaFornecedor        _consultaFornecedor;
        private readonly IConsultaPedidoVenda       _consultaPedidoVenda;

        
        // implementa os valores dos repositorios que serao usados nas views (Lista de valrores dos campos)
        public PedidoVendaController( IConsultaUnidadeDeMedida   consultaUnidadeDeMedida, 
                                      IConsultaCondicaoPagamento consultaCondicaoPagamento,
                                      IConsultaTipoPedido        consultaTipoPedido,
                                      IConsultaListaPreco        consultaListaPreco,
                                      IConsultaMaterial          consultaMaterial,
                                      IConsultaIncoterm          consultaIncoterm,
                                      IConsultaCliente           consultaCliente,
                                      IConsultaFornecedor        consultaFornecedor,
                                      IConsultaPedidoVenda       consultaPedidoVenda
                                    )                
            {
                _consultaUnidadeDeMedida   = consultaUnidadeDeMedida;
                _consultaCondicaoPagamento = consultaCondicaoPagamento;
                _consultaTipoPedido        = consultaTipoPedido;
                _consultaListaPreco        = consultaListaPreco;
                _consultaMaterial          = consultaMaterial;
                _consultaIncoterm          = consultaIncoterm;
                _consultaCliente           = consultaCliente;
                _consultaFornecedor        = consultaFornecedor;
                _consultaPedidoVenda       = consultaPedidoVenda;
            }

        #endregion

        #region Criar e Editar Pedido de Venda
        // View de Criacao e Edicao de Pedido de Venda, muda o titulo da pagina e as funções estão localizadas no Html
        public ActionResult Index(string cotacao_editar)
        {
            var usuarioConectado = ObjectFactory.GetInstance<UsuarioConectado>();
            if (usuarioConectado.Perfis.Contains(Enumeradores.Perfil.Vendedor))
            {
                if (cotacao_editar == null)
                {
                    ViewBag.TituloDaPagina = "Pedido de Venda";
                }
                else
                {
                    ViewBag.TituloDaPagina = "Editar Pedido de Venda";
                    ViewBag.Pedido = cotacao_editar;
                }
                        
                ViewData["ActionEdicao"] = Url.Action("CriarPedidoVenda", "PedidoVenda");
            }            

            ViewBag.UnidadesDeMedida     = _consultaUnidadeDeMedida.ListarTodos();
            ViewBag.CondicoesDePagamento = _consultaCondicaoPagamento.ListarTodas();
            ViewBag.TipoPedidos          = _consultaTipoPedido.ListarTodas();
            ViewBag.ListaPreco           = _consultaListaPreco.ListarTodas();
            ViewBag.Centro               = _consultaMaterial.ListarCentro();
            ViewBag.Materiais            = _consultaMaterial.ListarTodas();
            ViewBag.Incoterms            = _consultaIncoterm.ListarTodas();
            ViewBag.Clientes             = _consultaCliente.Listar();
            return View("_CriarPedidoVenda");
           
        }
        #endregion       

        #region Consultar Pedidos

        // Consulta Pedido de Venda
        public ActionResult Consultar()
        {
            ViewBag.Clientes  = _consultaCliente.Listar();
            ViewBag.Materiais = _consultaMaterial.ListarTodas();
            return View("_ConsultarPedidoVenda"); 
        }

        // Listar Pedido Venda
        [HttpGet]
        public JsonResult ListarPedidoVenda(PaginacaoVm paginacaoVm, PedidoVendaFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaPedidoVenda.Listar(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        // Listar Linhas do Pedido
        [HttpGet]
        public ViewResult ConsultarLinhasPedido(string cotacao)
        {
            PedidoVendaCadastroVm vieModel = _consultaPedidoVenda.ListarLinhasPedido(cotacao);
            return View(vieModel);
        }

        // linhas do pedido
        [HttpGet]
        public JsonResult ListarLinhasPedido(PaginacaoVm paginacaoVm, string cotacao)
        {
            KendoGridVm kendoGridVm = _consultaPedidoVenda.ListarLinhasPedido(paginacaoVm, cotacao);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Consultar Cotacao
        // Consultar Cotacao
        [HttpPost]
        public JsonResult ConsultarCotacao(string cotacao)
        {
            try
            {
                if (cotacao == null)
                {
                    return Json(new { Sucesso = false, Mensagem = "Erro ao consultar a Cotação" });                        
                }

                PedidoVendaCadastroVm vieModel = _consultaPedidoVenda.ListarLinhasPedido(cotacao);
                return Json(vieModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = ex.Message + "Erro ao consultar a Cotação" });
            }

        }
        
        // Consultar Linhas Cotacao
        [HttpPost]
        public JsonResult ConsultarLinhasCotacao(string cotacao)
        {
            try
            {
                if (cotacao == null)
                {
                    return Json(new { Sucesso = false, Mensagem = "Não executou a chamada Json, cotacao nao informada" });
                }

                var dados = _consultaPedidoVenda.ListarLinhasCotacao(cotacao);
                return Json(dados, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = ex.Message + "Não executou a chamada Json" });
            }
        }

        #endregion                
         
    }   
}
