using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.ViewModel;
using StructureMap;

namespace Progas.Portal.UnitTest
{
    [TestClass]
    public class ConsultaDePedidoDeVendaTests
    {
        [TestMethod]
        public void ConsigoFiltrarPorMaterial()
        {
            var consultaPedidoVenda = ObjectFactory.GetInstance<IConsultaPedidoVenda>();
            var paginacaoVm = new PaginacaoVm
            {
                Page = 1,
                PageSize = 10,
                Take = 10
            };

            var pedidoVendaFiltroVm = new PedidoVendaFiltroVm
            {
                IdDoMaterial = 295
            };
            
            KendoGridVm kendoGridVm = consultaPedidoVenda.Listar(paginacaoVm, pedidoVendaFiltroVm);

            Assert.AreEqual(0 , kendoGridVm.QuantidadeDeRegistros);


        }
    }
}
