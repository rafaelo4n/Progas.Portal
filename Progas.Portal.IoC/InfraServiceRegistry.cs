﻿using Progas.Portal.Common;
using Progas.Portal.Infra.Services.Contracts;
using Progas.Portal.Infra.Services.Implementations;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Progas.Portal.IoC
{
    public class InfraServiceRegistry : Registry
    {
        public InfraServiceRegistry()
        {
            For<IValidadorUsuario>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<ValidadorUsuario>();

            For<IAuthenticationProvider>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<AuthenticationProvider>();

            For<IAccountService>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<AccountService>();

            For<IProvedorDeCriptografia>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<ProvedorDeCriptografiaMd5>();

            For<IEmailService>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<EmailService>();

            For<IGeradorDeSenha>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<GeradorDeSenha>();

            For<IGeradorDeMensagemDeEmail>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<GeradorDeMensagemDeEmail>();

            For<IGeradorDeEmail>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<GeradorDeEmail>();

        }
    }
}