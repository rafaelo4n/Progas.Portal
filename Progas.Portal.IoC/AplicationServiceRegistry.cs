using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Application.Services.Implementations;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Progas.Portal.IoC
{
    public class AplicationServiceRegistry : Registry
    {
        public  AplicationServiceRegistry()
        {
            For<ICadastroUsuario>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<CadastroUsuario>();          

            For<ICadastroCondicaoPagamento>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<CadastroCondicaoPagamento>();          

            For<IGerenciadorUsuario>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<GerenciadorUsuario>();

            For<ICadastroPedidoVenda>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<CadastroPedidoVenda>();

            For<IComunicacaoSap>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<ComunicacaoSap>();

        }

    }
}