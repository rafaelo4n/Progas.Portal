using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class PedidoVendaSalvarController : Controller
    {
        private readonly ICadastroPedidoVenda _cadastroPedidoVenda;

        public PedidoVendaSalvarController( ICadastroPedidoVenda cadastroPedidoVendaLinha)
        {
            _cadastroPedidoVenda = cadastroPedidoVendaLinha;
        }

        [HttpPost]
        public JsonResult Salvar(IList<PedidoVendaSalvarVm> pedido)
        {
             try
                {
                    if (pedido == null)
                    {
                        pedido = new List<PedidoVendaSalvarVm>();
                    }
                    _cadastroPedidoVenda.Salvar(pedido);
                    return Json(new { Sucesso = true, pedido });
                }
                catch (Exception ex)
                {
                    return Json(new { Sucesso = false, Mensagem = ex.Message + "Erro ao Salvar o Pedido de Venda." });
                }            
        }

        [HttpPost]
        public JsonResult Atualizar(IList<PedidoVendaSalvarVm> pedido)
        {
            try
            {
                if (pedido == null)
                {
                    pedido = new List<PedidoVendaSalvarVm>();

                }
                _cadastroPedidoVenda.Atualizar(pedido);
                return Json(new { Sucesso = true });
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = ex.Message + "Erro ao Atualizar o Pedido de Vendas." });
            }
        }

    }
}
