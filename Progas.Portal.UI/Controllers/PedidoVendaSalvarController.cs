using System;
using System.Web.Mvc;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.DTO;
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
                    PedidoSapRetornoDTO pedidoSapRetornoDTO = _cadastroPedidoVenda.Salvar(pedido);
                    return Json(new { Sucesso = true, Retorno = pedidoSapRetornoDTO });
                }
                catch (Exception ex)
                {
                    return Json(new { Sucesso = false, Mensagem = ex.Message});
                }            
        }

    }
}
