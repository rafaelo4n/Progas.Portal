using Progas.Portal.Domain.Services.Contracts;
using Progas.Portal.Domain.Services.Implementations;
using StructureMap;
using StructureMap.Pipeline;
using Registry = StructureMap.Configuration.DSL.Registry;

namespace Progas.Portal.IoC
{
    public class DomainServiceRegistry : Registry
    {
        public DomainServiceRegistry()
        {
            For<IAtualizadorDeItensDoPedidoDeVenda>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<AtualizadorDeItensDoPedidoDeVenda>();          
        }
    }
}
