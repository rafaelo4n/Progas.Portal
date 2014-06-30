using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.Infra.Repositories.Implementations;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Progas.Portal.IoC
{
    public class RepositoryRegistry: Registry
    {
        public RepositoryRegistry()
        {
            For<ITipoPedidos>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<TipoPedidos>();
            
            For<IUsuarios>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<Usuarios>();           

            For<IFornecedores>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<Fornecedores>();

            For<ICondicoesDePagamento>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<CondicoesDePagamento>();  
          
            For<IUnidadesDeMedida>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<UnidadesDeMedida>();            

            For<IListasPreco>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<ListasPreco>();

            For<IMateriais>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<Materiais>();

            For<IIncoterms>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<Incoterms>();

            For<IIncotermsCabs>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<IncotermsCabs>();

            For<IIncotermsLinhas>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<IncotermsLinhas>();

            For<IPedidosVenda>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<PedidosVenda>();

            For<IClientes>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<Clientes>();

            For<IClienteVendas>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<ClienteVendas>();

            For<IMotivosDeRecusa>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<MotivosDeRecusa>();

            For<IRepositorioDeStatusDoPedidoDeVenda>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<RepositorioDeStatusDoPedidoDeVenda>();

        }
    }
}