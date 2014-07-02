using FluentNHibernate.Mapping;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Mappings
{
    public class TransportadoraDoRepresentanteMap: ClassMap<TransportadoraDoRepresentante>
    {
        public TransportadoraDoRepresentanteMap()
        {
            Table("pro_fornecedor_trans_lib");
            Id(x => x.Id);
            References(x => x.Representante, "Codigo");
            References(x => x.Transportadora, "Numero_agente_frete");
            Map(x => x.TipoDeParceiro, "Funcao_parceiro");
        }
    }
}
