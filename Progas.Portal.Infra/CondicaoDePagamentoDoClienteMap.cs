using FluentNHibernate.Mapping;
using Progas.Portal.Domain;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra
{
    public class CondicaoDePagamentoDoClienteMap: ClassMap<CondicaoDePagamentoDoCliente>
    {
        public CondicaoDePagamentoDoClienteMap()
        {
            Table("pro_cliente_condicoes_lib");
            CompositeId()
                .KeyReference(x => x.Cliente, "Id_cliente")
                .KeyReference(x => x.CondicaoDePagamento, "Chave_condicao");

            References(x => x.Cliente, "Id_cliente").Not.Insert();
            References(x => x.CondicaoDePagamento, "Chave_condicao").Not.Insert();

            Map(x => x.DataDeValidade,"Data_fim_condicao");
            Map(x => x.Eliminacao);
        }
    }
}
