using Progas.Portal.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Progas.Portal.Infra.Mappings
{
    public class ClienteVendasMap : ClassMap<ClienteVenda>
    {
        public ClienteVendasMap()
        {
            Table("pro_cliente_vendas");
            Id(x => x.Id, "pro_id_cliente_vendas");
            Map(x => x.Id_cliente);
            Map(x => x.Org_vendas);
            Map(x => x.Can_dist);
            Map(x => x.Set_ativ);
            Map(x => x.Grupo_cli);
            Map(x => x.Id_fornecedor);
            Map(x => x.Pacote);
            Map(x => x.Hora_criacao);
            Map(x => x.Data_criacao);
            Map(x => x.Denominacao);
        }
    }
}
