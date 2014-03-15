using System;
using System.Collections.Generic;
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

        private readonly IConsultaTipoPedido        _consultaTipoPedido;
        private readonly IConsultaCondicaoPagamento _consultaCondicaoPagamento;
        private readonly IConsultaListaPreco        _consultaListaPreco;
        private readonly IConsultaMaterial          _consultaMaterial;
        private readonly IConsultaIncotermCab       _consultaIncotermCab;
        private readonly IConsultaCliente           _consultaCliente;
        private readonly IConsultaPedidoVenda       _consultaPedidoVenda;

        private List<MotivoDeRecusaVm> _motivosDeRecusa;

        private void PreencherMotivosDeRecusa()
        {
            _motivosDeRecusa = new List<MotivoDeRecusaVm>
            {
                new MotivoDeRecusaVm{Codigo = "00", Descricao = "Substituição do Produto"},
                new MotivoDeRecusaVm{Codigo = "01", Descricao = "Desistência do Cliente"},
                new MotivoDeRecusaVm{Codigo = "02", Descricao = "Atraso na Entrega"},
                new MotivoDeRecusaVm{Codigo = "03", Descricao = "Depósito não Efetuado"},
                new MotivoDeRecusaVm{Codigo = "04", Descricao = "Cheque não Enviado"},
                new MotivoDeRecusaVm{Codigo = "05", Descricao = "Nota Fiscal Cancelada"},
                new MotivoDeRecusaVm{Codigo = "10", Descricao = "Solicitação do cliente não justificada"},
                new MotivoDeRecusaVm{Codigo = "11", Descricao = "Saldo de Ordem"},
                new MotivoDeRecusaVm{Codigo = "12", Descricao = "Sem definição da Assistência Técnica"},
                new MotivoDeRecusaVm{Codigo = "50", Descricao = "Questão está sendo resolvida"},
                new MotivoDeRecusaVm{Codigo = "51", Descricao = "Não Liberado pelo financeiro"},
                new MotivoDeRecusaVm{Codigo = "52", Descricao = "Cancelamentos Remessas (acerto)"},
                new MotivoDeRecusaVm{Codigo = "53", Descricao = "Faturamento Intercompany"},
                new MotivoDeRecusaVm{Codigo = "54", Descricao = "Pedido em Duplicidade"},
                new MotivoDeRecusaVm{Codigo = "98", Descricao = "Ordem Substituída"},
                new MotivoDeRecusaVm{Codigo = "99", Descricao = "Política Comercial (Alçada)"},
            };

            
        }

        
        // implementa os valores dos repositorios que serao usados nas views (Lista de valrores dos campos)
        public PedidoVendaController(
            IConsultaCondicaoPagamento consultaCondicaoPagamento,
            IConsultaTipoPedido consultaTipoPedido,
            IConsultaListaPreco consultaListaPreco,
            IConsultaMaterial consultaMaterial,
            IConsultaIncotermCab consultaIncotermCab,
            IConsultaCliente consultaCliente,
            IConsultaPedidoVenda consultaPedidoVenda
            )
        {
            _consultaCondicaoPagamento = consultaCondicaoPagamento;
            _consultaTipoPedido = consultaTipoPedido;
            _consultaListaPreco = consultaListaPreco;
            _consultaMaterial = consultaMaterial;
            //_consultaIncoterm          = consultaIncoterm;
            _consultaIncotermCab = consultaIncotermCab;
            _consultaCliente = consultaCliente;
            _consultaPedidoVenda = consultaPedidoVenda;
            PreencherMotivosDeRecusa();

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
            ViewBag.MotivosDeRecusa = _motivosDeRecusa;
            
        }

        // Criacao de um novo pedido de venda
        public ActionResult Index()
        {
            ViewBag.TituloDaPagina = "Criar Pedido de Venda";
            PrepararViewBagParaTelaDeCadastroDePedido();
            return View("_CriarPedidoVenda");

        }

        public ActionResult EditarPedido(string idDaCotacao)
        {
            PrepararViewBagParaTelaDeCadastroDePedido();
            ViewBag.TituloDaPagina = "Editar Pedido de Venda";
            PedidoVendaCadastroVm pedidoVendaCadastroVm = _consultaPedidoVenda.Consultar(idDaCotacao);
            return View("_CriarPedidoVenda", pedidoVendaCadastroVm);
        }

        public ActionResult VisualizarPedido(string idDaCotacao)
        {
            PrepararViewBagParaTelaDeCadastroDePedido();
            ViewBag.TituloDaPagina = "Visualizar Pedido de Venda";
            PedidoVendaCadastroVm pedidoVendaCadastroVm = _consultaPedidoVenda.Consultar(idDaCotacao);
            return View("_CriarPedidoVenda",pedidoVendaCadastroVm);
          
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
