using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.ViewModel;
using StructureMap;

namespace Progas.Portal.UnitTest
{
    [TestClass]
    public class ConsultaDeCondicaoDePagamentoTests
    {
        [TestMethod]
        public void ListarCondicoesDoCliente()
        {
            var consultaCliente = ObjectFactory.GetInstance<IConsultaCliente>();
            IList<CondicaoDePagamentoCadastroVm> condicoesDePagamento = consultaCliente.ListarCondicoesDePagamento("0001000158", null);
            Assert.AreEqual(0, condicoesDePagamento.Count);
        }
    }
}
