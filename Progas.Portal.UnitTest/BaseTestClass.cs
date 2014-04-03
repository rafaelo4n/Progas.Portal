using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progas.Portal.Common;
using Progas.Portal.Infra.DataAccess;
using Progas.Portal.Infra.Model;
using Progas.Portal.IoC;
using StructureMap;

namespace Progas.Portal.UnitTest
{
    [TestClass]
    public class BaseTestClass
    {
        [AssemblyInitialize]
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
                                           emailDoPortal.Porta, emailDoPortal.HabilitarSsl)).Named(Constantes.ContaDeEmailProgas);
                }

            });

            IoCWorker.Configure();

            ObjectFactory.Configure(x => x.For<UsuarioConectado>()
                .HybridHttpOrThreadLocalScoped()
                .Use(new UsuarioConectado("fabiano", "Fabiano Machado", new List<Enumeradores.Perfil> { Enumeradores.Perfil.Vendedor })));
        }

    }
}
