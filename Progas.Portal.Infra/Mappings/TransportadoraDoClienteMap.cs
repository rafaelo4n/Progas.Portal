using FluentNHibernate.Mapping;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Mappings
{
    public class TransportadoraDoClienteMap: ClassMap<TransportadoraDoCliente>
    {
        public TransportadoraDoClienteMap()
        {
            Table("pro_cliente_trans_lib");
            Id(x => x.Id);
            References(x => x.Cliente, "Id_cliente");
            References(x => x.Transportadora, "Numero_agente_frete");
            Map(x => x.TipoDeParceiro, "Funcao_parceiro");
        }
    }
}
