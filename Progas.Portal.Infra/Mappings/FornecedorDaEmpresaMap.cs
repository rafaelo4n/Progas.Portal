using FluentNHibernate.Mapping;
using Progas.Portal.Domain.Entities;

namespace Progas.Portal.Infra.Mappings
{
    public class FornecedorDaEmpresaMap: ClassMap<FornecedorDaEmpresa>
    {
        public FornecedorDaEmpresaMap()
        {
            Table("pro_fornecedor_emp");
            CompositeId()
                .KeyProperty(x => x.Empresa)
                .KeyReference(x => x.Fornecedor, "Codigo");

            References(x => x.Fornecedor, "Codigo").Not.Insert();

            Map(x => x.Eliminacao);

        }
    }
}
