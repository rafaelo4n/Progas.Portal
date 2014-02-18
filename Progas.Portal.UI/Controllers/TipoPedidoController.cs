using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Common.Exceptions;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;
using Progas.Portal.Application.Queries.Contracts;

namespace Progas.Portal.UI.Controllers
{
    [ApiAuthorizationFilter]
    public class TipoPedidoController : ApiController
    {
        private readonly IConsultaTipoPedido _consultaTipoPedido;

        public TipoPedidoController(IConsultaTipoPedido consultaTipoPedido)
        {
            _consultaTipoPedido = consultaTipoPedido;
        }

    }
}