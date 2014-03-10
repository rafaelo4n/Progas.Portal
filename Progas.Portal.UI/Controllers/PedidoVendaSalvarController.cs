using System;
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
        public JsonResult Salvar(PedidoVendaSalvarVm pedido)
        {
             try
                {
                    _cadastroPedidoVenda.Salvar(pedido);
                    return Json(new { Sucesso = true, pedido });
                }
                catch (Exception ex)
                {
                    return Json(new { Sucesso = false, Mensagem = ex.Message + "Erro ao Salvar o Pedido de Venda." });
                }            
        }

    }
}
