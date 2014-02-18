using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using Progas.Portal.Common;
using Progas.Portal.Infra.DataAccess;
using Progas.Portal.Infra.Factory;
using Progas.Portal.Infra.Model;
using Progas.Portal.IoC;
using Progas.Portal.UI.App_Start;
using StructureMap;
using StructureMap.Pipeline;

namespace Progas.Portal.UI
{
    public class IocConfig
    {
        public static void RegisterIoc()
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

            ObjectFactory.Configure(x =>
                {
                    x.AddRegistry<ControllerRegistry>();
                    x.For<IControllerFactory>()
                     .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                     .Use<StructureMapControllerFactory>();
                });
            ControllerBuilder.Current.SetControllerFactory(ObjectFactory.GetInstance<IControllerFactory>());

            var container = ObjectFactory.Container;
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }
    }
}