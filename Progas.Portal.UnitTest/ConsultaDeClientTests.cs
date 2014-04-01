using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Common;
using Progas.Portal.Infra.DataAccess;
using Progas.Portal.Infra.Factory;
using Progas.Portal.Infra.Model;
using Progas.Portal.IoC;
using Progas.Portal.ViewModel;
using StructureMap;
using StructureMap.Pipeline;

namespace Progas.Portal.UnitTest
{
    [TestClass]
    public class ConsultaDeClientTests
    {

        [ClassInitialize]
        public static void Inicializar(TestContext testContext)
        {
            SessionManager.ConfigureDataAccess(ConfigurationManager.ConnectionStrings["Progas"].ConnectionString);

            var emailDoPortal = ConfigurationManager.GetSection("emailDoPortal") as EmailDoPortal;

            ObjectFactory.Configure(x =>
            {
                if (emailDoPortal != null)
                {
                    x.For<ContaDeEmail>()
                     .Singleton()
                     .Use(new ContaDeEmail("Portal De Vendas <" + emailDoPortal.RemetenteProgas + ">", emailDoPortal.Dominio,
                                           emailDoPortal.Usuario, emailDoPortal.Senha, emailDoPortal.Servidor,
                                           emailDoPortal.Porta,emailDoPortal.HabilitarSsl)).Named(Constantes.ContaDeEmailProgas);

        
                }

            });

            IoCWorker.Configure();

            ObjectFactory.Configure(x => x.For<UsuarioConectado>()
                .HybridHttpOrThreadLocalScoped()
                .Use(new UsuarioConectado("fabiano", "Fabiano Machado", new List<Enumeradores.Perfil> { Enumeradores.Perfil.Vendedor})));


        }


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
        public void ConsultaDeTeste()
        {
            var consultaCliente = ObjectFactory.GetInstance<IConsultaCliente>();
            Assert.AreEqual(0, consultaCliente.ConsultaDeTeste());
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
