using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.ViewModel;
using StructureMap;

namespace Progas.Portal.UnitTest
{
    [TestClass]
    public class ConsultaDeMaterialTests
    {
        [TestMethod]
        public void ConsigoConsultarMateriaisComPrecoAtivo()
        {
            var consultaMaterial = ObjectFactory.GetInstance<IConsultaMaterial>();
            var paginacao = new PaginacaoVm
            {
                Page = 1,
                PageSize = 10,
                Take = 10
            };

            var filtro = new MaterialFiltroVm
            {
                IdDoCliente = "0001000188",
                ComPrecoAtivo = true
            };
            KendoGridVm kendoGridVm = consultaMaterial.Listar(paginacao, filtro);

            Assert.AreEqual(2,kendoGridVm.QuantidadeDeRegistros);
        }
    }
}
