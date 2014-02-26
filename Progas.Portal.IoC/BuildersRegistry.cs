using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Common;
using Progas.Portal.Domain.Entities;
using Progas.Portal.ViewModel;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Progas.Portal.IoC
{
    public class BuildersRegistry : Registry
    {
        public BuildersRegistry()
        {
            For<IBuilder<TipoPedido, TipoPedidoCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<TipoPedidoCadastroBuilder>();

            For<IBuilder<CondicaoDePagamento, CondicaoDePagamentoCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<CondicaoPagamentoCadastroBuilder>();

            For<IBuilder<Usuario, UsuarioConsultaVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<UsuarioConsultaBuilder>();

            For<IBuilder<Enumeradores.Perfil, PerfilVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<PerfilBuilder>();

            For<IBuilder<UnidadeDeMedida, UnidadeDeMedidaCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<UnidadeDeMedidaCadastroBuilder>();            

            For<IBuilder<ListaPreco, ListaPrecoCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<ListaPrecoCadastroBuilder>();

            For<IBuilder<Material, MaterialCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<MaterialCadastroBuilder>();

            For<IBuilder<Incoterm, IncotermCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<IncotermCadastroBuilder>();

            For<IBuilder<IncotermCab, IncotermsCabCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<IncotermsCabCadastroBuilder>();

            For<IBuilder<IncotermLinhas, IncotermLinhasCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<IncotermsLinhasCadastroBuilder>();

            For<IBuilder<Fornecedor, FornecedorCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<FornecedorCadastroBuilder>();

            For<IBuilder<Cliente, ClienteCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<ClienteCadastroBuilder>();

            For<IBuilder<PedidoVenda, PedidoVendaCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<PedidoVendaCadastroBuilder>();

            For<IBuilder<PedidoVendaLinha, PedidoVendaLinhaCadastroVm>>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest))
                .Use<PedidoVendaLinhaCadastroBuilder>();
        }
    }
}