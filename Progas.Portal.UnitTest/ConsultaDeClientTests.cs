using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.ViewModel;
using StructureMap;

namespace Progas.Portal.UnitTest
{
    [TestClass]
    public class ConsultaDeClientTests
    {

        [TestMethod]
        public void ConsultarCliente()
        {
            var consultaCliente = ObjectFactory.GetInstance<IConsultaCliente>();
            var paginacao = new PaginacaoVm
            {
                Page = 1,
                PageSize = 10,
                Take = 10
            };
            KendoGridVm kendoGridVm = consultaCliente.ListarParaSelecao(paginacao, new ClienteFiltroVm());
        }

        [TestMethod]
        public void FiltrarPorMunicipio()
        {
            var consultaCliente = ObjectFactory.GetInstance<IConsultaCliente>();
            var paginacao = new PaginacaoVm
            {
                Page = 1,
                PageSize = 10,
                Take = 10
            };
            KendoGridVm kendoGridVm = consultaCliente.ListarParaSelecao(paginacao, new ClienteFiltroVm{Municipio = "Casca"});

            Assert.AreEqual(19, kendoGridVm.QuantidadeDeRegistros);
            
        }
    }
}
